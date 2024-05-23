using System.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public static ItemManager instance;
    private bool canDestroyScrew;
    private bool canDestroyWood;

    private void Awake() {
        instance = this;
    }

    void Update() {
        if (canDestroyScrew) {
            DestroyScrew();
        }
        if (canDestroyWood) {
            StartCoroutine(DestroyWood());
        }
    }

    public void SetCanDestroyScrew(bool value) {
        canDestroyScrew = value;
    }

    public void SetCanDestroyWood(bool value) {
        canDestroyWood = value;
    }

    void DestroyScrew() {
        if (Input.touchCount > 0) {
            foreach (Touch touch in Input.touches) {
                //if (PlaySceneButtonManager.instance.unscrewAmount > 0) {
                    if (touch.phase == TouchPhase.Began) {
                        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        Collider2D[] colliders = Physics2D.OverlapPointAll(touchPosition);
                        foreach (Collider2D collider in colliders) {
                            if (collider.CompareTag("Screw")) {
                                collider.gameObject.SetActive(false);
                                PlaySceneButtonManager.instance.StartCoroutine(PlaySceneButtonManager.instance.CounteractItemNoticePanel());
                                SetCanDestroyScrew(false);
                                return;
                            }
                        }
                    }
                //}
            }
        }
    }

    public IEnumerator DestroyWood() {
        if (Input.touchCount > 0) {
            foreach (Touch touch in Input.touches) {
                //if (PlaySceneButtonManager.instance.hammerAmount > 0) {
                    if (touch.phase == TouchPhase.Began) {
                        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        Collider2D[] colliders = Physics2D.OverlapPointAll(touchPosition);
                        foreach (Collider2D collider in colliders) {
                            if (collider.CompareTag("Wood")) {
                                HammerController.instance.animator.SetTrigger("isBreak");
                                HammerController.instance.transform.position = collider.gameObject.transform.position;
                                yield return new WaitForSeconds(1.0f);
                                collider.gameObject.SetActive(false);
                                StartCoroutine(ShakeCamera(0.15f, 0.2f));
                                yield return new WaitForSeconds(0.5f);
                                HammerController.instance.gameObject.SetActive(false);
                                PlaySceneButtonManager.instance.StartCoroutine(PlaySceneButtonManager.instance.CounteractWoodNoticePanel());
                                SetCanDestroyWood(false);
                                yield break;
                            }
                        }
                    }
                //}
            }
        }
    }

    IEnumerator ShakeCamera(float duration, float magnitude) {
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
