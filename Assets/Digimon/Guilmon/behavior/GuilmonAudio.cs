using UnityEngine;

public class GuilmonAudio : DigimonAudio {
    [SerializeField] private AudioClip attack_undefined;

    public override void PlaySound(string sound) {
        if (sound == "attack_claws") return;
    }
}