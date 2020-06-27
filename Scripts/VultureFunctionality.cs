using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureFunctionality : MonoBehaviour
{
    //public float speed;
    //Transform startTrans, endTrans, vultureTrans;
    public float stopTimer;
    Animation anim;
    //Animator swoopAnim;
    //bool towards = true;
    public bool isFlapping = false;
    RaycastHit hit;
    float detectionRange = 5.0f;
    public GameObject vultureDetect;

    // Start is called before the first frame update
    void Start()
    {
        stopTimer = 3.5f;
        //speed = 2.5f;
        //startTrans = gameObject.transform.GetChild(0);
        //endTrans = gameObject.transform.GetChild(1);
        //vultureTrans = gameObject.transform.GetChild(2);
        anim = GetComponent<Animation>();
        //swoopAnim = GameObject.Find("VultureGO").GetComponent<Animator>();
        anim.Play("VultureFlapping");
        vultureDetect = GameObject.Find("Detection");
    }

    // Update is called once per frame
    void Update()
    {
        //if (towards == true)
        //{
        //    vultureTrans.LookAt(startTrans);
        //    vultureTrans.position = Vector3.MoveTowards(vultureTrans.position, startTrans.position, speed * Time.deltaTime);

        //    if (vultureTrans.position == startTrans.position)
        //    {
        //        stopTimer -= Time.deltaTime;
        //        if (stopTimer <= 0)
        //        {
        //            towards = false;
        //            stopTimer = 1.0f;
        //        }
                
        //    }

        //}
        //else
        //{
        //    vultureTrans.LookAt(endTrans);
        //    vultureTrans.position = Vector3.MoveTowards(vultureTrans.position, endTrans.position, speed * Time.deltaTime);

        //    if (vultureTrans.position == endTrans.position)
        //    {
        //        stopTimer -= Time.deltaTime;
        //        if (stopTimer <= 0)
        //        {
        //            towards = true;
        //            stopTimer = 1.0f;
        //        }
        //    }
        //}

        SwoopAttack();
        Debug.DrawRay(vultureDetect.transform.position, vultureDetect.GetComponent<Transform>().TransformDirection(Vector3.right) * detectionRange, Color.red);
    }

    void SwoopAttack()
    {
        Vector3 downcast = vultureDetect.GetComponent<Transform>().TransformDirection(Vector3.right);
        Vector3 origin = vultureDetect.transform.position;

        if (anim.IsPlaying("VultureFlapping"))
        {
            isFlapping = true;
            if (isFlapping == true)
            {
                if (Physics.Raycast(origin, downcast, out hit, detectionRange))
                {
                    if (hit.transform.gameObject.tag == "Player")
                    {
                        anim.Stop("VultureFlapping");
                        anim.Play("VultureSwoop");
                    }
                }
            }
        }
        
        if (anim.IsPlaying("VultureSwoop"))
        {
            isFlapping = false;
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0)
            {
                anim.Stop("VultureSwoop");
                anim.Play("VultureFlapping");
                stopTimer = 3.5f;
            }
        }
        
        //if (Physics.Raycast(origin, downcast, out hit, detectionRange))
        //{
        //    if (hit.transform.gameObject.tag == "Player")
        //    {
        //        anim.Stop("VultureFlapping");
        //        anim.Play("VultureSwoop");
        //    }
        //}

    }
}
