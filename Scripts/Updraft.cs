using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updraft : MonoBehaviour
{

    GameObject Player;
    bool inDraft;
    [SerializeField]
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(inDraft == true)
        {
            Player.GetComponent<MovementController>().gravity = -20f;  
        }
        else
        {
            Player.GetComponent<MovementController>().gravity = 20f;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        inDraft = true;
    }
    private void OnTriggerExit(Collider other)
    {
        inDraft = false;
    }
}
