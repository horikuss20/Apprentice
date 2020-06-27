using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShowerMeteor : MonoBehaviour
{
    Collider[] hitColliders;
    private float damage = .4f;
    private bool canBurn = true;
    private float LifeTime = .5f;
    // Update is called once per frame
    void Update()
    {
        LifeTime -= Time.smoothDeltaTime;
        if (LifeTime <= 0)
        {
            Destroy(gameObject);
        }
        hitColliders = Physics.OverlapSphere(transform.position, .5f);
        transform.position += -transform.up * 20 * Time.deltaTime;
        foreach (Collider nearbyObj in hitColliders)
        {
            if (nearbyObj.tag == "PatrolEnemy")
            {
                nearbyObj.GetComponent<EnemyHealth>().TakeDamage(damage);
                if(canBurn == true)
                {
                    nearbyObj.GetComponent<EnemyHealth>().hitBurn();
                }

                if (nearbyObj.GetComponent<ShieldEnemy>())
                {
                    nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth -= damage;

                    if (nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth == 0)
                    {
                        nearbyObj.GetComponent<EnemyHealth>().TakeDamage(damage);
                        if (canBurn == true)
                        {
                            nearbyObj.GetComponent<EnemyHealth>().hitBurn();
                        }
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
