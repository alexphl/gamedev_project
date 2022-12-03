using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float damage = 1f;
    public float velocity = 0.2f;
    public int timeToLive = 5;

    void Start() {
        StartCoroutine(DestroyTimer(timeToLive));
    }

    // Update is called once per frame
    void Update() {
        transform.position += transform.up * velocity * Time.deltaTime;
    }

    private IEnumerator DestroyTimer(int ttl)
    {
        yield return new WaitForSeconds(ttl);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider hit) {
        if(hit.transform.tag == "Enemy")
        {
            hit.transform.GetComponent<Enemy>().GetHit(damage);
        }
        else if (hit.transform.tag == "Player")
        {
            hit.transform.GetComponent<Player>().GetHit(damage);
        }
        else if (hit.transform.tag == "Boss")
        {
            hit.transform.GetComponent<Boss>().GetHit(damage);
        }
        else if (hit.transform.tag == "Boss2")
        {
            hit.transform.GetComponent<Boss2>().GetHit(damage);
        }
        else if (hit.transform.tag == "Boss3")
        {
            hit.transform.GetComponent<Boss3>().GetHit(damage);
        }

        if (hit.transform.tag != "Projectile") Destroy(gameObject);
    }
}
