using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GumbisBoss : MonoBehaviour
{
    GameObject Player;
    GameObject HealthUI;
    public ParticleSystem HeatPS;
    [Header("=======ShootPoints=======")]
    [SerializeField]   
    GameObject BasicShootpoint, IceShootPoint, SprayPoint;
    public GameObject IceBolt;
    Transform PlayerTrans;
    public Vector3 playerPos, AttackPos;
    ObjectPooler objPooler;
    public float AttackCD;
    [SerializeField]
    [Header("=============================")]
    bool canBasicAttack, canIceAttack, canHeatAttack, canSprayAttack, canSpawnAttack;
    public float freezeTime, FreezeTime = 10f;
    public bool freeze;
    [SerializeField]
    public float heatAttackSet, heatAttackTime;
    public float FreezeLevel = 0, LastFreezeLevel = 0;
    float GumbisFreeze = 5.0f;
    [SerializeField]
    float currAttackTime;
    float currAttackSet;
    public GameObject FreezeBlock;
    public float AttackRange;
    [SerializeField]
    public float MaxRange;
    public int AttackNum, lastAttackNum;
    public GameObject SprayBolt;
    public Slider HeatSlider;
    public AudioClip IceBlockSound, AttackSound, ExplodeSound, BurnSound;
    AudioSource AudioSource;
    bool soundplayed, heatAtt;



    // Start is called before the first frame update
    void Start()
    {

        soundplayed = false;
        AudioSource = GetComponent<AudioSource>();
        HeatSlider.gameObject.SetActive(false);
        currAttackSet = 10.0f;
        HeatPS.Stop();
        HealthUI = GameObject.Find("HealthUI");
        heatAttackSet = 15.0f;
        heatAttackTime = heatAttackSet;
        Player = GameObject.FindGameObjectWithTag("Player");
        freezeTime = 1.0f;
        AttackCD = 2.0f;
        objPooler = ObjectPooler.Instance;
        PlayerTrans = GameObject.Find("PlayerFunctionality").transform;
    }

    // Update is called once per frame
    void Update()
    {
        AttackCD = Mathf.Clamp(AttackCD, 0, 2);
        FreezeLevel = Mathf.Clamp(FreezeLevel, 0, 10);
        AttackCD -= Time.deltaTime;
        playerPos = PlayerTrans.transform.position;
        AttackRange = Vector3.Distance(Player.transform.position, transform.position);
        HeatSlider.value = heatAttackTime;
        SprayPoint.transform.Rotate(0, 3, 0);

        #region Attack sets
        if (currAttackTime <= 0 && AttackRange <= MaxRange)
        {
            lastAttackNum = AttackNum;
            AttackNum = Random.Range(0, 4);
            if(AttackNum == lastAttackNum)
            {
                AttackNum = Random.Range(0, 4);
            }
            
            currAttackTime = currAttackSet;
        }

        if (AttackNum == 0 && AttackRange <= MaxRange)
        {
            canBasicAttack = true;
            BasicAttack();
            currAttackTime -= Time.deltaTime;
        }
        else
        {
            canBasicAttack = false;
        }

        if (AttackNum == 1 && AttackRange <= MaxRange)
        {
            canIceAttack = true;
            IceAttack();
            currAttackTime -= Time.deltaTime;
        }
        else
        {
            canIceAttack = false;
        }

        if (AttackNum == 2 && AttackRange <= MaxRange)
        {
            currAttackTime = heatAttackSet;
            canHeatAttack = true;
            HeatAttack();
            currAttackTime -= Time.deltaTime;
        }
        else
        {
            canHeatAttack = false;
        }

        if(AttackNum == 3 && AttackRange <= MaxRange)
        {
            canSprayAttack = true;
            SprayAttack();
            currAttackTime -= Time.deltaTime;
        }
        else
        {
            canSprayAttack = false;
        }

        if (AttackNum == 4 && AttackRange <= MaxRange)
        {
            canSpawnAttack = true;
            EnemySpawnAttack();
            currAttackTime -= Time.deltaTime;
        }
        else
        {
            canSpawnAttack = false;
        }
        #endregion

        #region IceAttack


        if (freezeTime <= 0)
        {
            Player.GetComponent<MovementController>().FreezeBlock.SetActive(false);
            Player.GetComponent<MovementController>().canMove = true;
            freeze = false;
            freezeTime = 1.0f;
        }

        if (freeze == true)
        {
            Player.GetComponent<MovementController>().FreezeBlock.SetActive(true);
            Player.GetComponent<MovementController>().canMove = false;
            freezeTime -= Time.deltaTime;
        }
        #endregion

        #region HeatAttack
        if(FreezeLevel == 10 && soundplayed != true)
        {
            AudioSource.PlayOneShot(IceBlockSound);
            soundplayed = true;
        }

        if (FreezeLevel >= 10)
        {
            
            GumbisFreeze -= Time.deltaTime;
            FreezeBlock.SetActive(true);
            canHeatAttack = false;
            HeatPS.Stop();
            AttackNum = Random.Range(0, 4);
            AttackCD = 5.0f;
            HeatSlider.gameObject.SetActive(false);
            heatAttackTime = heatAttackSet;
            if(GumbisFreeze <= 0)
            {
                soundplayed = false;
                FreezeBlock.SetActive(false);
                FreezeLevel = 0;
                GumbisFreeze = 5.0f;
            }
        }
        if (FreezeLevel < 5)
        {
            FreezeBlock.SetActive(false);
        }
        #endregion
        //if(heatAtt == true && soundplayed != true)
        //{
        //    AudioSource.PlayOneShot(BurnSound);
        //    soundplayed = true;
        //}
    }

    void IceAttack()
    {
        if (AttackCD <= 0 && canIceAttack == true)
        {
            AudioSource.PlayOneShot(AttackSound);
            Instantiate(IceBolt, IceShootPoint.transform.position, Quaternion.identity);
            AttackCD = 1.5f;
        }    
    }

    void HeatAttack()
    {
        if (AttackCD <= 0 && canHeatAttack == true)
        {
            heatAtt = true;
            HeatSlider.gameObject.SetActive(true);
            HeatPS.Play();
            heatAttackTime -= Time.deltaTime;
            if (heatAttackTime <= 0)
            {
                AudioSource.PlayOneShot(ExplodeSound);
                HealthUI.GetComponent<Health>().Damage(3);
                canHeatAttack = false;
                HeatPS.Stop();
                HeatSlider.gameObject.SetActive(false);
                AttackNum = Random.Range(0, 4);
            }
        }
        else
        {
            HeatPS.Stop();
        }
    }

    void BasicAttack()
    {
        if (AttackCD <= 0 && canBasicAttack == true)
        {
            AudioSource.PlayOneShot(AttackSound);
            Debug.Log("Attacking");
            objPooler.SpawnFromPool("BasicBolt", BasicShootpoint.transform.position, Quaternion.identity);
            AttackCD = 1.0f;
        }
    }

    void Freeze()
    {
        if (FreezeLevel < LastFreezeLevel && FreezeLevel <= 5 && FreezeLevel >= 0)
        {
            LastFreezeLevel = FreezeLevel;
        }
        if (FreezeLevel > LastFreezeLevel && FreezeLevel <= 5 && FreezeLevel >= 0)
        {
            LastFreezeLevel = FreezeLevel;
        }

    }
    void UnFreeze()
    {
        if (FreezeLevel >= 1)
        {
            
            FreezeTime -= Time.smoothDeltaTime;
            if (FreezeTime <= 0)
            {
                FreezeLevel -= 1;
                FreezeTime = 10;
            }
        }
    }
    public void AddFreeze()
    {
        FreezeLevel++;
    }

    void SprayAttack()
    {
        if (AttackCD <= 0 && canSprayAttack == true)
        {
            AudioSource.PlayOneShot(AttackSound);
            Instantiate(SprayBolt, SprayPoint.transform.position, Quaternion.identity);
            //objPooler.SpawnFromPool("SprayBolt", SprayPoint.transform.position, Quaternion.identity);
            AttackCD = 0.2f;
        }
    }

    void EnemySpawnAttack()
    {
        if (AttackCD <= 0 && canSpawnAttack == true)
        {
            objPooler.SpawnFromPool("EnemyBolt", BasicShootpoint.transform.position, Quaternion.identity);
            AttackNum = Random.Range(0, 4);
            //AttackCD = 0.2f;
        }
            
    }
}
