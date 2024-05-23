using UnityEngine;

public class HoleManager : MonoBehaviour {
    public static HoleManager instance;
    public GameObject screwPrefab;

    private void Awake() {
        instance = this;
    }

    void Start() {
    }

    void Update() {
        if (Input.touchCount > 0) {
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    Collider2D[] colliders = Physics2D.OverlapPointAll(touchPosition);
                    foreach (Collider2D collider in colliders) {
                        if (collider.gameObject == gameObject) {
                            bool isTouchingWood = false;
                            Collider2D[] surroundingColliders = Physics2D.OverlapCircleAll(transform.position, 0.1f); // Tùy chỉnh bán kính cho phù hợp
                            foreach (Collider2D surroundingCollider in surroundingColliders) {
                                if (surroundingCollider.CompareTag("Wood")) {
                                    isTouchingWood = true;
                                    break;
                                }
                            }
                            if (!isTouchingWood) {
                                Collider2D[] screws = Physics2D.OverlapPointAll(touchPosition);
                                bool hasScrewInside = false;
                                foreach (Collider2D screw in screws) {
                                    if (screw.CompareTag("Screw")) {
                                        hasScrewInside = true;
                                        break;
                                    }
                                }
                                if (!hasScrewInside) {
                                    if (ScrewManager.currentOutScrew != null) {
                                        Vector3 oldPosition = ScrewManager.currentOutScrew.transform.position;
                                        ScrewManager.currentOutScrew.SetOldHolePosition(oldPosition);
                                        Destroy(ScrewManager.currentOutScrew.gameObject);
                                        GameObject newScrew = Instantiate(screwPrefab, transform.position, Quaternion.identity);
                                        newScrew.GetComponent<ScrewManager>().SetOldHolePosition(oldPosition);
                                        ScrewManager.currentOutScrew = null;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
