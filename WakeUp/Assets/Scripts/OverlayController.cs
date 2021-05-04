using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OverlayController : Singleton<OverlayController>
{
    public TextMeshProUGUI MainText;
    public TextMeshProUGUI ClosingText;

    private Animator _Transition;
    private bool _IsOpen;
    // Start is called before the first frame update
    void Start()
    {
        _Transition = GetComponent<Animator>();
    }

    public void UpdateMainText(string mainText)
    {
        MainText.text = mainText;
    }

    public void UpdateClosingText(string mainText)
    {
        ClosingText.text = mainText;
    }

    public void Open(string mainText)
    {
        if (_IsOpen)
            return;
        MainText.text = mainText;
        _IsOpen = true;
        _Transition.SetBool("Open", _IsOpen);
    }

    public void Close()
    {
        if (!_IsOpen)
            return;

        _IsOpen = false;
        _Transition.SetBool("Open", _IsOpen);
    }
}
