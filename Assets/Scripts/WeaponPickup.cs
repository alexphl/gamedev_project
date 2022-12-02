using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    public GameObject weapon;
    public HUD_Controller playerHUD;

    void Start() {
        if (!playerHUD) playerHUD = GameObject.Find("HUD Overlay").GetComponent<HUD_Controller>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player") {
            playerHUD.showTextMessage("E to Equip " + weapon.name);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.tag == "Player") {
            playerHUD.hideTextMessage();
        }
    }

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
            playerHUD.hideTextMessage();
        }
    }
}
