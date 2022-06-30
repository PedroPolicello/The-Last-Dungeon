using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeloExit : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Lobby");
            SpawnPlayer.spawnLocal = new Vector3(-0.5f, -0.5f, -20f);

            SpawnPlayer.abrirFloresta = true;
            SpawnPlayer.abrirCaverna = true;
            SpawnPlayer.abrirGelo = true;
        }
    }
}
