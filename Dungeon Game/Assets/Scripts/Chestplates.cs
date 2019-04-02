using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StandardRobe : Armour
{
    public StandardRobe()
    {
        Refresh();
    }
    public void Refresh()
    {
        id = 2;
        type = "Chestplate";
        defense = 1;
        healthBoost = 0;
        manaBoost = 1;
        perks = new List<string>();
        name = "Standard Robe";
        lore = "Your First Wizard Robe";
        imageName = "RedYellowRobe";

    }
}
[Serializable]
public class LessStandardRobe : Armour
{
    public LessStandardRobe()
    {
        Refresh();
    }
    public void Refresh()
    {
        id = 5;
        type = "Chestplate";
        defense = 1;
        healthBoost = 1;
        manaBoost = 0;
        perks = new List<string>();
        perks.Add("Fierce");
        name = "Less Standard Robe";
        lore = "Found under a chest. Not in the chest, under it.";
        imageName = "RedYellowRobe";
    }
}
