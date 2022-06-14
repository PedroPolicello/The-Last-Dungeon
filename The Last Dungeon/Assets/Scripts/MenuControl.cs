using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuControl : MonoBehaviour
{
    public string sceneToLoad;

    public void SceneIntro()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void Exit()
    {
        Application.Quit();
        print("Quit");
    }
   
}
