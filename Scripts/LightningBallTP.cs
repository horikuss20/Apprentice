using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBallTP : MonoBehaviour
{
    Collider[] hitColliders;
	bool stopMove;
    void Start()
    {

    }


    void Update()
    {
		
        hitColliders = Physics.OverlapSphere(transform.position, 1f);
        Damage();
    }
    void Damage()
    {
        foreach (Collider nearbyObj in hitColliders)
        {
            if (nearbyObj.tag == "PatrolEnemy")
            {
				if (!nearbyObj.GetComponent<ShieldEnemy>())
				{
					nearbyObj.GetComponent<EnemyHealth>().TakeDamage(.035f);
				}
				if (nearbyObj.GetComponent<ShieldEnemy>())
				{
					nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth -= .035f;

					if (nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth == 0)
					{
						nearbyObj.GetComponent<EnemyHealth>().TakeDamage(.035f);
					}
				}
			}
        }

    }
}
