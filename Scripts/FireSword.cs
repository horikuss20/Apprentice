using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSword : MonoBehaviour
{
    public bool attacking = true;
    private float timer;
    Collider[] hitColliders;
    public BoxCollider FBC;
    public bool hitEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("PlayerFunctionality").transform);
        timer = .25f;
      
    }

    // Update is called once per frame
    void Update()
    {
        FBC = gameObject.transform.Find("FireSwordCollider").GetComponent<BoxCollider>();
        hitColliders = Physics.OverlapBox(FBC.transform.position, FBC.size, Quaternion.identity);
        foreach (Collider nearbyObj in hitColliders)
        {
            if(hitEnemy == false)
            {
            if (nearbyObj.tag == "PatrolEnemy")
            {
                nearbyObj.GetComponent<EnemyHealth>().TakeDamage(3f);

                if (nearbyObj.GetComponent<ShieldEnemy>())
                {
                    nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth -= 3f;

                    if (nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth == 0)
                    {
                        nearbyObj.GetComponent<EnemyHealth>().TakeDamage(3f);
                    }
                }

                hitEnemy = true;
            }

            }
        }
        if (attacking == true)
        {
            transform.Rotate(0, 0, 10, Space.Self);
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            attacking = false;
            Destroy(gameObject);
        }
       
          
      
    }
}
