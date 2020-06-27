using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBlock : MonoBehaviour
{
    public bool TouchingSpike = false;

    void Update()
    {
        if(TouchingSpike == true)
        {
            GameObject.Find("HealthUI").GetComponent<Health>().DamageHalf();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        TouchingSpike = true;
    }
    void OnTriggerExit(Collider other)
    {
        TouchingSpike = false;
    }
}
