using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour {

    public Vector3 equippedOffset = Vector3.zero; // where the equiped weapon is going to be relative to player
    public Vector3 dequippedOffset = Vector3.zero; // where the other weapon is going to be relative to player
    public Vector3 dropOffset = Vector3.zero; // where the replaced weapon pickup is spawned relative to the player

    private List<GameObject> weaponInventory = new List<GameObject>();
    private int initialChildCount = 0;
    private int currentWeapon = 0;
    private int nextWeapon = 1;
    private bool inventoryUpdated = false;
    private bool isWeaponInventoryFull = false;

    private GameObject pickupParentContainer; // The parent game object for weapon drops

    // Start is called before the first frame update
    void Start() {
        // Find Weapon Pickups object container, create one if doesn't exist
        pickupParentContainer = GameObject.Find("Weapon Pickups");
        if (!pickupParentContainer) {
                pickupParentContainer = new GameObject();
                pickupParentContainer.name = "Weapon Pickups";
        }

        initialChildCount = transform.childCount;
        weaponInventory.Clear();
    }

    // Update is called once per frame
    void Update() {
        if (transform.childCount > initialChildCount) {
            UpdateWeaponsList();
        }

        if (!isWeaponInventoryFull && inventoryUpdated) {
            inventoryUpdated = false;

            switch (weaponInventory.Count) {
                case 1:
                    EquipWeapon(currentWeapon);
                    break;
                case 2:
                    isWeaponInventoryFull = true;
                    DequipWeapon(nextWeapon);
                    break;
            }
        }
        else if (inventoryUpdated) {
            inventoryUpdated = false;

            GameObject replacedWeapon = weaponInventory[currentWeapon];
            weaponInventory[2].transform.position = replacedWeapon.transform.position;
            weaponInventory[2].transform.rotation = replacedWeapon.transform.rotation;
            weaponInventory[2].GetComponent<Weapon>().isEquipped = true;
            weaponInventory.RemoveAt(currentWeapon);
            DropWeapon(replacedWeapon);
            Destroy(replacedWeapon);
            currentWeapon = 1;
            nextWeapon = 0;
        }

        if (weaponInventory.Count == 2) {
            if (Input.GetKeyDown("1")) {
                switch (currentWeapon) {
                    case 1:
                        SwitchWeapon();
                        break;
                }
            }
            else if (Input.GetKeyDown("2")) {
                switch (currentWeapon) {
                    case 0:
                        SwitchWeapon();
                        break;
                }
            }
        }

        initialChildCount = transform.childCount;
    }

    void UpdateWeaponsList() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.CompareTag("Weapon") && !weaponInventory.Contains(child)) {
                weaponInventory.Add(child);
                inventoryUpdated = true;
            }
        }
    }

    void SwitchWeapon() {
        Vector3 equippedPosition = weaponInventory[currentWeapon].transform.position;
        Quaternion equippedRotation = weaponInventory[currentWeapon].transform.rotation;
        int equippedIndex = currentWeapon;

        weaponInventory[currentWeapon].transform.position = weaponInventory[nextWeapon].transform.position;
        weaponInventory[currentWeapon].transform.rotation = weaponInventory[nextWeapon].transform.rotation;
        weaponInventory[currentWeapon].GetComponent<Weapon>().isEquipped = false;

        weaponInventory[nextWeapon].transform.position = equippedPosition;
        weaponInventory[nextWeapon].transform.rotation = equippedRotation;
        weaponInventory[nextWeapon].GetComponent<Weapon>().isEquipped = true;

        currentWeapon = nextWeapon;
        nextWeapon = equippedIndex;
    }


    // Sets position of equipped weapon
    void EquipWeapon(int weaponToEquip) {
        Vector3 equippedPosition = transform.right * equippedOffset.x + transform.up * equippedOffset.y + transform.forward * equippedOffset.z;

        weaponInventory[weaponToEquip].transform.position += equippedPosition;

        weaponInventory[weaponToEquip].transform.Rotate(0, 180, 0, Space.Self);

        weaponInventory[weaponToEquip].GetComponent<Weapon>().isEquipped = true;
    }

    // Sets position of other weapon
    void DequipWeapon(int weaponToDequip) {
        Vector3 dequippedPosition = transform.right * dequippedOffset.x + transform.up * dequippedOffset.y + transform.forward * dequippedOffset.z;

        weaponInventory[weaponToDequip].transform.position += dequippedPosition;

        weaponInventory[weaponToDequip].transform.Rotate(45, 90, 0, Space.Self);

        weaponInventory[weaponToDequip].GetComponent<Weapon>().isEquipped = false;
    }

    // Spawns a new weapon pickup
    void DropWeapon(GameObject weapon) {
        GameObject weaponDrop = Instantiate(weapon.GetComponent<Weapon>().droppedModel, transform.position + dropOffset, Quaternion.identity) as GameObject;
        weaponDrop.transform.parent = pickupParentContainer.transform;
    }
}