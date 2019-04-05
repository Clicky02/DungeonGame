using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData data;

    public int level;
    public int experience;
    public int gold;

    public float baseHealth;
    public float baseMana;

    public string classType;

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
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat", FileMode.OpenOrCreate);

        PlayerData data = new PlayerData
        {
            level = level,
            experience = experience,
            gold = gold,

            baseHealth = baseHealth,
            baseMana = baseMana,

            helmet = helmet,
            chestplate = chestplate,
            boots = boots,
            weapon = weapon,

            classType = classType,

            inventory = inventory,
            unlockedAbilities = unlockedAbilities,
            basicAbility = basicAbility,
            equippedAbilities = equippedAbilities
        };

        bf.Serialize(file, data);
        file.Close();

    }
    
    public void SaveBackup()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerDataBackup.dat", FileMode.OpenOrCreate);

        PlayerData data = new PlayerData
        {
            level = level,
            experience = experience,
            gold = gold,

            baseHealth = baseHealth,
            baseMana = baseMana,

            helmet = helmet,
            chestplate = chestplate,
            boots = boots,
            weapon = weapon,

            classType = classType,

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
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/playerData.dat");
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            ResetInventory();
            if (data.level != null)
            level = data.level;
            if (data.experience != null)
            experience = data.experience;
            if (data.gold != null)
            gold = data.gold;

            if (data.classType != null)
            classType = data.classType;

            if (data.baseHealth != null)
            baseHealth = data.baseHealth;
            if (data.baseMana != null)
            baseMana = data.baseMana;
            
            if (data.helmet != null)
            helmet = data.helmet;
            if (data.chestplate != null)
            chestplate = data.chestplate;
            if (data.boots != null)
            boots = data.boots;
            if (data.weapon != null)
            weapon = data.weapon
            
            if (data.inventory != null)
            inventory = data.inventory;
            if (data.unlockedAbilities != null)
            unlockedAbilities = data.unlockedAbilities;
            if (data.basicAbility != null)
            basicAbility = data.basicAbility;
            if (data.equippedAbilities != null)
            equippedAbilities = data.equippedAbilities;

        }
        else if (File.Exists(Application.persistentDataPath + "/playerDataBackup.dat")) 
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/playerDataBackup.dat");
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            ResetInventory();
            if (data.level != null)
            level = data.level;
            if (data.experience != null)
            experience = data.experience;
            if (data.gold != null)
            gold = data.gold;

            if (data.classType != null)
            classType = data.classType;

            if (data.baseHealth != null)
            baseHealth = data.baseHealth;
            if (data.baseMana != null)
            baseMana = data.baseMana;
            
            if (data.helmet != null)
            helmet = data.helmet;
            if (data.chestplate != null)
            chestplate = data.chestplate;
            if (data.boots != null)
            boots = data.boots;
            if (data.weapon != null)
            weapon = data.weapon
            
            if (data.inventory != null)
            inventory = data.inventory;
            if (data.unlockedAbilities != null)
            unlockedAbilities = data.unlockedAbilities;
            if (data.basicAbility != null)
            basicAbility = data.basicAbility;
            if (data.equippedAbilities != null)
            equippedAbilities = data.equippedAbilities;
        }
        else
        {
            ResetInventory();
        }
        SaveBackup();
    }

    public void ResetInventory() 
    {
        level = 1;
        experience = 0;
        gold = 100;
        baseHealth = 15;
        baseMana = 20;
        classType = "Wizard";
        helmet = new StandardCap();
        chestplate = new StandardRobe();
        boots = new StandardBoots();
        weapon = new StandardWand();
        inventory = new List<Item>();
        inventory.Add(new LessStandardRobe());
        inventory.Add(new OgresWand());
        inventory.Add(new WandOfWhite());
        unlockedAbilities = new List<string>() {
            "Fireball",
            "Lightning",
            "MagicBolt"
        };
        basicAbility = "MagicBolt";
        equippedAbilities = new List<string>() {
            "Fireball",
            "Lightning",
            "",
            ""
        };

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
}

[Serializable]
class PlayerData
{
    public int level;
    public int experience;
    public int gold;

    public float baseHealth;
    public float baseMana;

    public string classType;

    public Armour helmet;
    public Armour chestplate;
    public Armour boots;
    public Weapon weapon;


    public List<Item> inventory;
    public List<string> unlockedAbilities;
    public string basicAbility;
    public List<string> equippedAbilities;
}
