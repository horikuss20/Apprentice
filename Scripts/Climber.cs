using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    [SerializeField]
    public Transform targetPos, startPos;
    public bool towards = true;
    [SerializeField]
    float speed;

    public GameObject FreezeBlock;
    public float FreezeLevel = 0;
    public float LastFreezeLevel = 0;
    public float FreezeTime = 5f;
    bool canPatrol;
    public GameObject HealthUI;

    // Start is called before the first frame update
    void Start()
    {
        HealthUI = GameObject.Find("HealthUI");
        FreezeBlock.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        FreezeLevel = Mathf.Clamp(FreezeLevel, 0, 1);
        Freeze();
        UnFreeze();
        if (FreezeLevel >= 1)
        {
            FreezeBlock.SetActive(true);
            canPatrol = false;
        }
        if (FreezeLevel < 1)
        {
            FreezeBlock.SetActive(false);
            canPatrol = true;
        }
    }

    void FixedUpdate()
    {
        if (targetPos != null)
            if (towards && canPatrol == true)
            {
                transform.position += transform.up * speed * Time.deltaTime;
                if (Vector3.Distance(transform.position, targetPos.position) < 1.0f)
                {
                    towards = false;
                }
            }
            else if (canPatrol == true)
            {
                transform.position += -transform.up * speed * Time.deltaTime;
                if (Vector3.Distance(transform.position, startPos.position) < 1.0f)
                {
                    {
                        towards = true;
                    }
                }
            }
    }

    void Freeze()
    {
        if (FreezeLevel < LastFreezeLevel && FreezeLevel <= 1 && FreezeLevel >= 0)
        {
            speed = speed + .6f;
            LastFreezeLevel = FreezeLevel;
        }
        if (FreezeLevel > LastFreezeLevel && FreezeLevel <= 1 && FreezeLevel >= 0)
        {
            speed = speed - .6f;
            LastFreezeLevel = FreezeLevel;
        }

    }
    void UnFreeze()
    {
        if (FreezeLevel >= 1)
        {
            FreezeTime -= Time.smoothDeltaTime;
            if (FreezeTime <= 0)
            {
                FreezeLevel -= 1;
                FreezeTime = 10;
            }
        }

    }


    public void AddFreeze()
    {
        FreezeLevel++;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            HealthUI.GetComponent<Health>().Damage(1);
        }
    }
}
