using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button button1, button2;
    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(StartGame);
        button2.onClick.AddListener(OpenEquipmentMenu);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Level1-0");
    }

    void OpenEquipmentMenu()
    {
        SceneManager.LoadScene("Equipment Menu");
    }
}
