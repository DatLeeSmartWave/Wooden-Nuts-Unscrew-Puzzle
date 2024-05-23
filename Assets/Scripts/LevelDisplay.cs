using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour {
    public static LevelDisplay Instance;
    public TextMeshProUGUI levelText;
    public GameObject[] levels;
    int levelPassed;

    private void Awake() {
        Instance = this;
    }

    void Start() {
        if (PlayerPrefs.GetInt(StringsManager.PlayBtnLoadScene) == 1)
            UpdateLevelDisplay();
        else if (PlayerPrefs.GetInt(StringsManager.LevelBtnLoadScene) == 1) {
            DisplayLevel();
        }

        if (levelText == null) {
            levelText = GetComponent<TextMeshProUGUI>();
        }
    }

    void Update() {
        //UpdateLevelDisplay();
    }

    void UpdateLevelDisplay() {
        levelPassed = PlayerPrefs.GetInt(StringsManager.LevelPassed, 1);
        for (int i = 0; i < levels.Length; i++) {
            levels[i].SetActive(i == levelPassed - 1);
            if (i == levelPassed - 1) {
                levelText.text = "Level " + (i + 1);
            }
        }
    }

    void DisplayLevel() {
        int levelIdx = PlayerPrefs.GetInt(StringsManager.LevelBtnIdx);
        if (levelIdx >= 1 && levelIdx <= levels.Length) {
            for (int i = 0; i < levels.Length; i++) {
                levels[i].SetActive(i == levelIdx - 1);
            }
            levelText.text = "Level " + levelIdx;
        } 
    }

    public void UpdateLevel() {
        PlayerPrefs.SetInt(StringsManager.LevelPassed, levelPassed + 1);
    }
}
