using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    GameObject Player;
    GameObject HealthUI;
    Collider[] hitColliders;
    public Vector3 playerPos;
    public Transform PlayerTrans;
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        HealthUI = GameObject.Find("HealthUI");
        PlayerTrans = GameObject.Find("PlayerFunctionality").transform;
        Player = GameObject.FindGameObjectWithTag("Player");
        playerPos = PlayerTrans.transform.position;
        transform.LookAt(new Vector3(playerPos.x, playerPos.y, transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        transform.position += transform.forward * 20 * Time.deltaTime;
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
                HealthUI.GetComponent<Health>().Damage(1);
                gameObject.SetActive(false);
                lifeTime = 8.0f;
            }
        }
    }
}
