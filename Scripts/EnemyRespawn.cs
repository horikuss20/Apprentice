using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    [SerializeField]
    GameObject EnemytoSpawn;

    public bool EnemyDead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemytoSpawn.activeInHierarchy == false)
        {
            EnemyDead = true;
        }

        if(EnemyDead == true)
        {
            Instantiate(EnemytoSpawn, transform.position, transform.rotation);
            EnemyDead = false;
        }
    }
}
