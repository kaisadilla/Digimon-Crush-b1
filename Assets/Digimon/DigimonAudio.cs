using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DigimonAudio : MonoBehaviour {
    [Header("Components")]
    [SerializeField] protected AudioSource source;
    [Header("Variables")]
    [SerializeField] protected AudioClip hit;
    [SerializeField] protected AudioClip dodge;
    [SerializeField] protected AudioClip gather;

    public void Stop() {
        source.Stop();
    }
    public void PlayHit() {
        source.clip = hit;
        source.Play();
    }
    public void PlayDodge() {
        source.clip = dodge;
        source.Play();
    }

    public void PlayGather() {
        source.clip = gather;
        source.Play();
    }

    protected void PlaySound(AudioClip sound) {
        source.clip = sound;
        source.Play();
    }

    public abstract void PlaySound(string sound);
}
