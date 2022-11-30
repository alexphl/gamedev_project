using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour {

    public Vector3 equippedPosition = Vector3.zero; // where the equiped weapon is going to be relative to player

    private List<GameObject> weaponInventory = new List<GameObject>();
    private int initialChildCount = 0;
    private int weaponToEquip = 0;
    private bool inventoryUpdated = false;

    // Start is called before the first frame update
    void Start() {
        initialChildCount = transform.childCount - 1;
    }

    // Update is called once per frame
    void Update() {
        if (transform.childCount > initialChildCount) {
            UpdateWeaponsList();
            initialChildCount = transform.childCount;
        }

        if (inventoryUpdated && weaponInventory.Count > 0) {
            EquipWeapon();
            inventoryUpdated = false;
        }
    }

    void UpdateWeaponsList() {
        weaponInventory.Clear();
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.CompareTag("Weapon")) {
                weaponInventory.Add(child);
            }
        }
        inventoryUpdated = true;
    }

    void EquipWeapon() {
        equippedPosition = transform.right * equippedPosition.x + transform.up * equippedPosition.y + transform.forward * equippedPosition.z;
        weaponInventory[weaponToEquip].transform.position += equippedPosition;
        weaponInventory[weaponToEquip].transform.Rotate(0, 180, 0, Space.Self);
        // set isEquipped in Weapon
    }
}
