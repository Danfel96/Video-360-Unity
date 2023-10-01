using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject panelLoading;
    [SerializeField] Animator anim;
    int currentSceneIndex;

    public static SceneLoader Instance { get; private set; }

    private void Awake() => Instance = this;

    void Start()
    {
        currentSceneIndex = GetSceneIndex();
    }

    public void SetActionAndLoadScene(string sceneName, Action action)
    {
        LoadSceneWithFade(sceneName);
    }

    public void LoadFirstScene()
    {
        if(panelLoading == null) { SceneManager.LoadScene(0); return; }
        StartCoroutine(LoadingProcess(0));
    }

    public void LoadNextScene()
    {
        if (panelLoading == null) { SceneManager.LoadScene(currentSceneIndex + 1); return; }
        StartCoroutine(LoadingProcess(currentSceneIndex + 1));
    }

    public void LoadPreviousScene()
    {
        if (panelLoading == null) { SceneManager.LoadScene(currentSceneIndex - 1); return; }
        StartCoroutine(LoadingProcess(currentSceneIndex - 1));
    }

    public void LoadScene(int sceneIndex)
    {
        if(panelLoading == null) { SceneManager.LoadScene(sceneIndex); return; }
        StartCoroutine(LoadingProcess(sceneIndex));
    }

    public void LoadScene(string sceneName)
    {
        if(panelLoading == null) { SceneManager.LoadScene(sceneName); return; }
        StartCoroutine(LoadingProcess(sceneName));
    }

    public void ReloadScene()
    {
        if (panelLoading == null) { SceneManager.LoadScene(currentSceneIndex); return; }
        StartCoroutine(LoadingProcess(currentSceneIndex));
    }

    IEnumerator LoadingProcess(int sceneIndex)
    {
        AsyncOperation progress = SceneManager.LoadSceneAsync(sceneIndex);
        if (!progress.isDone)
        {
            panelLoading.SetActive(true);
            yield return null;
        }
    }

    IEnumerator LoadingProcess(string sceneName)
    {
        AsyncOperation progress = SceneManager.LoadSceneAsync(sceneName);
        if(!progress.isDone)
        {
            panelLoading.SetActive(true);
            yield return null;
        }
    }
    
    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeOut(string sceneName)
    {
        if (anim == null)
            yield break;
        anim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1f);
        LoadScene(sceneName);
    }

    public void QuitGame() => Application.Quit();

    public int GetSceneIndex() => SceneManager.GetActiveScene().buildIndex;

    public string GetSceneName() => SceneManager.GetActiveScene().name;
}
