using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBolt : MonoBehaviour
{
    #region Variables
    GameObject HealthUI;
    GameObject Player;
    Collider[] hitColliders;
    public Vector3 playerPos;
    public Transform PlayerTrans;
    public float lifeTime;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Var Set
        PlayerTrans = GameObject.Find("PlayerFunctionality").transform;
        lifeTime = 8.0f;
        HealthUI = GameObject.Find("HealthUI");
        Player = GameObject.FindGameObjectWithTag("Player");
        #endregion
    }

    public void OnObjectSpawn()
    {

        playerPos = PlayerTrans.transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, playerPos, 3 * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        playerPos = PlayerTrans.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, playerPos, 3 * Time.deltaTime);

        #region Collision Dectection
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

        if (lifeTime <= 0)
        {
            gameObject.SetActive(false);
            lifeTime = 8.0f;
        }
    }
}
