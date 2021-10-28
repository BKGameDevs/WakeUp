using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UnityUIController : MonoBehaviour
{
    [SerializeField]
    protected bool _VisibleOnEnabled;
    [SerializeField]
    protected UIElementResponse[] _UIElementResponses;

    private VisualElement _Root;
    private bool _IsVisible;

    private void OnEnable()
    {
        _Root = GetComponent<UIDocument>().rootVisualElement;

        var buttonQuery = _Root.Query<Button>();

        foreach (var button in buttonQuery.ToList())
        {
            button.clicked -= () => ElementActivated(button.name);
            button.clicked += () => ElementActivated(button.name);
        }

        var stringFieldQuery = _Root.Query<BaseField<string>>();

        foreach (var stringField in stringFieldQuery.ToList())
        {
            stringField.UnregisterValueChangedCallback(evt => StringFieldValueChanged(stringField.name, evt.newValue));
            stringField.RegisterValueChangedCallback(evt => StringFieldValueChanged(stringField.name, evt.newValue));
        }

        if (!_VisibleOnEnabled)
            SetVisibility(false);
    }

    private void StringFieldValueChanged(string name, string newValue)
    {
        var elementResponse = _UIElementResponses.FirstOrDefault(x => x.ElementName == name);

        elementResponse?.TextValueChanged?.Invoke(newValue);
    }

    private void ElementActivated(string name)
    {
        var elementResponse = _UIElementResponses.FirstOrDefault(x => x.ElementName == name);

        elementResponse?.Activated?.Invoke();
    }

    public T GetElementValue<T>(string name)
    {
        var fieldElement = _Root.Q<BaseField<T>>(name);

        if (fieldElement == null)
            throw new InvalidOperationException($"fieldElement with name {name} does not exist");

        return fieldElement.value;
    }

    public void SetElementValue<T>(string name, T value)
    {
        var fieldElement = _Root.Q<BaseField<T>>(name);

        if (fieldElement == null)
            throw new InvalidOperationException($"fieldElement with name {name} does not exist");

        fieldElement.value = value;
    }

    public void SetElementText(string name, string text)
    {
        var textElement = _Root.Q<TextElement>(name);

        if (textElement == null)
            throw new InvalidOperationException($"textElement with name {name} does not exist");

        textElement.text = text;
    }

    public void SetElementEnabled(string name, bool enabled)
    {
        var element = _Root.Q<VisualElement>(name);

        if (element == null)
            throw new InvalidOperationException($"element with name {name} does not exist");

        element.SetEnabled(enabled);
    }

    public void SetElementVisibility(string name, bool visibility)
    {
        var element = _Root.Q<VisualElement>(name);

        if (element == null)
            throw new InvalidOperationException($"element with name {name} does not exist");

        element.visible = visibility;
    }

    public void SetVisibility(bool visibility) => SetVisibility(_Root, _IsVisible = visibility);

    private void SetVisibility(VisualElement root, bool visibility)
    {
        root.visible = visibility;
        foreach (var child in root.Children())
            SetVisibility(child, visibility);
    }

}

[Serializable]
public class UIElementResponse
{
    public string ElementName;
    public UnityEvent Activated;
    public UnityEvent<string> TextValueChanged;
}