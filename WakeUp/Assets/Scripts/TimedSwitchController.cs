using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSwitchController : SwitchController
{
    // Start is called before the first frame update
    protected override void Initialize()
    {
        base.Initialize();
        SetPromptText("Press X to Interact");
        //_SpriteRenderer = GetComponent<SpriteRenderer>();
        //_SwitchSound = GetComponent<AudioSource>();
    }

    
}
