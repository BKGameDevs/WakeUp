using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwitchController : InteractController
{
    public GameEvent OnSwitchTrigger;
    public Sprite DefaultLever;
    public Sprite ActiveLever;
    public bool SingleUse;
    public JustInTimeManager JustInTimeManager;

    public string SwitchType;

    private SpriteRenderer _SpriteRenderer;
    protected bool _SwitchedOn;

    private AudioSource _SwitchSound;

    protected override void Initialize()
    {
        base.Initialize();
        SetPromptText("Press X to Interact");
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _SwitchSound = GetComponent<AudioSource>();
    }

    protected void Switch()
    {
        _SwitchedOn = !_SwitchedOn;

        _SwitchSound.Play();
        _SpriteRenderer.sprite = _SwitchedOn ? ActiveLever : DefaultLever;

        DisablePrompt = SingleUse && _SwitchedOn;
    }

    protected override void StartInteraction()
    {
        base.StartInteraction();

        Switch();
        if (JustInTimeManager.TryOpenJustInTime(SwitchType))
            OverlayController.Instance.Closed += OverlayClosed;
        else
        {
            OnSwitchTrigger?.Raise();

            StopInteraction();
        }
        //SetOverlayText("The platforms are moving!");
        //OverlayController.Instance.Open(OverlayText);
    }    

    public override void StopInteraction()
    {
        base.StopInteraction();
        OverlayController.Instance.Close();
    }

    private void OverlayClosed(object sender, EventArgs args)
    {
        //TODO: If there is a action triggered from this event that disables player,
        //there will be a 1 second gap (end of close animation)
        //where player can still move
        OnSwitchTrigger?.Raise();
        OverlayController.Instance.Closed -= OverlayClosed;
    }

    protected override bool GetStartInteractionTrigger()
    {
        if (!SingleUse)
            return base.GetStartInteractionTrigger();

        return !_SwitchedOn ? base.GetStartInteractionTrigger() : false;
    }

    protected override bool GetStopInteractionTrigger()
    {
        if (!SingleUse)
            return base.GetStopInteractionTrigger();

        return IsInteracting ? base.GetStopInteractionTrigger() : false;
    }
}
