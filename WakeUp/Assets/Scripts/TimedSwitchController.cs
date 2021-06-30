using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimedSwitchController : SwitchController
{
    public GameEvent OnTimerEnd;
    public float Duration = 10f;
    //public float InitialRepeatDelay = 0.25f;
    //public float RepeatDelayDecreaseAmount = 0.5f;
    public AudioClip TimeSound;
    public AudioClip TickSound;

    public TickDelay[] TickDelays;
    private LinkedList<TickDelay> _TickDelays;

    private Dictionary<TickDelay, float> _AggregatedCalculatedEndTimes;
    
    private float _TimeElapsed;
    private LinkedListNode<TickDelay> _CurrentTickDelay;

    // Start is called before the first frame update
    protected override void Initialize()
    {
        base.Initialize();
        //SetPromptText("Press X to Interact");
        //_SpriteRenderer = GetComponent<SpriteRenderer>();
        //_SwitchSound = GetComponent<AudioSource>();
        _AggregatedCalculatedEndTimes = new Dictionary<TickDelay, float>();
        _TickDelays = new LinkedList<TickDelay>(TickDelays);

        var sum = TickDelays.Sum(x => x.Size);
        var aggregateSum = 0f;
        foreach (var tickDelay in TickDelays)
        {
            var ratio = tickDelay.Size / (float)sum;

            var time = Duration * ratio;
            _AggregatedCalculatedEndTimes[tickDelay] = aggregateSum += time;;
        }

        OnSwitchTrigger.Raised += OnSwitchTrigger_Raised;
    }

    private void OnSwitchTrigger_Raised(object sender, object e)
    {
        _TimeElapsed = 0;
        _CurrentTickDelay = _TickDelays.First;
        //var audioSource = AudioPlayer.PlayBackgroundStandalone(TimeSound, "Ticking");

        RecursiveTick();

        this.StartTimedAction(null,
            () =>
            {
                Switch();
                OnTimerEnd?.Raise();
                //AudioPlayer.StopBackground(audioSource);
            },
            Duration);
    }

    private void RecursiveTick()
    {
        if (_CurrentTickDelay == null || !_SwitchedOn)
            return;

        this.StartLoopingAction(
            //Loop action - Play Tick Sound
            () => AudioPlayer.StaticPlayOneShot(TickSound),
            //Loop condition - Time elapsed is less than calculated end time for this delay
            () => _TimeElapsed < _AggregatedCalculatedEndTimes[_CurrentTickDelay.Value],
            //Actual delay
            _CurrentTickDelay.Value.Delay,
            //Action that happens when loop stops - Go to next tick and restart
            () =>
            {
                _CurrentTickDelay = _CurrentTickDelay.Next;

                RecursiveTick();
            }
            );
    }

    private void FixedUpdate()
    {
        if (_SwitchedOn)
            _TimeElapsed += Time.fixedDeltaTime;
    }
}

[Serializable]
public class TickDelay
{
    //Integer ratio that this delay happens within duration
    public int Size;
    //Delay between tick one shots
    public float Delay;
}