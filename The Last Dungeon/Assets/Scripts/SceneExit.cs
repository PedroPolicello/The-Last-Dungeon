using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneExit : MonoBehaviour
{
    public string sceneToLoad;
   

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sceneToLoad == "MainGame")
            {
                SceneManager.LoadScene("MainGame");
                SpawnPlayer.spawnLocal = new Vector3(-0.78f, -1.07f, 30f);
            }
        }
    }
}
