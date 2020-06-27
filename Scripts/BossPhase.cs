using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase : MonoBehaviour
{

    public GameObject Cage;
    public GameObject Gumbis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Cage.activeInHierarchy == true)
        {
            Gumbis.GetComponent<GumbisBoss>().MaxRange = 0f;
            Gumbis.GetComponent<EnemyHealth>().canTakeDamage = false;
        }
        else
        {
            Gumbis.GetComponent<GumbisBoss>().MaxRange = 15f;
            Gumbis.GetComponent<EnemyHealth>().canTakeDamage = true;
        }
    }
}
