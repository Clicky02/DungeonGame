using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button button1, button2, button3;
    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(StartGame);
        button2.onClick.AddListener(OpenEquipmentMenu);
        button3.onClick.AddListener(OpenPlayerMenu);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Level1-0");
    }

    void OpenEquipmentMenu()
    {
        SceneManager.LoadScene("Equipment Menu");
    }

    void OpenPlayerMenu()
    {
        SceneManager.LoadScene("Player Menu");
    }

    public void OpenCharacterCreation()
    {
        SceneManager.LoadScene("CharacterCreation");
    }

    public void LevelUp()
    {
        GameData.data.LevelUp();
    }
}
