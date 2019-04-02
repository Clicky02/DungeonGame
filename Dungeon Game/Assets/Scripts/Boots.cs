using System;
using System.Collections.Generic;

[Serializable]
public class StandardBoots : Armour
{
    public StandardBoots()
    {
        Refresh();
    }
    public void Refresh()
    {
        id = 3;
        type = "Boots";
        defense = 1;
        healthBoost = 0;
        manaBoost = 1;
        perks = new List<string>();
        perks.Add("Vengeance");
        name = "Standard Boots";
        lore = "Your First Wizard Boots";
        imageName = "RedYellowBoot";

    }
  
}