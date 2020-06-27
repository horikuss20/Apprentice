using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : MonoBehaviour
{
    public GameObject[] VineWall;
    private bool canFade;
    private Color alphaColor;
    private float timeToFade = 1.0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject vines in VineWall)
            {
                vines.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("out");
            foreach (GameObject vines in VineWall)
            {
                vines.SetActive(true);
            }
        }
    }
}
