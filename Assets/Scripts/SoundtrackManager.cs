using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackManager : MonoBehaviour {

    [SerializeField] private AudioSource player;
    [SerializeField] private AudioClip[] soundtracks = new AudioClip[1];

    private void Start() {
        if(soundtracks.Length > 0) {
            StartCoroutine(PlayRandomSoundtrack());
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F5)) {
            StopAllCoroutines();
            StartCoroutine(PlayRandomSoundtrack());
        }
    }

    private IEnumerator PlayRandomSoundtrack() {
        player.Stop();
        yield return new WaitForSeconds(1f);
        while (true) {
            player.clip = soundtracks[Random.Range(0, soundtracks.Length)];
            player.Play();
            yield return new WaitForSeconds(player.clip.length + 10f);
        }
    }
}
