using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBolt : MonoBehaviour
{
    GameObject HealthUI;
    GameObject Player;
    Collider[] hitColliders;

    // Start is called before the first frame update
    void Start()
    {
        HealthUI = GameObject.Find("HealthUI");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += Vector3.right * (5 * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 7 * Time.deltaTime);
        hitColliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider nearbyObj in hitColliders)
        {
            if (nearbyObj.tag == "Wall" || nearbyObj.tag == "Ground")
            {
                //gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

    }

    public void OnObjectSpawn()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
			HealthUI.GetComponent<Health>().DamageHalf();
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
