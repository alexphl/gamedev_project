using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    public GameObject throwableModel;
    public float throwForce = 30f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Throwable();
    }

    void Throwable() {
        if (Input.GetButtonDown("Fire3")) {
            GameObject throwable = Instantiate(throwableModel, transform.position, transform.rotation);
            throwable.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * throwForce, ForceMode.VelocityChange);
        }
    }
}
