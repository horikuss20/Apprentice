using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSmash : MonoBehaviour
{
    GameObject Stones;
    private float timer = .2f;
    private float stoneRotation;
    // Start is called before the first frame update
    void Start()
    {
        stoneRotation = GameObject.Find("PlayerFunctionality").GetComponent<MagicSpells>().FireRotation;
      
		Stones = Resources.Load<GameObject>("Prefabs/Stone");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * 12.5f * Time.smoothDeltaTime;
        timer -= Time.smoothDeltaTime;
        if(timer <= 0)
        {
            Instantiate(Stones, transform.position + new Vector3(.5f, 0), Quaternion.Euler(0, stoneRotation, -100));
            Instantiate(Stones, transform.position + new Vector3(.5f, 0), Quaternion.Euler(0, stoneRotation, -95));
            Instantiate(Stones, transform.position + new Vector3(.5f, 0), Quaternion.Euler(0, stoneRotation, -90));
            Instantiate(Stones, transform.position + new Vector3(.5f, 0), Quaternion.Euler(0, stoneRotation, -85));
            Instantiate(Stones, transform.position + new Vector3(.5f, 0), Quaternion.Euler(0, stoneRotation, -80));
            timer = 1;
            GameObject.Find("PlayerFunctionality").GetComponent<MovementController>().canMove = true;
            Destroy(gameObject);
        }
    }
}
