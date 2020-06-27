using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public enum ShopNum
    {
      BuyHeart,
      LightMagic,
      DarkMagic,
      HealthPotion,
      MagicPotion,
      XpPotion,
      MoneyDebug
    }
    public ShopNum shopNum;
    public GameObject ShopCanvas;
    public bool inTrigger = false;
    private Text ShopText;
    private Text CurrMoneyText;
    public int CurrMoney;
    private GameObject ShopHalo;
    private GameObject StartMenu;
    private GameObject ShopMenu;
    private int Money;
    private AudioSource shopSource;
    public AudioClip buyClip;
    private GameObject gm;
    private GameObject BuyButton;
    private GameObject HeartButton;
    private GameObject HealthButton;
    Collider[] hitColliders;
    private bool PlayerIsNear;


    // Start is called before the first frame update
    void Start()
    {
        ShopHalo = GameObject.Find("YeOldShopHalo");
        ShopHalo.SetActive(false);
        BuyButton = GameObject.Find("BuyButton ");
        HealthButton = GameObject.Find("HealthButton");
        HeartButton = GameObject.Find("HeartButton");
        ShopCanvas = GameObject.Find("ShopCanvas");
        StartMenu = GameObject.Find("StartMenu");
        ShopMenu = GameObject.Find("ShopMenu");
        ShopText = GameObject.Find("ShopText").GetComponent<Text>();
        CurrMoneyText = GameObject.Find("CurrMoney").GetComponent<Text>();
        shopSource = GameObject.Find("SoundEffectPlayer").GetComponent<AudioSource>();
        gm = GameObject.Find("GameManager");
        
        ShopMenu.SetActive(false);
        ShopCanvas.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {

        Money = gm.GetComponent<GameManager>().oldCollectCount;

        if (inTrigger == true)
        {
            ShopHalo.SetActive(true);
            CurrMoneyText.text = Money.ToString();
            if (Input.GetKeyDown(KeyCode.F))
            {               
                ShopCanvas.SetActive(true);
            }
        }
        else
        {
            ShopHalo.SetActive(false);
        }

       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inTrigger = true;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = false;
            ShopCanvas.SetActive(false);
            GameObject.Find("SkillSystemCanvas").SetActive(false);
        }
    }
    public void Back()
    {
        ShopCanvas.SetActive(false);
    }
    public void BuyHeart()
    {
        shopNum = ShopNum.BuyHeart;
        HealthButton.GetComponent<Image>().color = Color.white;
        HeartButton.GetComponent<Image>().color = Color.yellow;
        if(gm.GetComponent<GameManager>().collectibleCounter >= 150)
        {
            BuyButton.GetComponent<Image>().color = Color.green;
        }
        if (gm.GetComponent<GameManager>().collectibleCounter < 150)
        {
            BuyButton.GetComponent<Image>().color = Color.red;
        }
        ShopText.text = "Adds another heart to your current health pool.";
    }
    public void BuyLightMagic()
    {
        shopNum = ShopNum.LightMagic;
        ShopText.text = "Gives you light magic. It's focused on area of effect and single target damage.";
    }
    public void BuyDarkMagic()
    {
        shopNum = ShopNum.DarkMagic;
        ShopText.text = "Gives you dark magic. It's focused on damage over time.";
    }
    public void HealthPotion()
    {
        shopNum = ShopNum.HealthPotion;
        HeartButton.GetComponent<Image>().color = Color.white;
        HealthButton.GetComponent<Image>().color = Color.yellow;
        if (gm.GetComponent<GameManager>().collectibleCounter >= 20)
        {
            BuyButton.GetComponent<Image>().color = Color.green;
        }
        if (gm.GetComponent<GameManager>().collectibleCounter < 20)
        {
            BuyButton.GetComponent<Image>().color = Color.red;
        }
        ShopText.text = "A useable health potion that will heal 1 heart of damage.";
    }
    public void MagicPotion()
    {
        shopNum = ShopNum.MagicPotion;
        ShopText.text = "A useable potion that will fully regen your magic";
    }
    public void XpPotion()
    {
        shopNum = ShopNum.XpPotion;
        ShopText.text = "A useable potion that will give you x XP";
    }
    public void DebugGetMoney()
    {
        shopNum = ShopNum.MoneyDebug;
        ShopText.text = "Get 100 coins";
    }
    public void Shop()
    {
        StartMenu.SetActive(false);
        ShopMenu.SetActive(true);
    }
    public void backToStart()
    {
        
        StartMenu.SetActive(true);
        ShopMenu.SetActive(false);
    }
    public void Buy()
    {
        switch (shopNum)
        {
            case ShopNum.BuyHeart:
                {
                    if (Money >= 150)
                    {
                        gm.GetComponent<GameManager>().oldCollectCount -= 150;
                        gm.GetComponent<GameManager>().collectibleCounter -= 150;
                        GameObject.Find("HealthUI").GetComponent<Health>().numOfHearts += 1;
                        GameObject.Find("HealthUI").GetComponent<Health>().health += 1;
                        shopSource.PlayOneShot(buyClip, 0.8f);
                        if (gm.GetComponent<GameManager>().collectibleCounter >= 150)
                        {
                            BuyButton.GetComponent<Image>().color = Color.green;
                        }
                        if (gm.GetComponent<GameManager>().collectibleCounter < 150)
                        {
                            BuyButton.GetComponent<Image>().color = Color.red;
                        }
                    }
                }
                break;
            case ShopNum.LightMagic:
                {
                    if (Money >= 200)
                    {
                        Money -= 200;

                    }
                }
                break;
            case ShopNum.DarkMagic:
                {
                    if (Money >= 200)
                    {
                        Money -= 200;
                    }
                }
                break;
            case ShopNum.HealthPotion:
                {
                    if (Money >= 20)
                    {
                        gm.GetComponent<GameManager>().collectibleCounter -= 20;
                        gm.GetComponent<GameManager>().oldCollectCount -= 20;
                        GameObject.Find("GameManager").GetComponent<GameManager>().healthPotion++;
                        shopSource.PlayOneShot(buyClip, 0.8f);
                        if (gm.GetComponent<GameManager>().collectibleCounter >= 20)
                        {
                            BuyButton.GetComponent<Image>().color = Color.green;
                        }
                        if (gm.GetComponent<GameManager>().collectibleCounter < 20)
                        {
                            BuyButton.GetComponent<Image>().color = Color.red;
                        }

                    }
                }
                break;
            case ShopNum.MagicPotion:
                {
                    if (Money >= 50)
                    {
                        Money -= 50;
                    }
                }
                break;
            case ShopNum.XpPotion:
                {
                    if (Money >= 100)
                    {
                        Money -= 100;
                    }
                }
                break;
            case ShopNum.MoneyDebug:
                {                   
                        gm.GetComponent<GameManager>().collectibleCounter += 100;
                }
                break;
            default:
                break;
        }
    }
}
