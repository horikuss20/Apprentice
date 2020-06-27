using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    GameObject Health;
    GameManager gm;
    Animation anim;
    AudioSource sfxSource;
    AudioClip healthSound;


    // Start is called before the first frame update
    void Start()
    {
        Health = GameObject.Find("HealthUI");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        healthSound = gm.healthClip;
        anim = GetComponent<Animation>();
        sfxSource = GameObject.Find("SoundEffectPlayer").GetComponent<AudioSource>();
        anim.Play("HealthRotate");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            sfxSource.PlayOneShot(healthSound, 1.0f);
            Health.GetComponent<Health>().Heal();
            Destroy(gameObject);
        }
    }
}
