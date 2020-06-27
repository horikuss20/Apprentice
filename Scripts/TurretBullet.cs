using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float bulletRot;
    Collider[] hitColliders;
    public float lifeTime;
    public GameObject Turret;
    // Start is called before the first frame update
    void Start()
    {
        bulletRot = GameObject.Find("Gnome Turret(Clone)").GetComponent<Turret>().shootRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletRot == 1)
        {
            transform.position += new Vector3(1 * -15, 0, 0) * Time.deltaTime;
        }
        if (bulletRot == 0)
        {
            transform.position += new Vector3(1 * 15, 0, 0) * Time.deltaTime;
        }

        hitColliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider nearbyObj in hitColliders)
        {
            if (nearbyObj.tag == "Wall" || nearbyObj.tag == "Ground")
            {
                Debug.Log("Wall");
                GameObject.Destroy(gameObject);
            }

            if (nearbyObj.tag == "PatrolEnemy")
            {
                nearbyObj.GetComponent<EnemyHealth>().TakeDamage(0.5f);

                if (nearbyObj.GetComponent<ShieldEnemy>())
                {
                    nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth -= 0.5f;

                    if (nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth == 0)
                    {
                        nearbyObj.GetComponent<EnemyHealth>().TakeDamage(0.5f);
                    }
                }
                GameObject.Destroy(gameObject);
            }
        }
    }
}
