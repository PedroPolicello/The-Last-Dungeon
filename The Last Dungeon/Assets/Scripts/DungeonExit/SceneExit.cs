using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Lobby");
            SpawnPlayer.spawnLocal = new Vector3(1f, -1f, 26f);

            SpawnPlayer.abrirFloresta = true;
        }
    }
}
