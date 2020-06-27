using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkillSystemNew : MonoBehaviour
{

	#region Variables
	public bool canOpen;
	public int exp;
	int expCost;
	bool hasBought;
	GameObject gm;

	string slot1;
	string slot2;
	string slot3;
	string basicspell;

	Sprite curtier1;
	Sprite curtier2;
	Sprite curtier3;

	bool curSelection;
	string curSkill;
	int curSpellXP;
	string curDes;
	GameObject expbar;
	Slider slider;
	#endregion

	public string baseequip;
	public string slot1equip;
	public string slot2equip;
	public string slot3equip;
	GameObject mainoutline;
	GameObject baseoutline;
	int type; //0 = base spell, 1 is any normal spell

	#region Skill Strings
	public readonly string meteor = "Meteor";
	public readonly string meteorshower = "MeteorShower";
	public readonly string firegreatsword = "FireGreatsword";
	public readonly string conflagration = "Conflagration";

	public readonly string iceball = "IceBall";
	public readonly string iceprism = "IcePrism";
	public readonly string frostblast = "Frostblast";
	public readonly string vortex = "Vortex";

	public readonly string voltdagger = "VoltDagger";
	public readonly string lightninglance = "LightningLance";
	public readonly string chainlightning = "ChainLightning";
	public readonly string balllightning = "BallLightning";

	public readonly string rockblast = "RockBlast";
	public readonly string beesummon = "BeeSummon";
	public readonly string mekigsummon = "MekigSummon";
	public readonly string stoneskin = "StoneSkin";
	#endregion

	#region Ability EXP Ints
	public int meteorXP;
	public int meteorshowerXP;
	public int greatswordXP;
	public int conflagrationXP;

	public int iceprismXP;
	public int iceballXP;
	public int frostblastXP;
	public int vortexXP;

	public int rockblastXP;
	public int beeXP;
	public int mekigneerXP;
	public int stoneskinXP;

	public int voltdaggerXP;
	public int lightlanceXP;
	public int chainlightXP;
	public int balllightXP;

	#endregion

	#region UI Canvass
	GameObject SkillUI;
	#endregion

	#region Audio
	AudioSource ssSource;
	AudioClip buyClip;
	AudioClip failClip;
	AudioClip pickClip;
	#endregion

	#region UI Text and Picture Sets
	Text desText;
	Text nameText, typetext;
	GameObject selectImage;
	GameObject leftimage, rightimage;
	Sprite CurImage;
	[SerializeField]
	GameObject equipGO;
	GameObject quitGO;

	GameObject tier1, tier2, tier3;

	GameObject mainselectGO,baseselectGO;

	GameObject selectGO;

	GameObject leftarrowGO, rightarrowGO;

	GameObject baseslotGO, slot1GO, slot2GO, slot3GO;

	GameObject slot1selectGO, slot2selectGO, slot3selectGO;

	#endregion

	void Start()
    {
		ssSource = GetComponentInChildren<AudioSource>();
		pickClip = Resources.Load<AudioClip>("Audio/SelectSound");
		buyClip = Resources.Load<AudioClip>("Audio/BuySound");
		failClip = Resources.Load<AudioClip>("Audio/FailSound");
		gm = GameObject.Find("GameManager");
        MainTypeSwap();
		curSkill = meteor;
	}

	private void OnEnable()
	{
		//if(SceneManager.GetActiveScene().name != "MainMenu")
		SceneManager.sceneLoaded += SceneSetup;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= SceneSetup;
	}

	private void SceneSetup(Scene scene, LoadSceneMode mode)
	{
		if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Tutorial_Scene")
		{
			gm.GetComponent<SaveInputManager>().LoadGame();
			Debug.Log("Loaded game");

            #region Switch for Pic Set
            
			switch (baseequip)
			{
				case "FireGreatsword":
					{
						baseslotGO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireGreatsword");
					}
					break;
				case "Frostblast":
					{
						baseslotGO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/FrostBlast");
					}
					break;
				case "VoltDagger":
					{
						baseslotGO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/VoltDagger");
					}
					break;
				case "RockBlast":
					{
						baseslotGO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/RockBlast");
					}
					break;
			}
			switch (slot1equip)
			{
				case "Meteor":
					{
						slot1GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Meteor");
					}
					break;
				case "MeteorShower":
					{
						slot1GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/TripleMeteor");
					}
					break;
				case "Conflagration":
					{
						slot1GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Pyromaniac");
					}
					break;
				case "IcePrism":
					{
						slot1GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IcePrism");
					}
					break;
				case "IceBall":
					{
						slot1GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IceBall");
					}
					break;
				case "Vortex":
					{
						slot1GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/Vortex");
					}
					break;
				case "LightningLance":
					{
						slot1GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/LightningLance");
					}
					break;
				case "ChainLightning":
					{
						slot1GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/PerfectChainLightning");
					}
					break;
				case "BallLightning":
					{
						slot1GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("BallLightning");
					}
					break;
				case "BeeSummon":
					{
						slot1GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/BeeSummon");
					}
					break;
				case "MekigSummon":
					{
						slot1GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/GnomeSummon");
					}
					break;
				case "StoneSkin":
					{
						slot1GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/Thorns");
					}
					break;
			}
			switch (slot2equip)
			{
				case "Meteor":
					{
						slot2GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Meteor");
					}
					break;
				case "MeteorShower":
					{
						slot2GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/TripleMeteor");
					}
					break;
				case "Conflagration":
					{
						slot2GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Pyromaniac");
					}
					break;
				case "IcePrism":
					{
						slot2GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IcePrism");
					}
					break;
				case "IceBall":
					{
						slot2GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IceBall");
					}
					break;
				case "Vortex":
					{
						slot2GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/Vortex");
					}
					break;
				case "LightningLance":
					{
						slot2GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/LightningLance");
					}
					break;
				case "ChainLightning":
					{
						slot2GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/PerfectChainLightning");
					}
					break;
				case "BallLightning":
					{
						slot2GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("BallLightning");
					}
					break;
				case "BeeSummon":
					{
						slot2GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/BeeSummon");
					}
					break;
				case "MekigSummon":
					{
						slot2GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/GnomeSummon");
					}
					break;
				case "StoneSkin":
					{
						slot2GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/Thorns");
					}
					break;
			}
			switch (slot3equip)
			{
				case "Meteor":
					{
						slot3GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Meteor");
					}
					break;
				case "MeteorShower":
					{
						slot3GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/TripleMeteor");
					}
					break;
				case "Conflagration":
					{
						slot3GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Pyromaniac");
					}
					break;
				case "IcePrism":
					{
						slot3GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IcePrism");
					}
					break;
				case "IceBall":
					{
						slot3GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IceBall");
					}
					break;
				case "Vortex":
					{
						slot3GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/Vortex");
					}
					break;
				case "LightningLance":
					{
						slot3GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/LightningLance");
					}
					break;
				case "ChainLightning":
					{
						slot3GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/PerfectChainLightning");
					}
					break;
				case "BallLightning":
					{
						slot3GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/BallLightning");
					}
					break;
				case "BeeSummon":
					{
						slot3GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/BeeSummon");
					}
					break;
				case "MekigSummon":
					{
						slot3GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/GnomeSummon");
					}
					break;
				case "StoneSkin":
					{
						slot3GO.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/Thorns");
					}
					break;
			}
			#endregion
		}
	}

	// Update is called once per frame
	void Update()
    {
		meteorXP = Mathf.Clamp(meteorXP, 0, 100);
		meteorshowerXP = Mathf.Clamp(meteorshowerXP, 0, 100);
		greatswordXP = Mathf.Clamp(greatswordXP, 0, 100);
		conflagrationXP = Mathf.Clamp(conflagrationXP, 0, 100);

		iceprismXP = Mathf.Clamp(iceprismXP, 0, 100);
		iceballXP = Mathf.Clamp(iceballXP, 0, 100);
		frostblastXP = Mathf.Clamp(frostblastXP, 0, 100);
		vortexXP = Mathf.Clamp(vortexXP, 0, 100);

		rockblastXP = Mathf.Clamp(rockblastXP, 0, 100);
		beeXP = Mathf.Clamp(beeXP, 0, 100);
		mekigneerXP = Mathf.Clamp(mekigneerXP, 0, 100);
		stoneskinXP = Mathf.Clamp(stoneskinXP, 0, 100);

		voltdaggerXP = Mathf.Clamp(voltdaggerXP, 0, 100);
		lightlanceXP = Mathf.Clamp(lightlanceXP, 0, 100);
		chainlightXP = Mathf.Clamp(chainlightXP, 0, 100);
		balllightXP = Mathf.Clamp(balllightXP,0,100);


		if (SkillUI == null)
		{
			SkillUI = GameObject.Find("SkillSystemCanvas");

			if (selectGO == null)
			{
				selectGO = GameObject.Find("SelectBG");

				if (slot1selectGO == null)
					slot1selectGO = GameObject.Find("Slot1Select");

				if (slot2selectGO == null)
					slot2selectGO = GameObject.Find("Slot2Select");

				if (slot3selectGO == null)
					slot3selectGO = GameObject.Find("Slot3Select");

				selectGO.SetActive(false);
			}

			if (leftarrowGO == null)
			{
				leftarrowGO = GameObject.FindGameObjectWithTag("LeftArrow");
				Debug.Log(leftarrowGO);
			}

			if (rightarrowGO == null)
			{
				rightarrowGO = GameObject.FindGameObjectWithTag("RightArrow");
				Debug.Log(rightarrowGO);
			}

			if (mainselectGO == null)
				mainselectGO = GameObject.FindGameObjectWithTag("MainSelect");

			if (baseoutline == null)
			{
				baseoutline = GameObject.Find("BaseOutline");
				baseoutline.SetActive(false);
			}

			if (mainoutline == null)
			{
				mainoutline = GameObject.Find("MainOutline");
				mainoutline.SetActive(false);
			}

			if (typetext == null)
				typetext = GameObject.Find("TypeTextGO").GetComponent<Text>();

			if (desText == null)
				desText = GameObject.Find("CurDesc").GetComponentInChildren<Text>();

			if (nameText == null)
				nameText = GameObject.Find("NameTextGO").GetComponent<Text>();

			if (baseselectGO == null)
				baseselectGO = GameObject.Find("BaseSelectGO");

			if (selectImage == null)
				selectImage = GameObject.Find("CurImage");

			if (rightimage == null)
				rightimage = GameObject.Find("RightImage");

			if (leftimage == null)
				leftimage = GameObject.Find("LeftImage");

			if (tier1 == null)
				tier1 = GameObject.Find("Tier1");

			if (tier2 == null)
				tier2 = GameObject.Find("Tier2");

			if (tier3 == null)
				tier3 = GameObject.Find("Tier3");

			if (baseslotGO == null)
				baseslotGO = GameObject.Find("BaseSlot");

			if (slot1GO == null)
				slot1GO = GameObject.Find("Slot1");

			if (slot2GO == null)
				slot2GO = GameObject.Find("Slot2");

			if (slot3GO == null)
				slot3GO = GameObject.Find("Slot3");

			if (equipGO == null)
				equipGO = GameObject.Find("EquipGO");

			if (quitGO == null)
				quitGO = GameObject.Find("CloseGO");

			SetListeners();

			SkillUI.SetActive(false);
		}
	}

	#region General Canvas Functions

	public void MainTypeSwap()
	{
		if (curSkill == firegreatsword || curSkill == frostblast || curSkill == rockblast || curSkill == voltdagger)
		{
			curSkill = mekigsummon;
			ssSource.PlayOneShot(pickClip);
			RightArrowClick();
			typetext.text = "Greater Spells";
			mainoutline.SetActive(true);
			baseoutline.SetActive(false);
		}
		else curSkill = meteor;
	}

	public void BaseTypeSwap()
	{
		if (curSkill != firegreatsword && curSkill != frostblast && curSkill != rockblast && curSkill != voltdagger)
		{
			curSkill = rockblast;
			ssSource.PlayOneShot(pickClip);
			RightArrowClick();
			typetext.text = "Lesser Spells";
			mainoutline.SetActive(false);
			baseoutline.SetActive(true);
		}
	}

	public void RightArrowClick()
	{
		ssSource.PlayOneShot(pickClip);
		switch (curSkill)
		{
			case "Meteor":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Meteor");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Pyromaniac");
					MeteorShowerSelect();
					nameText.text = "Meteor Shower";
				}
				break;
			case "MeteorShower":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/MeteorShower");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IcePrism");
					ConflagrationSelect();
					nameText.text = "Conflagration";
				}
				break;
			case "Conflagration":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Pyromaniac");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IceBall");
					IcePrismSelect();
					nameText.text = "Ice Prism";
				}
				break;
			case "IcePrism":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IcePrism");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/Vortex");
					nameText.text = "Ice Ball";
					IceBallSelect();
				}
				break;
			case "IceBall":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IceBall");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/LightningLance");
					VortexSelect();
					nameText.text = "Vortex";
				}
				break;
			case "Vortex":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/Vortex");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/GnomeSummon");
					LightningLanceSelect();
					nameText.text = "Lightning Lance";
				}
				break;
			case "LightningLance":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/LightningLance");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Meteor");
					MekigneerSelect();
					nameText.text = "Gnomish Turret";
				}
				break;
			case "MekigSummon":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/GnomeSummon");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/MeteorShower");
					MeteorSelect();
					nameText.text = "Meteor";
				}
				break;

			case "FireGreatsword":
				{
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/VoltDagger");
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireGreatsword");
					FrostBlastSelect();
					nameText.text = "Frost Blast";
				}
				break;
			case "Frostblast":
				{
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/RockBlast");
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/FrostBlast");
					VoltDaggerSelect();
					nameText.text = "Volt Dagger";
				}
				break;
			case "VoltDagger":
				{
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireGreatsword");
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/VoltDagger");
					RockBlastSelect();
					nameText.text = "Rock Blast";
				}
				break;
			case "RockBlast":
				{
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/FrostBlast");
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/RockBlast");
					FireGreatswordSelect();
					nameText.text = "Fire Greatsword";
				}
				break;
		}
	}

	public void LeftArrowClick()
	{
		ssSource.PlayOneShot(pickClip);
		switch (curSkill)
		{
			case "Meteor":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/LightningLance");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Meteor");
					MekigneerSelect();
					nameText.text = "Gnomish Turret";
				}
				break;
			case "MeteorShower":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/GnomeSummon");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/MeteorShower");
					MeteorSelect();
					nameText.text = "Meteor";
				}
				break;
			case "Conflagration":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Meteor");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Pyromaniac");
					MeteorShowerSelect();
					nameText.text = "Meteor Shower";
				}
				break;
			case "IcePrism":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/MeteorShower");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IcePrism");
					ConflagrationSelect();
					nameText.text = "Conflagration";
				}
				break;
			case "IceBall":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/Pyromaniac");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IceBall");
					IcePrismSelect();
					nameText.text = "Ice Prism";
				}
				break;
			case "Vortex":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IcePrism");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/Vortex");
					IceBallSelect();
					nameText.text = "Ice Ball";
				}
				break;
			case "LightningLance":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/IceBall");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/LightningLance");
					VortexSelect();
					nameText.text = "Vortex";
				}
				break;
			case "MekigSummon":
				{
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/Vortex");
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/GnomeSummon");
					LightningLanceSelect();
					nameText.text = "Lightning Lance";
				}
				break;


			case "FireGreatsword":
				{
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireGreatsword");
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/VoltDagger");
					RockBlastSelect();
					nameText.text = "Rock Blast";
				}
				break;
			case "Frostblast":
				{
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/FrostBlast");
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/RockBlast");
					FireGreatswordSelect();
					nameText.text = "Fire Greatsword";
				}
				break;
			case "VoltDagger":
				{
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Electric/VoltDagger");
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Fire/FireGreatsword");
					FrostBlastSelect();
					nameText.text = "Frost Blast";
				}
				break;
			case "RockBlast":
				{
					rightimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Earth/RockBlast");
					leftimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Water/FrostBlast");
					VoltDaggerSelect();
					nameText.text = "Volt Dagger";
				}
				break;
		}

	}

	public void SetListeners()
	{
		RemoveListeners();

		#region Listener Set
		#region General Listeners

		slot1selectGO.GetComponent<Button>().onClick.AddListener(SetSkill1);
		slot2selectGO.GetComponent<Button>().onClick.AddListener(SetSkill2);
		slot3selectGO.GetComponent<Button>().onClick.AddListener(SetSkill3);

		quitGO.GetComponent<Button>().onClick.AddListener(CloseUI);

		leftarrowGO.GetComponent<Button>().onClick.AddListener(LeftArrowClick);
		rightarrowGO.GetComponent<Button>().onClick.AddListener(RightArrowClick);

		mainselectGO.GetComponent<Button>().onClick.AddListener(MainTypeSwap);
		baseselectGO.GetComponent<Button>().onClick.AddListener(BaseTypeSwap);

		equipGO.GetComponent<Button>().onClick.AddListener(Equip);
		#endregion
		#endregion
	}

	public void RemoveListeners()
	{
		#region General Listeners

		slot1selectGO.GetComponent<Button>().onClick.RemoveAllListeners();
		slot2selectGO.GetComponent<Button>().onClick.RemoveAllListeners();
		slot3selectGO.GetComponent<Button>().onClick.RemoveAllListeners();

		leftarrowGO.GetComponent<Button>().onClick.RemoveAllListeners();
		rightarrowGO.GetComponent<Button>().onClick.RemoveAllListeners();

		quitGO.GetComponent<Button>().onClick.RemoveAllListeners();

		mainselectGO.GetComponent<Button>().onClick.RemoveAllListeners();
		baseselectGO.GetComponent<Button>().onClick.RemoveAllListeners();

		equipGO.GetComponent<Button>().onClick.RemoveAllListeners();
		#endregion
	}

	public void OpenUI()
	{
		SkillUI.SetActive(true);
		SetListeners();
		curSkill = mekigsummon;
		RightArrowClick();
		mainoutline.SetActive(true);
	}

	public void CloseUI()
	{
		SkillUI.SetActive(false);
		RemoveListeners();
	}

	public void SetSkill1()
	{
		if(slot2equip == curSkill)
		{
			slot2equip = null;
			slot2GO.GetComponent<Image>().sprite = null;
		}
		if (slot3equip == curSkill)
		{
			slot3equip = null;
			slot3GO.GetComponent<Image>().sprite = null;
		}
		slot1equip = curSkill;
		slot1GO.GetComponent<Image>().sprite = CurImage;
		selectGO.SetActive(false);
		ssSource.PlayOneShot(buyClip);
	}

	public void SetSkill2()
	{
		if (slot1equip == curSkill)
		{
			slot1equip = null;
			slot1GO.GetComponent<Image>().sprite = null;
		}
		if (slot3equip == curSkill)
		{
			slot3equip = null;
			slot3GO.GetComponent<Image>().sprite = null;
		}
		slot2equip = curSkill;
		slot2GO.GetComponent<Image>().sprite = CurImage;
		selectGO.SetActive(false);
		ssSource.PlayOneShot(buyClip);
	}

	public void SetSkill3()
	{
		if (slot1equip == curSkill)
		{
			slot1equip = null;
			slot1GO.GetComponent<Image>().sprite = null;
		}
		if (slot2equip == curSkill)
		{
			slot3equip = curSkill;
			slot2equip = null;
			slot2GO.GetComponent<Image>().sprite = null;
		}
		slot3equip = curSkill;
		slot3GO.GetComponent<Image>().sprite = CurImage;
		selectGO.SetActive(false);
		ssSource.PlayOneShot(buyClip);
	}

	public void Equip()
	{
		if (type == 1)
		{
			selectGO.SetActive(true);
			ssSource.PlayOneShot(pickClip);
		}

		if(type == 0)
		{
			baseequip = curSkill;
			baseslotGO.GetComponent<Image>().sprite = CurImage;
			ssSource.PlayOneShot(buyClip);
		}
	}

	public void SetCurSkill()
	{
		selectImage.GetComponent<Image>().sprite = CurImage;
		desText.text = curDes;
	}
	#endregion

	#region Fire Functions
	private void MeteorSelect()
	{
		curtier1 = Resources.Load<Sprite>("Fire/FireDamage");
		curtier2 = Resources.Load<Sprite>("Fire/FireManaCost");
		curtier3 = Resources.Load<Sprite>("Fire/TripleMeteor");
		curDes = "Summon a meteor from the sky to crash down on an enemy. Hold M to change where the meteor will land, and release to summon.";
		CurImage = Resources.Load<Sprite>("Fire/Meteor");
		curSkill = meteor;
		ssSource.PlayOneShot(pickClip);
		type = 1;

		SetCurSkill();
	}
	private void FireGreatswordSelect()
	{
		curtier1 = Resources.Load<Sprite>("Fire/FireDamage");
		curtier2 = Resources.Load<Sprite>("Fire/FireManaCost");
		curtier3 = Resources.Load<Sprite>("Fire/FireGreatsword");
		curDes = "Conjure a fire greatsword that will slash all enemies in front of you and apply a burn.";
		CurImage = Resources.Load<Sprite>("Fire/FireGreatsword");
		curSkill = firegreatsword;
		ssSource.PlayOneShot(pickClip);
		type = 0;

		SetCurSkill();
	}
	private void MeteorShowerSelect()
	{
		curtier1 = Resources.Load<Sprite>("Fire/FireDamage");
		curtier2 = Resources.Load<Sprite>("Fire/FireManaCost");
		curtier3 = Resources.Load<Sprite>("Fire/MeteorShower");
		curDes = "Summon a shower of meteors from the sky to hail down on enemies.";
		CurImage = Resources.Load<Sprite>("Fire/MeteorShower");
		curSkill = meteorshower;
		ssSource.PlayOneShot(pickClip);
		type = 1;

		SetCurSkill();
	}
	private void ConflagrationSelect()
	{
		curtier1 = Resources.Load<Sprite>("Fire/FireDamage");
		curtier2 = Resources.Load<Sprite>("Fire/FireManaCost");
		curtier3 = Resources.Load<Sprite>("Fire/Pyromaniac");
		curDes = "Engulf yourself in flames, dealing constant damage to nearby enemies. Killing enemies makes the fire bigger.";
		CurImage = Resources.Load<Sprite>("Fire/Pyromaniac");
		curSkill = conflagration;
		ssSource.PlayOneShot(pickClip);
		type = 1;

		SetCurSkill();
	}
	#endregion
	#region Water Functions
	private void FrostBlastSelect()
	{
		curtier1 = Resources.Load<Sprite>("Water/WaterDamage");
		curtier2 = Resources.Load<Sprite>("Water/WaterManaCost");
		curtier3 = Resources.Load<Sprite>("Water/FrostBlast");
		curDes = "Shoot out an icy blast that will pierce enemies and apply a stack of frost.";
		CurImage = Resources.Load<Sprite>("Water/FrostBlast");
		curSkill = frostblast;
		ssSource.PlayOneShot(pickClip);
		type = 0;

		SetCurSkill();
	}
	private void IcePrismSelect()
	{
		curtier1 = Resources.Load<Sprite>("Water/WaterDamage");
		curtier2 = Resources.Load<Sprite>("Water/WaterManaCost");
		curtier3 = Resources.Load<Sprite>("Water/IcePrism");
		curDes = "Fire a constant beam that grows in size until it reaches its max lenght, piercing enemies and applying frost stacks quickly.";
		CurImage = Resources.Load<Sprite>("Water/IcePrism");
		curSkill = iceprism;
		ssSource.PlayOneShot(pickClip);
		type = 1;

		SetCurSkill();
	}
	private void IceBallSelect()
	{
		curtier1 = Resources.Load<Sprite>("Water/WaterDamage");
		curtier2 = Resources.Load<Sprite>("Water/WaterManaCost");
		curtier3 = Resources.Load<Sprite>("Water/IceBall");
		curDes = "Throw a ball of ice out in front of you that will explode into ice shards that damage enemies and apply stacks of frost.";
		CurImage = Resources.Load<Sprite>("Water/IceBall");
		curSkill = iceball;
		ssSource.PlayOneShot(pickClip);
		type = 1;

		SetCurSkill();
	}
	private void VortexSelect()
	{
		curtier1 = Resources.Load<Sprite>("Water/WaterDamage");
		curtier2 = Resources.Load<Sprite>("Water/WaterManaCost");
		curtier3 = Resources.Load<Sprite>("Water/Vortex");
		curDes = "Draw in nearby enemies by summoning a Vortex in front of you.";
		CurImage = Resources.Load<Sprite>("Water/Vortex");
		curSkill = vortex;
		ssSource.PlayOneShot(pickClip);
		type = 1;

		SetCurSkill();
	}
	#endregion
	#region Earth Functions
	private void RockBlastSelect()
	{
		curtier1 = Resources.Load<Sprite>("Earth/EarthDamage");
		curtier2 = Resources.Load<Sprite>("Earth/EarthManaCost");
		curtier3 = Resources.Load<Sprite>("Earth/RockBlast");
		curDes = "Summon a boulder out of the ground and split it into shards that are fired in front of you.";
		CurImage = Resources.Load<Sprite>("Earth/RockBlast");
		curSkill = rockblast;
		ssSource.PlayOneShot(pickClip);
		type = 0;

		SetCurSkill();
	}
	private void MekigneerSelect()
	{
		curtier1 = Resources.Load<Sprite>("Earth/EarthDamage");
		curtier2 = Resources.Load<Sprite>("Earth/EarthManaCost");
		curtier3 = Resources.Load<Sprite>("Earth/MkII");
		curDes = "Summon a Gnome Mekigneer that shoots a spread of bullets at nearby enemies. Using this ability will cause the Mekigneer to create a turret that will fire at enemies.";
		CurImage = Resources.Load<Sprite>("Earth/GnomeSummon");
		curSkill = mekigsummon;
		ssSource.PlayOneShot(pickClip);
		type = 1;

		SetCurSkill();
	}
	#endregion
	#region Electricity Functions
	private void VoltDaggerSelect()
	{
		curtier1 = Resources.Load<Sprite>("Electric/ElectricDamage");
		curtier2 = Resources.Load<Sprite>("Electric/LightningManaCost");
		curtier3 = Resources.Load<Sprite>("Electric/VoltDagger");
		curDes = "Quickly throw out three charged daggers in front of you, dealing low damage with a low cooldown.";
		CurImage = Resources.Load<Sprite>("Electric/VoltDagger");
		curSkill = voltdagger;
		ssSource.PlayOneShot(pickClip);
		type = 0;

		SetCurSkill();
		
	}
	private void LightningLanceSelect()
	{
		curtier1 = Resources.Load<Sprite>("Electric/ElectricDamage");
		curtier2 = Resources.Load<Sprite>("Electric/LightningManaCost");
		curtier3 = Resources.Load<Sprite>("Electric/LightningLance");
		curDes = "Throw a lightning lance in front of you that pierces and calls down lightning on targets it hits.";
		CurImage = Resources.Load<Sprite>("Electric/LightningLance");
		curSkill = lightninglance;
		ssSource.PlayOneShot(pickClip);
		type = 1;

		SetCurSkill();
	}
	#endregion
}
