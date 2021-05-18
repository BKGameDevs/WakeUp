using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwitchController : InteractController, IHasOverlayText
{
    public GameEvent OnSwitchTrigger;
    public Sprite ActiveLever;

    private SpriteRenderer _SpriteRenderer;
    private string _OverlayText;
    private bool _SwitchedOn;

    private AudioSource _SwitchSound;

    protected override void Initialize()
    {
        base.Initialize();
        SetPromptText("Press X to Interact");
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _SwitchSound = GetComponent<AudioSource>();
    }

    protected override void StartInteraction()
    {
        base.StartInteraction();
        
        SetOverlayText("The platforms are moving!");
        OverlayController.Instance.Open(_OverlayText);

    }    
    protected override void StopInteraction()
    {
        base.StopInteraction();
        OverlayController.Instance.Close(); 
        _SwitchSound.Play();
        _SpriteRenderer.sprite = ActiveLever;
        DisablePrompt = _SwitchedOn = true;
        OverlayController.Instance.Closed += OverlayClosed;
    }

    private void OverlayClosed(object sender, EventArgs args){
        OnSwitchTrigger?.Raise();
        OverlayController.Instance.Closed -= OverlayClosed;
    }

    protected override bool GetStartInteractionTrigger()
    {
        return !_SwitchedOn ? base.GetStartInteractionTrigger() : false;
    }
    protected override bool GetStopInteractionTrigger()
    {
        return !_SwitchedOn ? base.GetStopInteractionTrigger() : false;
    }

    public void SetOverlayText(string text)
    {
        _OverlayText = text;
    }
}
