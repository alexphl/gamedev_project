using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticWeapon : MonoBehaviour
{
    public GameObject bulletModel;
    public bool isAutomatic = false;
    public int numberOfProjectiles = 1;
    public float rateOfFire = 100f;

    private float cooldown = 0f; // time to wait between shots
    private float rateFactor = 100f; // for scaling the rate of fire, numbers have to be 1, 10, 100, etc.

    // Update is called once per frame
    void Update() {
        switch (isAutomatic) {
            case true:
                if (Input.GetButton("Fire1") && cooldown <= 0f) {
                    cooldown = rateFactor / rateOfFire;
                    Shoot();
                }
                break;
            case false:
                if (Input.GetButtonDown("Fire1") && cooldown <= 0f) {
                    cooldown = rateFactor / rateOfFire;
                    Shoot();
                }
                break;
        }

        cooldown = cooldown > 0f ? cooldown - Time.deltaTime : 0f;
    }

    void Shoot() {
        Instantiate(bulletModel, transform.position, transform.rotation);
    }
}
