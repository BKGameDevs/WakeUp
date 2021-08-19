using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SettingsController : Singleton<SettingsController>
{
    public AudioListener AudioListener;

    private VisualElement _Root;
    private bool _IsOpen;

    private void OnEnable()
    {
        _Root = GetComponent<UIDocument>()?.rootVisualElement;
        var volumeSlider = _Root.Q<Slider>("Volume");

        if (volumeSlider != null)
            volumeSlider.RegisterValueChangedCallback(Slider_Changed);

        var backButton = _Root.Q<Button>("Back");

        if (backButton != null)
            backButton.clicked += Back_Clicked;

        SetVisibility(_Root, false);
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

    private void SetVisibility(VisualElement root, bool visibility)
    {
        root.visible = visibility;
        foreach (var child in root.Children())
            SetVisibility(child, visibility);
    }
    public void Open()
    {
        if (_IsOpen)
            return;

        _IsOpen = true;
        SetVisibility(_Root, _IsOpen);
    }

    public void Close()
    {
        if (!_IsOpen)
            return;

        _IsOpen = false;
        SetVisibility(_Root, _IsOpen);
    }
}
