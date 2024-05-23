using UnityEngine;

public class ScrewControlManager : MonoBehaviour {
    public static ScrewControlManager instance;
    public Animator animator;
    public static ScrewControlManager presentOutScrew = null;
    private Vector3 previousHolePosition;

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
                            if (presentOutScrew != this) {
                                if (presentOutScrew != null) {
                                    presentOutScrew.GoInside();
                                }
                                presentOutScrew = this;
                                animator.SetTrigger(StringsTextManager.isMoveOut);
                            } else {
                                animator.SetTrigger(StringsTextManager.isMoveIn);
                                presentOutScrew = null;
                            }
                            break;
                        }
                    }
                }
            }
        }
    }

    public void GoInside() {
        animator.SetTrigger(StringsTextManager.isMoveIn);
    }

    public void ScrewSoundEffect() {
        PlaySoundEffectManager.instance.audioEffectSource.PlayOneShot(PlaySoundEffectManager.instance.screwEffectSound);
    }

    public void SetPreviousHolePosition(Vector3 position) {
        previousHolePosition = position;
    }

    public void MoveToPreviousHole() {
        transform.position = previousHolePosition;
    }
}
