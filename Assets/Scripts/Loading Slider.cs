using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashSceneSlider : MonoBehaviour {
    Slider slider;

    void Start() {
        slider = GetComponent<Slider>();
        StartCoroutine(SmoothFillFunction());
    }

    IEnumerator SmoothFillFunction() {
        float duration = 3f; // 
        float elapsed = 0f;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            slider.value = Mathf.Lerp(0, 100, elapsed / duration);
            yield return null;
        }
        slider.value = 100; 
    }

    private void Update() {
        if(slider.value >= 100) {
            SceneManager.LoadScene("HomeScene");
        }
    }
}
