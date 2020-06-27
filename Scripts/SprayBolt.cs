using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBolt : MonoBehaviour
{
    #region Variables
    GameObject HealthUI;
    GameObject Player;
    public float lifeTime;
    Collider[] hitColliders;
    public GameObject spraypoint;
    public Transform PlayerTrans;
    public Vector3 playerPos;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Var Set
        PlayerTrans = GameObject.Find("PlayerFunctionality").transform;
        playerPos = PlayerTrans.transform.position;
        spraypoint = GameObject.Find("Spraypoint");
        lifeTime = 8.0f;
        HealthUI = GameObject.Find("HealthUI");
        Player = GameObject.FindGameObjectWithTag("Player");
        #endregion

        transform.LookAt(playerPos);

    }

    public void OnObjectSpawn()
    {
        Debug.Log("im back");
    }

    // Update is called once per frame
    void Update()
    {
        #region Bolt Movement
        spraypoint = GameObject.Find("Spraypoint");
        lifeTime -= Time.deltaTime;
        //transform.position += -transform.up * 6 * Time.deltaTime;
        transform.position += transform.forward * 5 * Time.deltaTime;
        #endregion

        #region Collision Detection
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
        #endregion
    }
}
