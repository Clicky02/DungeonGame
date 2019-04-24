using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static GameData data;

    public int level;
    public int experience;
    public int gold;

    public float baseHealth;
    public float baseMana;
    public float baseAttack;
    public float baseAccuracy;
    public float baseDefense;
    public float baseCritChance;
    public float baseCritMultiplier;
    public float baseEvadeChance;


    public int endurance;
    public int strength;
    public int intelligence;
    public int dexterity;
    public int luck;

    public int unassignedStatPoints;

    public string classType;
    public string specialization;

    public string playerName;

    public Armour helmet;
    public Armour chestplate;
    public Armour boots;
    public Weapon weapon;

    public List<Item> inventory;
    public List<string> unlockedAbilities;
    public string basicAbility;
    public List<string> equippedAbilities;


    // Start is called before the first frame update
    void Awake()
    {
        if (data == null)
        {
            data = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else if (data != this)
        {
            Destroy(gameObject);
            //
        }

    }

    public void Save()
    {
        Save(Application.persistentDataPath + "/playerData.dat");
    }

    public void SaveBackup()
    {
        Save(Application.persistentDataPath + "/playerDataBackup.dat");
    }

    public void Save(string path)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.OpenOrCreate);

        PlayerData data = new PlayerData
        {
            level = level,
            experience = experience,
            gold = gold,

            baseHealth = baseHealth,
            baseMana = baseMana,
            baseAttack = baseAttack,
            baseDefense = baseDefense,
            baseAccuracy = baseAccuracy,
            baseCritChance = baseCritChance,
            baseCritMultiplier = baseCritMultiplier,
            baseEvadeChance = baseEvadeChance,

            unassignedStatPoints = unassignedStatPoints,

            endurance = endurance,
            strength = strength,
            intelligence = intelligence,
            dexterity = dexterity,
            luck = luck,

            helmet = helmet,
            chestplate = chestplate,
            boots = boots,
            weapon = weapon,

            classType = classType,
            specialization = specialization,

            playerName = playerName,

            inventory = inventory,
            unlockedAbilities = unlockedAbilities,
            basicAbility = basicAbility,
            equippedAbilities = equippedAbilities
        };

        bf.Serialize(file, data);
        file.Close();
    }

    public void OnDisable()
    {
        Save();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
        {
            Load(Application.persistentDataPath + "/playerData.dat");
            //SceneManager.LoadScene("Main Menu",LoadSceneMode.Single);
            SaveBackup();
        }
        else if (File.Exists(Application.persistentDataPath + "/playerDataBackup.dat"))
        {
            Load(Application.persistentDataPath + "/playerDataBackup.dat");
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
            SaveBackup();
        }
        else
        {
            SceneManager.LoadScene("CharacterCreation", LoadSceneMode.Single);
        }

    }

    public void Load(string path)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenRead(path);
        PlayerData data = (PlayerData)bf.Deserialize(file);
        Debug.Log(data);
        file.Close();

        level = data.level;
        experience = data.experience;
        gold = data.gold;

        if (data.classType != null)
            classType = data.classType;

        specialization = data.specialization;

        baseHealth = data.baseHealth;
        baseMana = data.baseMana;
        baseAttack = data.baseAttack;
        baseDefense = data.baseDefense;
        baseAccuracy = data.baseAccuracy;
        baseCritChance = data.baseCritChance;
        baseCritMultiplier = data.baseCritMultiplier;
        baseEvadeChance = data.baseEvadeChance;


        endurance = data.endurance;
        strength = data.strength;
        intelligence = data.intelligence;
        dexterity = data.dexterity;
        luck = data.luck;

        unassignedStatPoints = data.unassignedStatPoints;
        playerName = data.playerName;


        if (data.helmet != null)
            helmet = data.helmet;
        if (data.chestplate != null)
            chestplate = data.chestplate;
        if (data.boots != null)
            boots = data.boots;
        if (data.weapon != null)
            weapon = data.weapon;

        if (data.inventory != null)
            inventory = data.inventory;
        if (data.unlockedAbilities != null)
            unlockedAbilities = data.unlockedAbilities;
        if (data.basicAbility != null)
            basicAbility = data.basicAbility;
        if (data.equippedAbilities != null)
            equippedAbilities = data.equippedAbilities;
    }

    public List<Armour> GetArmourList()
    {
        List<Armour> a = new List<Armour>
        {
            boots,
            chestplate,
            helmet
        };
        return a;
    }

    public bool AddExperience(int exp)
    {
        experience += exp;
        if (experience > GetExperienceForNextLevel())
        {
            int leftoverEXP = experience - GetExperienceForNextLevel();
            LevelUp();
            AddExperience(leftoverEXP);
            return true;
        }
        return false;
    }

    public void LevelUp()
    {
        experience = 0;
        level += 1;
        unassignedStatPoints += 4;
        baseHealth += 1;
        baseMana += 1;
        if (level % 2 == 1)
        {
            if (specialization == "endurance")
                endurance++;
            else if (specialization == "strength")
                strength++;
            else if (specialization == "intelligence")
                intelligence++;
            else if (specialization == "dexterity")
                dexterity++;
            else if (specialization == "luck")
                luck++;
        }
    }

    public int GetExperienceForNextLevel()
    {
        return Mathf.RoundToInt(100f * Mathf.Pow(1.5f, (float)(level - 1)));
    }

    public float GetHealth()
    {
        return GetHealth(endurance);
    }

    public float GetHealth(int endurance)
    {
        return baseHealth * (1f + (endurance * 0.05f));
    }

    public float GetMana()
    {
        return GetMana(intelligence);
    }

    public float GetMana(int intelligence)
    {
        return baseMana * (1f + (intelligence * 0.04f));
    }

    public float GetAttack()
    {
        return GetAttack(strength, dexterity);
    }

    public float GetAttack(int strength, int dexterity)
    {
        return baseAttack * (1f + (0.06f * strength) + (0.02f * dexterity));
    }

    public float GetDefense()
    {
        return GetDefense(strength, endurance);
    }

    public float GetDefense(int strength, int endurance)
    {
        return baseDefense * (1f + (0.03f * strength) + (0.04f * endurance));
    }

    public float GetAccuracy()
    {
        return GetAccuracy(dexterity, intelligence);
    }

    public float GetAccuracy(int dexterity, int intelligence)
    {
        return baseAccuracy * (1f + (0.003f * dexterity) + (0.002f * intelligence));
    }

    public float GetCritChance()
    {
        return GetCritChance(dexterity, strength, intelligence);
    }

    public float GetCritChance(int dexterity, int strength, int intelligence)
    {
        return baseCritChance * (1f + (0.09f * dexterity) + (0.03f * strength) + (0.03f * intelligence));
    }

    public float GetCritMultiplier()
    {
        return GetCritMultiplier(dexterity, strength);
    }

    public float GetCritMultiplier(int dexterity, int strength)
    {
        return baseCritMultiplier * (1f + (0.0075f * dexterity) + (0.0075f * strength));
    }

    public float GetEvadeChance()
    {
        return GetEvadeChance(dexterity, endurance);
    }

    public float GetEvadeChance(int dexterity, int endurance)
    {
        return baseEvadeChance * (1f + (0.025f * dexterity) + (0.03f * endurance));
    }

    public float GetRandomNumber(bool inFavor, bool neutral)
    {
        if (neutral)
        {
            return UnityEngine.Random.Range(0.0f, 1.0f);
        }
        else if (inFavor)
        {
            return UnityEngine.Random.Range(0.0f, 1.0f);
        }
        else
        {
            return UnityEngine.Random.Range(0.0f, 1.0f);
        }
    }

    public bool DoesSucceed(float chanceOfSuccess, bool luckAffected, bool shouldFail = false)
    {

        float chanceOfFailure = (1 - chanceOfSuccess);
        float chance = chanceOfFailure;
        if (luckAffected)
        {
            if (shouldFail)
            {
                chanceOfSuccess -= (luck / 7000f);
                chance = 1 - (Mathf.Pow(chanceOfSuccess, (luck / 75f) + 1f));
            }
            else
            {
                chanceOfFailure = (1 - chanceOfSuccess) - (luck / 7000f);
                chance = Mathf.Pow(chanceOfFailure, (luck / 75f) + 1f);
            }
        }
        return UnityEngine.Random.Range(0.0f, 1.0f) > chance;
    }
}

[Serializable]
class PlayerData
{
    public int level;
    public int experience;
    public int gold;

    public float baseHealth;
    public float baseMana;
    public float baseAttack;
    public float baseDefense;
    public float baseAccuracy;
    public float baseCritChance;
    public float baseCritMultiplier;
    public float baseEvadeChance;

    public int endurance;
    public int strength;
    public int intelligence;
    public int dexterity;
    public int luck;

    public int unassignedStatPoints;

    public string classType;
    public string specialization;
    public string playerName;

    public Armour helmet;
    public Armour chestplate;
    public Armour boots;
    public Weapon weapon;


    public List<Item> inventory;
    public List<string> unlockedAbilities;
    public string basicAbility;
    public List<string> equippedAbilities;
}
