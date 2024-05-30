using System.Collections;
using TMPro;
using UnityEngine;

public class VictoryConditions : MonoBehaviour {
    public static VictoryConditions Instance;
    public GameObject victoryBoard, levelVictoryBoard, buttonNext;
    private bool hasShownWinBoard = false;
    [SerializeField] TextMeshProUGUI timeToWinText;

    private void Awake() {
        Instance = this;
    }

    void Update() {
        if (!hasShownWinBoard) {
            GameObject[] woodObjects = GameObject.FindGameObjectsWithTag("Wood");
            if (woodObjects.Length == 0) {
                if (victoryBoard != null) {
                    if (PlayerPrefs.GetInt(StringsTextManager.PlayButtonLoadScene) == 1) {
                        PlaySceneButtonController.instance.goldenTicketNumber += 1;
                        PlayerPrefs.SetInt(StringsTextManager.GoldenTicketNumber, PlaySceneButtonController.instance.goldenTicketNumber);
                        //PlaySceneButtonController.instance.StopCoroutine(PlaySceneButtonController.instance.CountSeconds());
                        PlaySceneButtonController.instance.StopAllCoroutines();
                        timeToWinText.text = "YOU TOOK " + PlaySceneButtonController.instance.secondsElapsed.ToString() + " SECONDS";
                        //TimeManager.instance.PauseTimer();
                        StartCoroutine(ShowWinBoard());

                        string levelText = LevelDisplayManager.Instance.levelDisplayText.text;
                        if (levelText == "Level 2") {
                            PlayerPrefs.SetInt(StringsTextManager.BackgroundIdx, 2);
                        } else if (levelText == "Level 3") {
                            PlayerPrefs.SetInt(StringsTextManager.BackgroundIdx, 3);
                        } else if (levelText == "Level 4") {
                            PlayerPrefs.SetInt(StringsTextManager.BackgroundIdx, 4);
                        } else if (levelText == "Level 5") {
                            PlayerPrefs.SetInt(StringsTextManager.BackgroundIdx, 5);
                        } else if (levelText == "Level 6") {
                            PlayerPrefs.SetInt(StringsTextManager.BackgroundIdx, 6);
                        } else if (levelText == "Level 7") {
                            PlayerPrefs.SetInt(StringsTextManager.BackgroundIdx, 7);
                        } else if (levelText == "Level 8") {
                            PlayerPrefs.SetInt(StringsTextManager.BackgroundIdx, 8);
                        } else if (levelText == "Level 9") {
                            PlayerPrefs.SetInt(StringsTextManager.BackgroundIdx, 9);
                        }
                    }
                    hasShownWinBoard = true;
                }
            }
        }
    }

    IEnumerator ShowWinBoard() {
        LevelDisplayManager.Instance.UpdateNewLevel();
        yield return new WaitForSeconds(1f);
        PlaySoundEffectManager.instance.audioEffectSource.PlayOneShot(PlaySoundEffectManager.instance.winEffectSound);
        victoryBoard.SetActive(true);
    }
}
