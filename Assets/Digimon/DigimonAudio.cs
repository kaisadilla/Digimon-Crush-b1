using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DigimonAudio : MonoBehaviour {
    [Header("Components")]
    [SerializeField] protected AudioSource source;
    [Header("Variables")]
    [SerializeField] protected AudioClip hit;

    public void PlayHit() {
        source.clip = hit;
        source.Play();
    }

    protected void PlaySound(AudioClip sound) {
        source.clip = sound;
        source.Play();
    }

    public abstract void PlaySound(string sound);
}
