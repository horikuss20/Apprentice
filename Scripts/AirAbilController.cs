using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAbilController : MonoBehaviour
{
    private Transform groundCheck;
    private MeshRenderer pMesh;
    private Collider pCol;
    public Animator jumpAnim;
    public GameObject patrolEnemy;
    CharacterController controller;
    public GameObject Egg;
    public GameObject EggDropper;
    Vector3 inputs;
    #region Floats
    float speed;
    float jumpTimer;
    public float jumpSpeed;
    float gravity = 9.8f;
    float peckRange;
    float fallMultiplier = 2.5f;
    
    #endregion
    #region Bools
    bool isMoving;
    bool isJumping;
    bool canPeck;
    #endregion
    int extraJumps;
    public int maxJumps = 3;
    LayerMask Ground = 9;
    private Vector3 moveDirection = Vector3.zero;
    ObjectPooler objPooler;

    float Horizontal; 
    float Vertical; 

    void Start()
    {
        #region Setting Components
        controller = gameObject.GetComponent<CharacterController>();
        groundCheck = gameObject.transform.GetChild(0);
        pMesh = GetComponent<MeshRenderer>();
        pCol = GetComponent<Collider>();
        jumpAnim = GetComponentInChildren<Animator>();
        patrolEnemy = GameObject.FindGameObjectWithTag("PatrolEnemy");
        #endregion
        speed = 5.0f;
        jumpSpeed = 7.0f;
        extraJumps = 0;
        peckRange = 2.0f;
        gameObject.transform.position = new Vector3(0, 2, 0);
        objPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        moveDirection.x = Horizontal * speed;

        //if (moveDirection != Vector3.zero) transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        #region Peck Raycast
        int layerMask = 1 << 13;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, peckRange, layerMask))
        {
             Debug.DrawRay(transform.position, transform.forward * peckRange, Color.yellow);
             canPeck = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 2, Color.white);
            canPeck = false;
        }
        #endregion
        Peck();
        EggDrop();
        //Jumping();
        //#region Movement
        //moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        //controller.Move(moveDirection * Time.deltaTime);
        //#endregion
    }

    void Jumping()
    {
        if (gameObject.GetComponentInParent<CharacterController>().isGrounded)
        {
            extraJumps = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;
                extraJumps++;
            }
        }
        if (!gameObject.GetComponentInParent<CharacterController>().isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && extraJumps < maxJumps)
            {
                moveDirection.y = jumpSpeed;
                extraJumps++;
            }
        }
    }
    void Peck()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPeck == true)
        {
            patrolEnemy.GetComponent<PatrolEnemy>().health -= 1;
        }
    }

    void EggDrop()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            objPooler.SpawnFromPool("EggBomb", transform.position, Quaternion.identity);
        }
    }
}
