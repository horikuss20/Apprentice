using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    #region Variables
    public GameObject PlayerGO;
    ObjectPooler objPooler;
    public GameObject ShootPoint;
    public float ShootCD = 1.0f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Var Set
        objPooler = ObjectPooler.Instance;
        PlayerGO = GameObject.FindGameObjectWithTag("Player");
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        BallistaShoot();
    }

    public void BallistaShoot()
    { 
         ShootCD -= Time.deltaTime;
         if (ShootCD < 0)
         {
            objPooler.SpawnFromPool("Arrow", ShootPoint.transform.position, ShootPoint.transform.rotation);
            ShootCD = 2.0f;
         } 
    }
}
