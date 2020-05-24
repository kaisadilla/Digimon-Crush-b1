using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDatabase : MonoBehaviour {
    //Singleton pattern.
    public static AudioDatabase Instance { get; private set; }
    void Awake() {
        Instance = this;
    }

    public AudioClip agumonClaws;
}
