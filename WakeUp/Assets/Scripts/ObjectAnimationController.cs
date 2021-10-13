using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimationController : MonoBehaviour
{
    private Animator _Transition;
    public string TriggerName;
    public float Time;
    private bool _Playing;

    // Start is called before the first frame update
    void Start()
    {
        _Transition = GetComponent<Animator>();
    }

    void Update()
    {
        if (!_Playing)
        {
            _Playing = true;
            PlayAnimationAfterTime(Time);
        }   
    }

    void PlayAnimation()
    {
        if (TriggerName != "") _Transition.SetTrigger(TriggerName);
    }

    void PlayAnimationAfterTime(float time)
    {
        this.StartTimedAction(null, () =>
        {
            PlayAnimation();
        }, time);
    }

    
}
