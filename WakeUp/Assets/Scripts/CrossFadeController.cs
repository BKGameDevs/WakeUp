using UnityEngine.Events;
using UnityEngine;

public class CrossFadeController : Singleton<OverlayController>
{
    private Animator _Transition;
    private bool _IsFading;

    // Start is called before the first frame update
    void Start()
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
        
        RunCrossFadeWithAction(time, () => {
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

    public void RunCrossFadeWithAction(float time, UnityAction action)
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
            time);
        },
        time);

        // perform action while screen is black
        

       
    }
}
