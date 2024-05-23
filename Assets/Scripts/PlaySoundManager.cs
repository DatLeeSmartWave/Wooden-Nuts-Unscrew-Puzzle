using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundManager : MonoBehaviour {
    public static PlaySoundManager instance;
    public AudioSource audioSource;
    public AudioClip screwSound;
    public AudioClip popSound;
    public AudioClip loseSound;
    public AudioClip winSound;
    // Start is called before the first frame update
    void Start() {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (PlayerPrefs.GetInt(StringsManager.SoundKey) == 1) {
            audioSource.volume = 1;
        }
        else audioSource.volume = 0;
    }
}
