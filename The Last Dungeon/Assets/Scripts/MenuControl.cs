using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuControl : MonoBehaviour
{
    public string sceneToLoad;

    public void StartGame()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void Exit()
    {
        Application.Quit();
        print("Quit");
    }
   
}
