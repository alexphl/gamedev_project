using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthShield : MonoBehaviour {

    public float health = 100f;
    public float maxShield = 1f; // max shield value
    public float shieldRegenRate = 0.1f; // how much shield regenerates per frame
    public float timeBeforeRegen = 5f; // time before shield regen takes place

    public bool isDead = false; // public so it can easily accessible by other scripts

    private float shield; // actual player shield
    private float timer = 0f;

    // Start is called before the first frame update
    void Start() {
        shield = maxShield;
    }

    // Update is called once per frame
    void Update() {
        // player is dead
        if (health <= 0) {
            isDead = true;
            health = 0f;
            return;
        }

        timer = timer > 0 ? timer - Time.deltaTime : 0;

        if (timer == 0) {
            shield = shield < maxShield ? shield + shieldRegenRate : maxShield;
        }
    }

    public void DoDamage(float damage) {
        timer = timeBeforeRegen;
        if (shield >= maxShield) {
            shield -= maxShield;
        }
        else {
            health -= damage;
        }
    }
}
