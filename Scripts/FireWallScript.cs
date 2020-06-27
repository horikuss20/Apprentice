using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallScript : MonoBehaviour
{
    public float duration = 6f;
    public bool enemyInTrigger;
    public GameObject Fire1;
    public GameObject Fire2;
    public GameObject Fire3;
    public GameObject Fire4;
    public GameObject Fire5;

    void Start()
    {
        Fire2.SetActive(false);
        Fire3.SetActive(false);
        Fire4.SetActive(false);
        Fire5.SetActive(false);
    }

    void Update()
    {
        if (duration <= 4.5)
        {
            Fire2.SetActive(true);
        }
        if (duration <= 4.2)
        {
            Fire3.SetActive(true);
        }
        if (duration <= 3.9)
        {
            Fire4.SetActive(true);
        }
        if (duration <= 3.7)
        {
            Fire5.SetActive(true);
        }
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
      
    }
}
