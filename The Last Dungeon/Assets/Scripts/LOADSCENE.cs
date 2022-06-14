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
                SpawnPlayer.spawnLocal = new Vector3(-436.8f, 7.95f, 572.52f);
            }
        }
    }
}
