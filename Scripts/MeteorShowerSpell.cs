using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShowerSpell : MonoBehaviour
{
    public int MeteorRotation;
    public float MeterorSpawnPoint;
    public GameObject Meteor;
	[SerializeField]
    float LifeTimer = 3;
    float spawnTimer = .05f;
    public bool negitive = false;
    public float PorV = 1;
    public string MSButton;
	GameObject GM;
	MagicSystem magicSystem;

	private void Start()
    {
		GM = GameObject.Find("GameManager");
		magicSystem = GameObject.Find("MagicUI").GetComponent<MagicSystem>();

		MSButton = GameObject.Find("PlayerFunctionality").GetComponent<MagicSpells>().UsedButton;
        if(MSButton == "j")
        {
            GameObject.Find("MagicUI").GetComponent<MagicSystem>().activeAbillity1 = true;
            GameObject.Find("MagicUI").GetComponent<MagicSystem>().ability1Image.fillAmount = 0;
        }
        if (MSButton == "k")
        {
            GameObject.Find("MagicUI").GetComponent<MagicSystem>().activeAbillity2 = true;
            GameObject.Find("MagicUI").GetComponent<MagicSystem>().ability2Image.fillAmount = 0;
        }
        if (MSButton == "l")
        {
            GameObject.Find("MagicUI").GetComponent<MagicSystem>().activeAbillity3 = true;
            GameObject.Find("MagicUI").GetComponent<MagicSystem>().ability3Image.fillAmount = 0;
        }
        if (MSButton == ";")
        {
            GameObject.Find("MagicUI").GetComponent<MagicSystem>().activeAbillity4 = true;
            GameObject.Find("MagicUI").GetComponent<MagicSystem>().ability4Image.fillAmount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

		LifeTimer -= Time.smoothDeltaTime;

		if (Input.GetKeyDown(MSButton))
        {
            if (LifeTimer >= 0)
            {
                if (MSButton == "j")
                {
                    GameObject.Find("MagicUI").GetComponent<MagicSystem>().activeAbillity1 = false;
						GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability1CoolDown = 3f;

                }
                if (MSButton == "k")
                {
                    GameObject.Find("MagicUI").GetComponent<MagicSystem>().activeAbillity2 = false;
					if(GM.GetComponent<PendantSystemNew>().pSide1 == GM.GetComponent<PendantSystemNew>().Shamrock || GM.GetComponent<PendantSystemNew>().pSide2 == GM.GetComponent<PendantSystemNew>().Shamrock)
					{
						int rocknum = Random.Range(0, 10);
						if(rocknum == 3)
						{
							GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability2CoolDown = 0;
							GM.GetComponentInChildren<AudioSource>().PlayOneShot(GM.GetComponent<PendantSystemNew>().shamrockSwap);
						}
						else
							GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability2CoolDown = 3f;
					}
					else
						GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability2CoolDown = 3f;
                }
                if (MSButton == "l")
                {
                    GameObject.Find("MagicUI").GetComponent<MagicSystem>().activeAbillity3 = false;
					if (GM.GetComponent<PendantSystemNew>().pSide1 == GM.GetComponent<PendantSystemNew>().Wizard || GM.GetComponent<PendantSystemNew>().pSide2 == GM.GetComponent<PendantSystemNew>().Wizard)
					{
						magicSystem.Ability3CoolDown = 3f;
					}
					if (GM.GetComponent<PendantSystemNew>().pSide1 == GM.GetComponent<PendantSystemNew>().Shamrock || GM.GetComponent<PendantSystemNew>().pSide2 == GM.GetComponent<PendantSystemNew>().Shamrock)
					{
						int rocknum = Random.Range(0, 10);
						if (rocknum == 3)
						{
							GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability3CoolDown = 0;
							GM.GetComponentInChildren<AudioSource>().PlayOneShot(GM.GetComponent<PendantSystemNew>().shamrockSwap);
						}
						else
							GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability3CoolDown = 3f;
					}
					else
						GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability3CoolDown = 3f;
                }
                if (MSButton == ";")
                {
                    GameObject.Find("MagicUI").GetComponent<MagicSystem>().activeAbillity4 = false;
					if (GM.GetComponent<PendantSystemNew>().pSide1 == GM.GetComponent<PendantSystemNew>().Shamrock || GM.GetComponent<PendantSystemNew>().pSide2 == GM.GetComponent<PendantSystemNew>().Shamrock)
					{
						int rocknum = Random.Range(0, 10);
						if (rocknum == 3)
						{
							GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability4Cooldown = 0;
							GM.GetComponentInChildren<AudioSource>().PlayOneShot(GM.GetComponent<PendantSystemNew>().shamrockSwap);
						}
						else
							GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability4Cooldown = 3f;
					}
					else
						GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability4Cooldown = 3f;
                }
                Destroy(gameObject);
            }
        }

        if (negitive == false)
        {
            PorV = 1;
        }
        else
        {
            PorV = -1;
        }
           
        if(LifeTimer <= 0)
        {
            if (MSButton == "k")
            {
                GameObject.Find("MagicUI").GetComponent<MagicSystem>().activeAbillity2 = false;

				if (GM.GetComponent<PendantSystemNew>().pSide1 == GM.GetComponent<PendantSystemNew>().Shamrock || GM.GetComponent<PendantSystemNew>().pSide2 == GM.GetComponent<PendantSystemNew>().Shamrock)
				{
					int rocknum = Random.Range(0, 10);
					if (rocknum == 3)
					{
						GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability2CoolDown = 0;
						GM.GetComponentInChildren<AudioSource>().PlayOneShot(GM.GetComponent<PendantSystemNew>().shamrockSwap);
					}
					else
						GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability2CoolDown = 3f;
				}
				else
					GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability2CoolDown = 3f;
            }
            if (MSButton == "l")
            {
                GameObject.Find("MagicUI").GetComponent<MagicSystem>().activeAbillity3 = false;
				if (GM.GetComponent<PendantSystemNew>().pSide1 == GM.GetComponent<PendantSystemNew>().Wizard || GM.GetComponent<PendantSystemNew>().pSide2 == GM.GetComponent<PendantSystemNew>().Wizard)
				{
					magicSystem.Ability3CoolDown = 1.5f;
				}
				if (GM.GetComponent<PendantSystemNew>().pSide1 == GM.GetComponent<PendantSystemNew>().Shamrock || GM.GetComponent<PendantSystemNew>().pSide2 == GM.GetComponent<PendantSystemNew>().Shamrock)
				{
					int rocknum = Random.Range(0, 10);
					if (rocknum == 3)
					{
						GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability3CoolDown = 0;
						GM.GetComponentInChildren<AudioSource>().PlayOneShot(GM.GetComponent<PendantSystemNew>().shamrockSwap);
					}
					else
						GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability3CoolDown = 3f;
				}
				else
					GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability3CoolDown = 3f;
            }
            if (MSButton == ";")
            {
                GameObject.Find("MagicUI").GetComponent<MagicSystem>().activeAbillity4 = false;
				if (GM.GetComponent<PendantSystemNew>().pSide1 == GM.GetComponent<PendantSystemNew>().Shamrock || GM.GetComponent<PendantSystemNew>().pSide2 == GM.GetComponent<PendantSystemNew>().Shamrock)
				{
					int rocknum = Random.Range(0, 10);
					if (rocknum == 3)
					{
						GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability4Cooldown = 0;
						GM.GetComponentInChildren<AudioSource>().PlayOneShot(GM.GetComponent<PendantSystemNew>().shamrockSwap);
					}
					else
						GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability4Cooldown = 3f;
				}
				else
					GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability4Cooldown = 3f;
			}
            Destroy(gameObject);
        }

       
        if(spawnTimer >= 0)
        {
            spawnTimer -= Time.smoothDeltaTime;
        }
        else if(spawnTimer <= 0)
        {
          MeteorRotation = Random.Range(0, 60);
            MeterorSpawnPoint = Random.Range(-2.5f, 2.5f);
            Instantiate(Meteor, transform.position + new Vector3(MeterorSpawnPoint, 0), Quaternion.Euler(0, 0, MeteorRotation * PorV));
            negitive = !negitive;
            spawnTimer = .05f;
        }
    }
}
