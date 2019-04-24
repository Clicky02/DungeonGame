using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreationScript : MonoBehaviour
{
    private float timeSinceLoad = 0.0f;
    private bool counting = true;
    public int step = 1;
    public GameObject stepOne;
    public GameObject stepTwo;
    public GameObject stepThree;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counting) {
            timeSinceLoad += Time.deltaTime;
            if (timeSinceLoad > 5f)
            {
                counting = false;
                NextStep();
            }
        }
    }

    public void SetClass(int classNumber)
    {
        if (classNumber == 1)
        {
            GameData.data.classType = "Wizard";
        }
        NextStep();
    }

    public void SetSpecialization(int specializationNumber)
    {
        GameData.data.endurance = 3;
        GameData.data.strength = 3;
        GameData.data.luck = 3;
        GameData.data.intelligence = 3;
        GameData.data.dexterity = 3;

        switch (specializationNumber)
        {
            case 1:
                GameData.data.specialization = "endurance";
                GameData.data.endurance = 6;
                break;
            case 2:
                GameData.data.specialization = "strength";
                GameData.data.strength = 6;
                break;
            case 3:
                GameData.data.specialization = "luck";
                GameData.data.luck = 6;
                break;
            case 4:
                GameData.data.specialization = "intelligence";
                GameData.data.intelligence = 6;
                break;
            case 5:
                GameData.data.specialization = "dexterity";
                GameData.data.dexterity = 6;
                break;
        }
        FinalizeCharacter();
        NextStep();
        
    }

    public void FinalizeCharacter()
    {
        GameData data = GameData.data;
        data.level = 1;
        data.experience = 0;
        data.gold = 100;

        data.baseHealth = 50;
        data.baseMana = 10;
        data.baseAttack = 30;
        data.baseDefense = 10;
        data.baseAccuracy = 0.8f;
        data.baseCritChance = 0.07f;
        data.baseCritMultiplier = 1.3f;
        data.baseEvadeChance = 0.05f;

        data.unassignedStatPoints = 0;
        data.playerName = "Luxusor";

        data.helmet = new StandardCap();
        data.chestplate = new StandardRobe();
        data.boots = new StandardBoots();
        data.weapon = new StandardWand();
        data.inventory = new List<Item>();
        data.inventory.Add(new LessStandardRobe());
        data.inventory.Add(new OgresWand());
        data.inventory.Add(new WandOfWhite());
        data.unlockedAbilities = new List<string>() {
            "Fireball",
            "Lightning",
            "MagicBolt"
        };
        data.basicAbility = "MagicBolt";
        data.equippedAbilities = new List<string>() {
            "Fireball",
            "Lightning",
            "",
            ""
        };
    }

    public void NextStep()
    {
        if (step == 1)
        {
            stepOne.SetActive(false);
            stepTwo.SetActive(true);
        }
        else if (step == 2)
        {
            stepTwo.SetActive(false);
            stepThree.SetActive(true);
        }
        else if (step == 3)
        {
            SceneManager.LoadScene("Main Menu");
        }
        step++;
    }
}
