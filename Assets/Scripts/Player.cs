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

    private bool isDead = false;

    private float health = 100f;
    private float shield; // actual player shield
    private float timer = 0f;

    public Transform spawn;
    public HUD_Controller playerHUD;

    public int respawnTimer = 0;
    public float invincibility = 500f;
    public bool canBeHit = true;

    private void Start()
    {
        if (!spawn)
        {
            GameObject parent = GameObject.Find("Spawns_Generated");
            if (!parent)
            {
                parent = new GameObject();
                parent.name = "Spawns_Generated";
            }

            spawn = new GameObject().transform;
            spawn.name = "PlayerSpawn";
            spawn.position = this.transform.position;
            spawn.parent = parent.transform;
        }

        if (!playerHUD) playerHUD = GameObject.Find("HUD Overlay").GetComponent<HUD_Controller>();
        StartCoroutine(Spawn(0));
    }

    void Update()
    {
        timer = timer > 0 ? timer - Time.deltaTime : 0;

        if (timer == 0 && shield > 0)
        {
            shield = shield < maxShield ? shield + shieldRegenRate : maxShield;
            playerHUD.SetShield(shield);
        }
    }

    public void GetHit(float damage)
    {
        if (canBeHit)
        {
            timer = timeBeforeRegen;
            if (shield > 0f)
            {
                shield -= damage;
                playerHUD.SetShield(shield);
            }
            else
            {
                health -= damage;
                playerHUD.SetHealth(health);

                if (health <= 0) Die();
            }

            StartCoroutine(Invincibility());
        }
    }

    private IEnumerator Invincibility()
    {
        canBeHit = false;
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(invincibility / 1000);
        this.GetComponent<Renderer>().material.color = Color.green;
        canBeHit = true;
    }

    public void Die()
    {
        isDead = true;
        StartCoroutine(Spawn(respawnTimer));
        StartCoroutine(playerHUD.ShowDeathScreen(3));
    }

    private IEnumerator Spawn(int timer)
    {
        yield return new WaitForSeconds(timer);
        isDead = false;

        playerHUD.SetMaxHealth(maxHealth);
        playerHUD.SetMaxShield(maxShield);

        health = maxHealth;
        shield = maxShield;

        this.GetComponent<Renderer>().material.color = Color.green;
        transform.position = spawn.position;
    }
}
