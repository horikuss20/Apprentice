using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public GameObject patrolHealthBar;
    #region Variables
    public float speed;
    public GameObject HealthUI;
    Animation deathAnim;
    [SerializeField] float deathTimer;
    public Transform targetPos, startPos;
    bool towards = true;
    bool isDamaging = false;
    public float health = 1f;
    public float FreezeLevel = 0;
    public float LastFreezeLevel = 0;
    public float FreezeTime = 10f;
    public float dmgTimer = 1.0f;
    public GameObject FreezeBlock;
    public bool CanPatrol;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Var Set
        speed = 3;
        HealthUI = GameObject.Find("HealthUI");
        deathAnim = GetComponent<Animation>();
        deathTimer = 1.0f;
        CanPatrol = true;
        #endregion
        FreezeBlock.SetActive(false);
        patrolHealthBar = gameObject.transform.Find("HPCanvas").gameObject;
    }

    void Update()
    {
        if (gameObject.transform.rotation.y <= 0)
        {

            patrolHealthBar.transform.rotation = Quaternion.Euler(0, 360, 0);
        }
        if (gameObject.transform.rotation.y >= 0f)
        {
            patrolHealthBar.transform.rotation = Quaternion.Euler(0, 360, 0);
        }

        #region Func Call
        Die();
        Freeze();
        UnFreeze();
        #endregion

        speed = Mathf.Clamp(speed, 0f, Mathf.Infinity);

        #region Freeze Functionality
        FreezeLevel = Mathf.Clamp(FreezeLevel, 0, 5);
        if (isDamaging == true)
        {
            dmgTimer -= Time.deltaTime;
        }
        if (dmgTimer <= 0 && isDamaging == true)
        {
            HealthUI.GetComponent<Health>().Damage(1);
            dmgTimer = 1.0f;
        }
        if(FreezeLevel >= 5)
        {
            FreezeBlock.SetActive(true);
        }
        if (FreezeLevel < 5)
        {
            FreezeBlock.SetActive(false);
        }
        #endregion
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        #region Patrol
        if (targetPos != null)
        if(CanPatrol == true)
            {
                if (towards)
                {
                    transform.LookAt(targetPos.position);
                    transform.position += transform.forward * speed * Time.deltaTime;
                    if (Vector3.Distance(transform.position, targetPos.position) < 1.0f)
                    {
                        towards = false;
                    }
                }
                else
                {
                    transform.LookAt(startPos.position);
                    transform.position += transform.forward * speed * Time.deltaTime;
                    if (Vector3.Distance(transform.position, startPos.position) < 1.0f)
                    {
                        {
                            towards = true;
                        }
                    }
                }
            }

        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            isDamaging = true;
            HealthUI.GetComponent<Health>().Damage(1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isDamaging = false;
        dmgTimer = 1.0f;
    }

    void Die()
    {
        if(health <= 0)
        {
            deathAnim.Play("Death");
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    void Freeze()
    {
        if (FreezeLevel < LastFreezeLevel && FreezeLevel <= 5 && FreezeLevel >= 0)
        {
            speed = speed + .6f;
            LastFreezeLevel = FreezeLevel;
        }
        if(FreezeLevel > LastFreezeLevel && FreezeLevel <= 5 && FreezeLevel >= 0)
        {
            speed = speed - .6f;
            LastFreezeLevel = FreezeLevel;
        }
             
    }
    void UnFreeze()
    {
        if(FreezeLevel >= 1)
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
