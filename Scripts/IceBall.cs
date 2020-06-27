using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    public GameObject iceSpike;
    private float PlayersY;
    private float PlayersX;
    Collider[] hitColliders;
    protected float Animation;
    private bool hitObj = false;
    private float positive;
	private float timer;

    // Start is called before the first frame update
    void Start()
    {
        PlayersY = GameObject.Find("PlayerFunctionality").GetComponent<Transform>().position.y;
        PlayersX = GameObject.Find("PlayerFunctionality").GetComponent<Transform>().position.x;
        positive = GameObject.Find("PlayerFunctionality").GetComponent<MagicSpells>().FireStart;
		timer = 6;
    }

    // Update is called once per frame
    void Update()
    {
		timer -= Time.deltaTime;
		timer = Mathf.Clamp(timer, 0, 6);

		if(timer == 0)
		{
			Destroy(gameObject);
		}

        if(hitObj == true)
        {
            Instantiate(iceSpike, transform.position + new Vector3(0, 0), Quaternion.Euler(0, 0, 0));
            Instantiate(iceSpike, transform.position + new Vector3(0, 0), Quaternion.Euler(0, 0, 30));
            Instantiate(iceSpike, transform.position + new Vector3(0, 0), Quaternion.Euler(0, 0, 60));
            Instantiate(iceSpike, transform.position + new Vector3(0, 0), Quaternion.Euler(0, 0, 90));
            Instantiate(iceSpike, transform.position + new Vector3(0, 0), Quaternion.Euler(0, 0, 120));
            Instantiate(iceSpike, transform.position + new Vector3(0, 0), Quaternion.Euler(0, 0, 150));
            Instantiate(iceSpike, transform.position + new Vector3(0, 0), Quaternion.Euler(0, 0, 180));
            Instantiate(iceSpike, transform.position + new Vector3(0, 0), Quaternion.Euler(0, 0, 210));
            Instantiate(iceSpike, transform.position + new Vector3(0, 0), Quaternion.Euler(0, 0, 240));
            Instantiate(iceSpike, transform.position + new Vector3(0, 0), Quaternion.Euler(0, 0, 270));
            Instantiate(iceSpike, transform.position + new Vector3(0, 0), Quaternion.Euler(0, 0, 300));
            Instantiate(iceSpike, transform.position + new Vector3(0, 0), Quaternion.Euler(0, 0, 330));
            Instantiate(iceSpike, transform.position + new Vector3(0, 0), Quaternion.Euler(0, 0, 360));
            Destroy(gameObject);
        }
        Animation += Time.deltaTime;

        Animation = Animation % 5f;

        transform.position = MathParabola.Parabola(new Vector3(PlayersX, PlayersY, 0), new Vector3(PlayersX + (15 * positive),PlayersY,0),  4f, Animation / 2f);
        hitColliders = Physics.OverlapSphere(transform.position, 1f);

        foreach (Collider nearbyObj in hitColliders)
        {
            if (nearbyObj.tag == "Wall" || nearbyObj.tag == "Ground")
            {
                hitObj = true;
            }

            if (nearbyObj.tag == "PatrolEnemy")
            {
                if (nearbyObj.tag == "PatrolEnemy")
                {
                    nearbyObj.GetComponent<EnemyHealth>().TakeDamage(1.5f);

                    if (nearbyObj.GetComponent<ShieldEnemy>())
                    {
                        nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth -= 1.5f;

                        if (nearbyObj.GetComponent<ShieldEnemy>().ShieldHealth == 0)
                        {
                            nearbyObj.GetComponent<EnemyHealth>().TakeDamage(1.5f);
                        }
                    }
                    hitObj = true;
                }
            }
        }
    }
}
