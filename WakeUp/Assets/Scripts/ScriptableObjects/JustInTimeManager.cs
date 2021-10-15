using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioPlayer;

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

        if (opened)
            _JustInTimeStore[key] = Tuple.Create(result.Item1, true);

        return opened;
    }

    public void OpenJustInTime(string key)
    {
        var result = _JustInTimeStore[key];

        if (!result.Item2)
            OverlayController.Instance.Open(result.Item1);

        _JustInTimeStore[key] = Tuple.Create(result.Item1, true);
    }

    public void OpenJustInTime(JustInTimeSettings settings)
    {
        var gameObject = new GameObject("JustInTimeDelay");
        var behavior = gameObject.AddComponent<CoroutineBehavior>();

        behavior.StartTimedAction(null, () => OpenJustInTime(settings._Key), settings._Delay);
    }
}

[Serializable]
public class JustInTimeInstruction
{
    public string Key;
    public string Instruction;
}