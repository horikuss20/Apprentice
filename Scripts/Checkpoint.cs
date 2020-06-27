using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointMaster cm;
    public GameObject ORB;
	private GameObject gm;

    void Start()
    {
        cm = GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>();
		gm = GameObject.Find("GameManager");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cm.lastCheckPointPos = transform.position;
            ORB.GetComponent<Renderer>().material.color = Color.green;
			gm.GetComponent<SaveInputManager>().SaveGame();
        }
    }
}
