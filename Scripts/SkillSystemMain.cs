using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SkillSystemMain : MonoBehaviour
{
	//ui reference
	#region Variables
	public bool canOpen;
	public int exp;
	int expCost;
	bool hasBought;
	GameObject gm;
	#endregion

	#region Fire Bools
	public bool fireballBuy = false;
	public bool firewallBuy = false;
	public bool firedamageup1Buy = false;
	public bool firemanacost1Buy = false;
	public bool firedamageup2Buy = false;
	public bool firemanacost2Buy = false;
	public bool meteorBuy = false;
	public bool meteorshowerBuy = false;
	public bool triplemeteorBuy = false;
	public bool pyromaniacBuy = false;
	#endregion

	#region Water Bools
	public bool icicleBuy = false;
	public bool icebeamBuy = false;
	public bool waterdamage1Buy = false;
	public bool watermana1Buy = false;
	public bool waterdamage2Buy = false;
	public bool watermana2Buy = false;
	public bool iceballBuy = false;
	public bool iceprismBuy = false;
	public bool tenbelowBuy = false;
	public bool brainfreezeBuy = false;
	#endregion

	#region Fire Tier Equips
	public string firetier1Equip = null;
	public string firetier2Equip = null;
	public string firetier3Equip = null;
	public string firetier4Equip = null;
	public string firetier5Equip = null;
	#endregion

	#region Water Tier Equips
	public string watertier1Equip = null;
	public string watertier2Equip = null;
	public string watertier3Equip = null;
	public string watertier4Equip = null;
	public string watertier5Equip = null;
	#endregion

	bool curSelection;

	string curSkill;

	int CurElem = 0;

	#region Fire Strings
	public readonly string fireball = "Fireball";
	public readonly string firewall = "FireWall";
	public readonly string meteor = "Meteor";
	public readonly string meteorshower = "MeteorShower";
	public readonly string pyromaniac = "Pyromaniac";
	public readonly string triplemeteor = "TripleMeteor";
	public readonly string firedamageup1 = "FireDamageup1";
	public readonly string firedamageup2 = "FireDamageup2";
	public readonly string firemanacost1 = "FireManacost1";
	public readonly string firemanacost2 = "FireManacost2";
	#endregion

	#region Water Strings
	public readonly string snowball = "Snowball";
	public readonly string icicle = "Icicle";
	public readonly string icebeam = "Ice Beam";
	public readonly string waterdamage1 = "Waterdamage1";
	public readonly string watermana1 = "Watermana1";
	public readonly string waterdamage2 = "Waterdamage2";
	public readonly string watermana2 = "Watermana2";
	public readonly string iceball = "IceBall";
	public readonly string iceprism = "IcePrism";
	public readonly string tenbelow = "10Below";
	public readonly string brainfreeze = "Brain Freeze";
	#endregion

	#region UI Canvases
	GameObject curUI;
	GameObject fireUI;
	#endregion

	#region UI Text and Picture Sets
	GameObject desText;
	GameObject priceText;
	GameObject selectImage;
	GameObject curEXP;
	GameObject buyGO;
	GameObject equipGO;
	GameObject fireswapGO;
	GameObject waterswapGO;
	GameObject quitGO;
	GameObject resetGO;

	//Upgrade Images
	GameObject baseImage;
	//left
	GameObject tier1l;
	GameObject tier2l;
	GameObject tier3l;
	GameObject tier4l;
	GameObject tier5l;
	//right
	GameObject tier1r;
	GameObject tier2r;
	GameObject tier3r;
	GameObject tier4r;
	GameObject tier5r;

	#endregion

	#region Audio
	AudioSource ssSource;
	AudioClip buyClip;
	AudioClip failClip;
	AudioClip pickClip;
	#endregion


	void Start()
	{
		exp = 1000;
		ssSource = GetComponentInChildren<AudioSource>();
		pickClip = Resources.Load<AudioClip>("Audio/SelectSound");
		buyClip = Resources.Load<AudioClip>("Audio/BuySound");
		failClip = Resources.Load<AudioClip>("Audio/FailSound");
		gm = GameObject.Find("GameManager");
		watertier1Equip = icicle;
	}

	void Update()
	{
		if (canOpen && Input.GetKeyDown(KeyCode.F))
		{
			FireSwap();
			CurElem = 1; //fire
		}

		if(hasBought && buyGO != null)
		{
			buyGO.SetActive(false);
			equipGO.SetActive(true);
		}

		if(curUI != null)
		{
			if(curUI.activeInHierarchy)
			{
				ImageSet();
			}
		}

		if(!hasBought && buyGO != null)
		{
			buyGO.gameObject.SetActive(true);
			equipGO.SetActive(false);
		}

		if(equipGO != null)
		{
			if ((curSkill == firetier1Equip || curSkill == firetier2Equip || curSkill == firetier3Equip || curSkill == firetier4Equip || curSkill == firetier5Equip) || (curSkill == watertier1Equip || curSkill == watertier2Equip || curSkill == watertier3Equip || curSkill == watertier4Equip || curSkill == watertier5Equip))
			{
				equipGO.GetComponentInChildren<Text>().text = "Equipped";
			}
			else equipGO.GetComponentInChildren<Text>().text = "Equip";
		}

		if (curEXP != null)
			curEXP.GetComponent<Text>().text = exp.ToString();

		if (fireUI == null)
		{
			if (GameObject.Find("FireUICanvas"))
			{
				fireUI = GameObject.Find("FireUICanvas");

				curUI = fireUI;

				if (buyGO == null)
					buyGO = curUI.GetComponentInChildren<BuyBG>().gameObject;
				if (equipGO == null)
					equipGO = curUI.GetComponentInChildren<EquipGO>().gameObject;
				if (quitGO == null)
					quitGO = curUI.GetComponentInChildren<QuitGO>().gameObject;
				if (fireswapGO == null)
					fireswapGO = curUI.GetComponentInChildren<FireSwapGO>().gameObject;
				if (waterswapGO == null)
					waterswapGO = curUI.GetComponentInChildren<WaterSwapGO>().gameObject;
				if (resetGO == null)
					resetGO = curUI.GetComponentInChildren<ResetGO>().gameObject;

				buyGO.GetComponent<Button>().onClick.AddListener(Buy);
				equipGO.GetComponent<Button>().onClick.AddListener(Equip);
				quitGO.GetComponent<Button>().onClick.AddListener(QuitMenu);
				fireswapGO.GetComponent<Button>().onClick.AddListener(FireSwap);
				waterswapGO.GetComponent<Button>().onClick.AddListener(WaterSwap);
				resetGO.GetComponent<Button>().onClick.AddListener(ResetSkills);

				fireUI.SetActive(false);
			}
		}


		if (curUI != null)
		{
			if (desText == null)
				desText = curUI.GetComponentInChildren<DesText>().gameObject;
			if (priceText == null)
				priceText = curUI.GetComponentInChildren<CostText>().gameObject;
			if (selectImage == null)
				selectImage = curUI.GetComponentInChildren<CurImage>().gameObject;
			if (curEXP == null)
				curEXP = curUI.GetComponentInChildren<CurEXP>().gameObject;
			if (buyGO == null)
				buyGO = curUI.GetComponentInChildren<BuyBG>().gameObject;
			if (equipGO == null)
				equipGO = curUI.GetComponentInChildren<EquipGO>().gameObject;
			if (quitGO == null)
				quitGO = curUI.GetComponentInChildren<QuitGO>().gameObject;
			if (fireswapGO == null)
				fireswapGO = curUI.GetComponentInChildren<FireSwapGO>().gameObject;
			if (waterswapGO == null)
				waterswapGO = curUI.GetComponentInChildren<WaterSwapGO>().gameObject;
			//Setting Image references
			if (baseImage == null)
				baseImage = curUI.GetComponentInChildren<BaseImage>().gameObject;
			//Left
			if (tier1l == null)
				tier1l = GameObject.Find("Tier1limage");
			if (tier2l == null)
				tier2l = GameObject.Find("Tier2limage");
			if (tier3l == null)
				tier3l = GameObject.Find("Tier3limage");
			if (tier4l == null)
				tier4l = GameObject.Find("Tier4limage");
			if (tier5l == null)
				tier5l = GameObject.Find("Tier5limage");
			//Right
			if (tier1r == null)
				tier1r = GameObject.Find("Tier1rimage");
			if (tier2r == null)
				tier2r = GameObject.Find("Tier2rimage");
			if (tier3r == null)
				tier3r = GameObject.Find("Tier3rimage");
			if (tier4r == null)
				tier4r = GameObject.Find("Tier4rimage");
			if (tier5r == null)
				tier5r = GameObject.Find("Tier5riamge");

		}

		if(desText != null)
		{
			if (curSkill == null)
			{
				desText.GetComponent<Text>().text = "Select a Skill";
			}
		}
	}

	#region General Canvas Functions
	public void ResetSkills()
	{
		if (gm.GetComponent<GameManager>().collectibleCounter >= 25)
		{
			ssSource.PlayOneShot(buyClip);

			gm.GetComponent<GameManager>().collectibleCounter -= 25;

			firetier1Equip = null;
			firetier2Equip = null;
			firetier3Equip = null;
			firetier4Equip = null;
			firetier5Equip = null;

			watertier1Equip = null;
			watertier2Equip = null;
			watertier3Equip = null;
			watertier4Equip = null;
			watertier5Equip = null;

			if (fireballBuy)
			{
				fireballBuy = false;
				exp += 15;
			}
			if (firewallBuy)
			{
				firewallBuy = false;
				exp += 15;
			}
			if (firedamageup1Buy)
			{
				firedamageup1Buy = false;
				exp += 20;
			}
			if (firemanacost1Buy)
			{
				firemanacost1Buy = false;
				exp += 20;
			}
			if (firedamageup2Buy)
			{
				firedamageup2Buy = false;
				exp += 30;
			}
			if (firemanacost2Buy)
			{
				firemanacost2Buy = false;
				exp += 30;
			}
			if (meteorBuy)
			{
				meteorBuy = false;
				exp += 40;
			}
			if (meteorshowerBuy)
			{
				meteorshowerBuy = false;
				exp += 40;
			}
			if (pyromaniacBuy)
			{
				pyromaniacBuy = false;
				exp += 50;
			}
			if (triplemeteorBuy)
			{
				triplemeteorBuy = false;
				exp += 50;
			}

			if(icicleBuy)
			{
				icicleBuy = false;
				exp += 15;
			}
			if (icebeamBuy)
			{
				icebeamBuy = false;
				exp += 15;
			}
			if (waterdamage1Buy)
			{
				waterdamage1Buy = false;
				exp += 20;
			}
			if (watermana1Buy)
			{
				watermana1Buy = false;
				exp += 20;
			}
			if (waterdamage2Buy)
			{
				waterdamage2Buy = false;
				exp += 30;
			}
			if (watermana2Buy)
			{
				watermana2Buy = false;
				exp += 30;
			}
			if (iceballBuy)
			{
				iceballBuy = false;
				exp += 40;
			}
			if (iceprismBuy)
			{
				iceprismBuy = false;
				exp += 40;
			}
			if (tenbelowBuy)
			{
				tenbelowBuy = false;
				exp += 50;
			}
			if (brainfreezeBuy)
			{
				brainfreezeBuy = false;
				exp += 50;
			}
		}
		else ssSource.PlayOneShot(failClip);
	}

	public void QuitMenu()
	{
		curUI.SetActive(false);
	}
	
	public void ImageSet()
	{
		#region Fire Image Set
		//////////////////////////////////////////////////////////////////////////////////////////
		if (CurElem == 1) //FIRE
		{
			//FIREBALL
			if (firetier1Equip == fireball)
				tier1l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Fireball");
			else if (fireballBuy && firetier1Equip != fireball)
				tier1l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Fireballlock");

			else tier1l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			////////////////////////////////////////////////////////////////////////////////////////////

			//FIREWALL
			if (firetier1Equip == firewall)
				tier1r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireWall");
			else if (firewallBuy && firetier1Equip != firewall)
				tier1r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireWalllock");
			else tier1r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			///////////////////////////////////////////////////////////////////////////////////////////

			//FIREDAMAGE1
			if (firetier2Equip == firedamageup1)
				tier2l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireDamage");
			else if (firedamageup1Buy && firetier2Equip != firedamageup1)
				tier2l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Damagelock");
			else tier2l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			///////////////////////////////////////////////////////////////////////////////////////////

			//FIREMANA1
			if (firetier2Equip == firemanacost1)
				tier2r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireManaCost");
			else if (firemanacost1Buy && firetier2Equip != firemanacost1)
				tier2r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Manalock");
			else tier2r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			///////////////////////////////////////////////////////////////////////////////////////////

			//FIREDAMAGE2
			if (firetier3Equip == firedamageup2)
				tier3l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireDamage");
			else if (firedamageup2Buy && firetier3Equip != firedamageup2)
				tier3l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Damagelock");
			else tier3l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			////////////////////////////////////////////////////////////////////////////////////////////

			//FIREMANA2
			if (firetier3Equip == firemanacost2)
				tier3r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireManaCost");
			else if (firetier3Equip != firemanacost2 && firemanacost2Buy)
				tier3r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Manalock");
			else tier3r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			/////////////////////////////////////////////////////////////////////////////////////////////

			//METEOR
			if (firetier4Equip == meteor)
				tier4l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Meteor");
			else if (firetier4Equip != meteor && meteorBuy)
				tier4l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Meteorlock");
			else tier4l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			/////////////////////////////////////////////////////////////////////////////////////////

			//METEORSHOWER
			if (firetier4Equip == meteorshower)
				tier4r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/MeteorShower");
			else if (firetier4Equip != meteorshower && meteorshowerBuy)
				tier4r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/MeteorShowerlock");
			else tier4r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			///////////////////////////////////////////////////////////////////////////////////////////

			//TRIPLEMETEOR
			if (firetier5Equip == triplemeteor)
				tier5l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/TripleMeteor");
			else if (firetier5Equip != triplemeteor && triplemeteorBuy)
				tier5l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/TripleMeteorlock");
			else tier5l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			///////////////////////////////////////////////////////////////////////////////////////////

			//PYROMANIAC
			if (firetier5Equip == pyromaniac)
				tier5r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Pyromaniac");
			else if (firetier5Equip != pyromaniac && pyromaniacBuy)
				tier5r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Pyromaniaclock");
			else tier5r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
		}
		#endregion
		#region Water Image Setting
		if (CurElem == 2)//WATER
		{
			//ICEBEAM
			if (watertier1Equip == icebeam)
				tier1l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IceBeam");
			else if (watertier1Equip != icebeam && icebeamBuy)
				tier1l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IceBeamlock");
			else
				tier1l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			//ICICLE
			if (watertier1Equip == icicle)
				tier1r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/Icicle");
			else if (watertier1Equip != icicle && icicleBuy)
				tier1r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/Iciclelock");
			else tier1r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			//WATERDAMAGE1
			if (watertier2Equip == waterdamage1)
				tier2l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/WaterDamage");
			else if (watertier2Equip != waterdamage1 && waterdamage1Buy)
				tier2l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Damagelock");
			else tier2l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			//WATERMANA1
			if (watertier2Equip == watermana1)
				tier2r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/WaterManaCost");
			else if (watertier2Equip != watermana1 && watermana1Buy)
				tier2r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Manalock");
			else tier2r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////

			//WATERDAMAGE2
			if (watertier3Equip == waterdamage2)
				tier3l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/WaterDamage");
			else if (watertier3Equip != waterdamage2 && waterdamage2Buy)
				tier3l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Damagelock");
			else tier3l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			//WATERMANA2
			if (watertier3Equip == watermana2)
				tier3r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/WaterManaCost");
			else if (watertier3Equip != watermana2 && watermana2Buy)
				tier3r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Manalock");
			else tier3r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			//ICEPRISM
			if (watertier4Equip == iceprism)
				tier4l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IcePrism");
			else if (watertier4Equip != iceprism && iceprismBuy)
				tier4l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IcePrismlock");
			else tier4l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			//ICEBALL
			if (watertier4Equip == iceball)
				tier4r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IceBall");
			else if (watertier4Equip != iceball && iceballBuy)
				tier4r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IceBalllock");
			else tier4r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			//BRAINFREEZE
			if (watertier5Equip == brainfreeze)
				tier5l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/BrainFreeze");
			else if (watertier5Equip != brainfreeze && brainfreezeBuy)
				tier5l.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/BrainFreezelock");
			else tier5l.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			//10BELOW
			if (watertier5Equip == tenbelow)
				tier5r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/10Below");
			else if (watertier5Equip != tenbelow && tenbelowBuy)
				tier5r.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/10Belowlock");
			else tier5r.GetComponent<Image>().sprite = Resources.Load<Sprite>("GeneralSkill/Locked");
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		}
		#endregion
	}

	public void RemoveListeners()
	{
		tier1l.GetComponent<Button>().onClick.RemoveAllListeners();
		tier1r.GetComponent<Button>().onClick.RemoveAllListeners();
		tier2l.GetComponent<Button>().onClick.RemoveAllListeners();
		tier2r.GetComponent<Button>().onClick.RemoveAllListeners();
		tier3l.GetComponent<Button>().onClick.RemoveAllListeners();
		tier3r.GetComponent<Button>().onClick.RemoveAllListeners();
		tier4l.GetComponent<Button>().onClick.RemoveAllListeners();
		tier4r.GetComponent<Button>().onClick.RemoveAllListeners();
		tier5l.GetComponent<Button>().onClick.RemoveAllListeners();
		tier5r.GetComponent<Button>().onClick.RemoveAllListeners();
	}

	public void FireSwap()
	{
		curUI = fireUI;
		CurElem = 1; //fire
		if(!fireUI.activeInHierarchy)
		fireUI.SetActive(true);

		

		#region Image and Function Setting

		if (baseImage == null)
			baseImage = curUI.GetComponentInChildren<BaseImage>().gameObject;
		//Left
		if (tier1l == null)
			tier1l = GameObject.Find("Tier1limage");
		if (tier2l == null)
			tier2l = GameObject.Find("Tier2limage");
		if (tier3l == null)
			tier3l = GameObject.Find("Tier3limage");
		if (tier4l == null)
			tier4l = GameObject.Find("Tier4limage");
		if (tier5l == null)
			tier5l = GameObject.Find("Tier5limage");
		//Right
		if (tier1r == null)
			tier1r = GameObject.Find("Tier1rimage");
		if (tier2r == null)
			tier2r = GameObject.Find("Tier2rimage");
		if (tier3r == null)
			tier3r = GameObject.Find("Tier3rimage");
		if (tier4r == null)
			tier4r = GameObject.Find("Tier4rimage");
		if (tier5r == null)
			tier5r = GameObject.Find("Tier5rimage");

		RemoveListeners();

		#region Setting Listeners
		tier1l.GetComponent<Button>().onClick.AddListener(FireballSelect);
		tier1r.GetComponent<Button>().onClick.AddListener(FireWallSelect);
		tier2l.GetComponent<Button>().onClick.AddListener(FireDamage1Select);
		tier2r.GetComponent<Button>().onClick.AddListener(FireMana1Select);
		tier3l.GetComponent<Button>().onClick.AddListener(FireDamage2Select);
		tier3r.GetComponent<Button>().onClick.AddListener(FireMana2Select);
		tier4l.GetComponent<Button>().onClick.AddListener(MeteorSelect);
		tier4r.GetComponent<Button>().onClick.AddListener(MeteorShowerSelect);
		tier5l.GetComponent<Button>().onClick.AddListener(MeteorSplitSelect);
		tier5r.GetComponent<Button>().onClick.AddListener(FireDOTSelect);
		#endregion

		#region Image Setting
		baseImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireBase");
		ImageSet();

		#endregion
		#endregion
	}

	public void WaterSwap()
	{
		CurElem = 2; //water
		#region Image Setting

		if (baseImage == null)
			baseImage = curUI.GetComponentInChildren<BaseImage>().gameObject;
			baseImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/Snowball");
		//Left
		if (tier1l == null)
			tier1l = GameObject.Find("Tier1limage");
		if (tier2l == null)
			tier2l = GameObject.Find("Tier2limage");
		if (tier3l == null)
			tier3l = GameObject.Find("Tier3limage");
		if (tier4l == null)
			tier4l = GameObject.Find("Tier4limage");
		if (tier5l == null)
			tier5l = GameObject.Find("Tier5limage");
		//Right
		if (tier1r == null)
			tier1r = GameObject.Find("Tier1rimage");
		if (tier2r == null)
			tier2r = GameObject.Find("Tier2rimage");
		if (tier3r == null)
			tier3r = GameObject.Find("Tier3rimage");
		if (tier4r == null)
			tier4r = GameObject.Find("Tier4rimage");
		if (tier5r == null)
			tier5r = GameObject.Find("Tier5rimage");

		RemoveListeners();

		#region Setting Listeners
		tier1l.GetComponent<Button>().onClick.AddListener(IceBeamSelect);
		tier1r.GetComponent<Button>().onClick.AddListener(IcicleSelect);
		tier2l.GetComponent<Button>().onClick.AddListener(WaterDamage1Select);
		tier2r.GetComponent<Button>().onClick.AddListener(WaterMana1Select);
		tier3l.GetComponent<Button>().onClick.AddListener(WaterDamage2Select);
		tier3r.GetComponent<Button>().onClick.AddListener(WaterMana2Select);
		tier4l.GetComponent<Button>().onClick.AddListener(IcePrismSelect);
		tier4r.GetComponent<Button>().onClick.AddListener(IceBallSelect);
		tier5l.GetComponent<Button>().onClick.AddListener(BrainFreezeSelect);
		tier5r.GetComponent<Button>().onClick.AddListener(TenBelowSelect);
		#endregion

		#endregion
	}

	public void ElectricSwap()
	{
		
	}

	public void EarthSwap()
	{
		
	}

	public void BuyChecker()
	{
		if(curSelection)
		{
			hasBought = true;
		}
		if(!curSelection)
		{
			hasBought = false;
		}
	}

	public void BoughtAssigner()
	{
		BuyChecker();
		if (exp >= expCost && !hasBought)
		{
			#region Fire Checks
			if (curSkill == fireball)
			{
				fireballBuy = true;
				curSelection = fireballBuy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == firewall)
			{
				firewallBuy = true;
				curSelection = firewallBuy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == firedamageup1 && (fireballBuy || firewallBuy))
			{
				firedamageup1Buy = true;
				curSelection = firedamageup1Buy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == firedamageup2 && (firedamageup1Buy || firemanacost1Buy))
			{
				firedamageup2Buy = true;
				curSelection = firedamageup2Buy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == firemanacost1 && (fireballBuy || firewallBuy))
			{
				firemanacost1Buy = true;
				curSelection = firemanacost1Buy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == firemanacost2 && (firedamageup1Buy || firemanacost1Buy))
			{
				firemanacost2Buy = true;
				curSelection = firemanacost2Buy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == meteor && (firemanacost2Buy || firedamageup2Buy))
			{
				meteorBuy = true;
				curSelection = meteorBuy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == meteorshower && (firemanacost2Buy || firedamageup2Buy))
			{
				meteorshowerBuy = true;
				curSelection = meteorshowerBuy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == pyromaniac && (meteorBuy || meteorshowerBuy))
			{
				pyromaniacBuy = true;
				curSelection = pyromaniacBuy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == triplemeteor && (meteorBuy || meteorshowerBuy))
			{
				triplemeteorBuy = true;
				curSelection = triplemeteorBuy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			#endregion
			#region Water Checks
			else if (curSkill == icicle)
			{
				icicleBuy = true;
				curSelection = icicleBuy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);

			}
			else if (curSkill == icebeam)
			{
				icebeamBuy = true;
				curSelection = icebeamBuy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == waterdamage1 && (icicleBuy || icebeamBuy))
			{
				waterdamage1Buy = true;
				curSelection = waterdamage1Buy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == watermana1 && (icicleBuy || icebeamBuy))
			{
				watermana1Buy = true;
				curSelection = watermana1Buy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == waterdamage2 && (waterdamage1Buy || watermana1Buy))
			{
				waterdamage2Buy = true;
				curSelection = waterdamage2Buy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == watermana2 && (waterdamage1Buy || watermana1Buy))
			{
				watermana2Buy = true;
				curSelection = watermana2Buy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == iceball && (waterdamage2Buy || watermana2Buy))
			{
				iceballBuy = true;
				curSelection = iceballBuy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == iceprism && (waterdamage2Buy || watermana2Buy))
			{
				iceprismBuy = true;
				curSelection = iceprismBuy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == tenbelow && (iceballBuy || iceprismBuy))
			{
				tenbelowBuy = true;
				curSelection = tenbelowBuy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == brainfreeze && (iceballBuy || iceprismBuy))
			{
				brainfreezeBuy = true;
				curSelection = brainfreezeBuy;
				exp -= expCost;
				ssSource.PlayOneShot(buyClip);
			}
			#endregion
			else ssSource.PlayOneShot(failClip);
			BuyChecker();
		}
	}


	public void Equip()
	{
		if(hasBought)
		{
			#region Fire Checks
			if (curSkill == fireball && firetier1Equip != fireball)
			{
				firetier1Equip = fireball;
				Debug.Log(firetier1Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if(curSkill == firewall && firetier1Equip != firewall)
			{
				firetier1Equip = firewall;
				Debug.Log(firetier1Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if(curSkill == firedamageup1 && firetier2Equip != firedamageup1)
			{
				firetier2Equip = firedamageup1;
				Debug.Log(firetier2Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == firedamageup2 && firetier3Equip != firedamageup2)
			{
				firetier3Equip = firedamageup2;
				Debug.Log(firetier3Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == firemanacost1 && firetier2Equip != firemanacost1)
			{
				firetier2Equip = firemanacost1;
				Debug.Log(firetier2Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == firemanacost2 && firetier3Equip != firemanacost2)
			{
				firetier3Equip = firemanacost2;
				Debug.Log(firetier3Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if(curSkill == meteor && firetier4Equip != meteor)
			{
				firetier4Equip = meteor;
				Debug.Log(firetier4Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if(curSkill == meteorshower && firetier4Equip != meteorshower)
			{
				firetier4Equip = meteorshower;
				Debug.Log(firetier4Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if(curSkill == pyromaniac && firetier4Equip == meteorshower && firetier5Equip != pyromaniac)
			{
				firetier5Equip = pyromaniac;
				Debug.Log(firetier5Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == triplemeteor && firetier4Equip == meteor && firetier5Equip != triplemeteor)
			{
				firetier5Equip = triplemeteor;
				Debug.Log(firetier5Equip);
				ssSource.PlayOneShot(buyClip);
			}
			#endregion
			#region Water Checks
			else if (curSkill == icicle && watertier1Equip != icicle)
			{
				watertier1Equip = icicle;
				Debug.Log(watertier1Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == icebeam && watertier1Equip != icebeam)
			{
				watertier1Equip = icebeam;
				Debug.Log(watertier1Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == waterdamage1 && watertier2Equip != waterdamage1)
			{
				watertier2Equip = waterdamage1;
				Debug.Log(watertier2Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == watermana1 && watertier2Equip != watermana1)
			{
				watertier2Equip = watermana1;
				Debug.Log(watertier2Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == waterdamage2 && watertier3Equip != waterdamage2)
			{
				watertier3Equip = waterdamage2;
				Debug.Log(watertier3Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == watermana2 && watertier3Equip != waterdamage2)
			{
				watertier3Equip = watermana2;
				Debug.Log(watertier3Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == iceball && watertier4Equip != iceball)
			{
				watertier4Equip = iceball;
				Debug.Log(watertier1Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == iceprism && watertier4Equip != iceprism)
			{
				watertier4Equip = iceprism;
				Debug.Log(watertier4Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == tenbelow && watertier5Equip != tenbelow && watertier4Equip == iceball)
			{
				watertier5Equip = tenbelow;
				Debug.Log(watertier5Equip);
				ssSource.PlayOneShot(buyClip);
			}
			else if (curSkill == brainfreeze && watertier5Equip != brainfreeze && watertier4Equip == iceprism)
			{
				watertier5Equip = brainfreeze;
				Debug.Log(watertier5Equip);
				ssSource.PlayOneShot(buyClip);
			}

			#endregion
			else ssSource.PlayOneShot(failClip);
		}
	}

	public void Buy()
	{
		BoughtAssigner();
	}

	#region Fire Functions
	///////////////////////////////////////////////////////FIRE FUNCTIONS//////////////////////////////////////////////////////////////////
	public void FireballSelect()
	{
		expCost = 15;
		desText.GetComponent<Text>().text = "Fireball";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Fireball");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = fireballBuy; // fireball
		hasBought = curSelection;
		curSkill = fireball;
	}

	public void FireWallSelect()
	{
		expCost = 15;
		desText.GetComponent<Text>().text = "Fire Wall";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireWall");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = firewallBuy; //firewall
		hasBought = curSelection;
		curSkill = firewall;
	}

	public void FireDamage1Select()
	{
		expCost = 20;
		desText.GetComponent<Text>().text = "Damage Increase";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireDamage");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = firedamageup1Buy;
		hasBought = curSelection;
		curSkill = firedamageup1;
	}

	public void FireDamage2Select()
	{
		expCost = 20;
		desText.GetComponent<Text>().text = "Damage Increase 2";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireDamage");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = firedamageup2Buy;
		hasBought = curSelection;
		curSkill = firedamageup2;
	}

	public void FireMana1Select()
	{
		expCost = 30;
		desText.GetComponent<Text>().text = "Mana Cost Reduction";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireManaCost");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = firemanacost1Buy;
		hasBought = curSelection;
		curSkill = firemanacost1;
	}

	public void FireMana2Select()
	{
		expCost = 30;
		desText.GetComponent<Text>().text = "Mana Cost Reduction 2";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireManaCost");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = firemanacost2Buy;
		hasBought = curSelection;
		curSkill = firemanacost2;
	}

	public void MeteorSelect()
	{
		expCost = 40;
		desText.GetComponent<Text>().text = "Meteor";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Meteor");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = meteorBuy; //meteor
		hasBought = curSelection;
		curSkill = meteor;
	}

	public void MeteorShowerSelect()
	{
		expCost = 40;
		desText.GetComponent<Text>().text = "Meteor Shower";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/MeteorShower");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = meteorshowerBuy; //meteorshower
		hasBought = curSelection;
		curSkill = meteorshower;
	}

	public void FireDOTSelect()
	{
		expCost = 50;
		desText.GetComponent<Text>().text = "Pyromaniac";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Pyromaniac");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = pyromaniacBuy; //pyromaniac
		hasBought = curSelection;
		curSkill = pyromaniac;
	}

	public void MeteorSplitSelect()
	{
		expCost = 50;
		desText.GetComponent<Text>().text = "Meteor Split";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/TripleMeteor");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = triplemeteorBuy; //meteorsplit
		hasBought = curSelection;
		curSkill = triplemeteor;
	}
	////////////////////////////////////////////////////FIRE FUNCTIONS///////////////////////////////////////////////////////////////////
	#endregion
	#region Water Functions
	public void IcicleSelect()
	{
		expCost = 15;
		desText.GetComponent<Text>().text = "Icicle";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/Icicle");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = icicleBuy; //Icicle
		hasBought = curSelection;
		curSkill = icicle;
	}
	public void IceBeamSelect()
	{
		expCost = 15;
		desText.GetComponent<Text>().text = "Ice Beam";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IceBeam");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = icebeamBuy; //Ice Beam
		hasBought = curSelection;
		curSkill = icebeam;
	}
	public void WaterDamage1Select()
	{
		expCost = 20;
		desText.GetComponent<Text>().text = "Water Damage";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/WaterDamage");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = waterdamage1Buy; //water damage 1
		hasBought = curSelection;
		curSkill = waterdamage1;
	}
	public void WaterMana1Select()
	{
		expCost = 20;
		desText.GetComponent<Text>().text = "Water Mana Cost";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/WaterManaCost");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = watermana1Buy; //water mana 1
		hasBought = curSelection;
		curSkill = watermana1;
	}
	public void WaterDamage2Select()
	{
		expCost = 30;
		desText.GetComponent<Text>().text = "Water Damage 2";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/WaterDamage");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = waterdamage2Buy; //water damage 2
		hasBought = curSelection;
		curSkill = waterdamage2;
	}
	public void WaterMana2Select()
	{
		expCost = 30;
		desText.GetComponent<Text>().text = "Water Mana 2";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/WaterManaCost");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = watermana2Buy; //water mana 2
		hasBought = curSelection;
		curSkill = watermana2;
	}
	public void IcePrismSelect()
	{
		expCost = 40;
		desText.GetComponent<Text>().text = "Ice Prism";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IcePrism");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = iceprismBuy; //ice prism
		hasBought = curSelection;
		curSkill = iceprism;
	}
	public void IceBallSelect()
	{
		expCost = 40;
		desText.GetComponent<Text>().text = "Ice Ball";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IceBall");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = iceballBuy; //ice ball
		hasBought = curSelection;
		curSkill = iceball;
	}
	public void TenBelowSelect()
	{
		expCost = 50;
		desText.GetComponent<Text>().text = "10 Below";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/10Below");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = tenbelowBuy; //10 below
		hasBought = curSelection;
		curSkill = tenbelow;
	}
	public void BrainFreezeSelect()
	{
		expCost = 50;
		desText.GetComponent<Text>().text = "Brain Freeze";
		selectImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/BrainFreeze");
		priceText.GetComponent<Text>().text = expCost.ToString();
		ssSource.PlayOneShot(pickClip);
		curSelection = brainfreezeBuy; //Brain Freeze
		hasBought = curSelection;
		curSkill = brainfreeze;
	}
	#endregion
	//function for each upgrade for buttons
	#endregion
}
