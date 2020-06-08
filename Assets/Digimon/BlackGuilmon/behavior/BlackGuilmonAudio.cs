using UnityEngine;

public class BlackGuilmonAudio : DigimonAudio {
    [SerializeField] private AudioClip boost;
    [SerializeField] private AudioClip swallow;

    public override void PlaySound(string sound) {
        if (sound == "boost") PlaySound(boost);
        if (sound == "swallow") PlaySound(swallow);
    }
}