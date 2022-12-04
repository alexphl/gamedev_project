using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform movementPoints;

    public Boss2 bossModel;
    private Rigidbody body;

    private bool helperFlag = false;
    private bool movedFlag = false;

    private bool nearFlag = false;

    public Player player;

    private float speed = 10f;

    public int index = 0;

    private void Start()
    {
        body = bossModel.GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        
        transform.LookAt(player.transform);
        MoveTo(PickDestination());
    }

    private Transform PickDestination()
    {
        int i = 0;
        foreach (Transform t in movementPoints)
        {
            if (i == index) return t;
            i++;
        }
        index = 0;
        return PickDestination();
    }

    private void MoveTo(Transform destination)
    {
        if (!destination) return;
        Transform objectTransform = destination.transform;
        body.velocity = new Vector3(0, body.velocity.y, 0);
        float distance = speed * Time.deltaTime;
        Vector3 direction = new Vector3(objectTransform.position.x - transform.position.x, 0, objectTransform.position.z - transform.position.z);
        direction = direction.normalized;
        Vector3 movement = direction * distance;
        Vector3 newPosition = transform.position + movement;

        body.MovePosition(newPosition);


        if (Vector3.Distance(transform.position, destination.position) < 0.1 && !nearFlag)
        {
            index++;
        }
        else
        {
            nearFlag = false;
        }
    }
    public bool ChangePhase(Transform movementPoints, Transform helperPoints, int helperSize, int index)
    {
        if (movedFlag)
        {
            SetMovementPoints(movementPoints, 100);
            return true;
        }
        if (!helperFlag)
        {
            this.index = index;
            
            helperFlag = true;
        }
        if (helperFlag)
        {
            movedFlag = SetMovementPoints(helperPoints, helperSize);
        }
        
        return false;
    }

    public bool SetMovementPoints(Transform movementPoints, int size)
    {

        this.movementPoints = movementPoints;
        if (index >= size)
        {
            index = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FlagReset()
    {
        movedFlag = false;
        helperFlag = false;
    }
}
