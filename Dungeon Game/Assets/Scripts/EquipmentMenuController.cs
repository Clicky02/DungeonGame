using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EquipmentMenuController : MonoBehaviour
{
    public GameObject equipMenu;
    public GameObject equipment, loadout;
    public Button button1, button2, button3, button4, backButton, equipBackButton, equipButton, unequipButton, testButton;
    public List<Button> abilityButtons = new List<Button>();
    public GameObject inventoryObject; 
    public Text equipName;
    public Image equipImage;
    public Text lore, info;

    private Item viewedItem;
    private Ability viewedAbility;
    private int itemSpot;
    private Button currentButton;
    private bool abilityEquipMode = false;

    private int invButtonCount = 0;
    
    void Start()
    {

        backButton.onClick.AddListener(ReturnMainMenu);
        equipBackButton.onClick.AddListener(HideEquipMenu);
        equipButton.onClick.AddListener(Equip);

        if (testButton != null) testButton.onClick.AddListener(SwitchScreen);

        Sprite image1 = Resources.Load("Items/" + GameData.data.helmet.imageName, typeof(Sprite)) as Sprite;
        Sprite image2 = Resources.Load("Items/" + GameData.data.chestplate.imageName, typeof(Sprite)) as Sprite;
        Sprite image3 = Resources.Load("Items/" + GameData.data.boots.imageName, typeof(Sprite)) as Sprite;
        Sprite image4 = Resources.Load("Items/" + GameData.data.weapon.imageName, typeof(Sprite)) as Sprite;

        button1.transform.GetChild(0).GetComponent<Image>().sprite = image1;
        button2.transform.GetChild(0).GetComponent<Image>().sprite = image2;
        button3.transform.GetChild(0).GetComponent<Image>().sprite = image3;
        button4.transform.GetChild(0).GetComponent<Image>().sprite = image4;

        button1.onClick.AddListener(() => OpenEquipMenu("Helmet", true, button1));
        button2.onClick.AddListener(() => OpenEquipMenu("Chestplate", true, button2));
        button3.onClick.AddListener(() => OpenEquipMenu("Boots", true, button3));
        button4.onClick.AddListener(() => OpenEquipMenu("Weapon", true, button4));

        unequipButton.onClick.AddListener(UnequipAbility);

        SetEquippedAbilityButton(GameData.data.basicAbility, 0);
        for (int i  = 1; i <= 4; i++)
        {
        
                SetEquippedAbilityButton(GameData.data.equippedAbilities[i - 1], i);
            
        }

        LoadEquipmentIneventory();

    }

    void SetEquippedAbilityButton(string ability, int buttonNumber)
    {
        if (ability != "")
        {
            Type t = Type.GetType(ability);
            Ability a = (Ability)Activator.CreateInstance(t);
            SetEquippedAbilityButton(a, abilityButtons[buttonNumber], buttonNumber);
        }
        else
        {
            SetEquippedAbilityButton(null, abilityButtons[buttonNumber], buttonNumber);
        }
    }

    void SetEquippedAbilityButton(Ability a, Button b, int buttonNumber)
    {
        if (a != null)
        {
            Sprite abilityImage = Resources.Load(a.imageName, typeof(Sprite)) as Sprite;
            b.transform.GetChild(0).GetComponent<Image>().sprite = abilityImage;
        }
        else
        {
            b.transform.GetChild(0).GetComponent<Image>().sprite = null;
        }
        b.onClick.RemoveAllListeners();
        b.onClick.AddListener(() => EquippedAbilityButtons(a, true, buttonNumber, b));
    }

    void SwitchScreen()
    {
        equipment.SetActive(!equipment.activeInHierarchy);
        loadout.SetActive(!loadout.activeInHierarchy);
        ResetInventory();

        if (equipment.activeInHierarchy)
            LoadEquipmentIneventory();
        else
            LoadLoadoutInventory();
    }

    private void ResetInventory()
    {
       invButtonCount = 0;
       for (int i = inventoryObject.transform.childCount-1; i > 0; i--)
        {
            Destroy(inventoryObject.transform.GetChild(i).gameObject);
        }
    }

    void LoadEquipmentIneventory()
    {
        foreach (Item i in GameData.data.inventory)
        {

            PlaceItemButton(i);
        }
    }

    void LoadLoadoutInventory()
    {
        foreach (string abilityString in GameData.data.unlockedAbilities)
        {
            PlaceAbilityButton(abilityString);
        }
    }

    void PlaceAbilityButton(string s)
    {
        PlaceAbilityButton(s, invButtonCount);
        invButtonCount++;
    }

    void PlaceAbilityButton(string s, int num)
    {
        Type t = Type.GetType(s);
        Ability a = (Ability)Activator.CreateInstance(t);
        GameObject button = GameObject.Instantiate(Resources.Load("Button", typeof(GameObject)) as GameObject, inventoryObject.transform);
        Sprite image = Resources.Load(a.imageName, typeof(Sprite)) as Sprite;
        button.transform.GetChild(0).GetComponent<Image>().sprite = image;
        button.transform.localPosition = new Vector3(-240 + (160 * (num % 4)), 222 + (-144 * Mathf.Floor(num / 4)));
        Button b = button.GetComponent<Button>();
        b.onClick.AddListener(() => OpenEquipMenu(a, false, num, b));
    }

    void EquippedAbilityButtons(Ability a, bool equipped,int buttonNumber, Button button)
    {
        
        if (abilityEquipMode)
        {
            SetEquippedAbilityButton(viewedAbility, button, buttonNumber);
            EquipAbility(viewedAbility, buttonNumber);
            foreach (Button b in abilityButtons) b.interactable = true;
            abilityEquipMode = false;
        }
        else if (a != null)
        {
            OpenEquipMenu(a, equipped, buttonNumber, button);
        }
    }

    void PlaceItemButton(Item i)
    {
        PlaceItemButton(i, invButtonCount);
        invButtonCount++;
    }

    void PlaceItemButton(Item i, int num)
    {
        GameObject button = GameObject.Instantiate(Resources.Load("Button", typeof(GameObject)) as GameObject, inventoryObject.transform);
        Sprite image = Resources.Load("Items/" + i.imageName, typeof(Sprite)) as Sprite;
        button.transform.GetChild(0).GetComponent<Image>().sprite = image;
        button.transform.localPosition = new Vector3(-240 + (160 * (num % 4)), 222 + (-144 * Mathf.Floor(num / 4)));
        Button b = button.GetComponent<Button>();
        b.onClick.AddListener(() => OpenEquipMenu(i, false, num, b));
    }

    void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void OpenEquipMenu(Item i, bool equipped, int spot, Button button)
    {
        unequipButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(!equipped);
        equipName.text = i.name;
        equipImage.sprite = Resources.Load("Items/" + i.imageName, typeof(Sprite)) as Sprite;
        lore.text = i.lore;
        equipMenu.SetActive(true);
        if (i.type == "Helmet" || i.type == "Chestplate" || i.type == "Boots")
        {
            Armour a = i as Armour;
            info.text = "Defense - " + a.defense;
            if (a.healthBoost != 0)
            {
                info.text += Environment.NewLine + "Health Boost - " + a.healthBoost;
            }
            if (a.manaBoost != 0)
            {
                info.text += Environment.NewLine + "Mana Boost - " + a.manaBoost;
            }
            if (a.perks.Count > 0)
            {
                info.text += Environment.NewLine;
                foreach (string perk in a.perks) info.text += Environment.NewLine + perk;
            }
        }
        if (i.type == "Weapon")
        {
            Weapon w = i as Weapon;
            info.text = "Physical Attack - " + w.physicalAttack + Environment.NewLine + "Magic Attack - " + w.magicAttack;
            if (w.healthBoost != 0)
            {
                info.text += Environment.NewLine + "Health Boost - " + w.healthBoost;
            }
            if (w.manaBoost != 0)
            {
                info.text += Environment.NewLine + "Mana Boost - " + w.manaBoost;
            }
            if (w.perks.Count > 0)
            {
                info.text += Environment.NewLine;
                foreach (string perk in w.perks) info.text += Environment.NewLine + perk;
            }
        }

        viewedItem = i;
        itemSpot = spot;
        currentButton = button;
    }

    void OpenEquipMenu(Ability a, bool equipped, int spot, Button button)
    {
        unequipButton.gameObject.SetActive(equipped);
        equipButton.gameObject.SetActive(!equipped);
        equipName.text = a.name;
        equipImage.sprite = Resources.Load(a.imageName, typeof(Sprite)) as Sprite;
        lore.text = a.lore;
        equipMenu.SetActive(true);
        info.text = a.description;
        info.text += Environment.NewLine + ((a.type == "normal") ? "Normal" : "Basic") + " Spell";

        info.text += Environment.NewLine + Environment.NewLine + "Mana Cost - " + a.manaCost;
        info.text += Environment.NewLine + "Damage - " + (a.damage * GameData.data.weapon.magicAttack);

        viewedAbility = a;
        itemSpot = spot;
        currentButton = button;
    }

    void OpenEquipMenu(string slot, bool equipped, Button button)
    {
        if (slot == "Helmet") OpenEquipMenu(GameData.data.helmet, equipped, -1, button);
        else if (slot == "Chestplate") OpenEquipMenu(GameData.data.chestplate, equipped, -1, button);
        else if (slot == "Boots") OpenEquipMenu(GameData.data.boots, equipped, -1, button);
        else if (slot == "Weapon") OpenEquipMenu(GameData.data.weapon, equipped, -1, button);
    }

    void HideEquipMenu()
    {
        equipMenu.SetActive(false);
    }

    void Equip()
    {
        if (loadout.activeInHierarchy)
        {
            abilityEquipMode = true;
            if (viewedAbility.type == "basic")
            {
                foreach (Button b in abilityButtons)
                {
                    if (b != abilityButtons[0])
                        b.interactable = false;
                }
            }
            else
            {
                abilityButtons[0].interactable = false;
            }
        }
        else
        {
            Item ri = null;

            if (viewedItem.type == "Helmet")
            {
                ri = GameData.data.helmet;
                GameData.data.helmet = viewedItem as Armour;
                button1.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Items/" + GameData.data.helmet.imageName, typeof(Sprite)) as Sprite;
            }
            else if (viewedItem.type == "Chestplate")
            {
                ri = GameData.data.chestplate;
                GameData.data.chestplate = viewedItem as Armour;
                button2.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Items/" + GameData.data.chestplate.imageName, typeof(Sprite)) as Sprite;
            }
            else if (viewedItem.type == "Boots")
            {
                ri = GameData.data.boots;
                GameData.data.boots = viewedItem as Armour;
                button3.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Items/" + GameData.data.boots.imageName, typeof(Sprite)) as Sprite;
            }
            else if (viewedItem.type == "Weapon")
            {

                ri = GameData.data.weapon;
                GameData.data.weapon = viewedItem as Weapon;
                button4.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Items/" + GameData.data.weapon.imageName, typeof(Sprite)) as Sprite;
            }
            int index = GameData.data.inventory.IndexOf(viewedItem);
            GameData.data.inventory.RemoveAt(index);
            GameData.data.inventory.Insert(index, ri);
            Destroy(currentButton.gameObject);

            PlaceItemButton(ri, itemSpot);
        }
        HideEquipMenu();
    }

    void EquipAbility(Ability a, int aN)
    {
        if (aN == 0) GameData.data.basicAbility = a.GetType().ToString();
        else GameData.data.equippedAbilities[aN-1] = a.GetType().ToString();
    }

    void UnequipAbility()
    {
        SetEquippedAbilityButton("", itemSpot);
        if (itemSpot == 0) GameData.data.basicAbility = "";
            else GameData.data.equippedAbilities[itemSpot - 1] = "";
        HideEquipMenu();
    }
}
