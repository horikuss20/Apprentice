using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_1 : MonoBehaviour
{
    public GameObject Player;
    Vector3 playerPos;
    Transform PlayerTrans;
    public float AttackDist;
    public float PlayerDist;
    Collider[] hitColliders;
    public bool canAttack;
    [SerializeField]
    float attackRange;




    void Start()
    {
        PlayerTrans = GameObject.Find("PlayerFunctionality").transform;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        playerPos = PlayerTrans.transform.position;
        PlayerDist = Vector3.Distance(transform.position, playerPos);

        if (PlayerDist > 3 && canAttack == false)
        {            
            MoveToPlayer();
        }

        if(canAttack == true)
        {
            Attack();
        }


        int EnemyLayer = 1 << 13;
        RaycastHit hitEnemy;
        if (Physics.Raycast(transform.position, -transform.right, out hitEnemy, attackRange, EnemyLayer))
        {
            canAttack = true;
            Debug.DrawRay(transform.position, -transform.right * hitEnemy.distance, Color.yellow);
        }
        else
        {
            canAttack = false;
            Debug.DrawRay(transform.position, -transform.right * attackRange, Color.white);
        }

        if (Physics.Raycast(transform.position, transform.right, out hitEnemy, attackRange, EnemyLayer))
        {
            canAttack = true;
            Debug.DrawRay(transform.position, transform.right * hitEnemy.distance, Color.yellow);
        }
        else
        {
            canAttack = false;
            Debug.DrawRay(transform.position, transform.right * attackRange, Color.white);
        }


    }

    void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPos, 10 * Time.deltaTime);
        transform.LookAt(new Vector3(playerPos.x,transform.position.y, transform.position.z));
    }


    void Attack()
    {

    }

}
