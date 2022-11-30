using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletModel;
    public GameObject flashFX;
    public float muzzleOffset = 1f; // how much more in front is the muzzle relative to weapon
    public bool isAutomatic = false;
    public float rateOfFire = 100f;
    public float spread = 20f;
    public int numberOfProjectiles = 1; // projectiles to be fired TODO

    private float cooldown = 0f; // time to wait between shots
    private float rateFactor = 100f; // for scaling the rate of fire, numbers have to be 1, 10, 100, etc.
    private bool shootFlag = false;
    private bool slowFlag = false;

    // Update is called once per frame
    private void Update()
    {
        if (shootFlag)
        {
            
            if (isAutomatic)
            {
                Shoot();
            }
            else if (slowFlag == false)
            {
                Debug.Log("Shootin");
                slowFlag = true;
                StartCoroutine(SlowShoot());
            }

        }

    }

    public void Shoot()
    {
        Vector3 muzzlePosition = transform.position - transform.forward * muzzleOffset;

        // Muzzle flash
        Vector3 flashRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        Instantiate(flashFX, muzzlePosition, Quaternion.Euler(flashRotation));

        // sound

        // Projectiles
        Vector3 projectileTilt = new Vector3(transform.eulerAngles.x - 90, transform.eulerAngles.y, transform.eulerAngles.z);
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Instantiate(bulletModel, muzzlePosition, Quaternion.Euler(projectileTilt));
            projectileTilt.y = transform.eulerAngles.y;
            projectileTilt.y += Random.Range(-spread, spread);
        }

        

       
    }

    private IEnumerator SlowShoot()
    {
        
        while (shootFlag && slowFlag)
        {
            Shoot();
            yield return new WaitForSeconds(rateOfFire / 100 + 0.5f);
            Shoot();
        }
    }


    public void StartShoot()
    {
        shootFlag = true;
    }

    public void StopShoot()
    {
        shootFlag = false;
        slowFlag = false;
    }
}
