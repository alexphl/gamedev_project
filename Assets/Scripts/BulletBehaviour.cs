using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float damage = 1f;
    public float velocity = 1f;
    public float timeToLive = 5f;

    private float raycastLength = 0.5f;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * velocity * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, raycastLength)) {
            Debug.Log(hit.transform.name);
            Destroy(gameObject);
        }

        timer += Time.deltaTime;
        if (timer >= timeToLive) {
            Destroy(gameObject);
        }
    }
}
