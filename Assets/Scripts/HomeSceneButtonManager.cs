using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneButtonManager : MonoBehaviour {
    public static HomeSceneButtonManager instance;
    public RectTransform selectLevelPanel;
    Vector2 levelPanelFirstPos;
    [SerializeField] TextMeshProUGUI profileText;
    [SerializeField] TextMeshProUGUI setedProfileText;
    public GameObject purchaseBoard, profileBoard, shopBoard, soundOn, soundOff, soundIconOn, soundIconOff;
    public GameObject musicOn, musicOff, musicIconOn, musicIconOff;
    public GameObject vibrateOn, vibrateOff, vibrateIconOn, vibrateIconOff;
    public GameObject[] onGoingIcons;
    public GameObject[] lockIcons;
    public GameObject[] finishedIcons;
    public int goldenTicketAmount;
    public TextMeshProUGUI goldenTicketAmountText;

    // Key names for PlayerPrefs
    //private const string SoundKey = "SoundState";
    //private const string MusicKey = "MusicState";
    //private const string VibrateKey = "VibrateState";

    private void Awake() {
        instance = this;
    }

    private void Start() {
        levelPanelFirstPos = selectLevelPanel.position;
        //if(shopBoard.activeSelf) {
        //    goldenTicketAmount = PlayerPrefs.GetInt(StringsManager.GoldenTicketAmount, 0);
        //}
        LoadState();
    }

    private void Update() {
        int levelPassed = PlayerPrefs.GetInt(StringsManager.LevelPassed, 0);
        if (musicIconOn.activeSelf)
            PlayerPrefs.SetInt(StringsManager.MusicKey, 1);
        if (soundIconOn.activeSelf)
            PlayerPrefs.SetInt(StringsManager.SoundKey, 1);
        if (vibrateIconOn.activeSelf)
            PlayerPrefs.SetInt(StringsManager.VibrateKey, 1);
        UpdateLevelIcons(levelPassed);
        if (profileBoard.activeSelf) {
            setedProfileText.text = PlayerPrefs.GetString(StringsManager.ProfileText);
        }
        if (shopBoard.activeSelf) {
            goldenTicketAmount = PlayerPrefs.GetInt(StringsManager.GoldenTicketAmount);
            goldenTicketAmountText.text = goldenTicketAmount.ToString();
        }
    }

    private void UpdateLevelIcons(int levelPassed) {
        for (int i = 0; i < onGoingIcons.Length; i++) {
            onGoingIcons[i].SetActive(i == levelPassed - 1);
            lockIcons[i].SetActive(i >= levelPassed);
            finishedIcons[i].SetActive(i < levelPassed - 1);
        }
    }

    public void SoundBtn() {
        ToggleState(soundOn, soundOff, soundIconOn, soundIconOff, StringsManager.SoundKey);
    }

    public void MusicBtn() {
        ToggleState(musicOn, musicOff, musicIconOn, musicIconOff, StringsManager.MusicKey);
    }

    public void VibrateBtn() {
        ToggleState(vibrateOn, vibrateOff, vibrateIconOn, vibrateIconOff, StringsManager.VibrateKey);
    }

    private void ToggleState(GameObject onObj, GameObject offObj, GameObject iconOn, GameObject iconOff, string key) {
        bool currentState = onObj.activeSelf;
        onObj.SetActive(!currentState);
        offObj.SetActive(currentState);
        iconOn.SetActive(!currentState);
        iconOff.SetActive(currentState);
        PlayerPrefs.SetInt(key, currentState ? 0 : 1);
        PlayerPrefs.Save();
    }

    private void LoadState() {
        LoadToggleState(soundOn, soundOff, soundIconOn, soundIconOff, StringsManager.SoundKey);
        LoadToggleState(musicOn, musicOff, musicIconOn, musicIconOff, StringsManager.MusicKey);
        LoadToggleState(vibrateOn, vibrateOff, vibrateIconOn, vibrateIconOff, StringsManager.VibrateKey);
    }

    private void LoadToggleState(GameObject onObj, GameObject offObj, GameObject iconOn, GameObject iconOff, string key) {
        bool state = PlayerPrefs.GetInt(key, 1) == 1;
        onObj.SetActive(state);
        offObj.SetActive(!state);
        iconOn.SetActive(state);
        iconOff.SetActive(!state);
    }

    public void LevelPanelAppear() {
        selectLevelPanel.position = new Vector2(0, 0);
    }

    public void LevelPanelDisappear() {
        selectLevelPanel.position = levelPanelFirstPos;
    }

    public void PlayBtnLoadScene() {
        PlayerPrefs.SetInt(StringsManager.PlayBtnLoadScene, 1);
        PlayerPrefs.SetInt(StringsManager.LevelBtnLoadScene, 0);
        SceneManager.LoadScene("PlayScene");
    }

    public void LoadLevel(int level) {
        if (level < 1 || level > onGoingIcons.Length) {
            return;
        }

        if (onGoingIcons[level - 1].activeSelf || finishedIcons[level - 1].activeSelf) {
            PlayerPrefs.SetInt(StringsManager.PlayBtnLoadScene, 0);
            PlayerPrefs.SetInt(StringsManager.LevelBtnLoadScene, 1);
            PlayerPrefs.SetInt(StringsManager.LevelBtnIdx, level);
            SceneManager.LoadScene("PlayScene");
        }
    }

    public void ConfirmProfile() {
        PlayerPrefs.SetString(StringsManager.ProfileText, profileText.text);
    }

    public void PopSound() {
        PlaySoundManager.instance.audioSource.PlayOneShot(PlaySoundManager.instance.popSound);
    }

    public void BuyGoldenTicket(int amount) {
        goldenTicketAmount += amount;
        PlayerPrefs.SetInt(StringsManager.GoldenTicketAmount, goldenTicketAmount);
        goldenTicketAmountText.text = goldenTicketAmount.ToString();
        purchaseBoard.SetActive(true);
        StartCoroutine(HideObject(purchaseBoard));
    }

    IEnumerator HideObject(GameObject gameObject) {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);    
    }
}
