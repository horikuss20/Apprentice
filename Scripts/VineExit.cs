using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineExit : MonoBehaviour
{
    public GameObject VineWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            VineWall.SetActive(true);
        }
    }
}
