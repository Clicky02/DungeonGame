using System;
using System.Collections.Generic;

[Serializable]
public class StandardWand : Weapon
{
    public StandardWand()
    {
        Refresh();
    }
    public void Refresh()
    {
        id = 4;
        type = "Weapon";
        weaponType = "Wand";
        physicalAttack = 2;
        magicAttack = 3;
        healthBoost = 0;
        manaBoost = 3;
        perks = new List<string>();
        name = "Standard Wand";
        lore = "Your First Wand";
        imageName = "Staff1";

    }

}

[Serializable]
public class OgresWand : Weapon
{
    public OgresWand()
    {
        Refresh();
    }
    public void Refresh()
    {
        id = 6;
        type = "Weapon";
        weaponType = "Wand";
        physicalAttack = 2;
        magicAttack = 3;
        healthBoost = 0;
        manaBoost = 3;
        perks = new List<string>();
        name = "Ogre's Wand";
        lore = "Actually Kind of Ugly";
        imageName = "Staff1";

    }

}

[Serializable]
public class WandOfWhite : Weapon
{
    public WandOfWhite()
    {
        Refresh();
    }
    public void Refresh()
    {
        id = 6;
        type = "Weapon";
        weaponType = "Wand";
        physicalAttack = 4;
        magicAttack = 6;
        healthBoost = 0;
        manaBoost = 10;
        perks = new List<string>();
        name = "Wand of White";
        lore = "Well at least some of it's white.";
        imageName = "Staff1";

    }

}