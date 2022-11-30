using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float damage = 1f;
    public float velocity = 0.2f;
    public float timeToLive = 5f;

    private float raycastLength = 0.5f;
    private float timer = 0f;

    // Update is called once per frame
    void Update() {
        transform.position += transform.up * velocity * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, raycastLength)) {
            //Debug.Log(hit.transform.name);
            if(hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<Enemy>().GetHit();
            }
            else if (hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<Player>().GetHit();
            }
            // We've hit player, deduce health/shield
            if (hit.transform.gameObject.CompareTag("Player")) {
                HealthShield healthScript = hit.transform.gameObject.GetComponent<HealthShield>();

                if (healthScript) healthScript.DoDamage(damage);
            }
            Destroy(gameObject);
        }

        timer += Time.deltaTime;
        if (timer >= timeToLive) {
            Destroy(gameObject);
        }
    }
}
