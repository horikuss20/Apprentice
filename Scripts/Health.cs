using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public int maxhealth;
    private bool canPlay;
    public bool noDamage;
    public bool Iframe = false;
    public float timer = 1f;
    [SerializeField]
    float deathtimer = 2;

    GameObject playerGO;
    GameObject gm;
    GameObject knightPE;
    public Renderer pMesh;

    bool shieldOn;
    float shieldTimer;

    private AudioSource playerAudio;
    public AudioClip hit;
    public AudioClip death;

    int shamNum;

    Color iframeColor;
    float flashtimer;
    private Material mat;
    private Color[] colors = { Color.yellow, Color.red };
    public GameObject DamageBorder;
    float BorderTimer;

    void Start()
    {
        BorderTimer = 0.3f;
        DamageBorder.SetActive(false);
        flashtimer = 0.2f;
        playerGO = GameObject.Find("PlayerFunctionality");
        playerAudio = playerGO.GetComponent<AudioSource>();
        gm = GameObject.Find("GameManager");
        knightPE = GameObject.Find("KnightPE");
        canPlay = true;
        pMesh = playerGO.GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        shieldTimer -= Time.deltaTime;
        shieldTimer = Mathf.Clamp(shieldTimer, 0, 5);
        if (shieldTimer == 0)
        {
            shieldOn = false;
        }

        if (shieldOn)
        {
            knightPE.SetActive(true);
        }

        if (!shieldOn)
        {
            knightPE.SetActive(false);
        }

        HeartSet();
        Death();
        IframeTimer();
    }
    void HeartSet()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }


        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (health == i + .5f)
            {
                hearts[i].sprite = halfHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

    }
    public void Damage(int damage)
    {
        if (noDamage == false && Iframe == false)
        {


            //SHAMROCK DAMAGE
            if (gm.GetComponent<PendantSystemNew>().pSide1 == gm.GetComponent<PendantSystemNew>().Shamrock || gm.GetComponent<PendantSystemNew>().pSide2 == gm.GetComponent<PendantSystemNew>().Shamrock)
            {
                shamNum = Random.Range(1, 10);
                Debug.Log(shamNum);
                if (shamNum == 3)
                {
                    gm.GetComponentInChildren<AudioSource>().PlayOneShot(gm.GetComponent<PendantSystemNew>().shamrockSwap);
                }
                else
                {
                    if ((gm.GetComponent<PendantSystemNew>().pSide1 == gm.GetComponent<PendantSystemNew>().Knight || gm.GetComponent<PendantSystemNew>().pSide2 == gm.GetComponent<PendantSystemNew>().Knight) && shieldOn)
                    {
                        shieldOn = false;
                        return;
                    }

                    else if (gm.GetComponent<PendantSystemNew>().pSide1 == gm.GetComponent<PendantSystemNew>().Knight || gm.GetComponent<PendantSystemNew>().pSide2 == gm.GetComponent<PendantSystemNew>().Knight)
                    {
                        health -= damage;
                        shieldOn = true;
                        shieldTimer = 5;
                        Iframe = true;
                        timer = 1f;
                        if (canPlay == true)
                        {
                            playerAudio.PlayOneShot(hit, 1.0f);
                        }
                    }
                    else
                    {
                        health -= damage;
                        Iframe = true;
                        timer = 1f;
                        if (canPlay == true)
                        {
                            playerAudio.PlayOneShot(hit, 1.0f);
                        }
                    }
                }
            }

            //NO SHAMROCK DAMAGE
            if (gm.GetComponent<PendantSystemNew>().pSide1 != gm.GetComponent<PendantSystemNew>().Shamrock && gm.GetComponent<PendantSystemNew>().pSide2 != gm.GetComponent<PendantSystemNew>().Shamrock)
            {
                if ((gm.GetComponent<PendantSystemNew>().pSide1 == gm.GetComponent<PendantSystemNew>().Knight || gm.GetComponent<PendantSystemNew>().pSide2 == gm.GetComponent<PendantSystemNew>().Knight) && shieldOn)
                {
                    shieldOn = false;
                    return;
                }

                else if (gm.GetComponent<PendantSystemNew>().pSide1 == gm.GetComponent<PendantSystemNew>().Knight || gm.GetComponent<PendantSystemNew>().pSide2 == gm.GetComponent<PendantSystemNew>().Knight)
                {
                    health -= damage;
                    shieldOn = true;
                    shieldTimer = 5;
                    Iframe = true;
                    timer = 1f;
                    if (canPlay == true)
                    {
                        playerAudio.PlayOneShot(hit, 1.0f);
                    }
                    gm.GetComponentInChildren<AudioSource>().PlayOneShot(gm.GetComponent<PendantSystemNew>().knightSwap);
                }
                else
                {
                    health -= damage;
                    Iframe = true;
                    timer = 1f;
                    if (canPlay == true)
                    {
                        playerAudio.PlayOneShot(hit, 1.0f);
                    }
                }
            }
        }

    }
    public void DamageHalf()
    {
        if (noDamage == false && Iframe == false)
        {
            if ((gm.GetComponent<PendantSystemNew>().pSide1 == gm.GetComponent<PendantSystemNew>().Knight || gm.GetComponent<PendantSystemNew>().pSide2 == gm.GetComponent<PendantSystemNew>().Knight) && shieldOn)
            {
                shieldOn = false;
                return;
            }

            else if (gm.GetComponent<PendantSystemNew>().pSide1 == gm.GetComponent<PendantSystemNew>().Knight || gm.GetComponent<PendantSystemNew>().pSide2 == gm.GetComponent<PendantSystemNew>().Knight)
            {
                health -= .5f;
                shieldOn = true;
                shieldTimer = 5;
                Iframe = true;
                timer = 1f;
                if (canPlay == true)
                {
                    playerAudio.PlayOneShot(hit, 1.0f);
                }
                gm.GetComponentInChildren<AudioSource>().PlayOneShot(gm.GetComponent<PendantSystemNew>().knightSwap);
            }
            else
            {
                health -= .5f;
                Iframe = true;
                timer = 1f;
                if (canPlay == true)
                {
                    playerAudio.PlayOneShot(hit, 1.0f);
                }
            }
        }

    }
    void IframeTimer()
    {
        if (Iframe == true)
        {
            BorderTimer -= Time.deltaTime;
            DamageBorder.SetActive(true);
            if(BorderTimer <= 0)
            {
                DamageBorder.SetActive(false);
            }
            
            StartCoroutine(IFrameFlash());
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Iframe = false;
                BorderTimer = 0.3f;
                //if(pMesh.enabled == false)
                //{
                //    pMesh.enabled = true;
                //}
                //if(DamageBorder.activeInHierarchy == true)
                //{
                //    DamageBorder.SetActive(false);

                //}
            }
        }
    }

    public void Heal()
    {
        health += 1;
    }
    public void HealHalf()
    {
        health += .5f;
    }

    public void Death()
    {

        if (health <= 0)
        {
            Destroy(GameObject.Find("PlayerFunctionality").GetComponent<MagicSpells>());
            GameObject.Find("EndLevelPortal").GetComponent<EndLevelPlantScript>().SceneChangeTime = 2f;
            GameObject.Find("EndLevelPortal").GetComponent<EndLevelPlantScript>().fadeSpeed = .15f;
            GameObject.Find("EndLevelPortal").GetComponent<EndLevelPlantScript>().BeginFade(1);
            deathtimer -= Time.deltaTime;
            deathtimer = Mathf.Clamp(deathtimer, 0, 3);
            playerGO.GetComponent<Animator>().SetInteger("AnimNum", 1);
            playerGO.GetComponent<MovementController>().canMove = false;
            if (!playerAudio.isPlaying && canPlay == true)
            {
                playerAudio.PlayOneShot(death, 1.0f);
                canPlay = false;
            }

            playerGO.GetComponent<Animator>().enabled = true;
            playerGO.GetComponent<CharacterController>().enabled = false;
            if (deathtimer == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator IFrameFlash()
    {
        pMesh.enabled = false;
        yield return new WaitForSeconds(0.2f);
        pMesh.enabled = true;
        yield return new WaitForSeconds(0.2f);
        pMesh.enabled = false;
        pMesh.enabled = false;
        yield return new WaitForSeconds(0.2f);
        pMesh.enabled = true;
    }



    //yield return new WaitForSeconds(0.2f);
    //pMesh.enabled = true;
    //yield return new WaitForSeconds(0.2f);
    //pMesh.enabled = false;
    //yield return new WaitForSeconds(0.2f);
    //pMesh.enabled = true;
    //yield return new WaitForSeconds(0.2f);
    //pMesh.enabled = false;
    //yield return new WaitForSeconds(0.2f);
    //pMesh.enabled = true;
    //yield return new WaitForSeconds(0.2f);
}


