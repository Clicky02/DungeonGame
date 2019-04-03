    
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelData : MonoBehaviour
{

    public static LevelData data;
    
    public Player p;
    public List<Item> gatheredLoot = new List<Item>();
    public int gatheredGold = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (data == null)
        {
            data = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (data != this)
        {
            Destroy(gameObject);
        }
    }
}
