using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class SettingsController : MenuController<SettingsController>
{
    public UnityEvent BackClicked;
    public UnityEvent ControlsClicked;

    public SettingsController()
    {
        _DontDestroyOnLoad = false;
    }


    public override void RootInitialized(VisualElement root)
    {
        var volumeSlider = _Root.Q<Slider>("Volume");

        if (volumeSlider != null)
            volumeSlider.RegisterValueChangedCallback(Slider_Changed); 
        
        var controlsButton = _Root.Q<Button>("Controls_Button");

        if (controlsButton != null)
            controlsButton.clicked += () => ControlsClicked?.Invoke();

        var backButton = _Root.Q<Button>("Back_Button");

        if (backButton != null)
            backButton.clicked += () => BackClicked?.Invoke();
    }

    private void Slider_Changed(ChangeEvent<float> evt)
    {
        //TODO: Check if this actually best practicef
        AudioListener.volume = evt.newValue / 100;
    }
}
