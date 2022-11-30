using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    int startingHealth;
    public GameObject spawn;

    public float invincibility = 500f;
    private bool canBeHit = true;

    private void Start()
    {
        startingHealth = health;
        Spawn();
        
    }

    public void GetHit()
    {
        if (canBeHit)
        {
            health--;
            StartCoroutine(Invincibility());
            if (health <= 0)
            {
                StartCoroutine(Die());
            }
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
        health = startingHealth;
        transform.position = spawn.transform.position;
        

    }

}

