using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public float explosionTimer = 0.27f;
    Collider[] hitColliders;
    private bool Upgraded = false;
    private AudioSource magicSource;
    private AudioClip meteorClip;

    private void Start()
    {
        if (GameObject.Find("UpgradedMeteorSpawner(Clone)") == false)
        {
            Upgraded = false;
        }
        else 
        {
            Upgraded = true;
        }
        magicSource = GameObject.Find("SoundEffectPlayer").GetComponent<AudioSource>();
        meteorClip = GameObject.Find("PlayerFunctionality").GetComponent<MagicSpells>().meteorClip;

    }

    private void Update()
    {
      

        if (explosionTimer <= -0.1f)
        {
            Destroy(gameObject);
            magicSource.PlayOneShot(meteorClip, 0.7f);
        }

        hitColliders = Physics.OverlapSphere(transform.position, 3);
        explosionTimer -= Time.smoothDeltaTime;
        if (explosionTimer >= 0)
        {
            gameObject.transform.Translate(-gameObject.transform.up * .6f);
            //foreach (Collider nearbyObj in hitColliders)
            //{
            //    if (nearbyObj.tag == "PatrolEnemy")
            //    {
            //        explosionTimer = 0f;
            //    }
            //}
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, .5f);
            gameObject.transform.localScale = new Vector3(10, 10, 10);
           if(Upgraded == true)
            {
                foreach (Collider nearbyObj in hitColliders)
                {
                    if (nearbyObj.tag == "PatrolEnemy")
                    {

                        nearbyObj.GetComponent<EnemyHealth>().MeteorHealth -= 5f;

                        if (nearbyObj.GetComponent<ShieldEnemy>())
                        {
                            nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth -= 5f;

                            if (nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth == 0)
                            {
                                nearbyObj.GetComponent<EnemyHealth>().MeteorHealth -= 5f;
                            }
                        }
                    }
                }
            }
           if(Upgraded == false)
            {
                foreach (Collider nearbyObj in hitColliders)
                {
                    if (nearbyObj.tag == "PatrolEnemy")
                    {

                        nearbyObj.GetComponent<EnemyHealth>().health -= 10f;

                        if (nearbyObj.GetComponent<ShieldEnemy>())
                        {
                            nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth -= 10f;

                            if (nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth == 0)
                            {
                                nearbyObj.GetComponent<EnemyHealth>().health -= 10f;
                            }
                        }
                    }
                }
            }
           

          
        }

    }
}
