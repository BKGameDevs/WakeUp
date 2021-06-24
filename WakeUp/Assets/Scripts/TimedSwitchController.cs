using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSwitchController : SwitchController
{
    public GameEvent OnTimerEnd;
    public float Duration = 10f;
    public AudioClip TimeSound;

    // Start is called before the first frame update
    protected override void Initialize()
    {
        base.Initialize();
        //SetPromptText("Press X to Interact");
        //_SpriteRenderer = GetComponent<SpriteRenderer>();
        //_SwitchSound = GetComponent<AudioSource>();

        OnSwitchTrigger.Raised += OnSwitchTrigger_Raised;
    }

    private void OnSwitchTrigger_Raised(object sender, object e)
    {
        var audioSource = AudioPlayer.PlayBackgroundStandalone(TimeSound, "Ticking");
        this.StartTimedAction(null, 
            () =>
            {
                Switch();
                OnTimerEnd?.Raise();
                AudioPlayer.StopBackground(audioSource);
            },
            Duration);
    }
}
