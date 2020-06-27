using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    #region Variables
    float viewdistance = 15.0f;
    ObjectPooler objPooler;
    public GameObject Shootpoint;
    GameObject Player;
    bool canAttack;
    public bool canRetreat;
    bool isClose;
    float AttackCD;
    Animator  AttackAnim;
    float animCD;
    float retreatDist;
    public Vector3 playerPos;
    public Transform PlayerTrans;
    Vector3 xPos;
    Vector3 selfxPos;
    public float FreezeLevel = 0;
    public float LastFreezeLevel = 0;
    public float FreezeTime = 10f;
    public GameObject FreezeBlock;
    [SerializeField]
    float MaxFreeze;
    public GameObject RangedBolt;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Var Set
        PlayerTrans = GameObject.Find("PlayerFunctionality").transform;
        playerPos = PlayerTrans.transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
        objPooler = ObjectPooler.Instance;
        AttackAnim = GetComponent<Animator>();
        FreezeBlock.SetActive(false);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Wall/Ground Raycast
        int WallLayer = 1 << 12;
        RaycastHit hitWall;
        if (Physics.Raycast(transform.position, -transform.right, out hitWall, 1, WallLayer))
        {
            canRetreat = false;
            Debug.DrawRay(transform.position, -transform.right * hitWall.distance, Color.yellow);
        }
        else
        {
            canRetreat = true;
            Debug.DrawRay(transform.position, -transform.right * 1, Color.white);
        }
        //retreatDist = Vector3.Distance(transform.position, Player.transform.position);
        //if (canRetreat == true && isClose == true)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPos.x, transform.position.y, transform.position.z), -0.025f);
        //}
        #endregion

        #region Player Detection
        int layer = 1 << 11;
        RaycastHit hitGround;
        if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), transform.right, out hitGround, 10, layer))
        {
            isClose = true;
            Debug.DrawRay(transform.position, transform.right * hitGround.distance, Color.yellow);
        }
        else
        {
            isClose = false;
            Debug.DrawRay(transform.position, transform.right * 10, Color.white);
        }
        #endregion

        #region Attack
        AttackAnim.SetInteger("AnimNum", 0);
        if (canAttack && AttackCD <= 0)
        {
            Attack();
            AttackCD = 2.0f;
        }
        int layerMask = 1 << 11;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), transform.right, out hit, viewdistance, layerMask))
        {
            canAttack = true;
            AttackCD -= Time.deltaTime;
            Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.right * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.right * viewdistance, Color.white);
            canAttack = false;
        }
        #endregion

        Freeze();
        UnFreeze();

        #region Freeze Functionality
        FreezeLevel = Mathf.Clamp(FreezeLevel, 0, MaxFreeze);
        if (FreezeLevel >= MaxFreeze)
        {
            FreezeTime -= Time.smoothDeltaTime;
            canAttack = false;
            FreezeBlock.SetActive(true);
        }
        if (FreezeLevel < MaxFreeze)
        {
            FreezeBlock.SetActive(false);
        }
        #endregion
    }

    void Attack()
    {
        AttackAnim.SetInteger("AnimNum", 1);
        Instantiate(RangedBolt, Shootpoint.transform.position, Quaternion.identity);
        //objPooler.SpawnFromPool("RangedBolt", Shootpoint.transform.position, Quaternion.identity);

    }

    void Freeze()
    {
        if (FreezeLevel < LastFreezeLevel && FreezeLevel <= MaxFreeze && FreezeLevel >= 0)
        {
            
            LastFreezeLevel = FreezeLevel;
        }
        if (FreezeLevel > LastFreezeLevel && FreezeLevel <= MaxFreeze && FreezeLevel >= 0)
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
