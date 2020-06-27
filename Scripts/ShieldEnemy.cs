using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{
    #region GameObjects

    GameObject Player;
    public GameObject ArcaneBlast;
    public ParticleSystem ShieldPS;
    Animator ShieldAnim;
    public GameObject Shield;
    public Material DamageState;
    public Material ShieldState;
    public GameObject Body;
    public GameObject HealthUI;

    #endregion

    #region Floats

    float AttackRange;
    float BashRange;
    [SerializeField]
    public float ShieldHealth;
    public float speed;
    float BashStun;
    float BashCD;
    float waitTime;
    float startWaitTime;
    public float DamageTime;
    private float BashMin = -1.0f, BashMax = 3.0f;
    float ArcaneTime;
    float ChargeUp = 1.0f;
    public float FreezeLevel = 0;
    public float LastFreezeLevel = 0;
    public float FreezeTime = 10f;
    public GameObject FreezeBlock;
    [SerializeField]
    float MaxFreeze;

    #endregion

    #region Bools

    bool ShieldDown;
    bool towards = true;
    bool canLook = true;
    public bool canPatrol;
    public bool Waiting;
    bool canAttack;

    #endregion

    int AnimNum;

    #region Transforms

    public Transform PlayerTrans;
    public Transform targetPos, startPos;

    #endregion


    void Start()
    {

        #region Var. Set
        FreezeBlock.SetActive(false);
        HealthUI = GameObject.Find("HealthUI");
        PlayerTrans = GameObject.Find("PlayerFunctionality").transform;
        ShieldAnim = GetComponent<Animator>();
        DamageTime = 4.0f;
        speed = 1.0f;
        waitTime = startWaitTime;
        ArcaneTime = 0.2f;
        BashStun = 2.0f;
        ShieldDown = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<EnemyHealth>().canTakeDamage = false;

        #endregion

    }

    void Update()
    {
        //if(gameObject.GetComponent<EnemyHealth>().health <= 0)
        //{
        //    BashStun = 0;
        //}
        AttackRange = Vector3.Distance(Player.transform.position, transform.position);
        BashRange = Vector3.Distance(Player.transform.position, transform.position);
        #region Attack Stuff

        if (AttackRange <= 15)
        {

            canPatrol = false;
            Attack();
            if (canLook)
            {
                transform.LookAt(PlayerTrans);
            }
        }
        else
        {
            canPatrol = true;
        }

        #endregion

        #region Bash Stuff

        Mathf.Clamp(BashCD, BashMin, BashMax);
        if (Waiting == true)
        {
            waitTime -= Time.deltaTime;
        }
        if (waitTime <= 0)
        {
            Waiting = false;
        }

        if (BashRange <= 3 && BashCD <= 0)
        {
            //ShieldBash();
            //canPatrol = false;
        }
        else
        {
            BashCD -= Time.deltaTime;
            //Player.GetComponent<MovementController>().canMove = true;
            //canPatrol = true;
        }

        #endregion

        #region Shield Emmission

        if (ShieldHealth <= 0)
        {
            ShieldAnim.SetInteger("AnimNum", 0);
            ShieldPS.Emit(1000);
        }
        if (ShieldHealth <= 5)
        {
            ShieldAnim.SetInteger("AnimNum", 3);
            ShieldPS.Emit(200);
        }
        if (ShieldHealth <= 7)
        {
            ShieldPS.Emit(50);
        }
        if (ShieldHealth <= 9)
        {
            ShieldPS.Emit(25);
        }

        #endregion

        Freeze();
        UnFreeze();

        FreezeLevel = Mathf.Clamp(FreezeLevel, 0, MaxFreeze);
        if (FreezeLevel >= MaxFreeze)
        {
            FreezeTime -= Time.smoothDeltaTime;
            canLook = false;
            canAttack = false;
            FreezeBlock.SetActive(true);
        }
        if (FreezeLevel < MaxFreeze)
        {
            canLook = true;
            canAttack = true;
            FreezeBlock.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (canPatrol == true && Waiting == false)
        {
            Patrol();
        }
    }

    void Attack()
    {
            if (ShieldHealth <= 0)
            {
                ShieldAnim.SetInteger("AnimNum", 0);
                gameObject.GetComponent<EnemyHealth>().canTakeDamage = true;
                canLook = false;
                ShieldDown = true;
                ChargeUp -= Time.deltaTime;
                if (ChargeUp <= 0)
                {
                    ShieldPS.Emit(0);
                    ArcaneBlast.SetActive(true);
                    Body.GetComponent<Renderer>().material = DamageState;
                    ArcaneTime -= Time.deltaTime;
                    DamageTime -= Time.deltaTime;
                    if (ArcaneTime <= 0)
                    {

                        ArcaneBlast.SetActive(false);
                        if (DamageTime <= 0)
                        {
                            ShieldPS.Emit(25);
                            gameObject.GetComponent<EnemyHealth>().canTakeDamage = false;
                            Body.GetComponent<Renderer>().material = ShieldState;
                            DamageTime = 4.0f;
                            ShieldHealth = 10;
                            ArcaneTime = 0.2f;
                            ChargeUp = 1.0f;
                            canLook = true;
                        }
                    }
                }
            }
        
    }

    void ShieldBash()
    {
        ShieldAnim.SetInteger("AnimNum", 1);
        Player.GetComponent<MovementController>().canMove = false;
        Player.GetComponent<MovementController>().moveDirection.x = 0;
        BashStun -= Time.deltaTime;
        if (BashStun <= 0)
        {
            BashCD = 2.0f;
            BashStun = 2.0f;
            ShieldAnim.SetInteger("AnimNum", 0);
        }
    }

    void Patrol()
    {
        if (targetPos != null)
        if (startPos != null)
        if (towards)
        {
        transform.LookAt(targetPos.transform.position);
        transform.position += transform.forward * speed * Time.deltaTime;
        //transform.Rotate(0, -90, 0);
        if (Vector3.Distance(transform.position, targetPos.position) < 1.0f)
            {
                Wait();
                towards = false;
            }
        }
        else
        {
            transform.LookAt(startPos.transform.position);
            transform.position += transform.forward * speed * Time.deltaTime;
            //transform.Rotate(0, -90, 0);
            if (Vector3.Distance(transform.position, startPos.position) < 1.0f)
             {
                 Wait();
                 towards = true;
             }
        }
    }

    void Wait()
    {
        waitTime = 3.0f;
        Waiting = true;
        waitTime -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HealthUI.GetComponent<Health>().Damage(1);
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

         if (FreezeTime <= 0)
         {
             FreezeLevel -= MaxFreeze;
             FreezeTime = 10;
         }
        

    }
    public void AddFreeze()
    {
        FreezeLevel++;
    }
}
