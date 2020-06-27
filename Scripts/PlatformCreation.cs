using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreation : MonoBehaviour
{

    private GameObject Spider;
    private Rigidbody platformRB;
    public GameObject spiderPlatform;
    public int jumpCount;
    public float platformTime = 5f;
    public float jumpResetTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Spider = GameObject.FindGameObjectWithTag("Spider");
        platformRB = spiderPlatform.GetComponent<Rigidbody>();
        spiderPlatform.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpCount += 1;
        }

        if(jumpCount == 1)
        {
            jumpResetTimer -= Time.deltaTime;
        }

        if(jumpCount >= 2)
        {
            platformTime -= Time.deltaTime;
            jumpResetTimer = 1f;
        }

        if(jumpResetTimer <= 0)
        {
            jumpCount = 0;
            jumpResetTimer = 1f;
        }

        if(platformTime <= 0)
        {
            spiderPlatform.transform.parent = Spider.transform;
            spiderPlatform.SetActive(false);
            platformRB.constraints = RigidbodyConstraints.None;
            jumpCount = 0;
            platformTime = 5f;
        }
        spiderPlatform.transform.parent = Spider.transform;
        platformCheck();
    }

    private void platformCheck()
    {
        if (jumpCount == 2)
        {
            spiderPlatform.SetActive(true);
            spiderPlatform.transform.parent = null;
            platformRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
    }
}
