using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : InteractController, IHasOverlayText
{
    private string _OverlayText;
    public Sprite ActiveLever;
    private SpriteRenderer SpriteRenderer;
    protected override void Initialize()
    {
        base.Initialize();
        Prompt.text = "Press X to Interact";
        SpriteRenderer = GetComponent<SpriteRenderer>();
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
        SpriteRenderer.sprite = ActiveLever;

    }
    public void SetOverlayText(string text)
    {
        _OverlayText = text;
    }
}
