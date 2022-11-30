using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {
    public GameObject detonationFX;
    public float damage = 1f;
    public float fuseTime = 3f; // time before it detonates
    public float detonationRadius = 4f;
    public float detonationForce = 100f; // force applied to affected gameobjects from detonation

    // Update is called once per frame
    void Update() {
        fuseTime -= Time.deltaTime;
        if (fuseTime <= 0f) {
            Detonate();
        }
    }

    void Detonate() {
        Instantiate(detonationFX, transform.position, transform.rotation);
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, detonationRadius); // gets detonation affected gameobjects

        foreach (Collider affectedObject in affectedObjects) {
            Rigidbody rb = affectedObject.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(detonationForce, transform.position, detonationRadius);
                if(rb.gameObject.tag == "Enemy")
                {
                    rb.gameObject.GetComponent<Enemy>().GetHit();
                }
            }

            // damage objects - TODO
        }

        Destroy(gameObject);
    }
}
