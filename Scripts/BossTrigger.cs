using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject gumbis;
    public bool fight;
    public GameObject[] Walls;

    void Start()
    {
        gumbis = GameObject.Find("Gumbis (1)");
    }

    // Update is called once per frame
    void Update()
    {
        if(gumbis.GetComponent<EnemyHealth>().health <= 0)
        {
            foreach(GameObject Wall in Walls)
            {
                Wall.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            foreach(GameObject Wall in Walls)
            {
                Wall.SetActive(true);
            }
        }
    }
}
