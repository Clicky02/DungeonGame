using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StandardCap : Armour
{
    public StandardCap()
    {
        Refresh();
    }
    public void Refresh()
    {
        id = 1;
        type = "Helmet";
        defense = 1;
        healthBoost = 0;
        manaBoost = 1;
        perks = new List<string>();
        name = "Standard Cap";
        lore = "Your First Wizard Cap";
        imageName = "RedYellowHat";
        
    }
}
