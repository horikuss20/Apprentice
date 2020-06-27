using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private GameObject lwall;
    private GameObject rwall;
    private GameObject enemy1;
    private GameObject enemy2;
    private GameObject enemy3;
    private GameObject enemy4;
    private GameObject enemy5;
    private GameObject enemy6;
    private GameObject enemy7;
    // Start is called before the first frame update
    void Start()
    {
        enemy1 = GameObject.Find("1");
        enemy2 = GameObject.Find("2");
        enemy3 = GameObject.Find("3");
        enemy4 = GameObject.Find("4");
        enemy5 = GameObject.Find("5");
        enemy6 = GameObject.Find("6");
        enemy7 = GameObject.Find("7");
        lwall = GameObject.Find("LeftRoomWall");
        rwall = GameObject.Find("RightRoomWall");
        lwall.SetActive(false);
        rwall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!enemy1.activeInHierarchy && !enemy2.activeInHierarchy && !enemy3.activeInHierarchy && !enemy4.activeInHierarchy 
            && !enemy5.activeInHierarchy && !enemy6.activeInHierarchy && !enemy7.activeInHierarchy)
        {
            Destroy(lwall);
            Destroy(rwall);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            lwall.SetActive(true);
            rwall.SetActive(true);
        }
    }
}
