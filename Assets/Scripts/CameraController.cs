using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target; // Usually the player
    public Transform targetHead;
    public Transform pivot; // Used to decouple camera controls from player rotations
    public Vector3 offset; // Camera position relative to the player
    public float xSensitivity, ySensitivity;
    public float maxViewAngle, minViewAngle;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = null;
    }

    // Update is called once per frame
    void Update() {
        // Handle inputs
        float horizontal = Input.GetAxis("Mouse X") * xSensitivity;
        float vertical = Input.GetAxis("Mouse Y") * ySensitivity;

        // Rotate camera pivot
        pivot.Rotate(-vertical, horizontal, 0);
        //targetHead.Rotate(0, vertical, 0); // This rotates player's head to match the camera, but Animator overrides it.

        // Handle min and max vertical view angles to prevent camera flip and other weirdness.
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f) {
            pivot.rotation = Quaternion.Euler(maxViewAngle, pivot.eulerAngles.y, 0);
        } else if (pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 360f + minViewAngle) {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, pivot.eulerAngles.y, 0);
        }

        // Fix for pivot rotating on the Z axis when nobody asked it to.
        pivot.rotation = Quaternion.Euler(pivot.eulerAngles.x, pivot.eulerAngles.y, 0);
    }

    // Called after Update to prevent jitter
    void LateUpdate() {
        Quaternion rotation = Quaternion.Euler(pivot.eulerAngles.x, pivot.eulerAngles.y, 0f);
        transform.position = target.position - (rotation * offset);

        // Avoid below-ground camera clipping
        if (transform.position.y < (target.position.y + 0.5f)) {
            transform.position = new Vector3(transform.position.x, target.position.y + 0.5f, transform.position.z);
        }

        transform.LookAt(target);
    }
}
