using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MainMenuController : MenuController<MainMenuController>
{
    public UnityEvent ResumeClicked;
    public UnityEvent SettingsClicked;
    public UnityEvent QuitClicked;

    public MainMenuController()
    {
        _DontDestroyOnLoad = false;
    }

    public override void RootInitialized(VisualElement root)
    {
        var resumeButton = root.Q<Button>("Resume_Button");

        if (resumeButton != null)
            resumeButton.clicked += () => ResumeClicked?.Invoke();

        var settingsButton = root.Q<Button>("Settings_Button");

        if (settingsButton != null)
            settingsButton.clicked += () => SettingsClicked?.Invoke();

        var quitButton = root.Q<Button>("Quit_Button");

        if (quitButton != null)
            quitButton.clicked += () => QuitClicked?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Open();
    }


    public void Open(bool pause = true)
    {
        if (pause)
            Util.Pause();

        base.Open();
    }

    public void Close(bool resume = true)
    {
        if (resume)
            Util.Resume();

        base.Close();
    }
}
