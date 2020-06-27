using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOpener : MonoBehaviour
{
	GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
		gm = GameObject.Find("GameManager");
    }

	private void OnTriggerEnter(Collider other)
	{
		gm.GetComponent<SkillSystemMain>().canOpen = true;
	}

	private void OnTriggerExit(Collider other)
	{
		gm.GetComponent<SkillSystemMain>().canOpen = false;
	}
}
