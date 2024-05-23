using UnityEngine;

public class WoodStickManager : MonoBehaviour {
    private Vector3 firstPosition;
    private Quaternion firstRotation;

    void Start() {
        firstPosition = transform.position;
        firstRotation = transform.rotation;
    }

    public void UndoPosition() {
        transform.position = firstPosition;
        transform.rotation = firstRotation;
    }
}
