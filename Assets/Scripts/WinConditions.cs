using System.Collections;
using UnityEngine;

public class WinConditions : MonoBehaviour {
    public static WinConditions Instance;
    public GameObject winBoard, levelWinBoard, level30, nextButton;
    private bool hasShownWinBoard = false;

    private void Awake() {
        Instance = this;
    }

    void Update() {
        if (!hasShownWinBoard) {
            GameObject[] woodObjects = GameObject.FindGameObjectsWithTag("Wood");
            if (woodObjects.Length == 0) {
                if (winBoard != null) {
                    if (PlayerPrefs.GetInt(StringsManager.PlayBtnLoadScene) == 1) {
                        PlaySceneButtonManager.instance.goldenTicketAmount += 2;
                        PlayerPrefs.SetInt(StringsManager.GoldenTicketAmount, PlaySceneButtonManager.instance.goldenTicketAmount);
                        TimeManager.instance.StopTimer();
                        StartCoroutine(ShowWinBoard());
                    } 
                    else if (PlayerPrefs.GetInt(StringsManager.LevelBtnLoadScene) == 1) {
                        TimeManager.instance.StopTimer();
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
        LevelDisplay.Instance.UpdateLevel();
        yield return new WaitForSeconds(1f);
        PlaySoundManager.instance.audioSource.PlayOneShot(PlaySoundManager.instance.winSound);
        winBoard.SetActive(true);
    }

    IEnumerator ShowLevelWinBoard() {
        yield return new WaitForSeconds(1f);
        PlaySoundManager.instance.audioSource.PlayOneShot(PlaySoundManager.instance.winSound);
        levelWinBoard.SetActive(true);
    }
}
