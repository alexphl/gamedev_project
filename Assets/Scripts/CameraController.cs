using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target; // Usually the player
    public Vector3 offset; // Camera position relative to the player

    // Start is called before the first frame update
    void Start() {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {

    }

    // Final camera transforms should be called after Update to prevent jitter
    void LateUpdate() {
        UpdateCamera();
    }

    void UpdateCamera() {
        if (!target) return;

        transform.position = target.position - offset;

        transform.LookAt(target);
    }
}
