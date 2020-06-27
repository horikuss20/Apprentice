using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBolt : MonoBehaviour
{
    GameObject Player;
    Collider[] hitColliders;
    public Vector3 playerPos;
    public Transform PlayerTrans;
    public float freezeTime;
    GameObject Gumbis;
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Gumbis = GameObject.Find("Gumbis (1)");
        freezeTime = 1.0f;
        PlayerTrans = GameObject.Find("PlayerFunctionality").transform;
        Player = GameObject.FindGameObjectWithTag("Player");
        playerPos = PlayerTrans.transform.position;
        transform.LookAt(playerPos);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        transform.position += transform.forward * 5 * Time.deltaTime;
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
                Gumbis.GetComponent<GumbisBoss>().freeze = true;
                Debug.Log("Player hit");
                gameObject.SetActive(false);
                lifeTime = 8.0f;
            }
        }
    }
}
