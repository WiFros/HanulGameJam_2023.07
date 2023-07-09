using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeaneManager : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    public GameObject StartUI;
    public GameObject LoardingUI;
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("PlayScene");
        StartUI.SetActive(false);
        LoardingUI.SetActive(true);
        StartCoroutine(LoadMainSceneAsync());
    }
    private IEnumerator LoadMainSceneAsync()
    {
        Time.timeScale = 1;
        var loading = SceneManager.LoadSceneAsync("PlayScene");

        while (!loading.isDone)
        {
            progressBar.value = loading.progress;
            yield return null;
        }
    }
}
