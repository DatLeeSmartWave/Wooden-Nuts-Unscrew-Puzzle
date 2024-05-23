using System.Collections;
using UnityEngine;

public class BarrierManager : MonoBehaviour {
    private GameObject hiddenWood;
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
            StartCoroutine(CounterWood(collision.gameObject));
        }
    }

    IEnumerator CounterWood(GameObject gameObject) {
        yield return new WaitForSeconds(.5f);
        hiddenWood = gameObject;
        gameObject.SetActive(false);
    }

    public void RestoreHiddenWood() {
        if (hiddenWood != null) {
            hiddenWood.SetActive(true);
            hiddenWood.GetComponent<WoodManager>().ResetPosition();
            hiddenWood = null;
        }
    }

    public bool HasHiddenWood() {
        return hiddenWood != null;
    }
}
