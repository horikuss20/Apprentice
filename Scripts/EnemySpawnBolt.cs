using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBolt : MonoBehaviour
{

    GameObject HealthUI;
    GameObject Player;
    public float lifeTime;
    Collider[] hitColliders;
    public Transform PlayerTrans;
    public Vector3 playerPos;
    public GameObject ZerkerEnemy;
    public GameObject Bolt;
    bool canMove;
    ObjectPooler objPooler;


    // Start is called before the first frame update
    void Start()
    {
        objPooler = ObjectPooler.Instance;
        canMove = true;
        PlayerTrans = GameObject.Find("PlayerFunctionality").transform;
        playerPos = PlayerTrans.transform.position;
        lifeTime = 8.0f;
        HealthUI = GameObject.Find("HealthUI");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnObjectSpawn()
    {
        Debug.Log("im back");
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        transform.position += -transform.up * 3 * Time.deltaTime;


        hitColliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider nearbyObj in hitColliders)
        {
            if (nearbyObj.tag == "Wall" || nearbyObj.tag == "Ground")
            {
                objPooler.SpawnFromPool("Zerker", new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z + 0.3f), Quaternion.identity);
                gameObject.SetActive(false);
            }
            else
            {
                transform.position += -transform.up * 3 * Time.deltaTime;
            }
            if (nearbyObj.gameObject.tag == "Player")
            {
                HealthUI.GetComponent<Health>().Damage(1);
                gameObject.SetActive(false);
                lifeTime = 8.0f;
            }
        }
    }
}

