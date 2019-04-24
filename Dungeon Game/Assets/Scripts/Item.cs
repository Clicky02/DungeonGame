using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Item
{
    public string imageName;
    public int id;
    public string type = "consumable";
    public string name;
    public string lore;
    public int levelRequirement = 0;
}
