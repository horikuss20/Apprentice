using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabMovement : MonoBehaviour
{
    private Transform groundCheck;
    private MeshRenderer pMesh;
    private Collider pCol;
    private Rigidbody pRB;
    public float speed;
    bool canMove = true;
    Vector3 inputs;
    bool isMoving;

    //jump
    bool isJumping;
    public bool inWater = false;
    [SerializeField]
    float jumpTimer;
    public bool isGrounded;
    float jumpHeight = 5;
    LayerMask Ground = 9;
    float jumpGrav = 6;

    float fallMultiplier = 2.5f;


    void Start()
    {
        #region Setting Components
        groundCheck = gameObject.transform.GetChild(0);
        pMesh = GetComponent<MeshRenderer>();
        pCol = GetComponent<Collider>();
        pRB = GetComponent<Rigidbody>();
        #endregion
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Water();

        

        //jump
        jumpTimer -= Time.deltaTime;
        jumpTimer = Mathf.Clamp(jumpTimer, 0, Mathf.Infinity);

        if (pRB.velocity.y < 0)
        {
            //gravity increase to speed up fall
            pRB.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        //movement
        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");

        if (inputs != Vector3.zero) //if player is moving
        {
            //transform.forward = inputs;
            isMoving = true;
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            inWater = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            inWater = false;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            gameObject.GetComponent<Rigidbody>().MovePosition(gameObject.GetComponent<Rigidbody>().position + inputs * speed * Time.fixedDeltaTime);
        }
    }

    void Water()
    {     
        if (inWater == true)
        {
            Physics.gravity = new Vector3(0, -2f, 0);
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

           
            //pRB.velocity = Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else
        {
            Physics.gravity = new Vector3(0, -10, 0);
        }
    }
}
