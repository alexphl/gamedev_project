using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool color = true;
    public float speed;
    private bool threatFlag = false;
    private bool moveFlag;
    //private Animation enemyAnimator;
    GameObject player;
    public GameObject spawn;
    Rigidbody body;
    private bool idleFlag;

    public int health = 1;
    private bool isAlive = true;
    private bool shootFlag;

    public EnemyWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
        //enemyAnimator = gameObject.GetComponent<Animation>();
        player = GameObject.Find("Player");
        StartCoroutine(Spawn(0));
        idleFlag = true;
        if (color) this.GetComponent<Renderer>().material.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        if (threatFlag && player)
        {
            Threaten();
        }
        else if (moveFlag)
        {
            GoHome();
        }
        else if (idleFlag)
        {
            Idle();
        }
        if (shootFlag)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        weapon.StartShoot();
    }

    private IEnumerator Spawn(float time)
    {

        yield return new WaitForSeconds(time);
        isAlive = true;
        idleFlag = true;
        this.transform.position = spawn.transform.position;
        this.transform.rotation = spawn.transform.rotation;

    }

    private void Idle()
    {
        //play idle animation
    }

    private void GoHome()
    {
        
    }

    private void Threaten()
    {
        MoveTo(player.transform);
    }

    public void Respawn()
    {

        StartCoroutine(Spawn(3));
    }

    private void FixedUpdate()
    {
        if (isAlive) CheckForThreat();
    }

    private void CheckForThreat()
    {
        if (!player) return;

        if ((player.transform.position - this.transform.position).sqrMagnitude < 10 * 10)
        {
            moveFlag = false;
            threatFlag = false;
            this.transform.LookAt(2 * transform.position - player.transform.position);
            shootFlag = true;
            
        }

        else if ((player.transform.position - this.transform.position).sqrMagnitude < 18 * 18)
        {
            threatFlag = true;
            moveFlag = true;
            
        }
        
        else
        {
            threatFlag = false;
            shootFlag = false;
            weapon.StopShoot();
        }
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

    public void GetHit()
    {
        health--;
        if (health == 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            //enemyAnimator.Play("gothit");
        }
    }

    private IEnumerator Die()
    {
        isAlive = false;
        idleFlag = false;
        threatFlag = false;
        moveFlag = false;
        shootFlag = false;
        //enemyAnimator.Stop();
        //enemyAnimator.Play("gothit");
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
