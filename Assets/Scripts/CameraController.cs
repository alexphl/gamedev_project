using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target; // Usually the player
    public Transform pivot; // Used to decouple camera controls from player rotations
    public Vector3 offset; // Camera position relative to the player

    // Start is called before the first frame update
    void Start() {
        //Cursor.lockState = CursorLockMode.Locked;

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = null;
    }

    // Update is called once per frame
    void Update() {

    }

    // Final camera transforms should be called after Update to prevent jitter
    void LateUpdate() {
        UpdateCamera();
    }

    void UpdateCamera() {
        // We may want to implement camera rotation so I'm leaving this in
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);

        transform.position = target.position - (rotation * offset);

        transform.LookAt(target);
    }
}
