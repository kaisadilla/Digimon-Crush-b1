using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GabumonAudio : DigimonAudio {
    [SerializeField] private AudioClip attack_guitarSmash;

    public override void PlaySound(string sound) {
        if (sound == "attack_guitar_smash") PlaySound(attack_guitarSmash);
    }
}