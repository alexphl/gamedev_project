using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager2 : MonoBehaviour
{
    public Transform shootPoints, shootPoints2, shootPoints3, shootPoints4;
    public GameObject shooter;
    public BossMovement bossM;
    public int phaseFlag;
    

    public Boss2 boss;
    public Player player;

    public Transform spawn;

    public GameObject door, door1, door2, door3;
    public Transform movementPoints1, movementPoints2, movementPoints3, movementPoints4;
    public Transform helperPoints1, helperPoints2,helperPoints3;

    public Transform cover;
    // Start is called before the first frame update
    void Start()
    {
        phaseFlag = 1;
        
        bossM.SetMovementPoints(movementPoints1, 100);
    }

    private void OnDisable()
    {
        door3.SetActive(false);
    }

    private void Update()
    {
        if(phaseFlag == 1)
        {
            shooter.GetComponent<ShootPoint>().Shoot(50f);
        }
        else if(phaseFlag == 2)
        {
            shooter.GetComponent<ShootPoint>().Shoot(50f);
            ShootPointsOn(shootPoints);
            
        }
        else if(phaseFlag == 3)
        {
            boss.SetHittable(false);
            if(bossM.ChangePhase(movementPoints2, helperPoints1, 2, 1))
            {
                boss.SetHittable(true);
            }
            
            door.SetActive(false);
            shooter.GetComponent<ShootPoint>().Shoot(50f, 2f, 50f);
        }
        else if(phaseFlag == 4)
        {
            bossM.FlagReset();
            ShootPointsOn(shootPoints2);
            shooter.GetComponent<ShootPoint>().Shoot(25f, 2f, 50f);
        }
        else if(phaseFlag == 5)
        {
            boss.SetHittable(false);
            if (bossM.ChangePhase(movementPoints3, helperPoints2, 3, (int) Mathf.Ceil((bossM.index+1)/4 )))
            {
                boss.SetHittable(true);
            }
            door1.SetActive(false);
            shooter.GetComponent<ShootPoint>().Shoot(25f, 3f, 10f);
        }
        else if (phaseFlag == 6)
        {
            shooter.GetComponent<ShootPoint>().Shoot(25f, 3f, 10f);
            bossM.FlagReset();
            ShootPointsOn(shootPoints3);
        }
        else if (phaseFlag == 7)
        {
            foreach (Transform t in cover)
            {
                t.gameObject.SetActive(false);
            }
            foreach (Transform t in shootPoints3)
            {
                t.gameObject.SetActive(false);
            }
            shooter.GetComponent<ShootPoint>().Shoot(25f, 5f, 115f);
            door2.SetActive(false);
            boss.SetHittable(false);
            if (bossM.ChangePhase(movementPoints4, helperPoints3, 6, (int)Mathf.Ceil((bossM.index % 10 - 1)/4 )))
            {
                boss.SetHittable(true);
            }
        }
        else if (phaseFlag == 8)
        {
            shooter.GetComponent<ShootPoint>().Shoot(25f, 5f, 115f);
            ShootPointsOn(shootPoints4);
        }

        if (player.deathFlag)
        {
            boss.gameObject.SetActive(false);
            boss.gameObject.SetActive(true);
            phaseFlag = 1;
            bossM.SetMovementPoints(movementPoints1, 100);
            bossM.FlagReset();
            boss.transform.position = spawn.position;
            bossM.index = 0;
        }

    }

    private void ShootPointsOn(Transform shootPoints)
    {
        foreach (Transform t in shootPoints)
        {
            t.gameObject.GetComponent<ShootPoint>().Shoot(25f);
        }
    }

    // Update is called once per frame

}
