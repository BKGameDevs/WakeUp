using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : InteractController, IHasOverlayText
{
    public GameEvent OnSwitchTrigger;
    public Sprite ActiveLever;

    private SpriteRenderer _SpriteRenderer;
    private string _OverlayText;
    private bool _SwitchedOn;

    protected override void Initialize()
    {
        base.Initialize();
        SetPromptText("Press X to Interact");
        _SpriteRenderer = GetComponent<SpriteRenderer>();
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
        _SpriteRenderer.sprite = ActiveLever;
        DisablePrompt = _SwitchedOn = true;
        OnSwitchTrigger?.Raise();
    }

    protected override bool GetStartInteractionTrigger()
    {
        return !_SwitchedOn ? base.GetStartInteractionTrigger() : false;
    }

    public void SetOverlayText(string text)
    {
        _OverlayText = text;
    }
}
