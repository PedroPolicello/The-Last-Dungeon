using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public static Vector3 spawnLocal;
    public static bool changeScene;

    public void Awake()
    {
        if(changeScene == true)
        {
           GameObject player = FindObjectOfType<PlayerControl>().gameObject;
            player.transform.position = spawnLocal;
        }
       
    }
}
