using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadIcon : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider slider;
    public Text progressTExt;
    public void loadLevel(int sceneIndex)
    {
       
       StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/ .9f);
            Debug.Log(operation.progress);
            slider.value = progress;
            progressTExt.text = progress * 100f + "%";
            yield return null;
        }
    }
}
