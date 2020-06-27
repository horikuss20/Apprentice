using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabShield : MonoBehaviour
{
    public GameObject Parent;
    public GameObject shield;
    public bool ShieldOn = false;
    // Start is called before the first frame update
    void Start()
    {
        Parent = GameObject.Find("PlayerFunctionality");
        shield = Parent.transform.Find("Shield").gameObject;
        shield.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        ShieldOn = GameObject.Find("HealthUI").GetComponent<Health>().Iframe;
        if (ShieldOn == true)
        {
            shield.SetActive(true);
        }
        else
        {
            shield.SetActive(false);
        }
    }
    
}
