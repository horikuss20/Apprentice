using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerTransformation : MonoBehaviour
{
    //transformation game objects
    public GameObject PlayerGO, waterGO, ElephantGO, CrabGO, ChameleonGO, airformGO;
    public MeshRenderer[] playerMRA, waterMRA, elephantMRA, crabMRA, chameleonMRA, airformMRA;
    //keeps track of current transformation
    public MeshRenderer[] currForm;
    Scene curScene;
    public AudioClip TransformClip;
    AudioSource gmSource;
    bool playSound = false;
    SpriteRenderer smokeSprite;
    float smokeTimer = 0;
    GameObject curTransMatch;
    bool crabInWater;
    float tranTimer = 0;

    void Start()
    {
        tranTimer = .2f;
    }


    void Update()
    {
        tranTimer -= Time.deltaTime;
        tranTimer = Mathf.Clamp(tranTimer, 0, 5);

        gmSource = GameObject.Find("SoundEffectPlayer").GetComponent<AudioSource>();
        gmSource.clip = TransformClip;


        //crabInWater = CrabGO.GetComponent<CrabMovement>().inWater;
        if (playSound)
        {
            PlaySound();
        }

        //SMOKE
        if (smokeSprite != null)
        {
            smokeSprite = GameObject.Find("SmokeParent").GetComponentInChildren<SpriteRenderer>();
            smokeSprite.transform.position = new Vector3(PlayerGO.transform.position.x, PlayerGO.transform.position.y + 2, -2);

            if (smokeTimer > 0)
            {
                smokeSprite.enabled = true;

            }
            if (smokeTimer == 0)
            {
                smokeSprite.enabled = false;
            }
        }

        smokeTimer -= Time.deltaTime;
        smokeTimer = Mathf.Clamp(smokeTimer, 0, 10);
        //END SMOKE

        curScene = SceneManager.GetActiveScene();

    }

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += Sceneloadset;
    }

    void Sceneloadset(Scene Scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Options" && SceneManager.GetActiveScene().name != "Credits")
        {
            #region Gathering
            PlayerGO = GameObject.FindGameObjectWithTag("Player");
            ElephantGO = GameObject.FindGameObjectWithTag("Elephant");
            airformGO = GameObject.FindGameObjectWithTag("AirForm");
            CrabGO = GameObject.FindGameObjectWithTag("Crab");
            waterGO = GameObject.FindGameObjectWithTag("WaterForm");
            smokeSprite = GameObject.Find("SmokeParent").GetComponentInChildren<SpriteRenderer>();
            curTransMatch = PlayerGO;
            #endregion

            #region Array Set
            playerMRA = GameObject.FindGameObjectWithTag("PlayerMesh").GetComponentsInChildren<MeshRenderer>();
            waterMRA = waterGO.GetComponentsInChildren<MeshRenderer>();
            elephantMRA = ElephantGO.GetComponentsInChildren<MeshRenderer>();
            crabMRA = CrabGO.GetComponentsInChildren<MeshRenderer>();
            airformMRA = airformGO.GetComponentsInChildren<MeshRenderer>();
            currForm = playerMRA;
            #endregion

            #region Active Set
            foreach (MeshRenderer mr in waterMRA)
            {
                mr.enabled = false;
            }
            foreach (MeshRenderer mr in elephantMRA)
            {
                mr.enabled = false;
            }
            foreach (MeshRenderer mr in crabMRA)
            {
                mr.enabled = false;
            }
            foreach (MeshRenderer mr in airformMRA)
            {
                mr.enabled = false;
            }

            waterGO.GetComponentInChildren<Collider>().enabled = false;
            ElephantGO.GetComponent<Collider>().enabled = false;
            CrabGO.GetComponent<Collider>().enabled = false;
            airformGO.GetComponent<Collider>().enabled = false;
            #endregion
        }
    }

    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= Sceneloadset;
    }

    private void PlaySound()
    {
        gmSource.PlayOneShot(TransformClip);
        playSound = false;

    }

    public void Transform(MeshRenderer[] formToChange)
    {
        if (currForm != formToChange)
        {
            playSound = true;
            smokeTimer = 0.3f;
            foreach (MeshRenderer mr in currForm)
            {
                mr.enabled = false;
            }
        }

        currForm = formToChange;

        foreach (MeshRenderer mr in formToChange)
        {
            mr.enabled = true;
        }

    }
}
