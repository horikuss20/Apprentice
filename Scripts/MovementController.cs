using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	//PLAYER MOVEMENT
	protected CharacterController playerController;
	public float speed;
	public bool canMove = true;
    [SerializeField]
	float jumpSpeed;
	public int jumpNum;
	public float gravity = 9.8f;
	public Vector3 moveDirection = Vector3.zero;
	float Horizontal;
	float jumpTimer;
    public float AirTime;
    bool CountAirTime;
	Animator playerAnim;
	float glideTimer = 2;
	float attackTimer;
	bool glidebool;
	bool hasjumped;
	public float dashtimer;
	public float dashspeed = 18.6f;
	bool diddash;
	int dashnum;
	float dashcooldown;
    public GameObject FreezeBlock;
	//PLAYER MOVEMENT

	bool playanim;
	//Health UI Reference
	public GameObject HealthUI;

	//PLAYER AUDIO
	private AudioSource playerSource;
	public AudioClip playerAttack;
	public AudioClip playerJump;
    public AudioClip dash;


	//MAGIC SYSTEM
	MagicSystem msRef;
	GameObject staff;
	private GameObject waterForm;

	//Attack
	public float AttackRange = 3f;
	public bool canAttack = false;
	public GameObject patrolEnemy;

	void Start()
	{
		//MAGIC
		staff = GameObject.Find("Staff");
		msRef = GameObject.Find("MagicUI").GetComponent<MagicSystem>();
        waterForm = GameObject.Find("WaterForm");
		waterForm.SetActive(false);
		//MAGIC

		//PLAYER
		jumpSpeed = 12;
		playerController = gameObject.GetComponent<CharacterController>();
		//PLAYER

		Time.timeScale = 1.0f;

		//HEALTH
		HealthUI = GameObject.Find("HealthUI");
		//HEALTH

		//ANIMATION
		playerAnim = GetComponent<Animator>();
		//ANIMATION

		//AUDIO
		playerSource = GetComponent<AudioSource>();
        //AUDIO
        FreezeBlock.SetActive(false);
	}

	protected virtual void Update()
	{
		if (Time.timeScale == 0)
		{
			return;
		}

		dashspeed = 18.6f;

		dashtimer -= Time.deltaTime;
		dashtimer = Mathf.Clamp(dashtimer, 0, 1);

		dashcooldown -= Time.deltaTime;
		dashcooldown = Mathf.Clamp(dashcooldown, 0, 1);

		jumpTimer -= Time.deltaTime;
		jumpTimer = Mathf.Clamp(jumpTimer, 0, 1);

        int groundLayer = 1 << 9;

        RaycastHit hitground;
		
			Debug.DrawRay(transform.position, transform.forward * 0.6f, Color.red);
			Debug.DrawRay(transform.position, -transform.forward * 0.6f, Color.red);

        if (Physics.Raycast(transform.position, -transform.up, out hitground, 1.5f, groundLayer))
        {
            CountAirTime = false;
            Debug.DrawRay(transform.position, -transform.up, Color.green);
        }
        else CountAirTime = true;

		if (diddash && dashtimer == 0)
		{
			canMove = true;
			diddash = false;
		}

        if (CountAirTime)
        {
            AirTime += Time.deltaTime;
        }
		//PLAYER

		// Apply gravity


		attackTimer -= Time.deltaTime;
		attackTimer = Mathf.Clamp(attackTimer, 0, 10f);

		glideTimer -= Time.deltaTime;

		Move();
		//PLAYER
	}


	//PLAYER MOVEMENT
	protected virtual void Move()
	{

        if (playerController.isGrounded && canMove)
        {
            if (waterForm.activeInHierarchy)
            {
                speed = 20;
            }
            if (!waterForm.activeInHierarchy)
                speed = 10;

			gameObject.GetComponent<Animator>().SetInteger("AnimNum", 1);

            gravity = 16;

            dashnum = 1;

            moveDirection = new Vector3(Horizontal, 0, 0);

            if (moveDirection != Vector3.zero) transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            jumpNum = 2;

            hasjumped = false;
        }

		if(canMove)
			Horizontal = Input.GetAxis("Horizontal");

		if (!diddash && Input.GetKeyDown(KeyCode.LeftShift) && dashnum != 0 && dashcooldown == 0)
		{
			Dash();
		}

		if(jumpTimer != 0)
		{
			hasjumped = true;
		}

		if(jumpTimer == 0 && hasjumped)
		{
			gravity = 30;
		}

		if (!waterForm.activeInHierarchy)
		{
			if (Input.GetButtonDown("Jump") && jumpNum != 0 && canMove)
			{
                CountAirTime = true;
				gravity = 20;
                Jump();
				jumpNum--;
			}
			//GLIDING
			if (Input.GetButton("Jump") && glideTimer <= 0 && jumpNum != 2 && glidebool)
			{
				gravity = 1f;
				playanim = true;
			}
			if (Input.GetButtonUp("Jump") && glidebool)
			{
				gravity = 9.8f;
				playanim = false;
			}
			//GLIDING
		}

		// Move the controller
	}
	//PLAYER MOVEMENT

	private void FixedUpdate()
	{
		if (Time.timeScale == 0)
		{
			return;
		}

		if (canMove)
		{
			moveDirection.x = Horizontal * 10;
		}

		if (!canMove)
		{
			if (diddash)
			{
				if (diddash)
				{
					if (Horizontal == 0)
					{
						Horizontal = 1;
					}
					else if(Horizontal >= .01f)
					{
						Horizontal = 1;
					}
					else if(Horizontal < -.01f)
					{
						Horizontal = -1;
					}
					moveDirection.x = Horizontal * 18.6f;
				}
				else moveDirection.x = 0;
			}
		}
			playerController.Move(moveDirection * Time.fixedDeltaTime);


			if (!diddash)
			{
				moveDirection.y -= gravity * Time.deltaTime * 2;
			}
		
	}
		private void Dash()
		{
			dashnum--;
			dashtimer = .25f;
			dashcooldown = .75f;
			canMove = false;
			diddash = true;
			moveDirection.y = 0;
            playerSource.PlayOneShot(dash);
        }

	//PLAYER JUMP
	protected virtual void Jump()
	{
		jumpTimer = .35f;
		moveDirection.y = jumpSpeed;
		glideTimer = .55f;
		hasjumped = true;
		playerSource.PlayOneShot(playerJump);
		GetComponent<Animator>().SetInteger("AnimNum", 2);

	}
	//PLAYER JUMP
}
