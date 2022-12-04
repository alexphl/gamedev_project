using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager1 : MonoBehaviour
{
    public int phaseFlag;
    public GameObject shield;
  
    public Transform shootPoints;
    private Transform[] firePos;

    public Transform crystalManager;
    public Transform crystalManager2;

    public Boss boss;
    public Player player;
    

    public float RotationSpeed;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        foreach (Transform t in shootPoints)
        {
            if (index > 3) t.gameObject.SetActive(false);
            index++;
        }
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(phaseFlag == 1)
        {
            PhaseOne(RotationSpeed, 50);
        }
        else if(phaseFlag == 2)
        {
            if (index == 0)
            {
                boss.SetHittable(false);
                crystalManager.gameObject.SetActive(true);
                index = -1;
            }
            PhaseTwo(RotationSpeed, 100f);
        }
        else if(phaseFlag == 3)
        {
            Debug.Log("Phase 3");
            if (index == -1)
            {
                index = 0;
                foreach (Transform t in shootPoints)
                {
                    if (index > 3) t.gameObject.SetActive(true);
                    index++;
                }
                index = 0;
            }
            PhaseOne(RotationSpeed * 4, 25f);
        }
        else if(phaseFlag == 4)
        {
            if (index == 0)
            {
                boss.SetHittable(false);
                crystalManager.gameObject.SetActive(true);
                crystalManager2.gameObject.SetActive(true);
                foreach (Transform t in crystalManager)
                {
                    t.gameObject.SetActive(true);
                    index++;
                }
                foreach (Transform t in crystalManager2)
                {
                    t.gameObject.SetActive(true);
                    index++;
                }
                index = -1;
            }
            PhaseFour(RotationSpeed * 4, 50f);
        }
        else if(phaseFlag == 5)
        {
            PhaseOne(RotationSpeed * 6, 25f);
        }
        else if(phaseFlag == 6)
        {
            if (index == -1)
            {
                boss.SetHittable(false);
                index = 0;
                crystalManager.gameObject.SetActive(true);
                crystalManager2.gameObject.SetActive(true);
                foreach (Transform t in crystalManager)
                {
                    t.gameObject.SetActive(true);
                    index++;
                }
                foreach (Transform t in crystalManager2)
                {
                    t.gameObject.SetActive(true);
                    index++;
                }
                index = 0;
            }
            PhaseFour(RotationSpeed*4, 25f);
        }

        if (player.deathFlag)
        {
            boss.gameObject.SetActive(false);
            boss.gameObject.SetActive(true);
            phaseFlag = 1;
            index = 0;
            crystalManager.gameObject.SetActive(false);
            crystalManager2.gameObject.SetActive(false);
            foreach (Transform t in shootPoints)
            {
                if (index > 3) t.gameObject.SetActive(false);
                index++;
            }
            index = 0;
            boss.SetHittable(true);
        }
        shield.SetActive(crystalManager.gameObject.activeSelf || crystalManager2.gameObject.activeSelf);
        boss.SetHittable(!crystalManager.gameObject.activeSelf && !crystalManager2.gameObject.activeSelf);
        
    }

    private void PhaseOne(float RotationSpeed, float cooldown)
    {
        //Shoot
        foreach(Transform t in shootPoints)
        { 
            if (t.gameObject.activeSelf) t.gameObject.GetComponent<ShootPoint>().Shoot(cooldown);
        }
        //Rotate
        transform.Rotate(Vector3.up * (RotationSpeed * Time.deltaTime));
        
    }

    private void PhaseTwo(float RotationSpeed, float cooldown)
    {
        foreach (Transform t in shootPoints)
        {
            if (t.gameObject.activeSelf) t.gameObject.GetComponent<ShootPoint>().Shoot(cooldown);
        }
        transform.Rotate(Vector3.up * (RotationSpeed * Time.deltaTime));
        
        
    }

    private void PhaseFour(float RotationSpeed, float cooldown)
    {
        foreach (Transform t in shootPoints)
        {
            if (t.gameObject.activeSelf) t.gameObject.GetComponent<ShootPoint>().Shoot(cooldown);
        }
        transform.Rotate(Vector3.up * (RotationSpeed * Time.deltaTime));
        
        
    }
}
