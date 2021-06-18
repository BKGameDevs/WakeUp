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

        var button = _Root.Q<Button>("Resume");

        if (button != null)
            button.clicked += Resume_clicked;

        SetVisibility(_Root, false);
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

    public void Open()
    {
        if (_IsOpen)
            return;

        _IsOpen = true;
        SetVisibility(_Root, _IsOpen);
        //_Transition.SetBool("Open", _IsOpen);
    }

    public void Close()
    {
        if (!_IsOpen)
            return;

        _IsOpen = false;
        SetVisibility(_Root, _IsOpen);
        //_Transition.SetBool("Open", _IsOpen);
    }
}
