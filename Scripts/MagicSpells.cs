using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicSpells : MonoBehaviour
{
    #region FSpells
    public enum BaseSpells
    {
        fireSword,
        frostBlast,
        rockBlast,
        shockBolt,
        none,
    }
    #endregion
    #region MainSpells
    public enum MainSpells
    {
       
        Conflagration,
        meteor,
        meteorshower,
       
        iceprism,
        iceball,
		Vortex,

        ChainLightning,
        LightningLance,
		BallLightning,

        Gnome,
        Bee,
		StoneSkin,

        none,
    }
    #endregion

    public GameObject freezeBolt;
    //Fire magic game objects
    public GameObject FireSword;
    public GameObject BigFireBall;
    public GameObject MeteorOutline;
    public GameObject Firewall;
    public GameObject MeteorShower;
    public GameObject UpgradedMeteorOutline;
    public GameObject UpgradedMeteorShower;
    public GameObject engulfFlames;
    //Water magic game objects
    public GameObject IceBlast;
    public GameObject SnowBall;
    public GameObject Icicle;
    public GameObject IceBeam;
    public GameObject IceBall;
    //public GameObject IcePrism;
    //public GameObject IceBall;
    //public GameObject UpgradedIcePrism;
    //public GameObject UpgradedIceBall;

    //Earth Gameobjects
    public GameObject Rock;
    public GameObject TurretOutline;

    //Lightning Attacks
    public GameObject electricBolt;

    public SkillSystemNew ssn;
    public BaseSpells basespell;
    public MainSpells KSpells;
	public MainSpells LSpells;
	public MainSpells SemiSpells;

    public float oppositeFireRotation;
    public float spellUseCooldownTimer = .25f;
    public bool usingSpell = false;
    public float FireStart;
    public float FireRotation;
    public GameObject LeafAttack;
    public bool stopAttack = true;
    public GameObject tfRef;
    private GameObject WaterForm;
    public GameObject WaterAttack;
    public MagicSystem magicSystem;
    private AudioSource magicSource;
    public AudioClip leafClip;
    public AudioClip fireClip;
    public AudioClip thunderClip;
    public AudioClip iceClip;
    public AudioClip meteorClip;
    public AudioClip meteorShowerClip;
    public AudioClip freezeClip;
    public AudioClip frostClip;
    public AudioClip electricZap;
    public AudioClip lightningLance;
    public AudioClip rockBlast;
    public AudioClip iceThrow;
    Health hRef;
    GameObject GM;
    bool switchb = true;
    public Renderer[] rs;
    public string UsedButton;

    //Cooldown bool for attacking
    public bool canAbility2 = true;
    public bool canAbility3 = true;
    public bool canAbility1 = true;
    public bool canAbility4 = true;
    public bool canFreezeAbility = true;

	private int attacknum = 0;

	[SerializeField]
	float atktimer = 5f;

	// Water Form
	public bool inWaterForm = false;

    //To check if the spell uses mana continuasly uses mana
    public bool ConstantSpellInUse = false;
    public float ConstantManaUse;

	private void SpellSwap()
	{
			#region Base Spells
			if (ssn.baseequip == ssn.firegreatsword)
			{
				basespell = BaseSpells.fireSword;
			}
			if (ssn.baseequip == ssn.rockblast)
			{
				basespell = BaseSpells.rockBlast;
			}
			if (ssn.baseequip == ssn.voltdagger)
			{
				basespell = BaseSpells.shockBolt;
			}
			if (ssn.baseequip == ssn.frostblast)
			{
				basespell = BaseSpells.frostBlast;
			}
			if (ssn.baseequip == null)
			{
				basespell = BaseSpells.none;	
			}
			#endregion

			#region Main Spells

			#region K spells
			if (ssn.slot1equip == ssn.meteor)
			{
				KSpells = MainSpells.meteor;
			}
			if (ssn.slot1equip == ssn.meteorshower)
			{
				KSpells = MainSpells.meteorshower;
			}
			if (ssn.slot1equip == ssn.conflagration)
			{
				KSpells = MainSpells.Conflagration;
			}
			if (ssn.slot1equip == ssn.beesummon)
			{
				KSpells = MainSpells.Bee;
			}
			if (ssn.slot1equip == ssn.mekigsummon)
			{
				KSpells = MainSpells.Gnome;
			}
			if (ssn.slot1equip == ssn.stoneskin)
			{
				KSpells = MainSpells.StoneSkin;
			}
			if (ssn.slot1equip == ssn.iceprism)
			{
				KSpells = MainSpells.iceprism;
			}
			if (ssn.slot1equip == ssn.iceball)
			{
				KSpells = MainSpells.iceball;
			}
			if (ssn.slot1equip == ssn.vortex)
			{
				KSpells = MainSpells.Vortex;
			}
			if (ssn.slot1equip == ssn.lightninglance)
			{
				KSpells = MainSpells.LightningLance;
			}
			if (ssn.slot1equip == ssn.balllightning)
			{
				KSpells = MainSpells.BallLightning;
			}
			if (ssn.slot1equip == ssn.chainlightning)
			{
				KSpells = MainSpells.ChainLightning;
			}
			if (ssn.slot1equip == null)
			{
				KSpells = MainSpells.none;
			}
			#endregion
			#region L Spells
			if (ssn.slot2equip == ssn.meteor)
			{
				LSpells = MainSpells.meteor;
			}
			if (ssn.slot2equip == ssn.meteorshower)
			{
				LSpells = MainSpells.meteorshower;
			}
			if (ssn.slot2equip == ssn.conflagration)
			{
				LSpells = MainSpells.Conflagration;
			}
			if (ssn.slot2equip == ssn.beesummon)
			{
				LSpells = MainSpells.Bee;
			}
			if (ssn.slot2equip == ssn.mekigsummon)
			{
				LSpells = MainSpells.Gnome;
			}
			if (ssn.slot2equip == ssn.stoneskin)
			{
				LSpells = MainSpells.StoneSkin;
			}
			if (ssn.slot2equip == ssn.iceprism)
			{
				LSpells = MainSpells.iceprism;
			}
			if (ssn.slot2equip == ssn.iceball)
			{
				LSpells = MainSpells.iceball;
			}
			if (ssn.slot2equip == ssn.vortex)
			{
				LSpells = MainSpells.Vortex;
			}
			if (ssn.slot2equip == ssn.lightninglance)
			{
				LSpells = MainSpells.LightningLance;
			}
			if (ssn.slot2equip == ssn.balllightning)
			{
				LSpells = MainSpells.BallLightning;
			}
			if (ssn.slot2equip == ssn.chainlightning)
			{
				LSpells = MainSpells.ChainLightning;
			}
			if (ssn.slot2equip == null)
			{
				LSpells = MainSpells.none;
			}
			#endregion
			#region ;Spells
			if (ssn.slot3equip == ssn.meteor)
			{
				SemiSpells = MainSpells.meteor;
			}
			if (ssn.slot3equip == ssn.meteorshower)
			{
				SemiSpells = MainSpells.meteorshower;
			}
			if (ssn.slot3equip == ssn.conflagration)
			{
				SemiSpells = MainSpells.Conflagration;
			}
			if (ssn.slot3equip == ssn.beesummon)
			{
				SemiSpells = MainSpells.Bee;
			}
			if (ssn.slot3equip == ssn.mekigsummon)
			{
				SemiSpells = MainSpells.Gnome;
			}
			if (ssn.slot3equip == ssn.stoneskin)
			{
				SemiSpells = MainSpells.StoneSkin;
			}
			if (ssn.slot3equip == ssn.iceprism)
			{
				SemiSpells = MainSpells.iceprism;
			}
			if (ssn.slot3equip == ssn.iceball)
			{
				SemiSpells = MainSpells.iceball;
			}
			if (ssn.slot3equip == ssn.vortex)
			{
				SemiSpells = MainSpells.Vortex;
			}
			if (ssn.slot3equip == ssn.lightninglance)
			{
				SemiSpells = MainSpells.LightningLance;
			}
			if (ssn.slot3equip == ssn.balllightning)
			{
				SemiSpells = MainSpells.BallLightning;
			}
			if (ssn.slot3equip == ssn.chainlightning)
			{
				SemiSpells = MainSpells.ChainLightning;
			}
			if (ssn.slot3equip == null)
			{
				SemiSpells = MainSpells.none;
			}
			#endregion
			#endregion
	}

	void Awake()
    {

        WaterForm = GameObject.Find("WaterForm");
        tfRef = GameObject.Find("PlayerFunctionality");
        magicSystem = GameObject.Find("MagicUI").GetComponent<MagicSystem>();
        magicSource = GameObject.Find("SoundEffectPlayer").GetComponent<AudioSource>();
        hRef = GameObject.Find("HealthUI").GetComponent<Health>();
        GM = GameObject.Find("GameManager");
        ssn = GM.GetComponent<SkillSystemNew>();
    }

    void Update()
    {
        //ConstantSpell();
        spellUseCooldown();

        SpellSwap();

		atktimer -= Time.deltaTime;
		Mathf.Clamp(atktimer, 0, 5);
		//if(atktimer == 0)
		//{
		//	attacknum = 0;
		//	Debug.Log("timer is 0");
		//}

		//stopAttack = magicSystem.canRegen;

		//if (WaterForm.activeInHierarchy && magicSystem.currMana <= 0)
		//{
		//    DoWaterForm();
		//}

		attacknum = Mathf.Clamp(attacknum, 0, 3);

		if (attacknum == 0)
		{
			magicSystem.Ability1CoolDown = 1.2f;
			attacknum = 3;
		}

		if (Input.GetKeyDown(KeyCode.D))
        {
            FireStart = 1;
            FireRotation = 0;
            oppositeFireRotation = 180;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            FireStart = -1f;
            FireRotation = 180;
            oppositeFireRotation = 0;
        }
        if(canFreezeAbility == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(freezeBolt, transform.position + new Vector3(1 * FireStart, 0), Quaternion.Euler(0, oppositeFireRotation, 90));
                Instantiate(freezeBolt, transform.position + new Vector3(1 * FireStart, .5f), Quaternion.Euler(0, oppositeFireRotation, 90));
                Instantiate(freezeBolt, transform.position + new Vector3(1 * FireStart, -.5f), Quaternion.Euler(0, oppositeFireRotation, 90));
                magicSystem.FreezeAbilityImage.fillAmount = 0;
                canFreezeAbility = false;
                magicSource.PlayOneShot(iceClip, 0.7f);
            }
        }
      

        if (usingSpell == false)
        {
            #region JAttack
            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.Semicolon))
            {
                if (stopAttack == true)
                {
                    if (canAbility1 == true)
                    {
						atktimer = 3;
						switch (basespell)
                        {
                            case BaseSpells.fireSword:
                                {
                                    Instantiate(FireSword, transform.position + new Vector3(1 * FireStart, 0), Quaternion.Euler(0, oppositeFireRotation ,0));

									
										magicSystem.Ability1CoolDown = .15f;
									magicSystem.ability1Image.fillAmount = 0;
									attacknum--;
                                        magicSource.PlayOneShot(leafClip, 0.75f);
									
                                }
                                break;
                            case BaseSpells.frostBlast:
                                {
                                    Instantiate(IceBlast, transform.position + new Vector3(2f * FireStart, 0), Quaternion.Euler(0, oppositeFireRotation, 0));
                                    magicSource.PlayOneShot(frostClip, 1.0f);
								
										magicSystem.Ability1CoolDown = .2f;
									magicSystem.ability1Image.fillAmount = 0;

									attacknum--;
									
                                }
                                break;
                            case BaseSpells.rockBlast:
                                {
                                    Instantiate(Rock, transform.position + new Vector3(.5f * FireStart, -2), Quaternion.Euler(0, oppositeFireRotation, 0));
                                    magicSource.PlayOneShot(rockBlast, 0.7f);

									
										magicSystem.Ability1CoolDown = .2f;
									magicSystem.ability1Image.fillAmount = 0;

									attacknum--;

									
                                }
                                break;
                            case BaseSpells.shockBolt:
                                {
                                    
                                    Instantiate(electricBolt, transform.position + new Vector3(.5f * FireStart, 0), Quaternion.Euler(0, FireRotation, 0));
                                    Instantiate(electricBolt, transform.position + new Vector3(.5f * FireStart, -.5f), Quaternion.Euler(0, FireRotation, 0));
                                    Instantiate(electricBolt, transform.position + new Vector3(.5f * FireStart, .5f), Quaternion.Euler(0, FireRotation, 0));
                                    magicSource.PlayOneShot(electricZap, 1.0f);

									
										magicSystem.Ability1CoolDown = .2f;
									magicSystem.ability1Image.fillAmount = 0;


									attacknum--;

									
                                }
                                break;
                            case BaseSpells.none:
                                {

                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            #endregion
            #region KAttack
            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.Semicolon))
            {

            }
            else if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.Semicolon))
            {
                if (stopAttack == true)
                {

                    if (canAbility2 == true)
                    {
                        switch (KSpells)
                        {
                            case MainSpells.meteor:
                                {
                                    UsedButton = "k";
                                    Debug.Log("Meteor magic system");
                                    Instantiate(MeteorOutline, transform.position + new Vector3(1 * FireStart, -1f), Quaternion.Euler(0, FireRotation, 0));

                                }
                                break;
                            case MainSpells.meteorshower:
                                {
                                    UsedButton = "k";
                                    usingSpell = true;
                                    Instantiate(MeteorShower, transform.position + new Vector3(5 * FireStart, 4), Quaternion.Euler(0, FireRotation, 0));
                                    magicSource.PlayOneShot(meteorShowerClip, 1f);
                                }
                                break;
							case MainSpells.Conflagration:
								{
                                    UsedButton = "k";
                                    usingSpell = true;
                                    Instantiate(engulfFlames, transform.position , Quaternion.Euler(0, 0, 0));
                                }
								break;
							case MainSpells.iceprism:
								{
									UsedButton = "k";
									usingSpell = true;
									Instantiate(IceBeam, transform.position + new Vector3(1 * FireStart, 0), Quaternion.Euler(0, FireRotation, -90));
                                    magicSource.PlayOneShot(freezeClip, 0.5f);
                                }
								break;
							case MainSpells.iceball:
								{
                                    usingSpell = true;
                                    Instantiate(IceBall, transform.position + new Vector3(1.5f * FireStart, 0), Quaternion.Euler(0, oppositeFireRotation, 90));
                                    magicSystem.Ability2CoolDown = 1f;
                                    magicSystem.ability2Image.fillAmount = 0;
                                    canAbility2 = false;
                                    magicSource.PlayOneShot(iceThrow, 0.7f);
                                }
								break;
                            case MainSpells.Vortex:
                                {
                                    UsedButton = "k";
                                    GameObject.Instantiate(Resources.Load("Prefabs/VortexOutline") as GameObject, transform.position + new Vector3(0, 0, 0), Quaternion.Euler(0, FireRotation, 0));
                                }
                                break;
							case MainSpells.Bee:
                                {
                                    usingSpell = true;
                                    magicSystem.Ability2CoolDown = 3f;
                                    magicSystem.ability2Image.fillAmount = 0;
                                    canAbility2 = false;
                                }
                                break;
                            case MainSpells.Gnome:
                                {
                                    UsedButton = "k";                   
                                    Instantiate(TurretOutline, transform.position + new Vector3(1 * FireStart, -1f), Quaternion.Euler(0, FireRotation, 0));                            
                                }
                                break;
							case MainSpells.StoneSkin:
								{
									usingSpell = true;
									magicSystem.Ability2CoolDown = 3f;
									magicSystem.ability2Image.fillAmount = 0;
									canAbility2 = false;
								}
								break;
							case MainSpells.ChainLightning:
                                {
                                    usingSpell = true;
                                    magicSystem.Ability2CoolDown = 3f;
                                    magicSystem.ability2Image.fillAmount = 0;
                                    canAbility2 = false;
                                }

                                break;
                            case MainSpells.LightningLance:
                                {
                                    usingSpell = true;
                                    GameObject.Instantiate(Resources.Load("Prefabs/LightningSpear") as GameObject, transform.position + new Vector3(0, 0, 0), Quaternion.Euler(0,oppositeFireRotation,90));
                                    magicSystem.Ability2CoolDown = 2f;
                                    magicSystem.ability2Image.fillAmount = 0;
                                    canAbility2 = false;
                                    magicSource.PlayOneShot(lightningLance, 0.7f);
                                }
                                break;
							case MainSpells.BallLightning:
								{
									usingSpell = true;
									magicSystem.Ability2CoolDown = 3f;
									magicSystem.ability2Image.fillAmount = 0;
									canAbility2 = false;
								}
								break;
                            case MainSpells.none:
                                {

                                }
                                break;
                        }
                        
                    }
                }
            }

            #endregion
            #region LAttack
            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.Semicolon) && Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.Semicolon))
            {

            }
            else if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Semicolon))
            {
                if (stopAttack == true)
                {
                    if (canAbility3 == true)
                    {
                        switch (LSpells)
                        {
							case MainSpells.meteor:
								{
									UsedButton = "l";
									Debug.Log("Meteor magic system");
									Instantiate(MeteorOutline, transform.position + new Vector3(1 * FireStart, -1f), Quaternion.Euler(0, FireRotation, 0));

                                }
								break;
							case MainSpells.meteorshower:
								{
									UsedButton = "l";
									usingSpell = true;
									Instantiate(MeteorShower, transform.position + new Vector3(5 * FireStart, 4), Quaternion.Euler(0, FireRotation, 0));
                                    magicSource.PlayOneShot(meteorShowerClip, 0.7f);
                                }
								break;
							case MainSpells.Conflagration:
								{
                                    UsedButton = "l";
                                    usingSpell = true;
									Instantiate(engulfFlames, transform.position, Quaternion.Euler(0, 0, 0));
								}
								break;
                            case MainSpells.iceball:
                                {
                                    usingSpell = true;
                                    Instantiate(IceBall, transform.position + new Vector3(1.5f * FireStart, 0), Quaternion.Euler(0, oppositeFireRotation, 90));
                                    magicSystem.Ability3CoolDown = 1f;
                                    magicSystem.ability3Image.fillAmount = 0;
                                    canAbility3 = false;
                                    magicSource.PlayOneShot(iceThrow, 1f);
                                }
                                break;
                            case MainSpells.iceprism:
                                {
                                    UsedButton = "l";
                                    usingSpell = true;
                                    Instantiate(IceBeam, transform.position + new Vector3(1 * FireStart, 0), Quaternion.Euler(0, FireRotation, -90));
                                    magicSource.PlayOneShot(freezeClip, 0.5f);
                                }
                                break;
                            case MainSpells.Vortex:
                                {
                                    UsedButton = "l";
                                    GameObject.Instantiate(Resources.Load("Prefabs/VortexOutline") as GameObject, transform.position + new Vector3(0, 0, 0), Quaternion.Euler(0, FireRotation, 0));
                                }
                                break;
                            case MainSpells.Bee:
								{
									usingSpell = true;
									magicSystem.Ability3CoolDown = 3f;
									magicSystem.ability3Image.fillAmount = 0;
									canAbility4 = false;
								}
								break;
							case MainSpells.Gnome:
								{
                                    UsedButton = "l";
                                    Instantiate(TurretOutline, transform.position + new Vector3(1 * FireStart, -1f), Quaternion.Euler(0, FireRotation, 0));
                                }
								break;
							case MainSpells.StoneSkin:
								{
									usingSpell = true;
									magicSystem.Ability2CoolDown = 3f;
									magicSystem.ability2Image.fillAmount = 0;
									canAbility4 = false;
								}
								break;
							case MainSpells.ChainLightning:
								{
									usingSpell = true;
									magicSystem.Ability3CoolDown = 3f;
									magicSystem.ability3Image.fillAmount = 0;
									canAbility1 = false;
								}

								break;
							case MainSpells.LightningLance:
								{
									usingSpell = true;
									GameObject.Instantiate(Resources.Load("Prefabs/LightningSpear") as GameObject, transform.position + new Vector3(0, 0, 0), Quaternion.Euler(0, oppositeFireRotation, 90));
									magicSystem.Ability3CoolDown = 2f;
									magicSystem.ability3Image.fillAmount = 0;
									canAbility1 = false;
                                    magicSource.PlayOneShot(lightningLance, 0.7f);
                                }
								break;
							case MainSpells.BallLightning:
								{
									usingSpell = true;
									magicSystem.Ability3CoolDown = 3f;
									magicSystem.ability3Image.fillAmount = 0;
									canAbility4 = false;
								}
								break;
                            case MainSpells.none:
                                {

                                }
                                break;
                        }
                    }
                }
            }
            #endregion
            #region ;Attack
            if (Input.GetKeyDown(KeyCode.K) ||Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.Semicolon) && Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.Semicolon) && Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Semicolon))
            {

            }
            else if (Input.GetKeyDown(KeyCode.Semicolon))
            {
                if (stopAttack == true)
                {
                    if (canAbility4 == true)
                    {
                        switch (SemiSpells)
                        {
							case MainSpells.meteor:
								{
									UsedButton = ";";
									Debug.Log("Meteor magic system");
									Instantiate(MeteorOutline, transform.position + new Vector3(1 * FireStart, -1f), Quaternion.Euler(0, FireRotation, 0));

                                }
								break;
							case MainSpells.meteorshower:
								{
									UsedButton = ";";
									usingSpell = true;
									Instantiate(MeteorShower, transform.position + new Vector3(5 * FireStart, 4), Quaternion.Euler(0, FireRotation, 0));
                                    magicSource.PlayOneShot(meteorShowerClip, 0.7f);
                                }
								break;
							case MainSpells.Conflagration:
								{
                                    UsedButton = ";";
                                    usingSpell = true;
									Instantiate(engulfFlames, transform.position, Quaternion.Euler(0, 0, 0));
								}
								break;
                            case MainSpells.iceball:
                                {
                                    usingSpell = true;
                                    Instantiate(IceBall, transform.position + new Vector3(1.5f * FireStart, 0), Quaternion.Euler(0, oppositeFireRotation, 90));
                                    magicSystem.Ability4Cooldown = 1f;
                                    magicSystem.ability4Image.fillAmount = 0;
                                    canAbility4 = false;
                                    magicSource.PlayOneShot(iceThrow, 1f);
                                }
                                break;
                            case MainSpells.iceprism:
                                {
                                    UsedButton = ";";
                                    usingSpell = true;
                                    Instantiate(IceBeam, transform.position + new Vector3(1 * FireStart, 0), Quaternion.Euler(0, FireRotation, -90));
                                    magicSource.PlayOneShot(freezeClip, 0.5f);
                                }
                                break;
                            case MainSpells.Vortex:
                                {
                                    UsedButton = ";";
                                    GameObject.Instantiate(Resources.Load("Prefabs/VortexOutline") as GameObject, transform.position + new Vector3(0, 0, 0), Quaternion.Euler(0, FireRotation, 0));
                                }
                                break;
                            case MainSpells.Bee:
								{
									usingSpell = true;
									magicSystem.Ability4Cooldown = 3f;
									magicSystem.ability4Image.fillAmount = 0;
									canAbility4 = false;
								}
								break;
							case MainSpells.Gnome:
								{
                                    UsedButton = ";";
                                    Instantiate(TurretOutline, transform.position + new Vector3(1 * FireStart, -1f), Quaternion.Euler(0, FireRotation, 0));
                                }
								break;
							case MainSpells.StoneSkin:
								{
									usingSpell = true;
									magicSystem.Ability4Cooldown = 3f;
									magicSystem.ability4Image.fillAmount = 0;
									canAbility4 = false;
								}
								break;
							case MainSpells.ChainLightning:
								{
									usingSpell = true;
									magicSystem.Ability4Cooldown = 3f;
									magicSystem.ability4Image.fillAmount = 0;
									canAbility1 = false;
								}

								break;
							case MainSpells.LightningLance:
								{
									usingSpell = true;
									GameObject.Instantiate(Resources.Load("Prefabs/LightningSpear") as GameObject, transform.position + new Vector3(0, 0, 0), Quaternion.Euler(0, oppositeFireRotation, 90));
									magicSystem.Ability4Cooldown = 2f;
									magicSystem.ability4Image.fillAmount = 0;
									canAbility1 = false;
                                    magicSource.PlayOneShot(lightningLance, 0.7f);
                                }
								break;
							case MainSpells.BallLightning:
								{
									usingSpell = true;
									magicSystem.Ability4Cooldown = 3f;
									magicSystem.ability4Image.fillAmount = 0;
									canAbility4 = false;
								}
								break;
                            case MainSpells.none:
                                {

                                }
                                break;
                        }
                    }
                }
            }
            #endregion
        }
    }
    private void spellUseCooldown()
    {
        if(usingSpell == true)
        {
            spellUseCooldownTimer -= Time.smoothDeltaTime;
            if(spellUseCooldownTimer <= 0)
            {
                spellUseCooldownTimer = .25f;
                usingSpell = false;
            }
        }
    }
}

//  private void DoWaterForm()
//  {
//switchb = !switchb;
//hRef.noDamage = !hRef.noDamage;

//if (switchb)
//{
//          inWaterForm = false;
//	tfRef.transform.position = WaterForm.transform.position;
//          foreach (MeshRenderer mr in rs)
//          {
//              mr.enabled = true;
//          }
//          WaterForm.SetActive(false);
//}

//if (!switchb)
//{
//          inWaterForm = true;
//	WaterForm.SetActive(true);
//	WaterForm.transform.position = tfRef.transform.position;

//          foreach (MeshRenderer mr in rs)
//          {
//              mr.enabled = false;
//          }
//      }

//  }
//  private void ConstantSpell()
//  {
//      if(ConstantSpellInUse == true)
//      {
//          magicSystem.currMana -= (Time.smoothDeltaTime + ConstantManaUse);
//      }
//      if(magicSystem.currMana <= 1)
//      {
//          gameObject.GetComponent<MovementController>().canMove = true;
//          GameObject.Find("IceBeam(Clone)").GetComponent<IceBeamScript>().canmove = true;
//          ConstantSpellInUse = false;
//      }
//  }

