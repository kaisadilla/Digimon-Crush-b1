using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpmonAudio : DigimonAudio {
    [SerializeField] private AudioClip attack_undefined;

    public override void PlaySound(string sound) {
        if (sound == "attack_claws") return;
    }
}
