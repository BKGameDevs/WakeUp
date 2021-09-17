using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MenuController<MainMenuController>
{
    public override void RootInitialized(VisualElement root)
    {
        var resumeButton = root.Q<Button>("Resume");

        if (resumeButton != null)
            resumeButton.clicked += Resume_clicked;

        var settingsButton = root.Q<Button>("Settings");

        if (settingsButton != null)
            settingsButton.clicked += Settings_Clicked;

        var quitButton = root.Q<Button>("Quit");

        if (quitButton != null)
            quitButton.clicked += Quit_clicked;
    }

    private void Settings_Clicked()
    {
        Close(false);
        SettingsController.Instance.Open();
    }

    private void Quit_clicked()
    {
#if (!UNITY_EDITOR)
        Util.Quit();
#else
        Close();
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Open();
    }

    private void Resume_clicked()
    {
        Close();
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
