using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRemove : MonoBehaviour
{
    public GameObject FloorToRemove;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<PatrolEnemy>().health <= 0)
        {
            FloorToRemove.SetActive(false);
        }
    }
}
