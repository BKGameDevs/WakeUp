using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorController : InteractController, IHasOverlayText
{
    public UnityEvent DoorOpened;

    private Animator _DoorAnimator;
    private bool _IsDoorOpen;

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
        {
            DoorOpened?.Invoke(); 
            //SectionManager.PlayerSpawned += SectionManager_PlayerSpawned;
            //SectionManager.Next();
        }
    }

    //private void SectionManager_PlayerSpawned(object sender, System.EventArgs e)
    //{
    //    StopInteraction();

    //    SectionManager.PlayerSpawned -= SectionManager_PlayerSpawned;
    //}

    public override void StopInteraction()
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

        if (_IsDoorOpen)
            //Interaction after door is open can only be stopped by a change in section or scene
            stopInteraction = false;

        return stopInteraction;
    }

    private void OpenDoor()
    {
        _IsDoorOpen = true;
        _DoorAnimator.SetTrigger("Open");
    }
}
