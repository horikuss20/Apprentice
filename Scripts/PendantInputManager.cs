using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendantInputManager : MonoBehaviour
{
	PendantSystemNew psn;
	AudioSource pimSource;
	public AudioClip pimClip;
	GameObject pSelector;

    void Start()
    {
		psn = GameObject.Find("GameManager").GetComponent<PendantSystemNew>();
		pimSource = psn.GetComponentInChildren<AudioSource>();
		pSelector = GameObject.Find("PendantSelector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void KnightClick()
	{
		pimSource.PlayOneShot(psn.knightSwap);
		psn.KnightSelect();
		if (psn.playForge)
		{
			pSelector.GetComponent<PendantSelect>().canplay = true;
			pSelector.GetComponent<PendantSelect>().pTimer = .15f;
			pSelector.GetComponent<PendantSelect>().psAnim.Play("PendantForge");
			psn.playForge = false;
		}
	}

	public void TurtleClick()
	{
		pimSource.PlayOneShot(psn.turtleSwap);
		psn.TurtleSelect();
		if (psn.playForge)
		{
			pSelector.GetComponent<PendantSelect>().canplay = true;
			pSelector.GetComponent<PendantSelect>().pTimer = .15f;
			pSelector.GetComponent<PendantSelect>().psAnim.Play("PendantForge");
			psn.playForge = false;
		}
	}

	public void BearClick()
	{
		pimSource.PlayOneShot(psn.bearSwap);
		psn.BearSelect();
		if (psn.playForge)
		{
			pSelector.GetComponent<PendantSelect>().canplay = true;
			pSelector.GetComponent<PendantSelect>().pTimer = .15f;
			pSelector.GetComponent<PendantSelect>().psAnim.Play("PendantForge");
			psn.playForge = false;
		}
	}

	public void ShamrockClick()
	{
		pimSource.PlayOneShot(psn.shamrockSwap);
		psn.ShamrockSelect();
		if (psn.playForge)
		{
			pSelector.GetComponent<PendantSelect>().canplay = true;
			pSelector.GetComponent<PendantSelect>().pTimer = .15f;
			pSelector.GetComponent<PendantSelect>().psAnim.Play("PendantForge");
			psn.playForge = false;
		}
	}

	public void EagleClick()
	{
		pimSource.PlayOneShot(psn.eagleSwap);
		psn.EagleSelect();
		if (psn.playForge)
		{
			pSelector.GetComponent<PendantSelect>().canplay = true;
			pSelector.GetComponent<PendantSelect>().pTimer = .15f;
			pSelector.GetComponent<PendantSelect>().psAnim.Play("PendantForge");
			psn.playForge = false;
		}
	}

	public void GoddessClick()
	{
		pimSource.PlayOneShot(psn.goddessSwap);
		psn.GoddessSelect();
		if (psn.playForge)
		{
			pSelector.GetComponent<PendantSelect>().canplay = true;
			pSelector.GetComponent<PendantSelect>().pTimer = .15f;
			pSelector.GetComponent<PendantSelect>().psAnim.Play("PendantForge");
			psn.playForge = false;
		}
	}

	public void CastleClick()
	{
		pimSource.PlayOneShot(psn.castleSwap);
		psn.CastleSelect();
		if (psn.playForge)
		{
			pSelector.GetComponent<PendantSelect>().canplay = true;
			pSelector.GetComponent<PendantSelect>().pTimer = .15f;
			pSelector.GetComponent<PendantSelect>().psAnim.Play("PendantForge");
			psn.playForge = false;
		}
	}

	public void WizardClick()
	{
		pimSource.PlayOneShot(psn.wizardSwap);
		psn.WizardSelect();
		if (psn.playForge)
		{
			pSelector.GetComponent<PendantSelect>().canplay = true;
			pSelector.GetComponent<PendantSelect>().pTimer = .15f;
			pSelector.GetComponent<PendantSelect>().psAnim.Play("PendantForge");
			psn.playForge = false;
		}
	}
}
