using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuControl : MonoBehaviour
{
    
    public void StartGame()
    {
        new WaitForSeconds(10);
        SceneManager.LoadScene("CutSceneInicial");
        ONLOAD._playerIsDead = false;
    }

    public void Exit()
    {
        Application.Quit();
        print("Quit");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Lobby");
        ONLOAD._playerIsDead = false;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }
}