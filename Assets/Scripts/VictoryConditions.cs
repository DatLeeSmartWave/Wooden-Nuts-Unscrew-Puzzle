using System.Collections;
using UnityEngine;

public class VictoryConditions : MonoBehaviour {
    public static VictoryConditions Instance;
    public GameObject victoryBoard, levelVictoryBoard, buttonNext;
    private bool hasShownWinBoard = false;

    private void Awake() {
        Instance = this;
    }

    void Update() {
        if (!hasShownWinBoard) {
            GameObject[] woodObjects = GameObject.FindGameObjectsWithTag("Wood");
            if (woodObjects.Length == 0) {
                if (victoryBoard != null) {
                    if (PlayerPrefs.GetInt(StringsTextManager.PlayButtonLoadScene) == 1) {
                        PlaySceneButtonController.instance.goldenTicketNumber += 2;
                        PlayerPrefs.SetInt(StringsTextManager.GoldenTicketNumber, PlaySceneButtonController.instance.goldenTicketNumber);
                        TimeManager.instance.PauseTimer();
                        StartCoroutine(ShowWinBoard());
                    } 
                    else if (PlayerPrefs.GetInt(StringsTextManager.LevelButtonLoadScene) == 1) {
                        TimeManager.instance.PauseTimer();
                        StartCoroutine(ShowLevelWinBoard());
                    } 
                    hasShownWinBoard = true;
                } 
                //else if (level30.activeSelf) {
                //    if (PlayerPrefs.GetInt(StringsManager.LevelBtnLoadScene) == 1) {
                //        TimeManager.instance.StopTimer();
                //        StartCoroutine(ShowLevelWinBoard());
                //    }
                //    hasShownWinBoard = true;
                //}
            }
        }
    }

    IEnumerator ShowWinBoard() {
        LevelDisplayManager.Instance.UpdateNewLevel();
        yield return new WaitForSeconds(1f);
        PlaySoundEffectManager.instance.audioEffectSource.PlayOneShot(PlaySoundEffectManager.instance.winEffectSound);
        victoryBoard.SetActive(true);
    }

    IEnumerator ShowLevelWinBoard() {
        yield return new WaitForSeconds(1f);
        PlaySoundEffectManager.instance.audioEffectSource.PlayOneShot(PlaySoundEffectManager.instance.winEffectSound);
        levelVictoryBoard.SetActive(true);
    }
}
