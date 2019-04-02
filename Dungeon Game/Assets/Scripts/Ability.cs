using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability
{
    public Player p;
    public string name;
    public string lore;
    public string description;
    public string imageName;
    public string type = "normal";
    public int abilityNumber;

    public float manaCost;
    public float damage;
    public float cooldown;

    public abstract void Instantiate(Player p);

    public abstract bool Cast(float damage, float manaCost, bool crit);

    public void SetButton(Button abilityButton, int number)
    {
        Sprite image = Resources.Load(imageName, typeof(Sprite)) as Sprite;
        abilityButton.transform.GetChild(0).GetComponent<Image>().sprite = image;
        UIAbilityScript script = abilityButton.GetComponent<UIAbilityScript>();
        script.ability = number;
        script.c = p.c;
        script.p = p;
        abilityNumber = number;
    }
}
