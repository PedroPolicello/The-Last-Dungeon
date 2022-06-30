using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LOADSCENE : MonoBehaviour
{
    public string sceneToLoad;
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sceneToLoad == "DungeonAreia")
            {
                SpawnPlayer.changeScene = true;
                SceneManager.LoadScene("DungeonAreia");
                SpawnPlayer.spawnLocal = new Vector3(4.64f, 0f, 47.7f);
            }
            else if (sceneToLoad == "DungeonCaverna")
            {
                SpawnPlayer.changeScene = true;
                SceneManager.LoadScene("DungeonCaverna");
                SpawnPlayer.spawnLocal = new Vector3(-80f, 4f, 32f);
            }
             else if (sceneToLoad == "DungeonFloresta")
            {
                SpawnPlayer.changeScene = true;
                SceneManager.LoadScene("DungeonFloresta");
                SpawnPlayer.spawnLocal = new Vector3(90f, 1f, 40f);
            }
             else if (sceneToLoad == "DungeonGelo")
            {
                SpawnPlayer.changeScene = true;
                SceneManager.LoadScene("DungeonGelo");
                SpawnPlayer.spawnLocal = new Vector3(0.3f, 22f, 63f);
            }
        }
    }
}
