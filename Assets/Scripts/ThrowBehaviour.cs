using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBehaviour : MonoBehaviour {

    public GameObject throwableModel;
    public int maxThrowables = 3; // the player can carry
    public float throwForceMultiplier = 1f;
    private Camera camera;

    private int numberOfThrowables; // the player has

    void Start() {
        camera = Camera.main;
        numberOfThrowables = maxThrowables;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire3") && numberOfThrowables > 0) {
            Vector3 mousePos = getMouseInfo().point;
            mousePos.y = 0;

            Vector3 playerPos = transform.position;
            playerPos.y = 0;

            float throwDist = Vector3.Distance(mousePos, playerPos);

            GameObject throwable = Instantiate(throwableModel, transform.position, transform.rotation);
            throwable.GetComponent<Rigidbody>().AddForce(transform.forward * throwDist * throwForceMultiplier, ForceMode.VelocityChange);
            numberOfThrowables--;
        }
    }

    // "valid" may be false if the surface is not aimable (ie HUD) or if raycasting fails
    private RaycastHit getMouseInfo()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out var hitInfo);
        return hitInfo;
    }
}
