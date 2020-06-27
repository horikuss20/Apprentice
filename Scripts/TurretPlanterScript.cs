using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlanterScript : MonoBehaviour
{
    public GameObject Turret;
    private float PlayersY;
    private float PlayersX;
    Collider[] hitColliders;
    protected float Animation;
    private bool hitObj = false;
    private float positive;
    private float timer;
    public Vector3 endPoint;
    public float rotation;
    public bool notOnGround;
    private bool canhitObj = false;

    // Start is called before the first frame update
    void Start()
    {
        notOnGround = true;
        PlayersY = GameObject.Find("PlayerFunctionality").GetComponent<Transform>().position.y;
        PlayersX = GameObject.Find("PlayerFunctionality").GetComponent<Transform>().position.x;
        positive = GameObject.Find("PlayerFunctionality").GetComponent<MagicSpells>().FireStart;
        timer = 1.35f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, 6);

        if (timer == 0)
        {
            if (notOnGround == false)
            {
                Instantiate(Turret, transform.position + new Vector3(0, -.65f), Quaternion.Euler(0, rotation, 0));
            if (rotation == 0)
            {
                GameObject.Find("Gnome Turret(Clone)").GetComponent<Turret>().shootRotation = 1;
            }
                Destroy(gameObject);
            }
             
            if(notOnGround == true)
            {
                canhitObj = true;
            }
        }

        if (hitObj == true)
        {
            Instantiate(Turret, transform.position + new Vector3(0, -.65f), Quaternion.Euler(0, rotation ,0));
            if (rotation == 0)
            {
                GameObject.Find("Gnome Turret(Clone)").GetComponent<Turret>().shootRotation = 1;
            }
           
            Destroy(gameObject);
        }
        Animation += Time.deltaTime;

        Animation = Animation % 5f;

        transform.position = MathParabola.Parabola(new Vector3(PlayersX, PlayersY, 0), endPoint, 2.5f, Animation/1.5f);
        hitColliders = Physics.OverlapSphere(transform.position, 1f);

        foreach (Collider nearbyObj in hitColliders)
        {
            if(canhitObj == true)
            {
                if (nearbyObj.tag == "Ground" || nearbyObj.tag == "Wall")
                {
                    hitObj = true;
                }
            }    
        }
    }
}
