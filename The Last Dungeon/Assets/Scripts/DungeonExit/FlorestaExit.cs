using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlorestaExit : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Lobby");
            SpawnPlayer.spawnLocal = new Vector3(10f, -1f, 12f);

            SpawnPlayer.abrirCaverna = true;
        }
    }
}
