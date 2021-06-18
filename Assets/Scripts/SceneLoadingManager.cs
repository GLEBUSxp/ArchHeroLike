using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using EventBusSystem;


public class SceneLoadingManager : MonoBehaviour
{
    public int sceneID;

    public TMP_Text loadingText;
    public TMP_Text loadingProgerssText;
    public TMP_Text winText;
    public Button startButton;

    public void StartLoadingScene()
    {
        loadingText.gameObject.SetActive(true);
        loadingProgerssText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);

        StartCoroutine(AsyncLoad());
    }


    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        while (!operation.isDone) {
            float progress = operation.progress / 0.9f;
            loadingProgerssText.text = string.Format("{0:0}", progress*100);
            yield return null;
        }
    }
}

