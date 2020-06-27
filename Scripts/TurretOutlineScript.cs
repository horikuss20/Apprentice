using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretOutlineScript : MonoBehaviour
{
    private bool isMoving = true;
    private float moveDistance = 2;
    public GameObject TurretPlanter;
    Collider[] hitColliders;
    private bool hitWall = false;
    private string meteorButton;
    GameObject GM;
    MagicSystem magicSystem;
    public float spawnpoint;
    private float TurretRotate;
    private bool onGround;

    void Start()
    {
        Destroy(GameObject.Find("Gnome Turret(Clone)"));
        TurretRotate =  GameObject.Find("PlayerFunctionality").GetComponent<MagicSpells>().oppositeFireRotation;
        GM = GameObject.Find("GameManager");
        magicSystem = GameObject.Find("MagicUI").GetComponent<MagicSystem>();
        meteorButton = GameObject.Find("PlayerFunctionality").GetComponent<MagicSpells>().UsedButton;
        GameObject.Find("PlayerFunctionality").GetComponent<MagicSpells>().stopAttack = false;
    }


    void Update()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 1.2f);

        if (moveDistance <= 0)
        {
            isMoving = false;
        }
        if (isMoving == true)
        {
            moveDistance -= Time.deltaTime;
            if (hitWall == false)
            {
                gameObject.transform.Translate(Vector3.right * .1f);

            }
        }
        if (!Input.GetKey(meteorButton))
        {
            isMoving = false;
        }
        if (isMoving == false)
        {
            if (Input.GetKeyUp(KeyCode.J) && meteorButton == "j")
            {
                GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability1CoolDown = 3f;
                GameObject.Find("MagicUI").GetComponent<MagicSystem>().ability1Image.fillAmount = 0;
            }
            if (Input.GetKeyUp(KeyCode.K) && meteorButton == "k")
            {
                GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability2CoolDown = 3f;
                GameObject.Find("MagicUI").GetComponent<MagicSystem>().ability2Image.fillAmount = 0;
            }
            if (Input.GetKeyUp(KeyCode.L) && meteorButton == "l")
            {
                if (GM.GetComponent<PendantSystemNew>().pSide1 == GM.GetComponent<PendantSystemNew>().Wizard || GM.GetComponent<PendantSystemNew>().pSide2 == GM.GetComponent<PendantSystemNew>().Wizard)
                {
                    magicSystem.Ability3CoolDown = 1f;
                }
                else
                    GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability3CoolDown = 3f;
                GameObject.Find("MagicUI").GetComponent<MagicSystem>().ability3Image.fillAmount = 0;
            }
            if (Input.GetKeyUp(KeyCode.Semicolon) && meteorButton == ";")
            {
                GameObject.Find("MagicUI").GetComponent<MagicSystem>().Ability4Cooldown = 3f;
                GameObject.Find("MagicUI").GetComponent<MagicSystem>().ability4Image.fillAmount = 0;
            }
            GameObject.Find("PlayerFunctionality").GetComponent<MagicSpells>().stopAttack = true;
            Instantiate(TurretPlanter, transform.position + new Vector3(0, spawnpoint), Quaternion.Euler(0, 0, 0));
            GameObject.Find("TurretPlanter(Clone)").GetComponent<TurretPlanterScript>().endPoint = gameObject.transform.position;
            GameObject.Find("TurretPlanter(Clone)").GetComponent<TurretPlanterScript>().rotation = TurretRotate;
            GameObject.Find("TurretPlanter(Clone)").GetComponent<TurretPlanterScript>().notOnGround = onGround;
            Destroy(gameObject);
        }
        foreach (Collider nearbyObj in hitColliders)
        {
            if (nearbyObj.tag == "Wall")
            {
                hitWall = true;
            }
            if (nearbyObj.tag == "Ground" || nearbyObj.tag == "Wall")
            {
                onGround = true;
            }
        }
    }
}
