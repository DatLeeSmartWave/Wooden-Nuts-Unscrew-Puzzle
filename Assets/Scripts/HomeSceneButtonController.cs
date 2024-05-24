using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneButtonController : MonoBehaviour {
    public static HomeSceneButtonController instance;
    public RectTransform selectLevelBoard;
    Vector2 levelPanelFirstPosition;
    Vector2 dailyRewardFirstPosition;
    //[SerializeField] TextMeshProUGUI personalProfileText;
    //[SerializeField] TextMeshProUGUI hasSetedProfileText;
    [SerializeField] RectTransform dailyRewardBoard;
    public GameObject purchasePanel, shopPanel, soundEffectOn, soundEffectOff;
    public GameObject musicEffectOn, musicEffectOff;
    public GameObject vibrateEffectOn, vibrateEffectOff;
    //public GameObject[] onGoingIcons;
    //public GameObject[] lockIcons;
    //public GameObject[] finishedIcons;
    public int goldenTicketNumber;
    public TextMeshProUGUI goldenTicketNumberText;

    // Key names for PlayerPrefs
    //private const string SoundKey = "SoundState";
    //private const string MusicKey = "MusicState";
    //private const string VibrateKey = "VibrateState";

    private void Awake() {
        instance = this;
    }

    private void Start() {
        levelPanelFirstPosition = selectLevelBoard.position;
        //if(shopBoard.activeSelf) {
        //    goldenTicketAmount = PlayerPrefs.GetInt(StringsManager.GoldenTicketAmount, 0);
        //}
        LoadState();
    }

    private void Update() {
        int levelPassed = PlayerPrefs.GetInt(StringsTextManager.LevelPassed, 0);
        if (musicEffectOn.activeSelf)
            PlayerPrefs.SetInt(StringsTextManager.MusicEffectKey, 1);
        if (soundEffectOn.activeSelf)
            PlayerPrefs.SetInt(StringsTextManager.SoundEffectKey, 1);
        if (vibrateEffectOn.activeSelf)
            PlayerPrefs.SetInt(StringsTextManager.VibrateEffectKey, 1);
        //UpdateLevelIcons(levelPassed);
        //if (profilePanel.activeSelf) {
        //    hasSetedProfileText.text = PlayerPrefs.GetString(StringsManager.PersonalInformationText);
        //}
        if (shopPanel.activeSelf) {
            goldenTicketNumber = PlayerPrefs.GetInt(StringsTextManager.GoldenTicketNumber);
            goldenTicketNumberText.text = goldenTicketNumber.ToString();
        }
    }

    //private void UpdateLevelIcons(int levelPassed) {
    //    for (int i = 0; i < onGoingIcons.Length; i++) {
    //        onGoingIcons[i].SetActive(i == levelPassed - 1);
    //        lockIcons[i].SetActive(i >= levelPassed);
    //        finishedIcons[i].SetActive(i < levelPassed - 1);
    //    }
    //}

    public void SoundBtn() {
        ToggleState(soundEffectOn, soundEffectOff, StringsTextManager.SoundEffectKey);
    }

    public void MusicBtn() {
        ToggleState(musicEffectOn, musicEffectOff, StringsTextManager.MusicEffectKey);
    }

    public void VibrateBtn() {
        ToggleState(vibrateEffectOn, vibrateEffectOff, StringsTextManager.VibrateEffectKey);
    }

    private void ToggleState(GameObject onObj, GameObject offObj, string key) {
        bool currentState = onObj.activeSelf;
        onObj.SetActive(!currentState);
        offObj.SetActive(currentState);
        PlayerPrefs.SetInt(key, currentState ? 0 : 1);
        PlayerPrefs.Save();
    }

    private void LoadState() {
        LoadToggleState(soundEffectOn, soundEffectOff, StringsTextManager.SoundEffectKey);
        LoadToggleState(musicEffectOn, musicEffectOff, StringsTextManager.MusicEffectKey);
        LoadToggleState(vibrateEffectOn, vibrateEffectOff, StringsTextManager.VibrateEffectKey);
    }

    private void LoadToggleState(GameObject onObj, GameObject offObj, string key) {
        bool state = PlayerPrefs.GetInt(key, 1) == 1;
        onObj.SetActive(state);
        offObj.SetActive(!state);
    }

    public void LevelPanelAppear(RectTransform panel) {
        panel.position = new Vector2(0, 0);
    }

    public void LevelPanelDisappear(RectTransform panel) {
        panel.position = levelPanelFirstPosition;
    }

    public void PlayBtnLoadScene() {
        PlayerPrefs.SetInt(StringsTextManager.PlayButtonLoadScene, 1);
        PlayerPrefs.SetInt(StringsTextManager.LevelButtonLoadScene, 0);
        SceneManager.LoadScene("PlayScene");
    }

    //public void LoadLevel(int level) {
    //    if (level < 1 || level > onGoingIcons.Length) {
    //        return;
    //    }

    //    if (onGoingIcons[level - 1].activeSelf || finishedIcons[level - 1].activeSelf) {
    //        PlayerPrefs.SetInt(StringsTextManager.PlayButtonLoadScene, 0);
    //        PlayerPrefs.SetInt(StringsTextManager.LevelButtonLoadScene, 1);
    //        PlayerPrefs.SetInt(StringsTextManager.LevelButtonIdx, level);
    //        SceneManager.LoadScene("PlayScene");
    //    }
    //}

    public void PopSound() {
        PlaySoundEffectManager.instance.audioEffectSource.PlayOneShot(PlaySoundEffectManager.instance.popEffectSound);
    }

    public void BuyGoldenTicket(int amount) {
        goldenTicketNumber += amount;
        PlayerPrefs.SetInt(StringsTextManager.GoldenTicketNumber, goldenTicketNumber);
        goldenTicketNumberText.text = goldenTicketNumber.ToString();
        purchasePanel.SetActive(true);
        StartCoroutine(HideObject(purchasePanel));
    }

    IEnumerator HideObject(GameObject gameObject) {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);    
    }
}
