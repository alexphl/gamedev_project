using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletModel;
    public bool isAutomatic = false;
    public float rateOfFire = 100f;
    public float spread = 20f;
    public int numberOfProjectiles = 1; // projectiles to be fired TODO

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
        // muzzle flash
        // sound
        Vector3 projectileTilt = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        for (int i = 0; i < numberOfProjectiles; i++) {
            Instantiate(bulletModel, transform.position, Quaternion.Euler(projectileTilt));
            projectileTilt.y = transform.eulerAngles.y;
            projectileTilt.y += Random.Range(-spread, spread);
        }
    }
}
