using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public CharacterController controller;
    public Transform player;
    public Animator animator;
    public Transform pivot;         // Used to decouple camera controls from player rotations
    public LayerMask groundMask;    // Used to disable aiming when hovering over HUD
    private Camera camera;

    public float speed = 3f;
    public float gravity = 9.8f;
    public float moveSpeed;
    public float rotationSpeed;

    private Vector3 moveHorizontal = Vector3.zero;
    private Vector3 moveVertical = Vector3.zero;

    void Start() {
        camera = Camera.main;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAtCursor();
    }

    private void Move()
    {
        moveHorizontal = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVertical.y = moveVertical.y < 0f ? -2f : moveVertical.y;
        moveVertical.y -= gravity * Time.deltaTime;

        if (moveHorizontal.magnitude > 0f)
        {
            controller.Move(moveHorizontal * speed * Time.deltaTime);
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
}
