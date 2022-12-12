using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    float _delayBeforeOut = 0.0f;
    float _delayAfterOut = 0.1f;

    float _delayBeforeIn = 0.5f;
    float _delayAfterIn = 0.0f;
    
    bool _isMusicFade = true;

    float _musicFadeOutDuration = 0.4f;
    
    public TransitionAnimation Anim;
    void Start(){SetOutDefault();SetInDefault();}
#region Set
    public TransitionManager SetDelayAfterOut(float t)
    {
        _delayAfterOut = t;
        return this;
    }
    public TransitionManager SetDelayBeforeIn(float t)
    {
        _delayBeforeIn = t;
        return this;
    }

    public TransitionManager SetMusicFade(bool b)
    {
        _isMusicFade = b;
        return this;
    }



    public TransitionManager SetOutStart(OutStartDelegate func)
    {
        OutStart = func;
        return this;
    }
    public TransitionManager SetOutEnd(OutEndDelegate func)
    {
        OutEnd = func;
        return this;
    }
    public TransitionManager SetOutStart(OutStartTransitionDelegate func)
    {
        OutStartTransition = func;
        return this;
    }
    public TransitionManager SetOutEnd(OutEndTransitionDelegate func)
    {
        OutEndTransition = func;
        return this;
    }


    public TransitionManager AddOutStart(OutStartDelegate func)
    {
        OutStart += func;
        return this;
    }
    public TransitionManager AddOutEnd(OutEndDelegate func)
    {
        OutEnd += func;
        return this;
    }
    public TransitionManager AddOutStart(OutStartTransitionDelegate func)
    {
        OutStartTransition += func;
        return this;
    }
    public TransitionManager AddOutEnd(OutEndTransitionDelegate func)
    {
        OutEndTransition += func;
        return this;
    }

    
    public TransitionManager SetInStart(InStartDelegate func)
    {
        InStart = func;
        return this;
    }
    public TransitionManager SetInEnd(InEndDelegate func)
    {
        InEnd = func;
        return this;
    }
    public TransitionManager SetInStart(InStartTransitionDelegate func)
    {
        InStartTransition = func;
        return this;
    }
    public TransitionManager SetInEnd(InEndTransitionDelegate func)
    {
        InEndTransition = func;
        return this;
    }


    public TransitionManager AddInStart(InStartDelegate func)
    {
        InStart += func;
        return this;
    }
    public TransitionManager AddInEnd(InEndDelegate func)
    {
        InEnd += func;
        return this;
    }
    public TransitionManager AddInStart(InStartTransitionDelegate func)
    {
        InStartTransition += func;
        return this;
    }
    public TransitionManager AddInEnd(InEndTransitionDelegate func)
    {
        InEndTransition += func;
        return this;
    }

#endregion Set


    public void SetOutDefault()
    {
        _delayBeforeOut = 0.0f;
        _delayAfterOut = 0.1f;
        _isMusicFade = true;
        _musicFadeOutDuration = 0.4f;
        OutStart = null;
        OutStartTransition = null;
        OutEnd = null;
        OutEndTransition = null;
    }
    public void SetInDefault()
    {
        _delayBeforeIn = 0.5f;
        _delayAfterIn = 0.0f;
        _isMusicFade = true;
        InStart = null;
        InStartTransition = null;
        InEnd = null;
        InEndTransition = null;
    }

    public delegate void OutStartDelegate();
    OutStartDelegate OutStart;
    public delegate void OutEndDelegate();
    OutEndDelegate OutEnd;

    public delegate void InStartDelegate();
    InStartDelegate InStart;
    public delegate void InEndDelegate();
    InEndDelegate InEnd;

    public delegate TransitionManager OutStartTransitionDelegate(); //Overload for Out() because the return type isn;t void. Could have used generic or with UnityAction, but since it's only for 1 function which is Out() and In() it'll be fine.
    OutStartTransitionDelegate OutStartTransition;
    public delegate TransitionManager OutEndTransitionDelegate();
    OutEndTransitionDelegate OutEndTransition;

    public delegate TransitionManager InStartTransitionDelegate();
    InStartTransitionDelegate InStartTransition;
    public delegate TransitionManager InEndTransitionDelegate();
    InEndTransitionDelegate InEndTransition;

    

    public TransitionManager Out()
    {
        StartCoroutine(TransitionOut());
        return this;
    }

    IEnumerator TransitionOut()
    {
        Singleton.Instance.Game.SetActiveAllInput(false);
        OutStart?.Invoke();
        OutStartTransition?.Invoke();
        yield return new WaitForSecondsRealtime(_delayBeforeOut);
        if(_isMusicFade)StartCoroutine(MusicFadeOut());
        yield return StartCoroutine(Anim.OutAnimation());
        yield return new WaitForSecondsRealtime(_delayAfterOut);
        Singleton.Instance.Game.SetActiveAllInput(true);
        OutEnd?.Invoke();
        OutEndTransition?.Invoke();
        SetOutDefault(); // Because the delegates might still reference a function from another scene
    }

    IEnumerator MusicFadeOut()
    {
        float t = 0;
        AudioManager audio = Singleton.Instance.Audio;
        while(t <= 1)
        {
            audio.SetMusicSourceVolume(1 - t);
            t += Time.unscaledDeltaTime/_musicFadeOutDuration;
            yield return null;
        }
        audio.SetMusicSourceVolume(0);
    }



    public TransitionManager In()
    {
        StartCoroutine(TransitionIn());
        return this;
    }
    IEnumerator TransitionIn()
    {
        Singleton.Instance.Game.SetActiveAllInput(false);
        InStart?.Invoke();
        InStartTransition?.Invoke();
        yield return new WaitForSecondsRealtime(_delayBeforeIn);
        Singleton.Instance.Audio.SetMusicSourceVolume(1); // Transition in doesn't need fading music, just do it instantly
        yield return StartCoroutine(Anim.InAnimation());
        yield return new WaitForSecondsRealtime(_delayAfterIn);
        Singleton.Instance.Game.SetActiveAllInput(true);
        InEnd?.Invoke();
        InEndTransition?.Invoke();
        SetInDefault();
    }
}
