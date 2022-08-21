using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //method for closing the game
    public void QuitGame()
    {
        Application.Quit();
    }

    //method for loading the game scene
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
