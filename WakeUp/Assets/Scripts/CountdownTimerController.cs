using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CountdownTimerController : MonoBehaviour
{
    public TextMeshProUGUI CountdownTimerText;
    public int CountdownTime = 65;

    private int _TimeLeft = 0;
    private Coroutine _TimeCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        _TimeLeft = CountdownTime;
        ClearTimer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartCountDown()
    {
        _TimeLeft = CountdownTime;
        _TimeCoroutine = this.StartLoopingAction(
            //Action to perform
            () =>
            {

                CountdownTimerText.color = _TimeLeft <= 10 ? Color.red : Color.white;
                var timeSpan = TimeSpan.FromSeconds(_TimeLeft);
                var timeString = timeSpan.ToString(@"m\:ss");
                CountdownTimerText.text = timeString;
                _TimeLeft--;
            },
            //Looping condition
            () => _TimeLeft >= 0,
            //Delay between iterations
            1,
            //Action after looping stops
            ClearTimer);
    }

    public void ResetTime()
    {
        if (_TimeCoroutine != null)
            StopCoroutine(_TimeCoroutine);

        _TimeLeft = 0;
        ClearTimer();
    }

    private void ClearTimer() => CountdownTimerText.text = string.Empty;
}
