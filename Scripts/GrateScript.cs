using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrateScript : MonoBehaviour
{
    private bool WaterForm;
    private BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        WaterForm = GameObject.Find("PlayerFunctionality").GetComponent<MagicSpells>().inWaterForm;
       

        if (WaterForm == true)
        {
            bc.enabled = false;
        }
        else
        {
            bc.enabled = true;
        }
    }
}
