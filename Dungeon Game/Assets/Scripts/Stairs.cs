using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : InteractableTile
{

    public string sceneName;

   
    public override void Interact(MoveEvent e)
    {
        Debug.Log("hi");
        if (e.e is Player) SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        else e.Cancel();
    }
}
