using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class SceneTransition : MonoBehaviour
{
    [Header("Transition Properties")]
    [SerializeField] protected float _outDuration = 0.5f;
    [SerializeField] protected float _inDuration = 0.5f;
    [SerializeField] protected float _delayAfterOut = 0;
    [SerializeField] protected float _delayBeforeIn = 0.5f;
    protected Action OnAfterOut;
    protected Action OnBeforeIn;
    public static Action s_onBeforeOut; // useful to disable input
    public static Action s_onAfterIn; // useful to enable input

    /// <summary>
    /// Load scene with transition
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public SceneTransition StartSceneTransition(string sceneName)
    {
        SceneTransition transition = Instantiate(this, Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(transition.gameObject);
        transition.StartCoroutine(transition.LoadSceneAnimation(sceneName));
        return transition;
    }


    /// <summary>
    /// Start Scene Transition but can also be used without changing scene
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public SceneTransition StartTransitionWithoutLoadingScene()
    {
        SceneTransition transition = Instantiate(this, Vector3.zero, Quaternion.identity);
        transition.StartCoroutine(transition.LoadAnimationWithoutLoadingScene());
        return transition;
    }

    /// <summary>
    /// Useful to make showing loading bar less laggy
    /// </summary>
    /// <param name="delay">Duration for the delay</param>
    /// <returns></returns>
    public SceneTransition SetDelayAfterOut(float delay)
    {
        _delayAfterOut = delay;
        return this;
    }

    /// <summary>
    /// Useful to make the start of In animation less laggy
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public SceneTransition SetDelayBeforeIn(float delay)
    {
        _delayBeforeIn = delay;
        return this;
    }

    /// <summary>
    /// Set the duration of the out animation
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public SceneTransition SetOutDuration(float duration)
    {
        _outDuration = duration;
        return this;
    }

    /// <summary>
    /// Set the duration of the in animation
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public SceneTransition SetInDuration(float duration)
    {
        _inDuration = duration;
        return this;
    }

    /// <summary>
    /// Add function to call after out animation. useful to add loading bar
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public SceneTransition AddListenerAfterOut(Action action)
    {
        OnAfterOut += action;
        return this;
    }

    /// <summary>
    /// Add function to call before in animation. useful to destroy loading bar
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public SceneTransition AddListenerBeforeIn(Action action)
    {
        OnBeforeIn += action;
        return this;
    }

    /// <summary>
    /// Remove OnAfterOut and OnBeforeIn
    /// </summary>
    /// <returns></returns>
    public SceneTransition RemoveAllListener()
    {
        OnAfterOut = null;
        OnBeforeIn = null;
        return this;
    }

    /// <summary>
    /// Start the animation
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator LoadSceneAnimation(string sceneName)
    {
        s_onBeforeOut?.Invoke();
        yield return StartCoroutine(OutAnimation());
        
        yield return new WaitForSecondsRealtime(_delayAfterOut);
        OnAfterOut?.Invoke();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        
        yield return new WaitForSecondsRealtime(_delayBeforeIn);
        OnBeforeIn?.Invoke();

        yield return StartCoroutine(InAnimation());
        Destroy(gameObject);
        s_onAfterIn?.Invoke();
    }

    /// <summary>
    /// Start the animation without loading scene
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadAnimationWithoutLoadingScene()
    {
        s_onBeforeOut?.Invoke();
        yield return StartCoroutine(OutAnimation());
        
        yield return new WaitForSecondsRealtime(_delayAfterOut);
        OnAfterOut?.Invoke();

        yield return new WaitForSecondsRealtime(_delayBeforeIn);
        OnBeforeIn?.Invoke();

        yield return StartCoroutine(InAnimation());
        Destroy(gameObject);
        s_onAfterIn?.Invoke();
    }

    /// <summary>
    /// The out animation itself
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator OutAnimation()
    {
        yield return null;
    }

    /// <summary>
    /// The in animation itself
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator InAnimation()
    {
        yield return null;
    }
}
