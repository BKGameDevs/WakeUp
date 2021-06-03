using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioPlayer : ScriptableObject
{
    private AudioSource _BackgroundMusic;

    public void PlayOneShot(AudioClip audioClip)
    {
        var gameObject = new GameObject("Sound");
        var behavior = gameObject.AddComponent<CoroutineBehavior>();
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 1f;

        behavior.StartCoroutine(Util.TimedAction(
            () => audioSource.PlayOneShot(audioClip),
            () => Destroy(gameObject),
            audioClip.length
            ));
    }

    public void PlayBackground(AudioClip audioClip)
    {
        if (_BackgroundMusic == null)
        {
            var gameObject = new GameObject("Background Music");
            _BackgroundMusic = gameObject.AddComponent<AudioSource>();
            gameObject.AddComponent<CoroutineBehavior>();
            _BackgroundMusic.loop = true;
        }

        _BackgroundMusic.clip = audioClip;
        _BackgroundMusic.volume = 0f;
        _BackgroundMusic.Play();
        var behavior = _BackgroundMusic.gameObject.GetComponent<CoroutineBehavior>();
        behavior.StartCoroutine(Util.StartFade(_BackgroundMusic, 1f, 0.5f));
    }

    public void StopBackground()
    {
        if (_BackgroundMusic != null)
        {
            _BackgroundMusic.Stop();
            Destroy(_BackgroundMusic.gameObject);
            _BackgroundMusic = null;
        }
    }

    public class CoroutineBehavior : MonoBehaviour
    {

    }

    private void OnDestroy()
    {
        _BackgroundMusic = null;
    }
}
