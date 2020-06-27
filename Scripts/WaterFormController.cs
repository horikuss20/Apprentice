using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFormController : MonoBehaviour
{
    CharacterController playerController;
    float speed;
    public bool inWater = false;
    private Vector3 moveDirection = Vector3.zero;
    private LineRenderer lRenderer;
    private AudioSource dolphinSource;
    public AudioClip dashClip;
    public AudioClip attackClip;
    float maxDashTime = 1.0f;
    float dashDistance = 8.0f;
    float dashSpeed = 2.0f;
    float dashStoppingSpeed = 0.1f;
    float currentDashTime;
    float gravity = 15f;
    float Horizontal;
    float Vertical;
    public ParticleSystem pSystem;

    void Start()
    {
        #region Setting Variables
        speed = 8;
        playerController = gameObject.GetComponent<CharacterController>();
        lRenderer = GetComponent<LineRenderer>();
        currentDashTime = maxDashTime;
        pSystem = GetComponent<ParticleSystem>();
        dolphinSource = GetComponent<AudioSource>();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (inWater == true)
        {
            #region Movement
            Vertical = Input.GetAxis("Vertical");
            Horizontal = Input.GetAxis("Horizontal");
            moveDirection = new Vector3(Horizontal, Vertical, 0);
            if (moveDirection != Vector3.zero) transform.rotation = Quaternion.LookRotation(moveDirection);
            moveDirection = moveDirection * speed;

            // Move the controller
            playerController.Move(moveDirection * Time.deltaTime);
            #endregion

            #region Water Attack
            if (Input.GetKey(KeyCode.R))
            {
                WaterJetAttack();
                dolphinSource.PlayOneShot(attackClip, 1.0f);
                Debug.DrawRay(gameObject.transform.GetChild(1).position, gameObject.transform.GetChild(1).TransformDirection(Vector3.left), Color.red);
            }
            #endregion

        }

        if (inWater == false)
        {
            //pSystem.Stop();
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = 0;
            moveDirection = new Vector3(Horizontal, 0, 0);
            if (moveDirection != Vector3.zero) transform.rotation = Quaternion.LookRotation(moveDirection);
            moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
            moveDirection *= speed;
            playerController.Move(moveDirection * Time.deltaTime);
        }

        #region Dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentDashTime = 0.0f;
            dolphinSource.PlayOneShot(dashClip, 1.0f);
        }
        if (currentDashTime < maxDashTime)
        {
            moveDirection = transform.forward * dashDistance;
            currentDashTime += dashStoppingSpeed;
            playerController.Move(moveDirection * Time.deltaTime * dashSpeed);
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        if (currentDashTime > maxDashTime)
        {
            currentDashTime = maxDashTime;
        }
        #endregion
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            pSystem.Play();
            inWater = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            pSystem.Stop();
            inWater = false;
        }
    }

    void WaterJetAttack()
    {
        RaycastHit hit;
        float hitRange = 2.5f;
        Vector3 forward = gameObject.transform.GetChild(1).TransformDirection(Vector3.left);
        Vector3 origin = gameObject.transform.GetChild(1).position;
        lRenderer.enabled = true;
        lRenderer.SetPosition(1, origin);
        lRenderer.SetPosition(0, origin + forward * hitRange);
        StartCoroutine(EndAttack(0.1f));

        Physics.Raycast(origin, forward, out hit, hitRange);
    }

    IEnumerator EndAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        lRenderer.enabled = false;
    }
}
