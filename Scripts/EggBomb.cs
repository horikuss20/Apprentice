using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBomb : MonoBehaviour, IPooledObjects
{
    Collider[] hitColliders;
    public GameObject patrolEnemy;
    public ParticleSystem explosion;
    public float explodetime;
    float speed;
    Vector3 move;
    bool canMove;
    bool canEmit;
    bool particleSystemPlayed;


    public void OnObjectSpawn()
    {
        explodetime = 0.3f;
        patrolEnemy = GameObject.FindGameObjectWithTag("PatrolEnemy");
        speed = 8;
        move = Vector3.zero;
    }



    // Update is called once per frame
    void Update()
    {
        


        if (explodetime < 0)
        {
            gameObject.SetActive(false);
            explodetime = 0.3f;
        }
        
        hitColliders = Physics.OverlapSphere(transform.position, 5);
        int layerMask = 1 << 9;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.5f, layerMask))
        {
            explodetime -= Time.deltaTime;
            explode();
            if (!particleSystemPlayed)
            {
                explosion.Play();
                particleSystemPlayed = true;
            }
            
            canMove = false;
            
        }
        else
        {
            Debug.DrawRay(transform.position, -transform.up * 0.5f, Color.white);
            canMove = true;
            particleSystemPlayed = false;
        }

        move.y = move.y - (9.8f * Time.deltaTime);
        if (canMove == true)
        {
            transform.position += Vector3.down * (speed * Time.deltaTime);
        }
    }


    void explode()
    {
        
        
        foreach(Collider nearbyObj in hitColliders)
        {
            if(nearbyObj.tag == "PatrolEnemy")
            {
                if(nearbyObj.GetComponent<PatrolEnemy>())
                {
                    nearbyObj.GetComponent<PatrolEnemy>().health -= 1;
                    nearbyObj.GetComponent<PatrolEnemy>().speed = 0;
                }

                if(nearbyObj.GetComponent<BallistaPatrolNew>())
                {
                    nearbyObj.GetComponent<BallistaPatrolNew>().health -= 1;
                    nearbyObj.GetComponent<BallistaPatrolNew>().speed = 0;
                }
            }
            Debug.Log("DO DAMAAJ");
        }
    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}
