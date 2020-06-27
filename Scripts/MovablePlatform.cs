using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    private GameObject lPoint;
    private GameObject rPoint;
    public float speed;
    private int direction = 1;
    private Vector3 movement;

    void Start()
    {
        lPoint = GameObject.Find("LPoint");
        rPoint = GameObject.Find("RPoint");
        speed = 0.5f;
    }

    void Update()
    {
        if (transform.position.x > rPoint.transform.position.x)
        {
            direction = -1;
        }
        else if (transform.position.x <= lPoint.transform.position.x)
        {
            direction = 1;
        }
        movement = Vector3.right * direction * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}