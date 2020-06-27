using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    GameObject Player;
	bool canBreak = false;

	private void Start()
	{
		Player = GameObject.Find("PlayerFunctionality");
	}

	private void Update()
	{
		//canBreak = Player.GetComponent<MovementController>().isCharging;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && canBreak)
		{
			Destroy(gameObject);
		}
	}
}
