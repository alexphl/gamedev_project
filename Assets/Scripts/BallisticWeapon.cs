using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticWeapon : MonoBehaviour
{
    public float damage = 1f;
    public float range = 10f;
    public int numberOfProjectiles = 1;
    public bool isAutomatic = false;
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, range)) {
            Debug.Log(hit.transform.name);
        }
    }
}
