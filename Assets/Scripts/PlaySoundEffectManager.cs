using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffectManager : MonoBehaviour {
    public static PlaySoundEffectManager instance;
    public AudioSource audioEffectSource;
    public AudioClip screwEffectSound;
    public AudioClip popEffectSound;
    public AudioClip loseEffectSound;
    public AudioClip winEffectSound;
    public AudioClip claimSound;
    public AudioClip hammerSound;
    public AudioClip unscrewSound;
    // Start is called before the first frame update
    void Start() {
        instance = this;
        audioEffectSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (PlayerPrefs.GetInt(StringsTextManager.SoundEffectKey) == 1) {
            audioEffectSource.volume = 1;
        }
        else audioEffectSource.volume = 0;
    }
}
