using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystemInputManager : MonoBehaviour
{
	GameObject gm;

	private void Start()
	{
		gm = GameObject.Find("GameManager");
	}

	public void OpenUI()
	{
		gm.GetComponent<SkillSystemNew>().OpenUI();
	}
}
