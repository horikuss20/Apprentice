using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WaterTrigger : MonoBehaviour
{
    //BoxCollider for the water GameObject
    BoxCollider boxCollider;
    //CharacterController for the character
    CharacterController controller;
    //Vector for the top and right side of the collider
    Vector3 boundsMax;
    //Vector for the bottom and left side of the collider
    Vector3 boundsMin;
    //Vector for the player's position once in the trigger
    Vector3 playerPos;
    //The vector to offset the player's position once in the trigger
    Vector3 offset;
    //The length of the dolphin to push into the water
    float dolphinLengthOffset;

    private void Awake()
    {
        //Find the BoxCollider of the dolphin to know how much to offset the position
        //bounds.extents gets half the size of the Bounds, so multiply it by 2 to get the
        //true length of the dolphin
        dolphinLengthOffset = GameObject.Find("DolphinFunctionality").
            GetComponent<BoxCollider>().bounds.extents.z * 2;

        //Find the CharacterController in order to move the player
        controller = GameObject.Find("PlayerFunctionality").
            GetComponent<CharacterController>();
    }
    void Start()
    {
        //Get the BoxCollider from the GameObject and set the max and min 
        //bounds in world space
        boxCollider = GetComponent<BoxCollider>();
        boundsMax = boxCollider.bounds.max;
        boundsMin = boxCollider.bounds.min;
    }
    private void OnTriggerEnter(Collider other)
    {   //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        //  Set playerPos, determine what side the player enters the trigger from,  //
        // set the offset depending on what side the player enters the trigger, and //
        //       use controller to move the player completely into the water        //
        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
       
        if (other.tag == "Player")
        { 
            //Set playerPos
            playerPos = other.transform.position;

            //if the player enters the trigger from the top
            if (playerPos.y >= boundsMax.y)
            {
                //set the offset's y to the top part of the trigger, adding the dolphin's length
                offset = new Vector3(playerPos.x, boundsMax.y + dolphinLengthOffset, playerPos.z);

                //move the player based on the offset calculated
                controller.Move(playerPos - offset);

                //since the player is entering from the top, rotate it to look down
                other.transform.rotation = Quaternion.LookRotation(Vector3.down);
            }

            //if the player enters the trigger from the bottom
            if (playerPos.y <= boundsMin.y)
            {
                //set the offset's y to the bottom part of the trigger, subtracting the dolphin's length
                offset = new Vector3(playerPos.x, boundsMin.y - dolphinLengthOffset, playerPos.z);
                
                //move the player based on the offset calculated
                controller.Move(playerPos - offset);

                //since the player is entering from the bottom, rotate it to look up
                other.transform.rotation = Quaternion.LookRotation(Vector3.up);
            }

            //if the player enters the trigger from the left
            if (playerPos.x <= boundsMin.x)
            {
                //set the offset's x to the left part of the trigger, subtracting the dolphin's length
                offset = new Vector3(boundsMin.x - dolphinLengthOffset, playerPos.y, playerPos.z);

                //move the player based on the offset calculated
                controller.Move(playerPos - offset);
            }

            //if the player enters the trigger from the right
            if (playerPos.x >= boundsMax.x)
            {
                //set the offset's x to the right part of the trigger, adding the dolphin's length
                offset = new Vector3(boundsMax.x + dolphinLengthOffset, playerPos.y, playerPos.z);
                
                //move the player based on the offset calculated
                controller.Move(playerPos - offset);
            }
        }
    }
}
