using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public CharacterController controller;

    public float speed = 3f;
    public float gravity = 9.8f;

    private Vector3 moveHorizontal = Vector3.zero;
    private Vector3 moveVertical = Vector3.zero;

    // Update is called once per frame
    void Update() {
        move();
    }

    void move() {
        moveHorizontal = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVertical.y = moveVertical.y < 0f ? -2f : moveVertical.y;
        moveVertical.y -= gravity * Time.deltaTime;

        if (moveHorizontal.magnitude > 0f) {
            //rotatePlayer();
            controller.Move(moveHorizontal * speed * Time.deltaTime);
        }

        controller.Move(moveVertical * Time.deltaTime);
    }
}
