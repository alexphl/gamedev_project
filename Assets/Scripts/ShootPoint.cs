using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPoint : MonoBehaviour
{
    public GameObject bulletModel;
    public float rateOfFire = 100f;

    private float startingCooldown = 50f;
    private float cooldown;
    private float currentCooldown;

    private void Start()
    {
        currentCooldown = startingCooldown;
    }

    public void Shoot(float cooldown)
    {
        
        currentCooldown -= rateOfFire * Time.deltaTime;
        // Muzzle 

        // sound

        // Projectiles
        if (currentCooldown <= 0) {
            Vector3 projectileTilt = new Vector3(transform.eulerAngles.x - 90, transform.eulerAngles.y, transform.eulerAngles.z);
            for (int i = 0; i < 1; i++)
                {
                    Instantiate(bulletModel, this.transform.position, Quaternion.Euler(projectileTilt));
                    projectileTilt.y = transform.eulerAngles.y;

                }
            currentCooldown = cooldown;
        }
    }

    public void Shoot(float cooldown, float numberOfProjectiles, float spread)
    {

        currentCooldown -= rateOfFire * Time.deltaTime;
        // Muzzle 

        // sound

        // Projectiles
        if (currentCooldown <= 0 && gameObject.activeInHierarchy)
        {
            Vector3 projectileTilt = new Vector3(transform.eulerAngles.x - 90, transform.eulerAngles.y, transform.eulerAngles.z);
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                Instantiate(bulletModel, this.transform.position, Quaternion.Euler(projectileTilt));
                projectileTilt.y = transform.eulerAngles.y;
                projectileTilt.y += Random.Range(-spread, spread);
            }
            currentCooldown = cooldown;
        }
    }
}
