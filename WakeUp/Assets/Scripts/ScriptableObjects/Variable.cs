using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variable<T> : PropertyChangedScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private T _InitialValue;
    public T InitialValue
    {
        get { return _InitialValue; }
        set
        {
            if (!_InitialValue.Equals(value))
            {
                _InitialValue = value;
                OnPropertyChanged();
            }
        }
    }

    private T _RuntimeValue;
    public T RuntimeValue
    {
        get { return _RuntimeValue; }
        set
        {
            if (!_RuntimeValue.Equals(value))
            {
                _RuntimeValue = value;
                OnPropertyChanged();
            }
        }
    }

    public void OnBeforeSerialize() { }

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }
}
