using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public int Meteors = 1;
    private int UpgradedInt = 3;
    public bool Upgraded = false;
    private float SpawnTimer = .5f;
    public GameObject Meteor;
    private float DeathTimer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        if(Upgraded == true)
        {
            Meteors = UpgradedInt;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer -= Time.smoothDeltaTime;
        if (SpawnTimer <= 0 && Meteors != 0)
        {
            Instantiate(Meteor, transform.position, Quaternion.Euler(0, 0, 0));
            Meteors -= 1;
            SpawnTimer = .5f;          
        }
        if(Meteors <= 0)
        {
            DeathTimer -= Time.smoothDeltaTime;
            if(DeathTimer <= 0)
            {
                Destroy(gameObject);

            }
        }
    }
}
