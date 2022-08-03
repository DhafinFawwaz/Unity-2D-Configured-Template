using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{

    float duration = 0.5f;
    float delayAfterOut = 0.2f;
    bool isMusicFade = true;
    bool isMusicFadeInstant = true;
    public TransitionAnimation anim;
    void Start(){SetOutDefault();SetInDefault();}
#region Set
    public TransitionManager SetDelayAfterOut(float t)
    {
        delayAfterOut = t;
        return this;
    }
    public TransitionManager SetDuration(float t)
    {
        duration = t;
        return this;
    }

    public TransitionManager SetMusicFade(bool b)
    {
        isMusicFade = b;
        return this;
    }

    public TransitionManager SetOutAnimation(OutAnimationDelegate func)
    {
        OutAnimation = func;
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
    public TransitionManager AddOutStart(OutStartDelegate func)
    {
        OutStart += func;
        return this;
    }
    public TransitionManager AddOutAnimation(OutAnimationDelegate func)
    {
        OutAnimation += func;
        return this;
    }
    public TransitionManager AddOutEnd(OutEndDelegate func)
    {
        OutEnd += func;
        return this;
    }

    public TransitionManager SetInAnimation(InAnimationDelegate func)
    {
        InAnimation = func;
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
    public TransitionManager AddInStart(InStartDelegate func)
    {
        InStart += func;
        return this;
    }
    public TransitionManager AddInAnimation(InAnimationDelegate func)
    {
        InAnimation += func;
        return this;
    }
    public TransitionManager AddInEnd(InEndDelegate func)
    {
        InEnd += func;
        return this;
    }

#endregion Set


    public void SetOutDefault()
    {
        duration = 0.5f;
        delayAfterOut = 1f;
        isMusicFade = true;
        isMusicFadeInstant = true;
        OutStart     = Singleton.Instance.transition.anim.OutStart;
        OutAnimation = Singleton.Instance.transition.anim.OutAnimation;
        OutEnd       = Singleton.Instance.transition.anim.OutEnd;
    }
    public void SetInDefault()
    {
        duration = 0.5f;
        delayAfterOut = 1f;
        isMusicFade = true;
        isMusicFadeInstant = true;
        InStart     = Singleton.Instance.transition.anim.InStart;
        InAnimation = Singleton.Instance.transition.anim.InAnimation;
        InEnd       = Singleton.Instance.transition.anim.InEnd;
    }

    public delegate void OutStartDelegate();
    OutStartDelegate OutStart;
    public delegate void OutAnimationDelegate(float t);
    OutAnimationDelegate OutAnimation;
    public delegate void OutEndDelegate();
    OutEndDelegate OutEnd;

    public delegate void InStartDelegate();
    InStartDelegate InStart;
    public delegate void InAnimationDelegate(float t);
    InAnimationDelegate InAnimation;
    public delegate void InEndDelegate();
    InEndDelegate InEnd;

    

    public TransitionManager Out()
    {
        StartCoroutine(TransitionOut());
        return this;
    }

    IEnumerator TransitionOut()
    {
        float t = 0;
        OutStart();
        while(t <= 1)
        {
            OutAnimation(t);
            if(isMusicFade)
                Singleton.Instance.audio.SetMusicSourceVolume(1 - t);
            t += Time.unscaledDeltaTime/duration;
            yield return null;
        }
        if(isMusicFade)
            Singleton.Instance.audio.SetMusicSourceVolume(0);
        yield return new WaitForSecondsRealtime(delayAfterOut);
        OutEnd();
        SetOutDefault();
    }
    public void OutDefault()
    {
        SetOutDefault();
        StartCoroutine(TransitionOut());
    }



    public TransitionManager In()
    {
        StartCoroutine(TransitionIn());
        return this;
    }
    IEnumerator TransitionIn()
    {
        float t = 0;
        if(isMusicFadeInstant)
            Singleton.Instance.audio.SetMusicSourceVolume(1);
        InStart();
        while(t <= 1)
        {
            InAnimation(t);
            
            t += Time.unscaledDeltaTime/duration;
            yield return null;
        }
        InEnd();
        SetInDefault();
    }
    public void InDefault()
    {
        SetInDefault();
        StartCoroutine(TransitionIn());
    }



    
}
