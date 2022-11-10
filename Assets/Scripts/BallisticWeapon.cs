using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticWeapon : MonoBehaviour
{
    public GameObject bulletModel;
    public bool isAutomatic = false;
    public int numberOfProjectiles = 1;
    public float rateOfFire = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() {
        Instantiate(bulletModel, transform.position, transform.rotation);
    }
}
