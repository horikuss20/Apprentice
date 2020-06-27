using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGate : MonoBehaviour
{
	GameObject playerGO;
	bool increaseJump = false;
	float jumptimer;
	AudioSource gateSource;

    void Start()
    {
		gateSource = gameObject.GetComponent<AudioSource>();
		playerGO = GameObject.Find("PlayerFunctionality");
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && jumptimer == 0)
		{
			gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
			gateSource.Play();
			increaseJump = true;
			jumptimer = 5f;
		}
	}

	void Update()
    {
		jumptimer -= Time.deltaTime;
		jumptimer = Mathf.Clamp(jumptimer, 0, 5);

		if(jumptimer == 0)
		{
			gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
		}

		if (increaseJump)
		{
			playerGO.GetComponent<MovementController>().jumpNum++;
			increaseJump = false;
		}
	}
}
