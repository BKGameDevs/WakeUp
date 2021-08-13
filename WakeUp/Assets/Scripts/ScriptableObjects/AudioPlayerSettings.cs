using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu]
public class AudioPlayerSettings : ScriptableObject
{
    public AudioClip AudioClip;
    public float Volume;
    public float SpatialBlend;
    public float MaxDistance;
    public float MinDistance;
    public AudioRolloffMode AudioRolloffMode;
}
