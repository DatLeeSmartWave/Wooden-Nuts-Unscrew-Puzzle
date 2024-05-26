using System.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public static ItemManager instance;
    private bool canDestroyScrewBool;
    private bool canDestroyWoodBool;
    private bool woodDestroyed; // Biến để theo dõi trạng thái phá hủy "wood"

    private void Awake() {
        instance = this;
    }

    void Update() {
        if (canDestroyScrewBool) {
            DestroyScrewFunction();
        }
        if (canDestroyWoodBool) {
            StartCoroutine(DestroyWoodFunction());
        }
    }

    public void SetUpCanDestroyScrew(bool value) {
        canDestroyScrewBool = value;
    }

    public void SetUpCanDestroyWood(bool value) {
        canDestroyWoodBool = value;
        woodDestroyed = false; // Reset trạng thái khi bắt đầu có thể phá hủy "wood"
    }

    void DestroyScrewFunction() {
        if (Input.touchCount > 0) {
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    PlaySoundEffectManager.instance.audioEffectSource.PlayOneShot(PlaySoundEffectManager.instance.unscrewSound);
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    Collider2D[] colliders = Physics2D.OverlapPointAll(touchPosition);
                    foreach (Collider2D collider in colliders) {
                        if (collider.CompareTag("Screw")) {
                            collider.gameObject.SetActive(false);
                            PlaySceneButtonController.instance.StartCoroutine(PlaySceneButtonController.instance.HideItemNoticePanel());
                            SetUpCanDestroyScrew(false);
                            return;
                        }
                    }
                }
            }
        }
    }

    public IEnumerator DestroyWoodFunction() {
        if (woodDestroyed) yield break; 

        if (Input.touchCount > 0) {
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    Collider2D[] colliders = Physics2D.OverlapPointAll(touchPosition);
                    foreach (Collider2D collider in colliders) {
                        if (collider.CompareTag("Wood")) {
                            woodDestroyed = true; // Đánh dấu "wood" đã bị phá hủy
                            HammerControllerScript.instance.animator.SetTrigger("isBreak");
                            HammerControllerScript.instance.transform.position = collider.gameObject.transform.position;
                            yield return new WaitForSeconds(1.0f);
                            collider.gameObject.SetActive(false);
                            StartCoroutine(VibrateCamera(0.15f, 0.2f));
                            PlaySoundEffectManager.instance.audioEffectSource.PlayOneShot(PlaySoundEffectManager.instance.hammerSound);
                            yield return new WaitForSeconds(0.5f);
                            HammerControllerScript.instance.gameObject.SetActive(false);
                            PlaySceneButtonController.instance.StartCoroutine(PlaySceneButtonController.instance.HideWoodNoticePanel());
                            SetUpCanDestroyWood(false);
                            yield break;
                        }
                    }
                }
            }
        }
    }

    IEnumerator VibrateCamera(float duration, float magnitude) {
        Vector3 originalPosition = Camera.main.transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            Camera.main.transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = originalPosition;
    }
}
