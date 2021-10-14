using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static WakeUp.Constants.PlayerInput;

public class TutorialSkipController : InteractController
{
    public UnityEvent TutorialSkipped;

    protected override void StartInteraction()
    {
        base.StartInteraction();

        TutorialSkipped?.Invoke();
    }

    protected override bool GetStartInteractionTrigger()
    {
        return IsPressingShift && InArea;
    }
}