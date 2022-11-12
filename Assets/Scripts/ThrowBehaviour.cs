using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBehaviour : MonoBehaviour {

    public GameObject throwableModel;
    public int maxThrowables = 3; // the player can carry
    public float throwForce = 5f;

    private int numberOfThrowables; // the player has

    void Start() {
        numberOfThrowables = maxThrowables;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire3") && numberOfThrowables > 0) {
            GameObject throwable = Instantiate(throwableModel, transform.position, transform.rotation);
            throwable.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
            numberOfThrowables--;
        }
    }
}
