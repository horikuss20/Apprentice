using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_2 : MonoBehaviour
{
    public GameObject Player;
    Vector3 playerPos;
    Transform PlayerTrans;
    public float AttackDist;
    public float PlayerDist;
    bool canAttack;
    bool isGrounded;
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

        if(isGrounded == false)
        {

        }

        if (PlayerDist > 3 && canAttack == false)
        {
            MoveToPlayer();
        }

        if (canAttack == true)
        {
            Attack();
        }

        int WallLayer = 1 << 13;
        RaycastHit hitEnemy;
        if (Physics.Raycast(transform.position, -transform.right, out hitEnemy, attackRange, WallLayer))
        {
            canAttack = true;
            Debug.DrawRay(transform.position, -transform.right * hitEnemy.distance, Color.yellow);
        }
        else
        {
            canAttack = false;
            Debug.DrawRay(transform.position, -transform.right * attackRange, Color.white);
        }

        if (Physics.Raycast(transform.position, transform.right, out hitEnemy, attackRange, WallLayer))
        {
            canAttack = true;
            Debug.DrawRay(transform.position, transform.right * hitEnemy.distance, Color.yellow);
        }
        else
        {
            canAttack = false;
            Debug.DrawRay(transform.position, transform.right * attackRange, Color.white);
        }

        int Ground = 1 << 9;
        RaycastHit hitGround;
        if(Physics.Raycast(transform.position, -transform.up, out hitGround, 1.75f, Ground))
        {
            isGrounded = true;
        }

    }

    void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPos.x - 3, playerPos.y, transform.position.z), 7 * Time.deltaTime);
    }


    void Attack()
    {
        //do attack here
    }
}
