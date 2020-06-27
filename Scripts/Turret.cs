using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float shootRotation;
    bool inRange;
    [SerializeField] GameObject ShootPoint, Projectile;
    float attackCD = 1;
    float AttackRange;



    // Update is called once per frame
    void Update()
    {
        if(inRange == true)
        {
            Attack();

        }
        attackCD -= Time.deltaTime;
        int EnemyLayer = 1 << 13;
        RaycastHit hitEnemy;
        if (Physics.Raycast(transform.position + new Vector3(0,.3f,0), -transform.right, out hitEnemy, 15, EnemyLayer))
        {

            inRange = true;
            Debug.DrawRay(transform.position, -transform.right * hitEnemy.distance, Color.yellow);
        }
        else
        {
            inRange = false;
            Debug.DrawRay(transform.position, -transform.right * 15, Color.white);
        }
    }


    void Attack()
    {
        if (attackCD <= 0 && shootRotation == 0)
        {
            Instantiate(Projectile, ShootPoint.transform.position, Quaternion.Euler(0,0,0));
            attackCD = 0.2f;
        }
        if (attackCD <= 0 && shootRotation == 1)
        {
            Instantiate(Projectile, ShootPoint.transform.position, Quaternion.Euler(0,0,0));
            attackCD = 0.2f;
        }
    }
}
