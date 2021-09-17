using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SettingsController : MenuController<SettingsController>
{
    public AudioListener AudioListener;

    public override void RootInitialized(VisualElement root)
    {
        var volumeSlider = _Root.Q<Slider>("Volume");

        if (volumeSlider != null)
            volumeSlider.RegisterValueChangedCallback(Slider_Changed);

        var backButton = _Root.Q<Button>("Back");

        if (backButton != null)
            backButton.clicked += Back_Clicked;
    }

    private void Back_Clicked()
    {
        Close();
        MainMenuController.Instance.Open(false);
    }

    private void Slider_Changed(ChangeEvent<float> evt)
    {
        AudioListener.volume = evt.newValue / 100;
    }
}
