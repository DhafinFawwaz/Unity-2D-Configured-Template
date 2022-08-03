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

    int key;
    public void EnterAnimation(){StartCoroutine(Enter());}
    public void ExitAnimation(){StartCoroutine(Exit());}
    public void DownAnimation(){StartCoroutine(Down());}
    public void UpAnimation(){StartCoroutine(Up());}
    IEnumerator Enter()
    {
        key++;
        int requirement = key;
        float currentScale = imageToResize.localScale.x;
        float t = 0;
        while (t <= 1 && requirement == key)
        {
            imageToResize.localScale = Vector3.one * Mathf.Lerp(currentScale, enterScale, Global.EaseOutQuartCurve(t));
            t += Time.unscaledDeltaTime / duration;
            yield return null;
        }
        if(requirement == key)imageToResize.localScale = Vector3.one * enterScale;
    }
    IEnumerator Exit()
    {
        key++;
        int requirement = key;
        float currentScale = imageToResize.localScale.x;
        float t = 0;
        while (t <= 1 && requirement == key)
        {
            imageToResize.localScale = Vector3.one * Mathf.Lerp(currentScale, exitScale, Global.EaseOutQuartCurve(t));
            t += Time.unscaledDeltaTime / duration;
            yield return null;
        }
        if(requirement == key)imageToResize.localScale = Vector3.one * exitScale;
    }
     
    IEnumerator Down()
    {
        key++;
        int requirement = key;
        float currentScale = imageToResize.localScale.x;
        float t = 0;
        while (t <= 1 && requirement == key)
        {
            imageToResize.localScale = Vector3.one * Mathf.Lerp(currentScale, downScale, Global.EaseOutQuartCurve(t));
            t += Time.unscaledDeltaTime / duration;
            yield return null;
        }
        if(requirement == key)imageToResize.localScale = Vector3.one * downScale;
    }
    IEnumerator Up()
    {
        key++;
        int requirement = key;
        float currentScale = imageToResize.localScale.x;
        float t = 0;
        while (t <= 1 && requirement == key)
        {
            imageToResize.localScale = Vector3.one * Mathf.Lerp(currentScale, upScale, Global.EaseOutQuartCurve(t));
            t += Time.unscaledDeltaTime / duration;
            yield return null;
        }
        if(requirement == key)imageToResize.localScale = Vector3.one * upScale;
    }

}
