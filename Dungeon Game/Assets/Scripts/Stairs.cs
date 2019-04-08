using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : InteractableTile
{

    public string sceneName;
    public Vector3Int spawnOffset;
   
    public override void Interact(MoveEvent e)
    {
        if (e.e is Player) LevelData.data.ChangeScene(sceneName, spawnOffset);
        else e.Cancel();
    }
}
