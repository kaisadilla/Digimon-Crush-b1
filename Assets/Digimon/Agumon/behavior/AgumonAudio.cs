using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgumonAudio : DigimonAudio {
    [SerializeField] private AudioClip attack_claws;
    [SerializeField] private AudioClip attack_flyingKick;
    [SerializeField] private AudioClip attack_pepperBreath;
    [SerializeField] private AudioClip attack_babyBurner;

    public override void PlaySound(string sound) {
        if (sound == "attack_claws") PlaySound(attack_claws);
        else if (sound == "attack_flyingKick") PlaySound(attack_flyingKick);
        else if (sound == "attack_pepperBreath") PlaySound(attack_pepperBreath);
        else if (sound == "attack_babyBurner") PlaySound(attack_babyBurner);
    }
}
