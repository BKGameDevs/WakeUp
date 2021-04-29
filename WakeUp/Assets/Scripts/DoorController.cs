using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : InteractController, IHasOverlayText
{
    private Animator _DoorAnimator;
    private bool _IsDoorOpen, _EnteredNextSection;

    private string _OverlayText;

    protected override void Initialize()
    {
        base.Initialize();
        _DoorAnimator = GetComponent<Animator>();
    }

    public void SetOverlayText(string text)
    {
        _OverlayText = text;
    }

    protected override void StartInteraction()
    {
        base.StartInteraction();

        if (!_IsDoorOpen)
            OverlayController.Instance.Open(_OverlayText);
        else
            SectionManager.Next();
    }

    protected override void StopInteraction()
    {
        base.StopInteraction();
        OverlayController.Instance.Close(); 
        if (GameManager.Instance.KeyObtained && !_IsDoorOpen) 
        {
            this.StartTimedAction(null, OpenDoor, 0.25f);
            Prompt.text = "Press X to Enter next section";
        }
    }


    protected override bool GetStopInteractionTrigger()
    {
        //Default stop interaction trigger is Escape key
        var stopInteraction = base.GetStopInteractionTrigger();

        if (_IsDoorOpen) //Interaction after door is open can only be stopped by a change in section or scene
            stopInteraction = _EnteredNextSection;

        return stopInteraction;
    }

    private void OpenDoor()
    {
        _IsDoorOpen = true;
        _DoorAnimator.SetTrigger("Open");
    }
}
