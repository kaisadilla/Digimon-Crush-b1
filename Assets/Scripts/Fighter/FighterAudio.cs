using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAudio : MonoBehaviour {
    [Header("Components")]
    [SerializeField] private AudioSource source;
    [Header("Variables")]
    [SerializeField] private AudioClip hit;

    public void PlayHit() {
        source.clip = hit;
        source.Play();
    }

}
