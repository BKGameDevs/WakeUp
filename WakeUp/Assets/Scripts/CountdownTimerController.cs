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
        this.StartLoopingAction(
            () =>
            {

                if (_TimeLeft <= 10)
                    CountdownTimerText.color = Color.red;
                var timeSpan = TimeSpan.FromSeconds(_TimeLeft);
                var timeString = timeSpan.ToString(@"m\:ss");
                CountdownTimerText.text = timeString;
                _TimeLeft--;
            },
            () => _TimeLeft >= 0,
            1,
            ClearTimer);
    }

    private void ClearTimer() => CountdownTimerText.text = string.Empty;
}
