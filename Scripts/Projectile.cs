using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Collider[] hitColliders;
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * 5 * Time.deltaTime;
        hitColliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider nearbyObj in hitColliders)
        {
            if (nearbyObj.tag == "Wall" || nearbyObj.tag == "Ground")
            {
                Debug.Log("Wall");
                gameObject.SetActive(false);
            }
            if (nearbyObj.gameObject.tag == "Player")
            { 
                Debug.Log("Player hit");
                gameObject.SetActive(false);
                lifeTime = 8.0f;
            }
        }
    }
}
