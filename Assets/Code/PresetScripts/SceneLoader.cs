using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Loading loadingBar;

    public void LoadScene(string sceneName)
    {
        loadingBar.gameObject.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneName));
    }


    public delegate void OnLoadingEndDelegate();
    OnLoadingEndDelegate OnLoadingEnd;
    public void AddOnLoadingEnd(OnLoadingEndDelegate func)
    {
        OnLoadingEnd += func;
    }


    IEnumerator LoadAsynchronously(string sceneName)
    {
        yield return null;
        Time.timeScale = 0;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);
            loadingBar.Fill(progress);
            yield return null;
        }
        if(OnLoadingEnd != null)
        {
            OnLoadingEnd();
            OnLoadingEnd = null;
        }
        loadingBar.gameObject.SetActive(false);
    }

    string sceneToLoad;
    public void LoadSceneWithTransition(string sceneName)
    {
        sceneToLoad = sceneName;
        Singleton.Instance.transition.Out()
            .AddOutEnd(LoadSceneName)
        ;
        AddOnLoadingEnd(Singleton.Instance.transition.InDefault);
    }
    void LoadSceneName()
    {
        loadingBar.gameObject.SetActive(true);
        StartCoroutine(LoadAsynchronouslyWithTransition(sceneToLoad));
    }
    IEnumerator LoadAsynchronouslyWithTransition(string sceneName)
    {
        yield return null;
        Time.timeScale = 0;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);
            loadingBar.Fill(progress);
            yield return null;
        }
        if(OnLoadingEnd != null)
        {
            OnLoadingEnd();
            OnLoadingEnd = null;
        }
        loadingBar.gameObject.SetActive(false);
    }
}
