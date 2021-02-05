using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PropertyChangedListener : MonoBehaviour
{
    public List<PropertyChangedScriptableObject> PropertyChangedSOs;

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        foreach (var propertyChangedSO in PropertyChangedSOs)
            propertyChangedSO.PropertyChanged += (s, e) => OnPropertyChanged(propertyChangedSO);
    }

    protected virtual void OnPropertyChanged(PropertyChangedScriptableObject propertyChangeSO)
    {

    }
}
