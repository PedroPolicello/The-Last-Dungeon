using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuControl : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("CutSceneInicial");
    }
    public void Exit()
    {
        Application.Quit();
        print("Quit");
    }
   
}
