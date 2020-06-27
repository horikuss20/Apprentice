using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicSystem : MonoBehaviour
{
    public enum AbilityNum
    {
        Fire,
        Water,
        Electric,
        Earth
    }
    public AbilityNum abilityNum;
    //public float MaxMana = 100f;
    //public float currMana;
    //public Slider ManaSlider;
    public Image Background;
    private Color FullManaColor = Color.red;
    private Color ManaColor = new Color(0, 0, 0, .1f);
    public Text AbilityName;
	bool thunderON;
	GameObject GM;
    MagicSpells MS;
    //public int AbilityNum = 0;
    public bool canRegen = false;
    //Element Images
    public Image ability1Image;
    public Image ability2Image;
    public Image ability3Image;
    public Image ability4Image;
    public Image FreezeAbilityImage;

    public bool activeAbillity1 = false;
    public bool activeAbillity2 = false;
    public bool activeAbillity3 = false;
    public bool activeAbillity4 = false;

    float regenTimer;

    //Element Attunement
    private bool isAttuned = false;

    //Spell Cooldown Timer
    public float Ability1CoolDown;
    public float Ability2CoolDown;
    public float Ability3CoolDown;
    public float Ability4Cooldown;

    void Start()
    {
        //currMana = MaxMana;
		GM = GameObject.Find("GameManager");
        MS = GameObject.Find("PlayerFunctionality").GetComponent<MagicSpells>();
        //ability2ImageFill.enabled = false;
        //ability3ImageFill.enabled = false;
        //ability4ImageFill.enabled = false;
        //LightningImageFill.enabled = false;
    }

    void Update()
    {
       
        //Cooldown code
        Cooldown();

		#region UI Swap
		switch (GM.GetComponent<SkillSystemNew>().baseequip)
		{
			case "FireGreatsword":
				{
					ability1Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/FireSwordFill");
				}
				break;
			case "Frostblast":
				{

					ability1Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/FrostBlastFill");
				}
				break;
			case "VoltDagger":
				{
					ability1Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/VoltFill");
				}
				break;
			case "RockBlast":
				{
					ability1Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/RockBlastFill");
				}
				break;
		}

		switch (GM.GetComponent<SkillSystemNew>().slot1equip)
		{
			case "Meteor":
				{
					ability2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/MeteorFill");
				}
				break;
			case "MeteorShower":
				{
					ability2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/MeteorShowerFill");
				}
				break;
			case "Conflagration":
				{
					ability2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/PyromaniacFill");
				}
				break;
			case "IcePrism":
				{
					ability2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/IcePrismFill");
				}
				break;
			case "IceBall":
				{
					ability2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/IceBallFill");
				}
				break;
			case "Vortex":
				{
					ability2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/VortexFill");
				}
				break;
			case "LightningLance":
				{
					ability2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/LightningLanceFill");
				}
				break;
			case "ChainLightning":
				{
					ability2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/ChainFill");
				}
				break;
			case "BallLightning":
				{
					ability2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/OrbFill");
				}
				break;
			case "BeeSummon":
				{
					ability2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/BeeFill");
				}
				break;
			case "MekigSummon":
				{
					ability2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/GnomeFill");
				}
				break;
			case "StoneSkin":
				{
					ability2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/ThornsFill");
				}
				break;
		}
		switch (GM.GetComponent<SkillSystemNew>().slot2equip)
		{
			case "Meteor":
				{
					ability3Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/MeteorFill");
				}
				break;
			case "MeteorShower":
				{
					ability3Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/MeteorShowerFill");
				}
				break;
			case "Conflagration":
				{
					ability3Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/PyromaniacFill");
				}
				break;
			case "IcePrism":
				{
					ability3Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/IcePrismFill");
				}
				break;
			case "IceBall":
				{
					ability3Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/IceBallFill");
				}
				break;
			case "Vortex":
				{
					ability3Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/VortexFill");
				}
				break;
			case "LightningLance":
				{
					ability3Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/LightningLanceFill");
				}
				break;
			case "ChainLightning":
				{
					ability3Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/ChainFill");
				}
				break;
			case "BallLightning":
				{
					ability3Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/OrbFill");
				}
				break;
			case "BeeSummon":
				{
					ability3Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/BeeFill");
				}
				break;
			case "MekigSummon":
				{
					ability3Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/GnomeFill");
				}
				break;
			case "StoneSkin":
				{
					ability3Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/ThornsFill");
				}
				break;
		}
		switch (GM.GetComponent<SkillSystemNew>().slot3equip)
		{
			case "Meteor":
				{
					ability4Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/MeteorFill");
				}
				break;
			case "MeteorShower":
				{
					ability4Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/MeteorShowerFill");
				}
				break;
			case "Conflagration":
				{
					ability4Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/PyromaniacFill");
				}
				break;
			case "IcePrism":
				{
					ability4Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/IcePrismFill");
				}
				break;
			case "IceBall":
				{
					ability4Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/IceBallFill");
				}
				break;
			case "Vortex":
				{
					ability4Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/VortexFill");
				}
				break;
			case "LightningLance":
				{
					ability4Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/LightningLanceFill");
				}
				break;
			case "ChainLightning":
				{
					ability4Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/ChainFill");
				}
				break;
			case "BallLightning":
				{
					ability4Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/OrbFill");
				}
				break;
			case "BeeSummon":
				{
					ability4Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/BeeFill");
				}
				break;
			case "MekigSummon":
				{
					ability2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/GnomeFill");
				}
				break;
			case "StoneSkin":
				{
					ability4Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("FillImages/ThornsFill");
				}
				break;
		}
		#endregion

		//    AttuneCountDown();
		//ManaSlider.value = currMana;

		regenTimer -= Time.deltaTime;
		regenTimer = Mathf.Clamp(regenTimer, 0, 3);

		//if(currMana<=0)
		//{
		//	thunderON = false;
		//}

		if(thunderON)
		{
			if (GM.GetComponent<PendantSystemNew>().pSide1 == GM.GetComponent<PendantSystemNew>().Shamrock || GM.GetComponent<PendantSystemNew>().pSide2 == GM.GetComponent<PendantSystemNew>().Shamrock)
			{
				int shamNum = Random.Range(1, 10);
				Debug.Log(shamNum + " On MS");
				if (shamNum == 3)
				{
					GM.GetComponentInChildren<AudioSource>().PlayOneShot(GM.GetComponent<PendantSystemNew>().shamrockSwap);
					//currMana -= 0;
				}
				//else currMana -= .4f;
			}
			//else currMana -= .4f;
		}

		//if(regenTimer == 0 && currMana <= MaxMana && MS.ConstantSpellInUse == false)
		//{
		//	canRegen = true;
		//}

   //     if (canRegen)
   //     {
   //         Background.color = Color.Lerp(ManaColor, FullManaColor, currMana / MaxMana);
			//if(GM.GetComponent<PendantSystemNew>().pSide1 == GM.GetComponent<PendantSystemNew>().Eagle || GM.GetComponent<PendantSystemNew>().pSide2 == GM.GetComponent<PendantSystemNew>().Eagle)
			//{
			//	currMana += 1f;
			//}
			//if (GM.GetComponent<PendantSystemNew>().pSide1 != GM.GetComponent<PendantSystemNew>().Eagle && GM.GetComponent<PendantSystemNew>().pSide2 != GM.GetComponent<PendantSystemNew>().Eagle)
			//{
			//	currMana += .5f;
			//}
   //     }
   //     if (currMana >= MaxMana)
   //     {
   //         canRegen = false;
   //     }
        //basic magic

        //if (currMana != MaxMana && currMana <= 0)
        //{
        //    Background.color = Color.Lerp(ManaColor, FullManaColor, currMana / MaxMana);
        //    currMana += .5f;
        //}
        //else
        //{
        Background.color = FullManaColor;
            if (Input.GetKeyDown(KeyCode.M))
            {
				canRegen = false;
				if(GM.GetComponent<PendantSystemNew>().pSide1 == GM.GetComponent<PendantSystemNew>().Wizard || GM.GetComponent<PendantSystemNew>().pSide2 == GM.GetComponent<PendantSystemNew>().Wizard)
				{
					regenTimer = 1;
				}
				if (GM.GetComponent<PendantSystemNew>().pSide1 != GM.GetComponent<PendantSystemNew>().Wizard && GM.GetComponent<PendantSystemNew>().pSide2 != GM.GetComponent<PendantSystemNew>().Wizard)
				{
					regenTimer = 2;
				}
                switch(abilityNum)
                {
                    case AbilityNum.Fire:
                        break;
                    case AbilityNum.Water:
                        break;
                    case AbilityNum.Electric:
                        break;
                    case AbilityNum.Earth:
                        break;
                    default:
                        break;
                }
                ////Fire
                //if (AbilityNub == 1)
                //{
                //    currMana -= 30;
                //}
                ////Water
                //if (AbilityNub == 2)
                //{
                //    currMana -= 10;
                //}
                ////Lightning
                //if (AbilityNub == 3)
                //{
                //    currMana -= 15;
                //}
                ////Earth
                //if (AbilityNub == 4)
                //{
                //    currMana -= 20;

                //}
            }
        //Atune magic
        //ManaSlider.value = currMana;
        //if (currMana != MaxMana)
        //{
        //    Background.color = Color.Lerp(ManaColor, FullManaColor, currMana / MaxMana);
        //    currMana += .5f;
        //}
        //else
        //if (Input.GetKeyDown(KeyCode.N) && isAttuned == false)
        //{
        //    //Fire
        //    if (AbilityNub == 1)
        //    {
        //        ability2ImageFill.enabled = true;
        //    }
        //    //Water
        //    if (AbilityNub == 2)
        //    {
        //        ability3ImageFill.enabled = true;
        //    }
        //    //Lightning
        //    if (AbilityNub == 3)
        //    {
        //        LightningImageFill.enabled = true;
        //    }
        //    //Earth
        //    if (AbilityNub == 4)
        //    {
        //        ability4ImageFill.enabled = true;
        //    }
        //    currMana = 0;
        //}

        // For changing the look of the mana bar and swaping ability types
        //Fire
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(abilityNum == AbilityNum.Earth)
            {
                abilityNum = AbilityNum.Fire;
            }
            else
            {
                abilityNum += 1;
            }
           
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(abilityNum == AbilityNum.Fire)
            {
                abilityNum = AbilityNum.Earth;
            }
            else
            {
                abilityNum -= 1;
            }
        }

        if (abilityNum == AbilityNum.Fire)
        {
            AbilityName.text = "Fire Wall";
            FullManaColor = Color.red;
            abilityNum = AbilityNum.Fire;
            if (!isAttuned)
            {
               
            }
        }
        //Water
        if (abilityNum == AbilityNum.Water)
        {
            FullManaColor = Color.blue;
            AbilityName.text = "Icicle";
            abilityNum = AbilityNum.Water;
            if (!isAttuned)
            {
              
            }
        }
        //Electric
        if (abilityNum == AbilityNum.Electric)
        {
            FullManaColor = Color.yellow;
            AbilityName.text = "Electric Ball";
            abilityNum = AbilityNum.Electric;
            if (!isAttuned)
            {
               
               
            }

        }
        //Earth
        if (abilityNum == AbilityNum.Earth)
        {
            FullManaColor = Color.green;
            AbilityName.text = "Sonic Leaf";
            abilityNum = AbilityNum.Earth;
            if (!isAttuned)
            {

            }

        }

        //Setting image to be active or not for the atunement magic
        //Fire
        //if (AbilityNub == 1)
        //{
        //    if (isAttuned == false)
        //    {
        //        ability2Image.enabled = true;
        //        ability3Image.enabled = false;
        //        ability4Image.enabled = false;
        //        LightningImage.enabled = false;
        //    }
        //}
        ////Water
        //if (AbilityNub == 2)
        //{
        //    if (isAttuned == false)
        //    {
        //        ability2Image.enabled = false;
        //        ability3Image.enabled = true;
        //        ability4Image.enabled = false;
        //        LightningImage.enabled = false;
        //    }

        //}
        ////Lightning
        //if (AbilityNub == 3)
        //{
        //    if (isAttuned == false)
        //    {
        //        ability2Image.enabled = false;
        //        ability3Image.enabled = false;
        //        ability4Image.enabled = false;
        //        LightningImage.enabled = true;
        //    }

        //}
        ////Earth
        //if (AbilityNub == 4)
        //{
        //    if (isAttuned == false)
        //    {
        //        ability2Image.enabled = false;
        //        ability3Image.enabled = false;
        //        ability4Image.enabled = true;
        //        LightningImage.enabled = false;
        //    }

        //}
       
    }
    //void AttuneCountDown()
    //{
    //    if (ability2ImageFill.enabled == true)
    //    {
    //        isAttuned = true;
    //        ability2ImageFill.fillAmount -= .001f;
    //        if (ability2ImageFill.fillAmount == 0)
    //        {
    //            ability2ImageFill.enabled = false;
    //            isAttuned = false;
    //        }

    //    }
    //    if (ability3ImageFill.enabled == true)
    //    {
    //        isAttuned = true;
    //        ability3ImageFill.fillAmount -= .001f;
    //        if (ability3ImageFill.fillAmount == 0)
    //        {
    //            ability3ImageFill.enabled = false;
    //            isAttuned = false;
    //        }

    //    }
    //    if (LightningImageFill.enabled == true)
    //    {
    //        isAttuned = true;
    //        LightningImageFill.fillAmount -= .001f;
    //        if (LightningImageFill.fillAmount == 0)
    //        {
    //            LightningImageFill.enabled = false;
    //            isAttuned = false;
    //        }

    //    }
    //    if (ability4ImageFill.enabled == true)
    //    {
    //        isAttuned = true;
    //        ability4ImageFill.fillAmount -= .001f;
    //        if (ability4ImageFill.fillAmount == 0)
    //        {
    //            ability4ImageFill.enabled = false;
    //            isAttuned = false;
    //        }

    //    }

    //}
    public void Cooldown()
    {
        //Thunder cooldown
        if (ability1Image.fillAmount != 1)
        {
            MS.canAbility1 = false;
            if (activeAbillity1 == false)
            {
                ability1Image.fillAmount += (Time.smoothDeltaTime / Ability1CoolDown);
            }
            
        }
        if (ability1Image.fillAmount == 1 && MS.canAbility1 == false)
        {
            MS.canAbility1 = true;
        }
        //Fire cooldown
        if (ability2Image.fillAmount != 1)
        {
            MS.canAbility2 = false;
            if (activeAbillity2 == false)
            {
                ability2Image.fillAmount += (Time.smoothDeltaTime / Ability2CoolDown);
            }
        }
       if(ability2Image.fillAmount == 1 && MS.canAbility2 == false)
        {
            MS.canAbility2 = true;
        }
        //Water Cooldown
        if (ability3Image.fillAmount != 1)
        {
            MS.canAbility3 = false;
            if (activeAbillity3 == false)
            {
                ability3Image.fillAmount += (Time.smoothDeltaTime / Ability3CoolDown);
            }
        }
        if (ability3Image.fillAmount == 1 && MS.canAbility3 == false)
        {
            MS.canAbility3 = true;
        }
        //Earth
        if (ability4Image.fillAmount != 1)
        {
            MS.canAbility4 = false;
            if (activeAbillity4 == false)
            {
                ability4Image.fillAmount += (Time.smoothDeltaTime / Ability4Cooldown);
            }
        }
        if (ability4Image.fillAmount == 1 && MS.canAbility4 == false)
        {
            MS.canAbility4 = true;
        }
        //Freeze
        if(FreezeAbilityImage.fillAmount != 1  && MS.canFreezeAbility == false)
        {
            FreezeAbilityImage.fillAmount += (Time.smoothDeltaTime / 1);
        }
        if(FreezeAbilityImage.fillAmount == 1f)
        {
            MS.canFreezeAbility = true;
        }
    } 
}
