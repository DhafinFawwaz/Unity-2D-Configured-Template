using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] SceneTransition[] _sceneTransition;
    [SerializeField] ProgressBar _loadingBar;

    public void LoadSceneWithProgressBar(string sceneName, int index = 0)
    {
        if(index > _sceneTransition.Length)
        {
            Debug.LogError("Please assign the transition at index " + index);
            return;
        }
        SceneTransition transition = Instantiate(_sceneTransition[index], Vector3.zero, Quaternion.identity);
        StartCoroutine(LoadAsynchronously(sceneName, transition));
    }

    IEnumerator LoadAsynchronously(string sceneName, SceneTransition transition)
    {
        SceneTransition.s_onBeforeOut?.Invoke();
        DontDestroyOnLoad(transition.gameObject);
        yield return transition.StartCoroutine(transition.OutAnimation());
        
        _loadingBar.gameObject.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);
            _loadingBar.Fill(progress);
            yield return null;
        }
        Time.timeScale = 1;

        _loadingBar.gameObject.SetActive(false);

        yield return transition.StartCoroutine(transition.InAnimation());
        Destroy(transition.gameObject);
        SceneTransition.s_onAfterIn?.Invoke();
    }
}

public static class SceneLoader
{
    public static void LoadSceneWithProgressBar(string sceneName, int index = 0)
        => Singleton.Instance.SceneLoader.LoadSceneWithProgressBar(sceneName, index);
}