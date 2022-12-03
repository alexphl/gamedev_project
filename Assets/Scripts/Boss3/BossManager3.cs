using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager3 : MonoBehaviour
{
    public int phaseFlag;
    public GameObject shield;
  
    public Transform shootPoints, shootPoints1, shootPoints2, shootPoints3, shootPoints4;
    private Transform[] firePos;

    public Transform crystalManager;

    public Boss3 boss;
    public Player player;

    int index = 0;
    public float RotationSpeed;

    float cooldown, projectiles, spread;


    // Start is called before the first frame update
    void Start()
    {
        phaseFlag = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ShootPointsOn(shootPoints);
        ShootPointsOn(shootPoints1);
        ShootPointsOn(shootPoints2);
        ShootPointsOn(shootPoints3);
        ShootPointsOn(shootPoints4);

        if (phaseFlag == 1)
        {
            cooldown = 50f;
            projectiles = 2f;
            spread = 5f;
        }
        else if (phaseFlag == 2)
        {
            if (index == 0)
            {
                crystalManager.gameObject.SetActive(true);
                index = 1;
            }
            
            projectiles = 5f;
            spread = 10f;
        }
        else if (phaseFlag == 3) 
        {
            if (index == 1)
            {
                crystalManager.gameObject.SetActive(true);
                index = 0;
            }
            projectiles = 8f;
            spread = 15f;
        }

        if (player.deathFlag)
        {
            boss.gameObject.SetActive(false);
            boss.gameObject.SetActive(true);
            phaseFlag = 1;
            crystalManager.gameObject.SetActive(false);
            crystalManager.gameObject.SetActive(true);
            
            boss.SetHittable(true);
        }
        shield.SetActive(crystalManager.gameObject.activeSelf);
        boss.SetHittable(!crystalManager.gameObject.activeSelf);
        
    }

    public void ShootPointsOn(Transform sp)
    {
        foreach (Transform t in sp)
        {
            t.gameObject.GetComponent<ShootPoint>().Shoot(cooldown, projectiles, spread);
        }
    }
}
