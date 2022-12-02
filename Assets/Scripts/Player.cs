using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float maxShield = 50f; // max shield value
    public float shieldRegenRate = 0.1f; // how much shield regenerates per frame
    public float timeBeforeRegen = 5f; // time before shield regen takes place

    public bool isDead = false; // public so it can easily accessible by other scripts

    private float health = 100f;
    private float shield; // actual player shield
    private float timer = 0f;

    public Transform spawn;
    public HUD_Controller playerHUD;

    public float invincibility = 500f;
    private bool canBeHit = true;

    private void Start()
    {
        if (!spawn) {
            GameObject parent = GameObject.Find("Spawns_Generated");
            if (!parent) {
                parent = new GameObject();
                parent.name = "Spawns_Generated";
            }

            spawn = new GameObject().transform;
            spawn.name = "PlayerSpawn";
            spawn.position = this.transform.position;
            spawn.parent = parent.transform;
        }

        Spawn();
    }

    void Update() {
        timer = timer > 0 ? timer - Time.deltaTime : 0;

        if (timer == 0 && shield > 0) {
            shield = shield < maxShield ? shield + shieldRegenRate : maxShield;
            playerHUD.SetShield(shield);
        }
    }

    public void GetHit(float damage)
    {
        if (canBeHit)
        {
            timer = timeBeforeRegen;
            if (shield > 0f) {
                shield -= damage;
            }
            else {
                health -= damage;
                playerHUD.SetHealth(health);

                if (health <= 0) StartCoroutine(Die());
            }

            StartCoroutine(Invincibility());
        }
    }

    private IEnumerator Invincibility()
    {
        canBeHit = false;
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(invincibility/1000);
        this.GetComponent<Renderer>().material.color = Color.green;
        canBeHit = true;
    }

    private IEnumerator Die()
    {
        //enemyAnimator.Stop();
        //enemyAnimator.Play("gothit");
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        Spawn();
    }

    private void Spawn()
    { 
        this.GetComponent<Renderer>().material.color = Color.green;
        playerHUD.SetMaxHealth(maxHealth);
        playerHUD.SetMaxShield(maxShield);
        health = maxHealth;
        shield = maxShield;
        transform.position = spawn.position;
    }

}

