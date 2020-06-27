using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingEnemy : MonoBehaviour
{
    GameObject Player;
    GameObject HealthUI;
    Collider[] hitColliders;
    public Vector3 playerPos;
    public Transform PlayerTrans;
    MeshRenderer[] mRend;
    CapsuleCollider Col;
    float DigTime;
    Animator AttackAnim;
    public float speed;
    public float viewdistance;
    ObjectPooler objPooler;
    public GameObject Shootpoint;
    float AttackCD;
    bool canAttack;


    // Start is called before the first frame update
    void Start()
    {
        AttackAnim = GetComponent<Animator>();
        speed = 7.0f;
        objPooler = ObjectPooler.Instance;
        Col = GetComponentInChildren<CapsuleCollider>();
        Col.enabled = true;
        mRend = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer rend in mRend)
        {
            rend.enabled = true;
        }
        HealthUI = GameObject.Find("HealthUI");
        PlayerTrans = GameObject.Find("PlayerFunctionality").transform;
        Player = GameObject.FindGameObjectWithTag("Player");
        playerPos = PlayerTrans.transform.position;
        DigTime = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Clamp(AttackCD, 0, 2);
        int PlayerLayer = 1 << 11;
        RaycastHit hitPlayer;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.right, out hitPlayer, viewdistance, PlayerLayer) ||
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.right, out hitPlayer, viewdistance, PlayerLayer) ||
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), transform.right, out hitPlayer, viewdistance, PlayerLayer))
        {
            AttackAnim.SetInteger("AnimNum", 1);
            canAttack = true;
            Debug.DrawRay(transform.position, transform.right * hitPlayer.distance, Color.yellow);
        }
        else
        {
            canAttack = false;
            AttackAnim.SetInteger("AnimNum", 0);
            Debug.DrawRay(transform.position, transform.right * 15, Color.white);
        }
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), -transform.right, out hitPlayer, viewdistance, PlayerLayer) ||
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), -transform.right, out hitPlayer, viewdistance, PlayerLayer) ||
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), -transform.right, out hitPlayer, viewdistance, PlayerLayer))
        {
            AttackAnim.SetInteger("AnimNum", 1);
            canAttack = true;
            Debug.DrawRay(transform.position, -transform.right * hitPlayer.distance, Color.yellow);
        }
        else
        {
            //canAttack = false;
            Debug.DrawRay(transform.position, -transform.right * 15, Color.white);
        }

        if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), -transform.right, out hitPlayer, 4, PlayerLayer))
        {
            Dig();
        }
        else
        {
            if (gameObject.GetComponentInChildren<EnemyHealth>() != null)
                gameObject.GetComponentInChildren<EnemyHealth>().canTakeDamage = true;
            foreach (MeshRenderer rend in mRend)
            {
                rend.enabled = true;
            }
            Col.enabled = true;
            //canAttack = true;
        }


        AttackCD -= Time.deltaTime;
        if(AttackCD <= 0 && canAttack)
        {
            Attack();
        }

        
        if (gameObject.GetComponentInChildren<EnemyHealth>().health <= 0)
        {
            canAttack = false;
        }
    }

    void Dig()
    {
        canAttack = false;
        gameObject.GetComponentInChildren<EnemyHealth>().canTakeDamage = false;
        DigTime -= Time.deltaTime;
        if(DigTime > 0)
        {
            foreach (MeshRenderer rend in mRend)
            {
                rend.enabled = false;
            }
            Col.enabled = false;
            Vector3.MoveTowards(transform.position, new Vector3(-playerPos.x, transform.position.y, transform.position.z), speed);
        }
        DigTime = 4.0f;
    }

    void Attack()
    {
        objPooler.SpawnFromPool("DigEnemyProj", Shootpoint.transform.position, Quaternion.identity);
        AttackCD = 1.0f;
    }
}
