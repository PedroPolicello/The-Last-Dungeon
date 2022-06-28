using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLobby : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("Lobby");
    }
}
