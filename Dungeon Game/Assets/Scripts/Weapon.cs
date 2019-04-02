using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Weapon : Item
{
    public float physicalAttack;
    public float magicAttack;
    public float healthBoost;
    public float manaBoost;
    public string weaponType;
    public List<string> perks;

}
