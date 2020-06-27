using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    private GameObject HealthUI;
    public float dmgTimer = 1.0f;
    bool inFire;
    private void Start()
    {
        HealthUI = GameObject.Find("HealthUI");
    }


    private void Update()
    {
        Mathf.Clamp(dmgTimer, 0, 1);
        if (inFire == true)
        {
            dmgTimer -= Time.deltaTime;
        }
        if (dmgTimer <= 0 && inFire == true)
        {
            HealthUI.GetComponent<Health>().Damage(1);
            dmgTimer = 1.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            inFire = true;
            HealthUI.GetComponent<Health>().Damage(1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inFire = false;
        dmgTimer = 1.0f;
    }


}
