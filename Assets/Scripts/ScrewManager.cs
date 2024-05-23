using UnityEngine;

public class ScrewManager : MonoBehaviour {
    public static ScrewManager instance;
    public Animator animator;
    public static ScrewManager currentOutScrew = null;
    private Vector3 oldHolePosition;

    void Start() {
        instance = this;
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (Input.touchCount > 0) {
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    Collider2D[] colliders = Physics2D.OverlapPointAll(touchPosition);
                    bool isTouchingUI = false;
                    foreach (Collider2D collider in colliders) {
                        if (collider.CompareTag("UI")) {
                            isTouchingUI = true;
                            break;
                        }
                    }
                    if (isTouchingUI) {
                        continue;
                    }
                    foreach (Collider2D collider in colliders) {
                        if (collider.gameObject == gameObject) {
                            if (currentOutScrew != this) {
                                if (currentOutScrew != null) {
                                    currentOutScrew.GoIn();
                                }
                                currentOutScrew = this;
                                animator.SetTrigger("isGoOut");
                            } else {
                                animator.SetTrigger("isGoIn");
                                currentOutScrew = null;
                            }
                            break;
                        }
                    }
                }
            }
        }
    }

    public void GoIn() {
        animator.SetTrigger("isGoIn");
    }

    public void ScrewSound() {
        PlaySoundManager.instance.audioSource.PlayOneShot(PlaySoundManager.instance.screwSound);
    }

    public void SetOldHolePosition(Vector3 position) {
        oldHolePosition = position;
    }

    public void MoveToOldHole() {
        transform.position = oldHolePosition;
    }
}
