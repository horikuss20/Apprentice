using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    GameObject Player;
	public float health;
    public float maxHealth;
	public bool canTakeDamage = true;
	float deathTimer;
	Animation deathAnim;
    public Slider EnemyHPSlider;
	Text enemyHP;
    private AudioSource enemySource;
	GameObject gm;
    public bool isBurning = false;
    private float StartBurnTime = 3;
    private float burnTime = 3;
    private GameObject CanvasParent;
    public GameObject burnUI;
    public float MeteorHealth;
    private float timerLightning = 1f;
    public bool hitByLightning = false;
    public bool usedLightning = false;
    public bool canBeSucked = false;
    Collider[] hitColliders;
    public bool hitWall = false;
    private Vector3 lastPos;
    public bool reachedLastPos = true;
    public bool canBeBurned;

    private void Start()
	{
        lastPos = transform.position;
        reachedLastPos = true;
        hitWall = false;
        canBeSucked = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        CanvasParent = gameObject.transform.Find("HPCanvas").gameObject;
        burnUI = CanvasParent.transform.Find("BurnIcon").gameObject;
        maxHealth = EnemyHPSlider.maxValue;
        health = maxHealth;
		deathAnim = GetComponent<Animation>();
		enemyHP = gameObject.GetComponentInChildren<Text>();
		Debug.Log(enemyHP);
		EnemyHPSlider.gameObject.SetActive(false);
        enemySource = GameObject.Find("SoundEffectPlayer").GetComponent<AudioSource>();
        enemySource.clip = GameObject.Find("GameManager").GetComponent<GameManager>().enemyHit;
        burnUI.SetActive(false);
        gm = GameObject.Find("GameManager");
        MeteorHealth = maxHealth;
        if (gameObject.GetComponent<Climber>())
        {
            canBeBurned = true;
        }
    }

	private void Update()
	{
     
        if (!gameObject.GetComponent<Climber>())
        {
            if(gameObject.transform.GetComponent<RangedEnemy>() == true)
            {
                hitColliders = Physics.OverlapSphere(gameObject.transform.position + new Vector3(0,.5f,0), .05f);
            }
            else
            {
                hitColliders = Physics.OverlapSphere(gameObject.transform.position, .1f);
            }
           
            if (canBeSucked == true && hitWall == false)
            {


                foreach (Collider nearbyObj in hitColliders)
                {
                    if (nearbyObj.tag == "Ground" || nearbyObj.tag == "Wall")
                    {
                        hitWall = true;
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Vortex(Clone)").transform.position, .1f);
                    }
                }
            }
            if (reachedLastPos == false && canBeSucked == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, lastPos, .1f);
                if (transform.position == lastPos)
                {
                    reachedLastPos = true;
                }
            }
        }
       

        if (hitByLightning == true)
        {
           
            if (usedLightning == true)
            {
                timerLightning -= Time.smoothDeltaTime;
                if (timerLightning <= 0)
                {
                    usedLightning = false;
                    hitByLightning = false;
                }
            }
            else
            {
                GameObject.Instantiate(Resources.Load("Prefabs/LightningStrike") as GameObject, transform.position + new Vector3(0, 3, 0), Quaternion.Euler(0,0,0));
                usedLightning = true;
            }
        }

        MeteorCheck();

		string hpText = health + "/" + maxHealth;
        Burn();

        if(enemyHP == null)
        {
            enemyHP.enabled = false;
        }

		if(gm.GetComponent<PendantSystemNew>().pSide1 == gm.GetComponent<PendantSystemNew>().Eagle || gm.GetComponent<PendantSystemNew>().pSide2 == gm.GetComponent<PendantSystemNew>().Eagle)
		{
			enemyHP.text = hpText;
		}
		else
		{ 
			enemyHP.text = "";
		}

        EnemyHPSlider.value = health;
		if(health <= 0)
		{
            Player.GetComponent<MovementController>().canMove = true;
			Death();
		}
        if(health < maxHealth)
        {
            EnemyHPSlider.gameObject.SetActive(true);
        }
	}

	public void TakeDamage(float damage)
	{
        if (canTakeDamage)
        {
            health -= damage;
            enemySource.clip = GameObject.Find("GameManager").GetComponent<GameManager>().enemyHit;
            if (!enemySource.isPlaying)
            {
				enemySource.Play();
            }
			gameObject.GetComponent<Animator>().SetInteger("AnimNum", 3);
        }

	}

	public void RestoreHealth()
	{
		health += 1;
	}
	
	public void Death()
	{
        if (GameObject.Find("EngulfingFlames(Clone)") != null)
        {
            GameObject.Find("EngulfingFlames(Clone)").GetComponent<EngulfingFlames>().size++;
			gameObject.GetComponent<Animator>().SetInteger("AnimNum", 2);
		}

		if(deathTimer == 0)
		{
			gm.GetComponent<PendantSystemNew>().gNum += 1;
			gameObject.SetActive(false);
			gm.GetComponent<SkillSystemNew>().exp += Random.Range(5, 10);
            gm.GetComponent<GameManager>().collectibleCounter += 1;
		}
	}

    public void Burn()
    {
        if(canBeBurned == false)
        {
            if (isBurning == true)
            {
                burnTime -= Time.smoothDeltaTime;
                health -= .06f;
                if (burnTime <= 0)
                {
                    isBurning = false;
                    burnTime = StartBurnTime;
                    burnUI.SetActive(false);
                }
            }
        }
    }

    public void hitBurn()
    {
        if(canBeBurned == false)
        {
            burnTime = StartBurnTime;
            isBurning = true;
            burnUI.SetActive(true);
        }
      
    }

    public void MeteorCheck()
    {
        if(canTakeDamage == true)
        {
            if (MeteorHealth < health && MeteorHealth <= 0 && GameObject.Find("UpgradedMeteorSpawner(Clone)").activeInHierarchy == true)
            {
                GameObject.Find("UpgradedMeteorSpawner(Clone)").GetComponent<MeteorSpawner>().Meteors += 1;
            }

            if (MeteorHealth < health)
            {
                health = MeteorHealth;
            }
            if (health < MeteorHealth)
            {

                MeteorHealth = health;
            }

        }

    }
}
