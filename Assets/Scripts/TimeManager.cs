using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    public static TimeManager instance;
    [SerializeField] TextMeshProUGUI timerText;
    public float remainingTime;
    [SerializeField] GameObject losePanel;
    private bool isTimerRunning = true;

    private void Awake() {
        instance = this;
    }

    void Update() {
        if (isTimerRunning) {
            if (remainingTime > 0) {
                remainingTime -= Time.deltaTime;
            } else if (remainingTime < 0) {
                remainingTime = 0;
                losePanel.SetActive(true);
                PlaySoundManager.instance.audioSource.PlayOneShot(PlaySoundManager.instance.loseSound);
                isTimerRunning = false; 
            }
            UpdateTimerText();
        }
    }

    public void StopTimer() {
        isTimerRunning = false;
    }
    
    public void RemainTimer() {
        isTimerRunning = true;
    }

    private void UpdateTimerText() {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
