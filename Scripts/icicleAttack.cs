using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icicleAttack : MonoBehaviour
{
    private float Lifetime = 2f;
    Collider[] hitColliders;
    void Start()
    {

    }


    void Update()
    {
        transform.position += transform.up * 10 * Time.deltaTime;
        hitColliders = Physics.OverlapSphere(transform.position, .5f);
        Damage();
        Lifetime -= Time.smoothDeltaTime;
        if(Lifetime <= 0)
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
                if (nearbyObj.GetComponent<PatrolEnemy>())
                {
                    nearbyObj.GetComponent<PatrolEnemy>().AddFreeze();
                }
                if (nearbyObj.GetComponent<GumbisBoss>())
                {
                    nearbyObj.GetComponent<GumbisBoss>().AddFreeze();
                }
                if (nearbyObj.GetComponent<Climber>())
                {
                    nearbyObj.GetComponent<Climber>().AddFreeze();
                }
                if (nearbyObj.GetComponent<ZerkerEnemy>())
                {
                    nearbyObj.GetComponent<ZerkerEnemy>().AddFreeze();
                }
                if (nearbyObj.GetComponent<RangedEnemy>())
                {
                    nearbyObj.GetComponent<RangedEnemy>().AddFreeze();
                }
                if (nearbyObj.GetComponent<ShieldEnemy>())
                {
                    nearbyObj.GetComponent<ShieldEnemy>().AddFreeze();
                }

                Destroy(gameObject);
                }
            }
        }
      
    }

