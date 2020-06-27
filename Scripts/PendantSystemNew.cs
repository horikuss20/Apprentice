using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PendantSystemNew : MonoBehaviour
{ 
	GameObject playerGO;
	public GameObject pCanvas;
	GameObject pKnight;
	GameObject pDragon;
	GameObject pBear;
	GameObject pShamrock;
	GameObject pEagle;
	GameObject pCastle;
	GameObject pWizard;
	GameObject pGoddess;
	GameObject pKnight2;
	GameObject pDragon2;
	GameObject pBear2;
	GameObject pShamrock2;
	GameObject pEagle2;
	GameObject pCastle2;
	GameObject pWizard2;
	GameObject pGoddess2;

	MagicSystem msRef;
	public bool pendantChoice;
	Text selectText;
	bool canplay;
	Health playerHealth;
	public bool playForge;

	public AudioClip knightSwap;
	public AudioClip turtleSwap;
	public AudioClip bearSwap;
	public AudioClip shamrockSwap;
	public AudioClip goddessSwap;
	public AudioClip eagleSwap;
	public AudioClip wizardSwap;
	public AudioClip castleSwap;
	public AudioClip goddessProc;

	//PENDANT CONTROL
	public string pCurSide;
	public string pSide1;
	public string pSide2;
	public string Shamrock = "Shamrock";
	public string Bear = "Bear";
	public string Dragon = "Dragon";
	public string Knight = "Knight";
	public string Goddess = "Goddess";
	public string Eagle = "Eagle";
	public string Wizard = "Wizard";
	public string Castle = "Castle";
	GameObject knightAOE;
	public int gNum;


	void Start()
	{
		gNum = 0;
		pendantChoice = true;
		if (playerGO == null && GameObject.Find("PlayerFunctionality"))
		{
			playerGO = GameObject.FindGameObjectWithTag("Player");
			msRef = GameObject.Find("MagicUI").GetComponent<MagicSystem>();
			knightAOE = GameObject.FindGameObjectWithTag("KnightPE");
			Debug.Log(knightAOE);
		}

		if (GameObject.Find("PendantCanvas") && pCanvas == null)
		{
			pCanvas = GameObject.Find("PendantCanvas");
			selectText = GameObject.Find("SelectionBG").GetComponentInChildren<Text>();
			pCanvas.SetActive(false);
			pKnight = GameObject.Find("KnightImage");
			pDragon = GameObject.Find("DragonImage");
			pBear = GameObject.Find("BearImage");
			pShamrock = GameObject.Find("ShamrockImage");
			pCastle = GameObject.Find("CastleImage");
			pGoddess = GameObject.Find("GoddessImage");
			pWizard = GameObject.Find("WizardImage");
			pEagle = GameObject.Find("EagleImage");
			pKnight2 = GameObject.Find("KnightRight");
			pDragon2 = GameObject.Find("DragonRight");
			pBear2 = GameObject.Find("BearRight");
			pShamrock2 = GameObject.Find("ShamrockRight");
			pCastle2 = GameObject.Find("CastleRight");
			pGoddess2 = GameObject.Find("GoddessRight");
			pWizard2 = GameObject.Find("WizardRight");
			pEagle2 = GameObject.Find("EagleRight");
			playerHealth = GameObject.Find("HealthUI").GetComponent<Health>();
		}

		////PENDANT
		//knightAOE = GameObject.Find("KnightPE");
		pSide1 = null;
		pSide2 = null;
		pCurSide = pSide1;
		//PENDANT
	}


	void Update()
	{

		if (playerGO == null && GameObject.Find("PlayerFunctionality"))
		{
			playerGO = GameObject.FindGameObjectWithTag("Player");
			msRef = GameObject.Find("MagicUI").GetComponent<MagicSystem>();
			knightAOE = GameObject.FindGameObjectWithTag("KnightPE");
			Debug.Log(knightAOE);
		}

		if(GameObject.Find("PendantCanvas") && pCanvas == null)
		{
			pCanvas = GameObject.Find("PendantCanvas");
			selectText = GameObject.Find("SelectionBG").GetComponentInChildren<Text>();
			pCanvas.SetActive(false);
			pKnight = GameObject.Find("KnightImage");
			pDragon = GameObject.Find("DragonImage");
			pBear = GameObject.Find("BearImage");
			pShamrock = GameObject.Find("ShamrockImage");
			pCastle = GameObject.Find("CastleImage");
			pGoddess = GameObject.Find("GoddessImage");
			pWizard = GameObject.Find("WizardImage");
			pEagle = GameObject.Find("EagleImage");
			pKnight2 = GameObject.Find("KnightRight");
			pDragon2 = GameObject.Find("DragonRight");
			pBear2 = GameObject.Find("BearRight");
			pShamrock2 = GameObject.Find("ShamrockRight");
			pCastle2 = GameObject.Find("CastleRight");
			pGoddess2 = GameObject.Find("GoddessRight");
			pWizard2 = GameObject.Find("WizardRight");
			pEagle2 = GameObject.Find("EagleRight");
			playerHealth = GameObject.Find("HealthUI").GetComponent<Health>();
		}

		if (pCanvas != null)
		{
			if (!pCanvas.activeInHierarchy)
			{
				pendantChoice = true;
			}

			//PENDANT EFFECTS

			//GODDESS PENDANT
			if (pSide1 == Goddess)
			{
				pGoddess.SetActive(true);
				if(pGoddess2.activeInHierarchy)
				{
					pGoddess2.SetActive(false);
				}
				if (gNum == 5)
				{
					gNum = 0;
					gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(goddessProc);
					playerHealth.Heal();
				}
			}
			if (pSide2 == Goddess)
			{
				pGoddess2.SetActive(true);
				if(pGoddess.activeInHierarchy)
				{
					pGoddess.SetActive(false);
				}
			}
			if(pSide1 != Goddess && pSide2 != Goddess)
			{
				pGoddess.SetActive(false);
				pGoddess2.SetActive(false);
			}
			//GODDESS PENDANT

			//CASTLE PENDANT
			if (pSide1 == Castle)
			{
				pCastle.SetActive(true);
				if(pCastle2.activeInHierarchy)
				{
					pCastle2.SetActive(false);
				}
			}
			if (pSide2 == Castle)
			{
				pCastle2.SetActive(true);
				if(pCastle.activeInHierarchy)
				{
					pCastle.SetActive(false);
				}
			}
			if(pSide1 != Castle && pSide2 != Castle)
			{
				pCastle.SetActive(false);
				pCastle2.SetActive(false);
			}
			//CASTLE PENDANT

			//EAGLE PENDANT
			if (pSide1 == Eagle)
			{
				pEagle.SetActive(true);
				if(pEagle2.activeInHierarchy)
				{
					pEagle2.SetActive(false);
				}
			}
			if (pSide2 == Eagle)
			{
				pEagle2.SetActive(true);
				if(pEagle.activeInHierarchy)
				{
					pEagle.SetActive(false);
				}
			}
			if(pSide1 != Eagle && pSide2 != Eagle)
			{
				pEagle.SetActive(false);
				pEagle2.SetActive(false);
			}
			//EAGLE PENDANT

			//WIZARD PENDANT
			if (pSide1 == Wizard)
			{
				pWizard.SetActive(true);
				if(pWizard2.activeInHierarchy)
				{
					pWizard2.SetActive(false);
				}
			}
			if (pSide2 == Wizard)
			{
				pWizard2.SetActive(true);
				if (pWizard.activeInHierarchy)
				{
					pWizard.SetActive(false);
				}
			}
			if(pSide1 != Wizard && pSide2 != Wizard)
			{
				pWizard.SetActive(false);
				pWizard2.SetActive(false);
			}
			//WIZARD PENDANT

			//BEAR PENDANT
			if (pSide1 == Bear)
			{
				pBear.SetActive(true);
				if(pBear2.activeInHierarchy)
				{
					pBear2.SetActive(false);
				}
			}
			if (pSide2 == Bear)
			{
				pBear2.SetActive(true);
				if(pBear.activeInHierarchy)
				{
					pBear.SetActive(false);
				}
			}
			if(pSide1 != Bear && pSide2 != Bear)
			{
				pBear.SetActive(false);
				pBear2.SetActive(false);
			}
			//BEAR PENDANT

			// TURTLE PENDANT
			if (pSide1 == Dragon)
			{
				//msRef.MaxMana = 150;
				pDragon.SetActive(true);
				if(pDragon2.activeInHierarchy)
				{
					pDragon2.SetActive(false);
				}
			}
			if(pSide2 == Dragon)
			{
				//msRef.MaxMana = 150;
				pDragon2.SetActive(true);
				if(pDragon.activeInHierarchy)
				{
					pDragon.SetActive(false);
				}
			}
			if (pSide1 != Dragon && pSide2 != Dragon)
			{
				//msRef.MaxMana = 100;
				pDragon.SetActive(false);
				pDragon2.SetActive(false);
			}
			// TURTLE PENDANT

			// KNIGHT PENDANT
			if (pSide1 == Knight)
			{
				knightAOE.SetActive(true);
				pKnight.SetActive(true);
				if(pKnight2.activeInHierarchy)
				{
					pKnight2.SetActive(false);
				}
			}
			if(pSide2 == Knight)
			{
				knightAOE.SetActive(true);
				pKnight2.SetActive(true);
				if (pKnight.activeInHierarchy)
				{
					pKnight.SetActive(false);
				}
			}
			if (pSide1 != Knight && pSide2 != Knight)
			{
				if(knightAOE != null)
				knightAOE.SetActive(false);
				pKnight.SetActive(false);
				pKnight2.SetActive(false);
			}
			// KNIGHT PENDANT

			// SHAMROCK PENDANT
			if (pSide1 == Shamrock)
			{
				pShamrock.SetActive(true);
				if(pShamrock2.activeInHierarchy)
				{
					pShamrock2.SetActive(false);
				}
			}
			if(pSide2 == Shamrock)
			{
				pShamrock2.SetActive(true);
				if(pShamrock.activeInHierarchy)
				{
					pShamrock.SetActive(false);
				}
			}
			if (pSide1 != Shamrock && pSide2 != Shamrock)
			{
				pShamrock.SetActive(false);
				pShamrock2.SetActive(false);
			}
			// SHAMROCK PENDANT

			//PENDANT EFFECTS


			if (Input.GetKeyDown(KeyCode.L))
			{
				canplay = true;
				if (pCurSide == pSide1)
				{
					pCurSide = pSide2;
					return;
				}
				pCurSide = pSide1;
			}


			////////////////////////////////////////////////MENU///////////////////////////////////////////////////////////////
			if (pendantChoice && pCanvas.activeInHierarchy)
			{
				selectText.text = "Side 1";
			}
			if (!pendantChoice && pCanvas.activeInHierarchy)
			{
				selectText.text = "Side 2";
			}
		}
	}

	public void KnightSelect()
	{
		if (!pendantChoice)
		{
			pSide2 = Knight;
			pCanvas.SetActive(false);
			playForge = true;
			return;
		}
		if (pendantChoice)
		{
			pSide1 = Knight;
			pendantChoice = false;
			return;
		}
	}

	public void TurtleSelect()
	{
		if (!pendantChoice)
		{
			pSide2 = Dragon;
			pCanvas.SetActive(false);
			playForge = true;
			return;
		}

		if (pendantChoice)
		{
			pSide1 = Dragon;
			pendantChoice = false;
			return;
		}
	}

	public void BearSelect()
	{
		if (!pendantChoice)
		{
			pSide2 = Bear;
			pCanvas.SetActive(false);
			playForge = true;
			return;
		}
		if (pendantChoice)
		{
			pSide1 = Bear;
			pendantChoice = false;
			return;
		}
	}

	public void ShamrockSelect()
	{
		if (!pendantChoice)
		{
			pSide2 = Shamrock;
			pCanvas.SetActive(false);
			playForge = true;
			return;
		}
		if (pendantChoice)
		{
			pSide1 = Shamrock;
			pendantChoice = false;
			return;
		}
	}

	public void GoddessSelect()
	{
		if (!pendantChoice)
		{
			pSide2 = Goddess;
			pCanvas.SetActive(false);
			playForge = true;
			return;
		}
		if (pendantChoice)
		{
			pSide1 = Goddess;
			pendantChoice = false;
			return;
		}
	}

	public void CastleSelect()
	{
		if (!pendantChoice)
		{
			pSide2 = Castle;
			pCanvas.SetActive(false);
			playForge = true;
			return;
		}
		if (pendantChoice)
		{
			pSide1 = Castle;
			pendantChoice = false;
			return;
		}
	}

	public void WizardSelect()
	{
		if (!pendantChoice)
		{
			pSide2 = Wizard;
			pCanvas.SetActive(false);
			playForge = true;
			return;
		}
		if (pendantChoice)
		{
			pSide1 = Wizard;
			pendantChoice = false;
			return;
		}
	}

	public void EagleSelect()
	{
		if (!pendantChoice)
		{
			pSide2 = Eagle;
			pCanvas.SetActive(false);
			playForge = true;
			return;
		}
		if (pendantChoice)
		{
			pSide1 = Eagle;
			pendantChoice = false;
			return;
		}
	}
	//MENU
}
