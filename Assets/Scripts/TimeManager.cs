using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    public static TimeManager instance;
    [SerializeField] TextMeshProUGUI realTimeText;
    public float restOfTime;
    [SerializeField] GameObject failPanel;
    private bool isTimerCountDown = true;

    private void Awake() {
        instance = this;
    }

    void Update() {
        if (isTimerCountDown) {
            if (restOfTime > 0) {
                restOfTime -= Time.deltaTime;
            } else if (restOfTime < 0) {
                restOfTime = 0;
                failPanel.SetActive(true);
                PlaySoundEffectManager.instance.audioEffectSource.PlayOneShot(PlaySoundEffectManager.instance.loseEffectSound);
                isTimerCountDown = false; 
            }
            UpdateTimerText();
        }
    }

    public void PauseTimer() {
        isTimerCountDown = false;
    }
    
    public void ContinueTimer() {
        isTimerCountDown = true;
    }

    private void UpdateTimerText() {
        int minutes = Mathf.FloorToInt(restOfTime / 60);
        int seconds = Mathf.FloorToInt(restOfTime % 60);
        realTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
