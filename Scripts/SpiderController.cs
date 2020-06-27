using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour {
    
    private GameObject Spider;
    private Transform groundCheck;
    private MeshRenderer pMesh;
    private CapsuleCollider pCol;
    private Rigidbody pRB;
    private Rigidbody platformRB;
    private bool isGrounded;
    private bool isJumping;
    public GameObject spiderPlatform;
    public int jumpCount;
    public float platformTime = 5f;
    public float jumpResetTimer = 1f;
    float fallMultiplier = 2.5f;
    bool isMoving;
    float jumpHeight = 5;
    LayerMask Ground = 9;
    Vector3 inputs;
    bool canMove = true;
    float jumpTimer;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        #region Setting Components
        groundCheck = transform.GetChild(0);
        pMesh = GetComponent<MeshRenderer>();
        pCol = GetComponent<CapsuleCollider>();
        pRB = GetComponent<Rigidbody>();
        Spider = GameObject.FindGameObjectWithTag("Spider");
        platformRB = spiderPlatform.GetComponent<Rigidbody>();
        spiderPlatform.SetActive(false);
        speed = 6;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (pRB.velocity.y < 0)
        {
            //gravity increase to speed up fall
            pRB.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                pRB.velocity = new Vector3(pRB.velocity.x, jumpHeight, 0);
                isJumping = true;
                isGrounded = false;
                jumpTimer = .45f;
            }
        }

        //movement
        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");

        if (inputs != Vector3.zero) //if player is moving
        {
            transform.forward = inputs;
            isMoving = true;
        }

        platformCheck();
    }

    private void platformCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpCount += 1;
        }

            if (jumpCount == 1)
        {
            jumpResetTimer -= Time.deltaTime;
        }

        if (jumpCount >= 2)
        {
            platformTime -= Time.deltaTime;
            jumpResetTimer = 1f;
        }

        if (jumpResetTimer <= 0)
        {
            jumpCount = 0;
            jumpResetTimer = 1f;
        }

        if (platformTime <= 0)
        {
            spiderPlatform.transform.parent = Spider.transform;
            spiderPlatform.SetActive(false);
            platformRB.constraints = RigidbodyConstraints.None;
            jumpCount = 0;
            platformTime = 5f;
        }
        if (jumpCount == 2)
        {
            spiderPlatform.SetActive(true);
            spiderPlatform.transform.parent = null;
            platformRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 9)
        {
            isJumping = false;
            isGrounded = true;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            pRB.MovePosition(pRB.position + inputs * speed * Time.fixedDeltaTime);
        }
    }
}
