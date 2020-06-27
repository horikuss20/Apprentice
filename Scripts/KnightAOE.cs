using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAOE : MonoBehaviour
{
	ParticleSystem knightPE;

	int damage = 1;
	float knightTimer = 0;
	public float radius = 4;
	Collider[] hits;

    void Update()
    {

		knightTimer -= Time.deltaTime;
		knightTimer = Mathf.Clamp(knightTimer, 0, 1);
		hits = Physics.OverlapSphere(transform.position, radius);

		if (hits != null)
		{
			if (knightTimer == 0)
			{
				foreach (Collider GO in hits)
				{
					if (GO.tag == "PatrolEnemy")
					{
						Debug.Log("Hit");
						GO.transform.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
					}
				}
				knightTimer = 1;
			}
		}
		
    }

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Debug.DrawLine(transform.position, transform.position + Vector3.forward);
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
