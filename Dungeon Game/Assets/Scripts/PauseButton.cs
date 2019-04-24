using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    private bool paused = false;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Clicked()
    {
        if (paused)
        {
            Time.timeScale = 1;
            paused = !paused;
            pauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            paused = !paused;
            pauseMenu.SetActive(true);
        }
    }

    public void Exit()
    {
        Time.timeScale = 1;
        Destroy(GameObject.Find("Player"));
        SceneManager.LoadScene("Main Menu",LoadSceneMode.Single);
        
    }
}
