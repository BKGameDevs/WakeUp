using UnityEngine.Events;
using UnityEngine;
using System;

public class CrossFadeController : Singleton<CrossFadeController>
{
    private Animator _Transition;
    private bool _IsFading;
    private Action _CurrentAction;

    protected override void OnAwake()
    {
        _Transition = GetComponent<Animator>();
    }

    public void InvokeCrossFade(object value)
    {
        if (_IsFading)
            return;

        var time = value is float ? (float)value : 3f;
        RunCrossFade(time);
    }

     public void InvokeCrossFadeWithAction(object value)
    {
        if (_IsFading)
            return;

        var time = value is float ? (float)value : 3f;
        
        RunCrossFadeWithAction(time, time, () => {
            Debug.Log("Action Logger");
        });
    }

    public void RunCrossFade(float time)
    {
        Util.StartTimedAction(this, 
        () => {
            _IsFading = true;
            _Transition.SetBool("Fade Out", _IsFading);
        },
        () => {
            _IsFading = false;
            _Transition.SetBool("Fade Out", _IsFading);
        },
         time);
    }

    public void RunCrossFadeWithAction(float fadeOutTime, float fadeInTime, UnityAction action)
    {
        //Start the fading animation and wait for X seconds
        Util.StartTimedAction(this, 
        () => {
            _IsFading = true;
            _Transition.SetBool("Fade Out", _IsFading);
        },
        () => {

             // Wait X seconds, then fade back in after the action is complated
            Util.StartTimedAction(this,
            action,
            () => {
                _IsFading = false;
                _Transition.SetBool("Fade Out", _IsFading);
            },
            fadeInTime);
        },
        fadeOutTime);

        // perform action while screen is black
    }
    public void RunCrossFade(Action action = null)
    {
        if (_IsFading)
            return;

        _CurrentAction = action;
        _IsFading = true;
        _Transition.SetBool("Fade Out", _IsFading);

        // perform action while screen is black
    }

    public void FadeIn()
    {
        _CurrentAction?.Invoke();

        this.StartTimedAction(null, () =>
        {
            _IsFading = false;
            _Transition.SetBool("Fade Out", _IsFading);
        }, 0.5f);
    }
}
