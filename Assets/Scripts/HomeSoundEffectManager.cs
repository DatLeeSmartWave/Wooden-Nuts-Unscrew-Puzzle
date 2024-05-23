using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSoundEffectManager : MonoBehaviour
{
    public static HomeSoundEffectManager Instance;
    public AudioSource backGroundMusicEffect;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        backGroundMusicEffect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // (SceneManager.GetActiveScene().name == "HomeScene") {
            if (PlayerPrefs.GetInt(StringsTextManager.MusicEffectKey) == 1)
                backGroundMusicEffect.volume = 1;
            else
                backGroundMusicEffect.volume = 0;
        //
    }
}
