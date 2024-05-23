using UnityEngine;

public class WoodManager : MonoBehaviour {
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start() {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void ResetPosition() {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
