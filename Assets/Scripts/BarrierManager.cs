using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BarrierManager : MonoBehaviour {
    private GameObject hiddenWoodStick;
    public static BarrierManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Wood")) {
            StartCoroutine(HideWood(collision.gameObject));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Wood")) {
            StartCoroutine(HideWood(collision.gameObject));
        }
    }
    IEnumerator HideWood(GameObject gameObject) {
        yield return new WaitForSeconds(0f);
        hiddenWoodStick = gameObject;
        gameObject.SetActive(false);
    }

    public void RestoreHiddenWood() {
        if (hiddenWoodStick != null) {
            hiddenWoodStick.SetActive(true);
            hiddenWoodStick.GetComponent<WoodStickManager>().UndoPosition();
            hiddenWoodStick = null;
        }
    }

    public bool HasHiddenWood() {
        return hiddenWoodStick != null;
    }
}
