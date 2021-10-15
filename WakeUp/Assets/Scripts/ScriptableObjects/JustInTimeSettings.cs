using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class JustInTimeSettings : ScriptableObject
{
    [SerializeField]
    internal string _Key;

    [SerializeField]
    internal float _Delay;
}
