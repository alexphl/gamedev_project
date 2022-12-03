using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public CharacterController controller;
    public Transform player;
    public Animator animator;
    public LayerMask groundMask;    // Used to disable aiming when hovering over HUD
    private Camera camera;

    public float speed = 3f;
    public float gravity = 9.8f;
    public float rotationSpeed;

    private bool canRoll = false;

    private Vector3 moveHorizontal = Vector3.zero;
    private Vector3 moveVertical = Vector3.zero;

    Player playerScript;

    void Start() {
        playerScript = gameObject.GetComponent<Player>();

        camera = Camera.main;
        controller = GetComponent<CharacterController>();
        canRoll = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAtCursor();

        if (Input.GetButtonDown("Roll") && canRoll) {
            StartCoroutine(DashRoll());
        }
    }

    private void Move()
    {
        moveHorizontal = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVertical.y = moveVertical.y < 0f ? -2f : moveVertical.y;
        moveVertical.y -= gravity * Time.deltaTime;

        if (moveHorizontal.magnitude > 0f)
        {
            controller.Move(moveHorizontal.normalized * speed * Time.deltaTime);
        }

        controller.Move(moveVertical * Time.deltaTime);
    }

    // Rotates player towards the mouse pointer
    private void LookAtCursor()
    {
        var (aimable, lookDir) = getLookDir();
        if (aimable) {transform.forward = lookDir;}
    }

    // "valid" may be false if the surface is not aimable (ie HUD) or if raycasting fails
    private (bool valid, Vector3 lookDir) getLookDir()
    {
        Vector3 lookDir = new Vector3();
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            lookDir = hitInfo.point - transform.position;
            lookDir.y = 0; // ignore y axis
            return (valid: true, lookDir: lookDir);
        }

        else {
            return (valid: false, lookDir: lookDir);
        }
    }

    private IEnumerator DashRoll() {
        // Set a cooldown for equipped qeapons
        for (int i = 0; i < transform.childCount - 2; i++) {
            transform.GetChild(i+2).gameObject.GetComponent<Weapon>().setCooldown(0.6f);
        }

        canRoll = false;
        playerScript.canBeHit = false;
        speed += 10;
        yield return new WaitForSeconds(1/6f);
        speed -= 16;
        playerScript.canBeHit = true;
        yield return new WaitForSeconds(1/3f);
        speed += 6;
        yield return new WaitForSeconds(3/2f);
        canRoll = true;
    }
}
