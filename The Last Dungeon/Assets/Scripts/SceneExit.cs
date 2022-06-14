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
                SpawnPlayer.spawnLocal = new Vector3(-429.6645f, 6.34f, 567.4065f);
            }
        }
    }
}
