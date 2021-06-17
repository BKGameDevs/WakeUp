using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class JustInTimeManager : ScriptableObject
{
    public JustInTimeInstruction[] JustInTimeInstructions;

    private Dictionary<string, Tuple<string, bool>> _JustInTimeStore;

    private void OnEnable()
    {
        _JustInTimeStore = new Dictionary<string, Tuple<string, bool>>();

        foreach (var jit in JustInTimeInstructions)
            _JustInTimeStore.Add(jit.Key, Tuple.Create(jit.Instruction, false));
    }

    public bool TryOpenJustInTime(string key)
    {
        Tuple<string, bool> result;
        bool opened;
        if (opened = _JustInTimeStore.TryGetValue(key, out result) && !result.Item2)
            OverlayController.Instance.Open(result.Item1);

        return opened;
    }

}

[Serializable]
public class JustInTimeInstruction
{
    public string Key;
    public string Instruction;
}