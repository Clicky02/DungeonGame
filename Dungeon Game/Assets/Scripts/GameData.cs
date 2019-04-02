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
            level = data.level;
            experience = data.experience;

            classType = data.classType;

            baseHealth = data.baseHealth;
            baseMana = data.baseMana;

            helmet = data.helmet;
            chestplate = data.chestplate;
            boots = data.boots;
            weapon = data.weapon;

            inventory = data.inventory;
            unlockedAbilities = data.unlockedAbilities;
            basicAbility = data.basicAbility;
            equippedAbilities = data.equippedAbilities;

        }
        else
        {
            ResetInventory();
        }
    }

    public void ResetInventory() 
    {
        level = 1;
        experience = 0;
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