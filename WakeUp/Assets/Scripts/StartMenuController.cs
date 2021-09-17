using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class StartMenuController : MenuController<StartMenuController>
{
    public UnityEvent StartClicked;
    public UnityEvent SettingsClicked;
    public UnityEvent QuitClicked;

    public StartMenuController()
    {
        _DontDestroyOnLoad = false;
    }

    public override void RootInitialized(VisualElement root)
    {
        var startButton = root.Q<Button>("Start_Button");

        if (startButton != null)
            startButton.clicked += () => StartClicked?.Invoke();

        var settingsButton = root.Q<Button>("Settings_Button");

        if (settingsButton != null)
            settingsButton.clicked += () => SettingsClicked?.Invoke();

        var quitButton = root.Q<Button>("Quit_Button");

        if (quitButton != null)
            quitButton.clicked += () => QuitClicked?.Invoke();
    }
}
