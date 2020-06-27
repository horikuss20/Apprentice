using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    public CheckpointMaster cm;
    private CharacterController CC;

    // Start is called before the first frame update
    void Awake()
    {
       
        cm = GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>();
       
     
    }
    private void Start()
    {
        CC = gameObject.GetComponent<CharacterController>();
        CC.enabled = false;
        gameObject.transform.position = cm.lastCheckPointPos;
        CC.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {

     
    }
    
}
