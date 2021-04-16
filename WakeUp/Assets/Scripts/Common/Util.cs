using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Util
{
    public static void StartTimedAction(this MonoBehaviour monoBehaviour, UnityAction before, UnityAction after, float time)
    {
        monoBehaviour.StartCoroutine(TimedAction(before, after, time));
    }

    public static IEnumerator TimedAction(UnityAction before, UnityAction after, float time)
    {
        before?.Invoke();
        yield return new WaitForSeconds(time);
        after?.Invoke();
    }
}
