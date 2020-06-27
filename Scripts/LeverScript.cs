using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public GameObject Door;
    public GameObject leverUp;
    public GameObject leverDown;
    private GameObject Light;
    private GameObject fPic;
    private AudioSource leverSource;
    public bool inTrigger;
    public bool doorOpen;

    // Start is called before the first frame update
    void Start()
    {
        inTrigger = false;
        leverUp.SetActive(false);
        Light = GameObject.Find("LevLight");
        Light.SetActive(false);
        fPic = GameObject.Find("FPic");
        fPic.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(inTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.F) && doorOpen == false)
            {
                leverDown.SetActive(false);
                leverUp.SetActive(true);
                doorOpen = true;
                inTrigger = false;
            }
        }

        if(inTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.F) && doorOpen == true)
            {
                leverDown.SetActive(true);
                leverUp.SetActive(false);
                Door.SetActive(true);
                doorOpen = false;
                inTrigger = false;
            }
        }

        if(doorOpen == true)
        {
            Door.SetActive(false);
        }

        if(doorOpen == false)
        {
            Door.SetActive(true);
        }

        if(inTrigger == true)
        {
            Light.SetActive(true);
            fPic.SetActive(true);
        }
        else
        {
            Light.SetActive(false);
            fPic.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        inTrigger = true;
        Light.SetActive(true);
        fPic.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }
}
