using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple script to destroy explosion prefab use it in every explosion effect so that the prefab
// destroys itself after effect is played
public class DestroyGameObject : MonoBehaviour {
    public float timer = 4;

    void Start() {
        StartCoroutine(DestroyTimer());
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
