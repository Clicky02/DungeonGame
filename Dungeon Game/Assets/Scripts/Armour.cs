using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Armour : Item
{
    public float defense;
    public float healthBoost;
    public float manaBoost;
    public List<string> perks;
    
}
