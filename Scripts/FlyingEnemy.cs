using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    GameObject Player;
    Transform PlayerTrans;
    public GameObject Shootpoint;
    public GameObject Beam;
    float BeamTime;
    public Transform targetPos, startPos;
    public float speed;
    float AttackRange;
    float AttackCD;
    bool canPatrol;
    bool towards = true;
    // Start is called before the first frame update
    void Start()
    {
        canPatrol = true;
        BeamTime = 1.0f;
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerTrans = GameObject.Find("PlayerFunctionality").transform;
    }

    // Update is called once per frame
    void Update()
    {
        AttackRange = Vector3.Distance(Player.transform.position, transform.position);
        //Shootpoint.transform.LookAt(PlayerTrans);
        if (AttackRange <= 10)
        {
            canPatrol = false;
            AttackCD -= Time.deltaTime;
            if(AttackCD <= 0)
            {
                Attack();
            }
            
            //BeamTime -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        #region Patrol
        if(canPatrol == true)
        {
            if (targetPos != null)
                if (towards)
                {
                    //transform.LookAt(targetPos.position);
                    transform.position += -transform.right * speed * Time.deltaTime;
                    if (Vector3.Distance(transform.position, targetPos.position) < 1.0f)
                    {
                        towards = false;
                    }
                }
                else
                {
                    //transform.LookAt(startPos.position);
                    transform.position += transform.right * speed * Time.deltaTime;
                    if (Vector3.Distance(transform.position, startPos.position) < 1.0f)
                    {
                        {
                            towards = true;
                        }
                    }
                }
        }

        #endregion
    }

    void Attack()
    {
        Instantiate(Beam,Shootpoint.transform.position, Quaternion.identity);
        AttackCD = 2.0f;
        //Beam.SetActive(true);
        //if(BeamTime <= 0)
        //{
        //    Beam.SetActive(false);
        //}
    }
}
