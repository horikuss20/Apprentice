using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZerkerEnemy : MonoBehaviour
{
    #region Variables
    bool isClose;
    bool canAttack;
    float AttackCD;
    float viewdistance = 10.0f;
    Collider[] hitColliders;
    GameObject HealthUI;
    public GameObject ChopSpot;
    GameObject player;
    public Vector3 playerPos;
    public Transform PlayerTrans;
    private Vector3 targetAngles;
    float AttackRange;
    bool toClose;
    Animator AttackAnim;
    Vector3 attackSize;
    bool canPursue;
    public float FreezeLevel = 0;
    public float LastFreezeLevel = 0;
    public float FreezeTime = 10f;
    public GameObject FreezeBlock;
    [SerializeField]
    float MaxFreeze;
    bool isFrozen;
    public float speed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Var Set
        speed = 0.05f;
        HealthUI = GameObject.Find("HealthUI");
        AttackAnim = GetComponentInParent<Animator>();
        PlayerTrans = GameObject.Find("PlayerFunctionality").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        FreezeBlock.SetActive(false);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Wall Detection Ray
        int WallLayer = 1 << 12;
        RaycastHit hitWall;
        if (Physics.Raycast(transform.position, transform.forward, out hitWall, 1, WallLayer))
        {
            canPursue = false;
            Debug.DrawRay(transform.position, transform.forward * hitWall.distance, Color.yellow);
        }
        else
        {
            canPursue = true;
            Debug.DrawRay(transform.position, -transform.right * 1, Color.white);
        }
        #endregion

        #region Player Detection Ray
        int PlayerLayer = 1 << 11;
        RaycastHit hitPlayer;
        if (isFrozen == false)
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.forward, out hitPlayer, 15, PlayerLayer) ||
                Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.forward, out hitPlayer, 15, PlayerLayer) ||
                Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.forward, out hitPlayer, 15, PlayerLayer) ||
                Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), transform.forward, out hitPlayer, 15, PlayerLayer))
            {
                canAttack = true;
                Debug.DrawRay(transform.position, transform.forward * hitPlayer.distance, Color.yellow);
            }
            else
            {
                canAttack = false;
                Debug.DrawRay(transform.position, transform.forward * 15, Color.white);
            }
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), -transform.forward, out hitPlayer, 15, PlayerLayer) ||
                Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), -transform.forward, out hitPlayer, 15, PlayerLayer) ||
                Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), -transform.forward, out hitPlayer, 15, PlayerLayer) ||
                Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), -transform.forward, out hitPlayer, 15, PlayerLayer))
            {
                canAttack = true;
                Debug.DrawRay(transform.position, -transform.forward * hitPlayer.distance, Color.yellow);
            }
            else
            {
                //canAttack = false;
                Debug.DrawRay(transform.position, -transform.forward * 15, Color.white);
            }
        }
        #endregion

        Freeze();
        UnFreeze();

        #region Attack Functionality
        attackSize = new Vector3(1, 2, 1);
        playerPos = PlayerTrans.transform.position;
        
        if (canAttack == true)
        {
           //if(AttackAnim.GetInteger("AnimNum") == 2)
           // {
           //     AttackAnim.SetInteger("AnimNum", 0);
           // }

            AttackAnim.SetInteger("AnimNum", 2);
            if (canPursue == true)
            {
                AttackAnim.SetInteger("AnimNum", 1);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPos.x - 2, transform.position.y, transform.position.z), speed);
            }
            transform.LookAt(new Vector3(playerPos.x, transform.position.y, transform.position.z));
            toClose = true;
            Attack();
        }
        else
        {
            toClose = false;
            AttackAnim.SetInteger("AnimNum", 0);
        }
        #endregion

        #region Freeze Functionality
        FreezeLevel = Mathf.Clamp(FreezeLevel, 0, MaxFreeze);
        if (FreezeLevel >= MaxFreeze)
        {
            FreezeTime -= Time.smoothDeltaTime;
            isFrozen = true;
            canAttack = false;
            FreezeBlock.SetActive(true);
        }
        if (FreezeLevel < MaxFreeze)
        {
            isFrozen = false;
            FreezeBlock.SetActive(false);
        }
        #endregion
    }

    private void LateUpdate()
    {
        AttackAnim.SetInteger("AnimNum", 0);
    }
    void Attack()
    {
        
        //play animation
        //Do attack
        hitColliders = Physics.OverlapBox(ChopSpot.transform.position, attackSize, Quaternion.identity);
        foreach (Collider nearbyObj in hitColliders)
        {
            if(nearbyObj.tag == "Player")
            {
                Debug.Log("hitting");
                HealthUI.GetComponent<Health>().Damage(1);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(ChopSpot.transform.position, attackSize);
    }

    void Freeze()
    {
        if (FreezeLevel < LastFreezeLevel && FreezeLevel <= MaxFreeze && FreezeLevel >= 0)
        {
            speed = 0.05f;
            LastFreezeLevel = FreezeLevel;
        }
        if (FreezeLevel > LastFreezeLevel && FreezeLevel <= MaxFreeze && FreezeLevel >= 0)
        {
            speed = speed - .006f;
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
