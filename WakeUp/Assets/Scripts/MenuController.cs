using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//TODO: Use this class for all menu controllers
public abstract class MenuController<T> : Singleton<T> where T : Component
{

    protected VisualElement _Root;
    protected bool _IsOpen;

    public virtual void Open()
    {
        if (_IsOpen)
            return;

        _IsOpen = true;
        SetVisibility(_Root, _IsOpen);
    }

    public virtual void Close()
    {
        if (!_IsOpen)
            return;

        _IsOpen = false;
        SetVisibility(_Root, _IsOpen);
    }

    private void SetVisibility(VisualElement root, bool visibility)
    {
        root.visible = visibility;
        foreach (var child in root.Children())
            SetVisibility(child, visibility);
    }
}
