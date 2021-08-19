using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : Singleton<MainMenuController>
{
    private VisualElement _Root;
    private bool _IsOpen;

    private void OnEnable()
    {
        _Root = GetComponent<UIDocument>()?.rootVisualElement;

        var resumeButton = _Root.Q<Button>("Resume");

        if (resumeButton != null)
            resumeButton.clicked += Resume_clicked;

        var settingsButton = _Root.Q<Button>("Settings");

        if (settingsButton != null)
            settingsButton.clicked += Settings_Clicked;

        var quitButton = _Root.Q<Button>("Quit");

        if (quitButton != null)
            quitButton.clicked += Quit_clicked;


        SetVisibility(_Root, false);
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

    private void SetVisibility(VisualElement root, bool visibility)
    {
        root.visible = visibility;
        foreach (var child in root.Children())
            SetVisibility(child, visibility);
    }

    private void Resume_clicked()
    {
        Close();
    }

    public void Open(bool pause = true)
    {
        if (_IsOpen)
            return;

        if (pause)
            Util.Pause();
        _IsOpen = true;
        SetVisibility(_Root, _IsOpen);
    }

    public void Close(bool resume = true)
    {
        if (!_IsOpen)
            return;

        _IsOpen = false;
        SetVisibility(_Root, _IsOpen);
        
        if (resume)
            Util.Resume();
    }
}
