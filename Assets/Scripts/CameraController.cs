using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target; // Usually the player
    public Transform pivot; // Used to decouple camera controls from player rotations
    public Vector3 offset; // Camera position relative to the player

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = null;
    }

    // Update is called once per frame
    void Update() {

    }

    // Called after Update to prevent jitter
    void LateUpdate() {
        UpdateCamera();
    }

    void UpdateCamera() {
        Quaternion rotation = Quaternion.Euler(pivot.eulerAngles.x, pivot.eulerAngles.y, 0f);
        transform.position = target.position - (rotation * offset);

        transform.LookAt(target);
    }
}
