using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplayManager : MonoBehaviour {
    public static LevelDisplayManager Instance;
    public TextMeshProUGUI levelDisplayText;
    public GameObject[] levels;
    int levelPassed;

    private void Awake() {
        Instance = this;
    }

    void Start() {
        if (PlayerPrefs.GetInt(StringsTextManager.PlayButtonLoadScene) == 1)
            UpdateLevelDisplay();
        else if (PlayerPrefs.GetInt(StringsTextManager.LevelButtonLoadScene) == 1) {
            DisplayCurrentLevel();
        }

        if (levelDisplayText == null) {
            levelDisplayText = GetComponent<TextMeshProUGUI>();
        }
    }

    void Update() {
        //UpdateLevelDisplay();
    }

    void UpdateLevelDisplay() {
        levelPassed = PlayerPrefs.GetInt(StringsTextManager.LevelPassed, 1);
        for (int i = 0; i < levels.Length; i++) {
            levels[i].SetActive(i == levelPassed - 1);
            if (i == levelPassed - 1) {
                levelDisplayText.text = "Level " + (i + 1);
            }
        }
    }

    void DisplayCurrentLevel() {
        int levelIdx = PlayerPrefs.GetInt(StringsTextManager.LevelButtonIdx);
        if (levelIdx >= 1 && levelIdx <= levels.Length) {
            for (int i = 0; i < levels.Length; i++) {
                levels[i].SetActive(i == levelIdx - 1);
            }
            levelDisplayText.text = "Level " + levelIdx;
        } 
    }

    public void UpdateNewLevel() {
        PlayerPrefs.SetInt(StringsTextManager.LevelPassed, levelPassed + 1);
    }
}
