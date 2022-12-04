using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Used for levels where player needs to eliminate all enemies to proceed
public class Level_KillEnemies : MonoBehaviour
{
    public GameObject portal;
    public Transform enemies;

    void Start()
    {
        if (!portal) portal = GameObject.Find("Goal");
        if (!enemies) enemies = GameObject.Find("Enemies").transform;

        portal.SetActive(false);
    }

    void FixedUpdate()
    {
        if (enemies.childCount == 0) {
            portal.SetActive(true);
        }
    }
}
