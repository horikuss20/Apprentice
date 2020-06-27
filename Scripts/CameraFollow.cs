using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Transform PlayerTrans;
    public GameObject GameManager;
    public float speed = 0.125f;
    public Vector3 offSet;
    public float scrollSpeed;
    float offsetTimer;
    float panSpeed = 10.0f;


    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
        offSet.x = 0.0f;
        offSet.y = 0.0f;
        offSet.z = -20.0f;
        offsetTimer = 0.5f;
    }

	private void Update()
	{
		if(PlayerTrans == null)
		{
			if (GameObject.FindGameObjectWithTag("Player"))
			{
				PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		if(PlayerTrans != null)
        FollowCam();
    }

    void FollowCam()
    {
        Vector3 desiredPosition = PlayerTrans.position + offSet;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
        transform.position = smoothedPosition;

        //if (Input.GetKey(KeyCode.A))
        //{
        //    offsetTimer -= Time.deltaTime;
        //    if(offsetTimer <= 0)
        //    {
        //        offSet.x = -5.0f;
        //    }
        //}
        //if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S))
        //{
        //    offsetTimer = 0.5f;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    offsetTimer -= Time.deltaTime;
        //    if (offsetTimer <= 0)
        //    {
        //        offSet.x = 5.0f;
        //        offsetTimer = 0.5f;
        //    }
        //}
    }
}
