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
        //var button = _Root.Q<Button>("Resume");
        //button.clicked += Button_clicked;
        _Root.visible = false;
    }

    private void Button_clicked()
    {
        Close();
    }

    public void Open(string mainText)
    {
        if (_IsOpen)
            return;

        _IsOpen = true;
        //_Transition.SetBool("Open", _IsOpen);
    }

    public void Close()
    {
        if (!_IsOpen)
            return;

        _IsOpen = false;
        _Root.visible = false;
        //_Transition.SetBool("Open", _IsOpen);
    }
}
