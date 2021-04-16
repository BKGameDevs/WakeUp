using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : InteractController, IHasOverlayText
{
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

    protected override void OnInteractChanged()
    {
        base.OnInteractChanged();
        if (IsInteracting)
        {
            if (!_IsDoorOpen)
            {
                OverlayController.Instance.Open(_OverlayText);
            }
            else
            {
                //Go to next section
            }
        }
        else if (!IsInteracting) //Closed overlay
        {
            OverlayController.Instance.Close();
            if (GameManager.Instance.KeyObtained)
            {
                this.StartTimedAction(null, OpenDoor, 0.25f);
                Prompt.text = "Press X to Enter next section";
            }
        }
    }

    protected override bool StopInteraction()
    {
        //TODO: Figure out how to Start and Stop interaction not using X and ESC in certain cases

        return base.StopInteraction();
    }

    private void OpenDoor()
    {
        _IsDoorOpen = true;
        _DoorAnimator.SetTrigger("Open");
    }
}
