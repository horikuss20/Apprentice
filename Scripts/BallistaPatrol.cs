using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaPatrol : MonoBehaviour
{
    public float speed;
    public GameObject HealthUI;
    public Transform targetPos, startPos;
    bool towards = true;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        HealthUI = GameObject.Find("HealthUI");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if(targetPos != null)
        //if(startPos != null)
        if (towards)
        {
            transform.LookAt(targetPos.transform.position);

            transform.position += transform.forward * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, targetPos.position) < 1.0f)
            {
                towards = false;
            }
        }
        else
        {
            transform.LookAt(startPos.transform.position);
            transform.position += transform.forward * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, startPos.position) < 1.0f)
            {
                {
                    towards = true;
                }
            }
        }
    }
}
