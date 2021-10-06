using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Util
{
    public static bool Paused { get; private set; }

    public static void Pause()
    {
        Time.timeScale = 0f;
        Paused = true;
    }
    public static void Resume()
    {
        Time.timeScale = 1f;
        Paused = false;
    }

    public static void Quit() => Application.Quit();

    public static Coroutine StartTimedAction(this MonoBehaviour monoBehaviour, UnityAction before, UnityAction after, float time)
    {
        return monoBehaviour.StartCoroutine(TimedAction(before, after, time));
    }

    public static IEnumerator TimedAction(UnityAction before, UnityAction after, float time)
    {
        before?.Invoke();
        yield return new WaitForSeconds(time);
        after?.Invoke();
    }

    public static Coroutine StartConditionalAction(this MonoBehaviour monoBehaviour, UnityAction before, UnityAction after, Func<bool> waitUntilCondition)
    {
        return monoBehaviour.StartCoroutine(ConditionalAction(before, after, waitUntilCondition));
    }

    public static IEnumerator ConditionalAction(UnityAction before, UnityAction after, Func<bool> waitUntilCondition)
    {
        before?.Invoke();
        yield return new WaitUntil(waitUntilCondition);
        after?.Invoke();
    }

    public static Coroutine StartLoopingAction(this MonoBehaviour monoBehaviour, UnityAction loopAction, Func<bool> repeatCondition, float repeatDelay, UnityAction endLoopAction = null)
    {
        return monoBehaviour.StartCoroutine(LoopingAction(loopAction, repeatCondition, repeatDelay, endLoopAction));
    }
    public static IEnumerator LoopingAction(UnityAction loopAction, Func<bool> repeatCondition, float repeatDelay, UnityAction endLoopAction = null)
    {
        while (repeatCondition?.Invoke() ?? false)
        {
            loopAction?.Invoke();
            yield return new WaitForSeconds(repeatDelay);
        }

        endLoopAction?.Invoke();
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
