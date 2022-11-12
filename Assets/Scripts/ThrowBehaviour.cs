using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBehaviour : MonoBehaviour {

    public GameObject throwableModel;
    public float throwForce = 5f;

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire3")) {
            GameObject throwable = Instantiate(throwableModel, transform.position, transform.rotation);
            throwable.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        }
    }
}
