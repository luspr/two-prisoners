using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSlide : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public float moveSpeed; //how fast do the doors move
    public float moveDistance; //how far do the doors move
    public float waitTime; //how long before door closes

    private float timer;
    private bool closeFlag;
    // TODO state should be a enumeration.
    private int state; //0 = closed, 1 = opening; 2 = open; 3 = waiting to close; 4 = closing;
    private Vector3 openDirection;    //assumption: doors move away from each other
    private Vector3 leftOpen;
    private Vector3 rightOpen;    //positions of the open doors
    private Vector3 leftClosed;
    private Vector3 rightClosed;    //positions of the closed doors


    void Start()
    {
        //initial calculations
        openDirection = leftDoor.position - rightDoor.position; //direction vector pointing from right to left door
        openDirection.Normalize();
        openDirection *= moveDistance;


        leftOpen = leftDoor.position + openDirection;   //calculate positions for open doors - doors moving in opposite directions of each other
        rightOpen = rightDoor.position - openDirection;
        leftClosed = leftDoor.position;   //positions for closed door
        rightClosed = rightDoor.position;
    }

    void OnTriggerEnter(Collider col)
    {
        switch (state)
        {
            case 0:
                state = 1;      //closed-->opening
                timer = 0;
                break;

            case 3:
                state = 2;      //waíting for close --> open
                timer = 0;
                break;

            case 4:
                state = 1;      //closing --> opening
                timer = (1 - (timer * moveSpeed)) / moveSpeed;
                break;
        }
    }

    void OnTriggerExit(Collider col)
    {
        switch (state)
        {

            case 1:
                closeFlag = true;   //for when player exits trigger zone before door could fully open
                break;

            case 2:
                state = 3;      //closed-->opening
                timer = 0;
                break;

        }
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        switch (state)
        {
            case 1:
                leftDoor.position = leftClosed + (leftOpen - leftClosed) * Mathf.Min(timer * moveSpeed, 1);    //move until doors are open
                rightDoor.position = rightClosed + (rightOpen - rightClosed) * Mathf.Min(timer * moveSpeed, 1);
                if ((timer * moveSpeed) >= 1)
                {
                    state = 2;  //doors are open
                    timer = 0;
                }
                break;

            case 2:
                if (closeFlag)
                {
                    state = 3;
                    closeFlag = false;
                }
                break;

            case 3:
                if (timer >= waitTime)
                {
                    state = 4;   //start closing as timer expires
                    timer = 0;
                }
                break;

            case 4:
                leftDoor.position = leftOpen + (leftClosed - leftOpen) * Mathf.Min(timer * moveSpeed, 1);    //move until doors are closed
                rightDoor.position = rightOpen + (rightClosed - rightOpen) * Mathf.Min(timer * moveSpeed, 1);
                if ((timer * moveSpeed) >= 1)
                {
                    state = 0;  //doors are closed
                    timer = 0;
                }
                break;

        }

    }
}
