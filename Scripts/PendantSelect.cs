using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendantSelect : MonoBehaviour
{
	GameObject gm;
	bool canSwap;
	public float pTimer;
	public bool canplay;
	AudioSource psSource;
	public AudioClip forgeClip;
	public Animation psAnim;

    void Start()
    {
		gm = GameObject.Find("GameManager");
		psSource = gameObject.GetComponent<AudioSource>();
		psAnim = gameObject.GetComponent<Animation>();
    }

    void Update()
    {
		pTimer -= Time.deltaTime;
		pTimer = Mathf.Clamp(pTimer, 0, 1.5f);

		if(pTimer == 0)
		{
			if (canplay)
			{
				gameObject.GetComponentInChildren<ParticleSystem>().Play();
				psSource.PlayOneShot(forgeClip);
				canplay = false;
			}
		}

		if (canSwap && Input.GetKeyDown(KeyCode.F))
		{
			gm.GetComponent<PendantSystemNew>().pCanvas.SetActive(true);
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			canSwap = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			canSwap = false;
		}
	}
}
