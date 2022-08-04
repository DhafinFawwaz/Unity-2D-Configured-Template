using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

#if UNITY_EDITOR 
using UnityEditor.Events;
[ExecuteInEditMode]
#endif
public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] float duration = 0.1f;
    [SerializeField] float enterScale = 1.2f;
    [SerializeField] float exitScale = 1f;
    [SerializeField] float downScale  = 0.8f;
    [SerializeField] float upScale  = 1f;
    [SerializeField] Transform imageToResize;


    public UnityEvent clickEvent;
    public UnityEvent pointerEnterEvent;
    public UnityEvent pointerExitEvent;
    public UnityEvent pointerDownEvent;
    public UnityEvent pointerUpEvent;

#if UNITY_EDITOR 
    void Awake()
    {
        //So that it only creates the listener once, when the component is dragged on, not when the scene is loaded.
        if(pointerEnterEvent == null)
        {
            pointerEnterEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(pointerEnterEvent, EnterAnimation);
        }
        if(pointerExitEvent == null)
        {
            pointerExitEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(pointerExitEvent, ExitAnimation);
        }
        if(pointerDownEvent == null)
        {
            pointerDownEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(pointerDownEvent, DownAnimation);
        }
        if(pointerUpEvent == null)
        {
            pointerUpEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(pointerUpEvent, UpAnimation);
        }
        if(imageToResize == null)imageToResize = transform;
    }
#endif


    public void PlaySound(AudioClip audioClip)
    {
        Singleton.Instance.audio.PlaySound(audioClip);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEnterEvent.Invoke();
        // StartCoroutine(Enter());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        pointerExitEvent.Invoke();
        // StartCoroutine(Exit());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownEvent.Invoke();
        // StartCoroutine(Down());
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pointerUpEvent.Invoke();
        // StartCoroutine(Up());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickEvent.Invoke();
    }

    public void EnterAnimation(){StartCoroutine(TweenScale(imageToResize, enterScale));}
    public void ExitAnimation(){StartCoroutine(TweenScale(imageToResize, exitScale));}
    public void DownAnimation(){StartCoroutine(TweenScale(imageToResize, downScale));}
    public void UpAnimation(){StartCoroutine(TweenScale(imageToResize, upScale));}

    int key;
    //Value will keep changing so that everytime a new TweenScale() coroutine is called,
    //the previous coroutine will be stopped and the new Scaling animation will be executed
    //without interuption.
    
    IEnumerator TweenScale(Transform trans, float endScale)
    {
        key++;
        int requirement = key;
        float startScale = trans.localScale.x;
        float t = 0;
        while (t <= 1 && requirement == key)
        {
            trans.localScale = Vector3.one * Mathf.Lerp(startScale, endScale, Global.EaseOutQuartCurve(t));
            t += Time.unscaledDeltaTime / duration;
            yield return null;
        }
        if(requirement == key)trans.localScale = Vector3.one * endScale;//if the key didn't change then get into endScale
    }

}
