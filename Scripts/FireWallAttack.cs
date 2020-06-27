using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallAttack : MonoBehaviour
{
    Collider[] hitColliders;
    void Start()
    {
        
    }

    void Update()
    {
        hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
        Damage();
    }
    void Damage()
    {
        foreach (Collider nearbyObj in hitColliders)
        {
            if (nearbyObj.tag == "PatrolEnemy")
            {
				
				
			    nearbyObj.GetComponent<EnemyHealth>().TakeDamage(.035f);
			
				if(nearbyObj.GetComponent<ShieldEnemy>())
				{
					nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth -= .035f;

					if(nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth == 0)
					{
						nearbyObj.GetComponent<EnemyHealth>().TakeDamage(.035f);
					}
				}
            }
        }

    }
}
