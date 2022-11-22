using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple script to destroy explosion prefab use it in every explosion effect so that the prefab
// destroys itself after effect is played
public class DestroyGameObject : MonoBehaviour {
    public float timer = 4f;

    // Update is called once per frame
    void Update() {
        if (timer <= 0) {
            Destroy(gameObject);
        }

        timer -= Time.deltaTime;
    }
}
