using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    public AudioPlayerSettings AudioSettings;

    public void PlayOneShot(AudioPlayerSettings audioSettings)
    {
        AudioPlayer.StaticPlayOneShot(audioSettings, transform.position);
    }

    public void PlayOneShot()
    {
        PlayOneShot(AudioSettings);
    }
}
