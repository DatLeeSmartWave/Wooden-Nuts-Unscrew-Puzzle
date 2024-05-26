using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneButtonController : MonoBehaviour {
    public static HomeSceneButtonController instance;
    
    public RectTransform selectLevelBoard;
    Vector2 levelPanelFirstPosition;
    Vector2 dailyRewardFirstPosition;
    [SerializeField] RectTransform dailyRewardBoard;
    public GameObject attentionSignIcon, doneIcon;
    public GameObject purchasePanel, shopPanel, soundEffectOn, soundEffectOff;
    public GameObject musicEffectOn, musicEffectOff;
    public GameObject vibrateEffectOn, vibrateEffectOff;
    public int goldenTicketNumber;
    public TextMeshProUGUI goldenTicketNumberText, dailyGoldenTicketNumberText;

    public GameObject[] checkIcons;
    public GameObject[] lockIcons;
    public GameObject[] backgrounds;

    private const string AttentionSignLastShownDateKey = "AttentionSignLastShownDate";
    private const string CheckIconKeyPrefix = "CheckIcon_";
    private const string BackgroundKeyPrefix = "Background_";
    private const string IsFirstTimeKey = "IsFirstTime";
    private const string LockIconKeyPrefix = "LockIcon_";

    private void Awake() {
        instance = this;
    }

    private void Start() {
        levelPanelFirstPosition = selectLevelBoard.position;
        LoadState();
        LoadCheckIconStates();
        UnlockIconsBasedOnBackgroundIdx();
        LoadBackgroundState();
        SetDefaultIconsIfFirstTime();
        CheckAndSetAttentionSign();
        if (soundEffectOn.activeSelf) PlayerPrefs.SetInt(StringsTextManager.SoundEffectKey, 1);
        if (musicEffectOn.activeSelf) PlayerPrefs.SetInt(StringsTextManager.MusicEffectKey, 1);
        if (vibrateEffectOn.activeSelf) PlayerPrefs.SetInt(StringsTextManager.VibrateEffectKey, 1);
    }

    private void Update() {
        goldenTicketNumber = PlayerPrefs.GetInt(StringsTextManager.GoldenTicketNumber);
        PlayerPrefs.SetInt(StringsTextManager.GoldenTicketNumber, goldenTicketNumber);
        goldenTicketNumberText.text = goldenTicketNumber.ToString();
        dailyGoldenTicketNumberText.text = goldenTicketNumberText.text;
    }

    private void CheckAndSetAttentionSign() {
        string lastShownDateString = PlayerPrefs.GetString(AttentionSignLastShownDateKey, string.Empty);
        string todayDateString = DateTime.Now.ToString("yyyyMMdd");
        if (lastShownDateString != todayDateString) {
            attentionSignIcon.SetActive(true);
            doneIcon.SetActive(false);
            PlayerPrefs.SetString(AttentionSignLastShownDateKey, todayDateString);
            PlayerPrefs.Save();
        }
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

    public void CollectDailyReward(int amount) {
        PlaySoundEffectManager.instance.audioEffectSource.PlayOneShot(PlaySoundEffectManager.instance.claimSound);
        goldenTicketNumber += amount;
        PlayerPrefs.SetInt(StringsTextManager.GoldenTicketNumber, goldenTicketNumber);
        goldenTicketNumberText.text = goldenTicketNumber.ToString();
        doneIcon.SetActive(true);
        attentionSignIcon.SetActive(false);
    }

    IEnumerator HideObject(GameObject gameObject) {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    public void BGButton(int buttonIndex) {
        foreach (GameObject checkIcon in checkIcons) {
            checkIcon.SetActive(false);
        }
        if (buttonIndex >= 0 && buttonIndex < checkIcons.Length && !lockIcons[buttonIndex].activeSelf) {
            checkIcons[buttonIndex].SetActive(true);
            SaveCheckIconState(buttonIndex);
            ActivateBackground(buttonIndex);
            PlayerPrefs.SetInt(StringsTextManager.BackgroundIdx, buttonIndex + 1);
            PlayerPrefs.Save();
        }
    }

    private void SaveCheckIconState(int activeIndex) {
        for (int i = 0; i < checkIcons.Length; i++) {
            PlayerPrefs.SetInt(CheckIconKeyPrefix + i, i == activeIndex ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    private void LoadCheckIconStates() {
        for (int i = 0; i < checkIcons.Length; i++) {
            bool isActive = PlayerPrefs.GetInt(CheckIconKeyPrefix + i, 0) == 1;
            checkIcons[i].SetActive(isActive);
            if (isActive) {
                ActivateBackground(i);
            }
        }
    }

    private void UnlockIconsBasedOnBackgroundIdx() {
        int backgroundIdx = PlayerPrefs.GetInt(StringsTextManager.BackgroundIdx);
        for (int i = 0; i < lockIcons.Length; i++) {
            lockIcons[i].SetActive(i >= backgroundIdx);
        }
    }

    private void SaveBackgroundState(int activeIndex) {
        PlayerPrefs.SetInt(BackgroundKeyPrefix + activeIndex, 1);
        PlayerPrefs.Save();
    }

    private void LoadBackgroundState() {
        for (int i = 0; i < backgrounds.Length; i++) {
            bool isActive = PlayerPrefs.GetInt(BackgroundKeyPrefix + i, 0) == 1;
            backgrounds[i].SetActive(isActive);
        }
    }

    private void SetDefaultIconsIfFirstTime() {
        bool isFirstTime = PlayerPrefs.GetInt(IsFirstTimeKey, 1) == 1;
        if (isFirstTime) {
            checkIcons[0].SetActive(true);
            backgrounds[0].SetActive(true);
            lockIcons[0].SetActive(false);
            PlayerPrefs.SetInt(CheckIconKeyPrefix + 0, 1);
            PlayerPrefs.SetInt(BackgroundKeyPrefix + 0, 1);
            PlayerPrefs.SetInt(LockIconKeyPrefix + 0, 0);
            PlayerPrefs.SetInt(IsFirstTimeKey, 0);
            PlayerPrefs.Save();
        }
    }

    private void ActivateBackground(int index) {
        foreach (var background in backgrounds) {
            background.SetActive(false);
        }
        backgrounds[index].SetActive(true);
    }
}
