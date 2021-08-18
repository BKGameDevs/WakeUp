using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioPlayer : ScriptableObject
{
    private AudioSource _BackgroundMusic;
    public static void StaticPlayOneShot(AudioPlayerSettings audioSettings, Vector3? position = null)
    {
        StaticPlayOneShot(audioSettings.AudioClip, audioSettings.Volume, audioSettings.SpatialBlend,
            audioSettings.MaxDistance, audioSettings.MinDistance, audioSettings.AudioRolloffMode, position);
    }

    public void PlayOneShot(AudioPlayerSettings audioSettings)
    {
        StaticPlayOneShot(audioSettings);
    }

    public static void StaticPlayOneShot(AudioClip audioClip, 
        float volume = 1f, float spatialBlend = 0f, 
        float maxDistance = 500f, float minDistance = 1f,
        AudioRolloffMode audioRolloffMode = AudioRolloffMode.Logarithmic, Vector3? position = null)
    {
        var gameObject = new GameObject("Sound");
        var behavior = gameObject.AddComponent<CoroutineBehavior>();
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = volume;

        audioSource.spatialBlend = spatialBlend;
        audioSource.maxDistance = maxDistance;
        audioSource.minDistance = minDistance;
        audioSource.rolloffMode = audioRolloffMode;

        gameObject.transform.position = position.HasValue ? position.Value : Vector3.zero;

        behavior.StartCoroutine(Util.TimedAction(
            () => audioSource.PlayOneShot(audioClip),
            () => Destroy(gameObject),
            audioClip.length
            ));
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        StaticPlayOneShot(audioClip);
    }

    public static AudioSource PlayBackgroundStandalone(AudioClip audioClip, string name)
    {
        var gameObject = new GameObject(name);
        var audioSource = gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<CoroutineBehavior>();
        audioSource.loop = true;

        audioSource.clip = audioClip;
        audioSource.volume = 0f;
        audioSource.Play();
        var behavior = audioSource.gameObject.GetComponent<CoroutineBehavior>();
        behavior.StartCoroutine(Util.StartFade(audioSource, 1f, 0.5f));

        return audioSource;
    }

    public void PlayBackground(AudioClip audioClip)
    {
        _BackgroundMusic = PlayBackgroundStandalone(audioClip, "Background Music");
        //if (_BackgroundMusic == null)
        //{
        //    var gameObject = new GameObject("Background Music");
        //    _BackgroundMusic = gameObject.AddComponent<AudioSource>();
        //    gameObject.AddComponent<CoroutineBehavior>();
        //    _BackgroundMusic.loop = true;
        //}

        //_BackgroundMusic.clip = audioClip;
        //_BackgroundMusic.volume = 0f;
        //_BackgroundMusic.Play();
        //var behavior = _BackgroundMusic.gameObject.GetComponent<CoroutineBehavior>();
        //behavior.StartCoroutine(Util.StartFade(_BackgroundMusic, 1f, 0.5f));
    }


    public static void StopBackground(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            Destroy(audioSource.gameObject);
        }
    }
    public void StopBackground()
    {
        StopBackground(_BackgroundMusic);
        _BackgroundMusic = null;
    }

    public class CoroutineBehavior : MonoBehaviour
    {

    }

    private void OnDestroy()
    {
        _BackgroundMusic = null;
    }
}
