using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    public GameObject weapon;

    // Start is called before the first frame update
    private void OnTriggerStay(Collider other) {
        if (other.transform.tag == "Player" && Input.GetButtonDown("Equip")) {
            gameObject.SetActive(false);
            foreach (Transform child in other.transform) {
                if (weapon == child.gameObject) {
                    Destroy(gameObject);
                    return;
                }
            }
            Instantiate(weapon, other.transform);
            Destroy(gameObject);
        }
    }
}
