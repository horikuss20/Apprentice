using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockBolt : MonoBehaviour
{
    Collider[] hitColliders;
    private float Lifetime = .4f;
    private float damage = 1f;
    private float speed = 25;
    void Start()
    {

    }


    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        hitColliders = Physics.OverlapSphere(transform.position, .5f);
        Damage();
        Lifetime -= Time.smoothDeltaTime;
        if (Lifetime <= 0)
        {
            Destroy(gameObject);
        }

    }
    void Damage()
    {
        foreach (Collider nearbyObj in hitColliders)
        {

            if (nearbyObj.tag == "Wall" || nearbyObj.tag == "Ground")
            {
                Destroy(gameObject);
            }

            if (nearbyObj.tag == "PatrolEnemy")
            {
                nearbyObj.GetComponent<EnemyHealth>().TakeDamage(damage);

                if (nearbyObj.GetComponent<ShieldEnemy>())
                {
                    nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth -= damage;

                    if (nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth == 0)
                    {
                        nearbyObj.GetComponent<EnemyHealth>().TakeDamage(damage);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}

