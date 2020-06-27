using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TransformationControllerNew : MonoBehaviour
{
	//transformation game objects
	public GameObject PlayerGO, waterGO, ElephantGO, CrabGO, ChameleonGO, airformGO;
	//keeps track of current transformation
	public GameObject CurTrans;
	Vector3 CurPosition;
	Scene curScene;
	public AudioClip TransformClip;
	AudioSource gmSource;
	bool playSound = false;
	SpriteRenderer smokeSprite;
	float smokeTimer;
	GameObject gameManager;
	bool wasJumping;


	void Start()
	{
		smokeSprite = GameObject.Find("SmokeParent").GetComponentInChildren<SpriteRenderer>();
		gmSource = GetComponentInChildren<AudioSource>();
		gmSource.clip = TransformClip;
		gameManager = GameObject.Find("GameManager");
	}


	void Update()
    {
		smokeSprite.transform.position = new Vector3(CurTrans.transform.position.x, CurTrans.transform.position.y, CurTrans.transform.position.z - 1);
		smokeSprite = GameObject.Find("SmokeParent").GetComponentInChildren<SpriteRenderer>();

		//gmSource.volume = GameObject.Find("OptionsCanvas").GetComponent<Slider>().value;

		smokeTimer -= Time.deltaTime;
		smokeTimer = Mathf.Clamp(smokeTimer, 0, 10);

		PlaySound();

		curScene = SceneManager.GetActiveScene();

		Transform();
		CurPosition = CurTrans.transform.position;


		if (smokeTimer > 0)
		{
			smokeSprite.enabled = true;

		}
		if (smokeTimer == 0)
		{
			smokeSprite.enabled = false;
		}
	}

	// called first
	void OnEnable()
	{
		SceneManager.sceneLoaded += Sceneloadset;
	}

	void Sceneloadset(Scene Scene, LoadSceneMode mode)
	{
		#region Gathering
		PlayerGO = GameObject.FindGameObjectWithTag("Player");
		ElephantGO = GameObject.FindGameObjectWithTag("Elephant");
		airformGO = GameObject.FindGameObjectWithTag("AirForm");
		CrabGO = GameObject.FindGameObjectWithTag("Crab");
        smokeSprite = GameObject.Find("SmokeParent").GetComponentInChildren<SpriteRenderer>();
        #endregion

        #region Active Set
        ElephantGO.SetActive(false);
        airformGO.SetActive(false);
        smokeSprite.enabled = false;
        CrabGO.SetActive(false);
        #endregion

        CurTrans = PlayerGO;
	}

	// called when the game is terminated
	void OnDisable()
	{
		SceneManager.sceneLoaded -= Sceneloadset;
	}

	private void PlaySound()
	{
		if(playSound)
		{
			gmSource.PlayOneShot(TransformClip);
			playSound = false;
		}
	}

	public void Transform()
	{
		//each if check checks for a keypress, and checks if the current transformation is not the same as the keypress.
		//if the current transformation is different from the keypress's transformation, the current GO is set inactive.
		//the current transformation becomes the new keypress's GO and then it is set active.

		if (CurTrans.GetComponent<CharacterController>().isGrounded && wasJumping)
		{
			wasJumping = false;
			playSound = true;
			smokeTimer = .5f;
			if (CurTrans != PlayerGO)
			{
				CurTrans.gameObject.SetActive(false);
			}
			PlayerGO.transform.position = CurPosition;
			CurTrans = PlayerGO;
			PlayerGO.SetActive(true);
		}

		if (Input.GetKeyDown(KeyCode.Space) && !CrabGO.GetComponent<CrabMovement>().inWater) // air form
		{
			playSound = true;
			smokeTimer = .5f;
			if (CurTrans != airformGO)
			{
				CurTrans.gameObject.SetActive(false);
			}
			airformGO.transform.position = CurPosition;
			CurTrans = airformGO;
			airformGO.SetActive(true);
			wasJumping = true;
		}

		if(CrabGO.GetComponent<CrabMovement>().inWater)
		{
			playSound = true;
			smokeTimer = .5f;
			if (CurTrans != waterGO)
			{
				CurTrans.gameObject.SetActive(false);
			}
			waterGO.transform.position = CurPosition;
			CurTrans = waterGO;
			waterGO.SetActive(true);
		}
	}
}
