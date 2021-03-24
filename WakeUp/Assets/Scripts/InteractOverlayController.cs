using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractOverlayController : MonoBehaviour
{
    public TextMeshProUGUI MainText;
    public TextMeshProUGUI ClosingText;

    private Animator _Transition;
    public BoolVariable OverlayOpen;
    // Start is called before the first frame update
    void Start()
    {
        _Transition = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMainText(string mainText) {
        MainText.text = mainText;
    }

    public void UpdateClosingText(string mainText)
    {
        ClosingText.text = mainText;
    }

    public void SetOverlayState(object value)
    {
        OverlayOpen.RuntimeValue = (bool)value;
        _Transition.SetBool("Open", OverlayOpen.RuntimeValue);
    }
}
