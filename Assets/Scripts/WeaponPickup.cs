using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weapon;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
      
            Debug.Log("check");
            if (other.transform.tag == "Player")
            {
            gameObject.SetActive(false);
            foreach (Transform transform in other.transform)
                {
                    if (transform.CompareTag("Weapon"))
                    {
                    weapon.transform.SetParent(other.transform);
                    weapon.transform.position = transform.position;
                    weapon.transform.rotation = transform.rotation;
                    weapon.transform.localScale = transform.localScale;
                    weapon.isEquipped = true;
                    Destroy(transform.gameObject);
                    break;
                    }
                }
            


        }
        
    }
    
}
