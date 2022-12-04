using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health = 100f;

    private int pursueDistance = 420;    // distance at which AI sees player
    private int minPlayerDistance = 75;  // how close the AI can get (smaller = closer)
    private int shootDistance; // Enemies begin shooting at half their pursuit distance
    private float moveSpeed;    // these are derived from speed
    private float moveShootSpeed;
    private Color ogColor; // used for damage effects, derived at runtime

    private bool isAlive;
    private bool pursueFlag;
    private bool idleFlag;
    private bool shootFlag;

    //private Animation enemyAnimator;
    public GameObject player;
    private Transform spawn;    // This was made optional. Set to public if needed.
    public Rigidbody body;
    public EnemyWeapon weapon;

    void Start()
    {
        if (!spawn) {
            GameObject parent = GameObject.Find("Spawns_Generated");
            if (!parent) {
                parent = new GameObject();
                parent.name = "Spawns_Generated";
            }

            spawn = new GameObject().transform;
            spawn.name = "EnemySpawn";
            spawn.position = this.transform.position;
            spawn.parent = parent.transform;
        }

        moveSpeed = speed;
        moveShootSpeed = moveSpeed / 2;

        body = gameObject.GetComponent<Rigidbody>();
        //enemyAnimator = gameObject.GetComponent<Animation>();
        player = GameObject.Find("Player");
        shootDistance = pursueDistance / 2;

        StartCoroutine(Spawn(0));

        this.GetComponent<Renderer>().material.color = Color.blue;
        ogColor = this.GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        if (pursueFlag && player)
        {
            Pursue();
        }
        else if (!pursueFlag)
        {
            GoHome();
        }
        else if (idleFlag)
        {
            Idle();
        }

        if (shootFlag && player)
        {
            speed = moveShootSpeed;
            weapon.StartShoot();
        }
        else
        {
            speed = moveSpeed;
            weapon.StopShoot();
        }
    }

    private void FixedUpdate()
    {
        if (isAlive) CheckForThreat();
    }

    private void CheckForThreat()
    {
        if (!player) {
            pursueFlag = false;
            shootFlag = false;
        }

        // Stop and shoot
        else if ((player.transform.position - this.transform.position).sqrMagnitude < minPlayerDistance)
        {
            pursueFlag = false;
            shootFlag = true;
            this.transform.LookAt(2 * transform.position - player.transform.position);
        }

        // Move and shoot
        else if ((player.transform.position - this.transform.position).sqrMagnitude < shootDistance)
        {
            pursueFlag = true;
            shootFlag = true;
            this.transform.LookAt(2 * transform.position - player.transform.position);
        }

        // Pursue without shooting
        else if ((player.transform.position - this.transform.position).sqrMagnitude < pursueDistance)
        {
            pursueFlag = true;
            shootFlag = false;
        }

        else
        {
            pursueFlag = false;
            shootFlag = false;
        }
    }

    private IEnumerator Spawn(float time)
    {
        yield return new WaitForSeconds(time);
        isAlive = true;
        idleFlag = true;
        this.transform.position = spawn.position;
        this.transform.rotation = spawn.rotation;
    }

    private void Idle()
    {
        //play idle animation
    }

    private void GoHome()
    {
        
    }

    private void Pursue()
    {
        MoveTo(player.transform);
    }

    public void Respawn()
    {
        StartCoroutine(Spawn(3));
    }

    //Logic to move towards player
    private void MoveTo(Transform destination)
    {
        if (!destination) return;

        Transform objectTransform = destination.transform;
        body.velocity = new Vector3(0, body.velocity.y, 0);
        float distance = speed * Time.deltaTime;
        Vector3 direction = new Vector3(objectTransform.position.x - transform.position.x, 0, objectTransform.position.z - transform.position.z);
        direction = direction.normalized;
        Vector3 movement = direction * distance;
        Vector3 newPosition = transform.position + movement;

        body.MovePosition(newPosition);
        body.MoveRotation(Quaternion.LookRotation(-movement));
    }

    public void GetHit(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            StartCoroutine(Die());
        }
        else
        {
            StartCoroutine(flashRed(1/6f));
            //enemyAnimator.Play("gothit");
        }
    }

    private IEnumerator flashRed(float duration)
    {
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(duration);
        this.GetComponent<Renderer>().material.color = ogColor;
    }

    private IEnumerator Die()
    {
        isAlive = false;
        idleFlag = false;
        pursueFlag = false;
        shootFlag = false;

        //enemyAnimator.Stop();
        //enemyAnimator.Play("gothit");
        StartCoroutine(flashRed(2f));

        yield return new WaitForSeconds(1/3f);
        this.gameObject.SetActive(false);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
