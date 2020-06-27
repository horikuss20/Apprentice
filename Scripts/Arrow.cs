using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    #region Variables
    GameObject HealthUI;
    GameObject Player;
    Collider[] hitColliders;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Var Set
        HealthUI = GameObject.Find("HealthUI");
        Player = GameObject.FindGameObjectWithTag("Player");
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * 8 * Time.deltaTime;

        #region Collision Detection
        hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (Collider nearbyObj in hitColliders)
        {
            if (nearbyObj.tag == "Wall" || nearbyObj.tag == "Ground")
            {
                Debug.Log("Wall");
                gameObject.SetActive(false);
            }
            if (nearbyObj.tag == "Player")
            {
                Debug.Log("Player hit");
                HealthUI.GetComponent<Health>().Damage(1);
                gameObject.SetActive(false);
            }
        }
        #endregion
    }
}
