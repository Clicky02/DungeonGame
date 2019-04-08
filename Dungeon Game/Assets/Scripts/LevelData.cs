    
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelData : MonoBehaviour
{

    public static LevelData data;
    
    public Player p;
    public List<Item> gatheredLoot = new List<Item>();
    public int gatheredGold = 0;

    Vector3Int currentOffset = Vector3Int.zero;
    
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

    public void ChangeScene(string sceneName)
    {
        currentOffset = Vector3Int.zero;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void ChangeScene(string sceneName, Vector3Int offset)
    {
        
        ChangeScene(sceneName);
        currentOffset = offset;
    }

    public void SetNewPlayer(Player np)
    {
        Debug.Log(currentOffset);
        np.mana = p.mana;
        np.health = p.health;
        Destroy(p.gameObject);
        p = np;
        p.tilePos += currentOffset;
        p.transform.position += currentOffset;
    }
}
