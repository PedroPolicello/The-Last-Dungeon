using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public static Vector3 spawnLocal;
    public static bool changeScene;

    //Abrir Floresta
    public static bool abrirFloresta;
    public GameObject cutsceneFloresta;

    //Abrir Caverna
    public static bool abrirCaverna;
    public GameObject cutsceneCaverna;

    //Abrir Gelo
    public static bool abrirGelo;
    public GameObject cutsceneGelo;

    public void Awake()
    {
        if(changeScene == true)
        {
           GameObject player = FindObjectOfType<PlayerControl>().gameObject;
            player.transform.position = spawnLocal;
        }

        if(abrirFloresta)
        {
            cutsceneFloresta.SetActive(true);
        }

        if (abrirCaverna)
        {
            cutsceneCaverna.SetActive(true);
        }

        if (abrirGelo)
        {
            cutsceneGelo.SetActive(true);
        }
    }
}
