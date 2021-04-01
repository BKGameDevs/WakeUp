using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFadeController : MonoBehaviour
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
        StartCoroutine(RunCrossFade(time));
    }

    public IEnumerator RunCrossFade(float time)
    {
        _IsFading = true;
        _Transition.SetBool("Fade Out", _IsFading);

        yield return new WaitForSeconds(time);

        _IsFading = false;
        _Transition.SetBool("Fade Out", _IsFading);
    }
}
