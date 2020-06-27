using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRope : MonoBehaviour
{
    public GameObject ropeBottom;
    float radius;
    public GameObject monkey;
    public LayerMask layer;
    float speed;
    bool inRange;
    float resetTimer;
    bool ropeCheck;

    // Start is called before the first frame update
    void Start()
    {
        #region Setting Variables
        radius = 1f;
        ropeBottom = GameObject.Find("Part9");
        speed = 100f;
        ropeCheck = true;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        //since monkey is inactive at start, the script needs to be constantly looking for monkey in order to assign it to this code
        monkey = GameObject.Find("Monkey");

        #region Setting Timer
        //initializing the resetTimer for the rope so it will allow players to grab the rope multiple times
        resetTimer -= Time.deltaTime;
        resetTimer = Mathf.Clamp(resetTimer, 0, 10);

        if (resetTimer <= .2) //if the resetTimer falls below .2 seconds,
        {
            resetTimer = 2f;
            ropeCheck = true;
        }
        #endregion

        #region Swinging Functionality
        if (ropeCheck == true)
        {
            if (Physics.CheckSphere(transform.position, radius, layer, QueryTriggerInteraction.Ignore)) //this is checking to see if the Monkey is in that specific trigger
            {
                inRange = true;
            }
            else
            {
                inRange = false;
            }

            if (inRange == true) //if the monkey is range of the SphereCheck, it'll become a child of the bottom part of the rope and switches controls to the rope and disables the monkey controller
            {
                monkey.transform.parent = ropeBottom.transform;
                monkey.transform.localPosition = Vector3.zero;
                monkey.GetComponent<MonkeyController>().enabled = false;
                monkey.GetComponent<Rigidbody>().useGravity = false;

                float Horizontal = Input.GetAxis("Horizontal") * speed;

                ropeBottom.GetComponent<Rigidbody>().AddForce(transform.right * Horizontal, ForceMode.Acceleration);

                if (Input.GetKeyDown(KeyCode.Space)) //if you press space while you're attached to the rope, you'll detach and maintain your speed from the rope
                {
                    ropeCheck = false;
                    inRange = false;
                    resetTimer = 2f;
                    monkey.transform.parent = null;
                    monkey.GetComponent<MonkeyController>().enabled = true;
                    monkey.GetComponent<Rigidbody>().useGravity = true;
                }
            }
        }
        #endregion
    }
}
