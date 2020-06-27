using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaPatrolNew : MonoBehaviour
{
    public float speed;
    public GameObject HealthUI;
    GameObject PlayerGO;
    public Transform targetPos, startPos;
    bool towards = true;
    public bool canPatrol;
    public bool Waiting;
    float viewdistance;
    Animation deathAnim;
    float deathTimer;
    public float waitTime;
    public float startWaitTime;


    public float health = 4f;


    public float FreezeLevel = 0;
    public float LastFreezeLevel = 0;
    public float FreezeTime = 10f;
    public GameObject FreezeBlock;

    void Start()
    {
        waitTime = startWaitTime;
        PlayerGO = GameObject.FindGameObjectWithTag("Player");
        viewdistance = 11;
        canPatrol = true;
        speed = 2;
        HealthUI = GameObject.Find("HealthUI");
        deathAnim = GetComponent<Animation>();
        deathTimer = 1.0f;
        FreezeBlock.SetActive(false);
    }

    private void Update()
    {
        if (FreezeLevel >= 5)
        {
            FreezeBlock.SetActive(true);
        }
        if (FreezeLevel < 5)
        {
            FreezeBlock.SetActive(false);
        }

        #region Raycasts
        int layerMask = 1 << 11;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right, out hit, viewdistance, layerMask))
        {
            //gameObject.GetComponentInChildren<Ballista>().canAttackL = true;
            Debug.DrawRay(transform.position, transform.right * hit.distance, Color.yellow);
            
            canPatrol = false;

        }
        else
        {
            Debug.DrawRay(transform.position, transform.right * 1000, Color.white);

            //gameObject.GetComponentInChildren<Ballista>().canAttackL = false;
            canPatrol = true;
        }
        #endregion
        Die();
        if(Waiting == true)
        {
            waitTime -= Time.deltaTime;
        }
        if (waitTime <= 0)
        {
            Waiting = false;
        }

        Freeze();
        UnFreeze();
        speed = Mathf.Clamp(speed, 0f, Mathf.Infinity);
        FreezeLevel = Mathf.Clamp(FreezeLevel, 0, 5);
    }
    void FixedUpdate()
    {
        if(canPatrol == true && Waiting == false)
        {
            Patrol();
        }
        
    }




    void Patrol()
    {
        if(targetPos != null)
        if(startPos != null)
        if (towards)
        {
            transform.LookAt(targetPos.transform.position);
            transform.position += transform.forward * speed * Time.deltaTime;
            transform.Rotate(0, -90, 0);
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
            transform.Rotate(0, -90, 0);
            if (Vector3.Distance(transform.position, startPos.position) < 1.0f)
            {
                Wait();
                towards = true;
                
            }
            
        }
    }
    void Die()
    {
        //if(health <= 0 && gameObject.GetComponentInChildren<Ballista>().canDie == true)
        //{
        //    deathAnim.Play("Death");
        //    deathTimer -= Time.deltaTime;
        //    if (deathTimer <= 0)
        //    {
        //        Destroy(gameObject);
        //    }
        //}
    }

    void Wait()
    {
        waitTime = 3.0f;
        Waiting = true;
        waitTime -= Time.deltaTime;
    }

    void Freeze()
    {
        if (FreezeLevel < LastFreezeLevel && FreezeLevel <= 5 && FreezeLevel >= 0)
        {
            speed = speed + .4f;
            LastFreezeLevel = FreezeLevel;
        }
        if (FreezeLevel > LastFreezeLevel && FreezeLevel <= 5 && FreezeLevel >= 0)
        {
            speed = speed - .4f;
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
}
