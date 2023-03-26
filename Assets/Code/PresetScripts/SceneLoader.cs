using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Loading _loadingBar;
    string _sceneToLoad;

    public delegate void OnLoadingEndDelegate();
    public delegate TransitionManager OnLoadingEndTransitionDelegate();
    OnLoadingEndDelegate OnLoadingEnd;
    OnLoadingEndTransitionDelegate OnLoadingEndTransition;
    public void AddOnLoadingEnd(OnLoadingEndDelegate func) => OnLoadingEnd += func;
    public void AddOnLoadingEnd(OnLoadingEndTransitionDelegate func) => OnLoadingEndTransition += func;

    public void LoadScene(string sceneName)
    {
        _loadingBar.gameObject.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        yield return null;
        Time.timeScale = 0;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);
            _loadingBar.Fill(progress);
            yield return null;
        }
        Time.timeScale = 1;

        _loadingBar.gameObject.SetActive(false);
    }

    public void LoadSceneWithTransition(string sceneName)
    {
        Singleton.Instance.Transition.Out()
            .AddOutEnd(() => {
                _loadingBar.gameObject.SetActive(true);
                StartCoroutine(LoadAsynchronouslyWithTransition(sceneName));
            });
        AddOnLoadingEnd(Singleton.Instance.Transition.In);
    }

    IEnumerator LoadAsynchronouslyWithTransition(string sceneName)
    {
        yield return null;
        Time.timeScale = 0;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);
            _loadingBar.Fill(progress);
            yield return null;
        }
        Time.timeScale = 1;

        OnLoadingEnd?.Invoke();
        OnLoadingEndTransition?.Invoke();
        OnLoadingEnd = null;
        OnLoadingEndTransition = null;
        _loadingBar.gameObject.SetActive(false);
    }

    
    public void LoadSceneWithTransitionWithoutLoading(string sceneName)
    {
        Singleton.Instance.Transition.Out()
            .SetDelayBeforeOut(0)
            .SetDelayAfterOut(0)
            .AddOutEnd(() => {
                SceneManager.LoadScene(sceneName);
                Singleton.Instance.Transition.In()
                    .SetDelayBeforeIn(0.05f)
                    .SetDelayAfterIn(0);
        });
    }
}
