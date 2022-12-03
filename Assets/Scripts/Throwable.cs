using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {
    public GameObject detonationFX;
    public float damage = 80f;
    public int fuseTime = 3; // time before it detonates
    public float detonationRadius = 4f;
    public float detonationForce = 100f; // force applied to affected gameobjects from detonation

    void Start() {
        StartCoroutine(DetonateTimer());
    }

    private IEnumerator DetonateTimer()
    {
        yield return new WaitForSeconds(fuseTime);
        Detonate();
    }

    void Detonate() {
        Instantiate(detonationFX, transform.position, transform.rotation);
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, detonationRadius); // gets detonation affected gameobjects

        foreach (Collider affectedObject in affectedObjects) {
            if(affectedObject.transform.tag == "Enemy")
            {
                affectedObject.transform.GetComponent<Enemy>().GetHit(damage);
            }
            else if (affectedObject.transform.tag == "Player")
            {
                affectedObject.transform.GetComponent<Player>().GetHit(damage);
            }
            else if (affectedObject.transform.tag == "Boss")
            {
                affectedObject.transform.GetComponent<Boss>().GetHit(damage);
            }
            else if (affectedObject.transform.tag == "Boss2")
            {
                affectedObject.transform.GetComponent<Boss2>().GetHit(damage);
            }
            else if (affectedObject.transform.tag == "Boss3")
            {
                affectedObject.transform.GetComponent<Boss3>().GetHit(damage);
            }

            Rigidbody rb = affectedObject.GetComponent<Rigidbody>();
            if (rb) {
                rb.AddExplosionForce(detonationForce, transform.position, detonationRadius);
            }

            // damage objects - TODO
        }

        Destroy(gameObject);
    }
}
