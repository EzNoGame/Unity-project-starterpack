using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAudioSystem : Singleton<AbstractAudioSystem>
{
    public abstract void PlaySFX(AudioClip clip, float volume = 1f);
}