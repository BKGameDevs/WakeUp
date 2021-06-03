using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Util
{
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
