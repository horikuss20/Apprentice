using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionDamage : MonoBehaviour
{
    public GameObject patrolEnemy;

    private void Start()
    {
        patrolEnemy = GameObject.FindGameObjectWithTag("PatrolEnemy");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PatrolEnemy"))
        {
            patrolEnemy.GetComponent<PatrolEnemy>().health -= 1;
        }
    }
}
