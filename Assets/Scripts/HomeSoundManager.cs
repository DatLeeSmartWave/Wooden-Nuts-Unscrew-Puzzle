using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSoundManager : MonoBehaviour
{
    public static HomeSoundManager Instance;
    public AudioSource backGroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        backGroundMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // (SceneManager.GetActiveScene().name == "HomeScene") {
            if (PlayerPrefs.GetInt(StringsManager.MusicKey) == 1)
                backGroundMusic.volume = 1;
            else
                backGroundMusic.volume = 0;
        //
    }
}
