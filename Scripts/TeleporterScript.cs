using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterScript : MonoBehaviour
{
    public enum FoundLevels
    {
        Water1,
        Water2,
        Water3,
        Fire1,
        Fire2,
        Fire3,
        Earth1,
        Earth2,
        Earth3,
        Lt1,
        Lt2,
        Lt3,
        Level1,
        Level2,
        Level3,
        None,
    }
    public enum FoundWorlds
    {
        Water,
        Fire,
        Earth,
        Lt,
        None,
    }
    public FoundLevels foundLevels;
    public FoundWorlds foundWorlds;
    public ParticleSystem ps;
    public Renderer[] Gm;
    public bool isOnTeleporter = false;
    public bool isTeleporting = false;
    public GameObject TPCover;
    public bool TooBig = false;
    private ParticleSystem.EmissionModule pse;
    public GameObject PlayerGO;
    public GameObject TPUI;
    private float loadSceneTime = 2.5f;
    public bool activateTP = false;
    public int tpLocation;
    //World UI's
    public GameObject LostCityUI;
    private GameObject FireLevelUI;
    private GameObject EarthLevelUI;
    private GameObject LightningLevelUI;
    private GameObject LevelUI;
    //Hub 
    public int HubLevelBuildLocation;
    //Water Level build numbers
    public int WaterLevelBuildLocation1;
    public int WaterLevelBuildLocation2;
    public int WaterLevelBuildLocation3;
    //Fire Level Build Numbers
    public int FireLevelBuildLocation1;
    public int FireLevelBuildLocation2;
    public int FireLevelBuildLocation3;
    //Earth Level Build Numbers
    public int EarthLevelBuildLocation1;
    public int EarthLevelBuildLocation2;
    public int EarthLevelBuildLocation3;
    //Lightning Level Build Numbers
    public int LightningLevelBuildLocation1;
    public int LightningLevelBuildLocation2;
    public int LightningLevelBuildLocation3;
    //level build numbers
    public int Level1BN;
    public int Level2BN;
    public int Level3BN;
    //World UI's Buttons
    private GameObject WaterTPButton;
    private GameObject FireTPButton;
    private GameObject EarthTPButton;
    private GameObject LightningTPButton;
    private GameObject WorldUI;
    //Checkpoint Master script
    private CheckpointMaster cm;
    //Buttons
    //Fire
    private GameObject FireButton1;
    private GameObject FireButton2;
    private GameObject FireButton3;
    //Water
    private GameObject WaterButton1;
    private GameObject WaterButton2;
    private GameObject WaterButton3;
    //Earth
    private GameObject EarthButton1;
    private GameObject EarthButton2;
    private GameObject EarthButton3;
    //Lt
    private GameObject LtButton1;
    private GameObject LtButton2;
    private GameObject LtButton3;
    //Levels
    private GameObject LevelButton1;
    private GameObject LevelButton2;
    private GameObject LevelButton3;


    private GameObject gameman;

    private void Awake()
    {
        TPUI = GameObject.Find("TPUI");
        cm = GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>();
		gameman = GameObject.Find("GameManager");
    }
    private void Start()
    {
        //Finding UI
        LevelUI = TPUI.transform.Find("LevelSelectUI ").gameObject;
        FireLevelUI = TPUI.transform.Find("WaterLevelSelectUI (Fire)").gameObject;
        LostCityUI = TPUI.transform.Find("WaterLevelSelectUI").gameObject;
        EarthLevelUI = TPUI.transform.Find("LightningLevelSelectUI (Earth)").gameObject;
        LightningLevelUI = TPUI.transform.Find("LightningLevelSelectUI ").gameObject;
        WorldUI = TPUI.transform.Find("WorldSelectUI").gameObject;
        TPCover = GameObject.Find("TPCover");
        PlayerGO = GameObject.Find("PlayerFunctionality");
        //Finding Buttons
        //Fire
        FireButton1 = FireLevelUI.transform.Find("Fire-1Button").gameObject;
        FireButton2 = FireLevelUI.transform.Find("Fire-2 Button").gameObject;
        FireButton3 = FireLevelUI.transform.Find("Fire-3 Button").gameObject;
        //Water
        WaterButton1 = LostCityUI.transform.Find("Water-1Button ").gameObject;
        WaterButton2 = LostCityUI.transform.Find("Water-2 Button").gameObject;
        WaterButton3 = LostCityUI.transform.Find("Water-3 Button").gameObject;
        //Earth
        EarthButton1 = EarthLevelUI.transform.Find("Earth-1Button ").gameObject;
        EarthButton2 = EarthLevelUI.transform.Find("Earth-2 Button").gameObject;
        EarthButton3 = EarthLevelUI.transform.Find("Earth-3 Button").gameObject;
        //Lt
        LtButton1 = LightningLevelUI.transform.Find("Lightning-1Button ").gameObject;
        LtButton2 = LightningLevelUI.transform.Find("Lightning-2 Button").gameObject;
        LtButton3 = LightningLevelUI.transform.Find("Lightning-3 Button").gameObject;
        //World Buttons
        FireTPButton = WorldUI.transform.Find("FireWorld").gameObject;
        WaterTPButton = WorldUI.transform.Find("LostCity").gameObject;
        EarthTPButton = WorldUI.transform.Find("EarthLevel").gameObject;
        LightningTPButton = WorldUI.transform.Find("LightningLevel").gameObject;
        //Levels
        LevelButton1 = LevelUI.transform.Find("Level1").gameObject;
        LevelButton2 = LevelUI.transform.Find("Level2").gameObject;
        LevelButton3 = LevelUI.transform.Find("Level3").gameObject;

        TPUI.SetActive(false);
        pse = ps.emission;
        pse.enabled = false;
        TPCover.SetActive(false);
     
}

    private void Update()
    {
        teleportEffect();

        Gm = GetComponentsInChildren<Renderer>();
        if(isOnTeleporter == true)
        {
            cm.lastCheckPointPos = transform.position;
            //Cases for turning on the level buttons
            TurnOnLevels();
            TurnOnWorlds();

            for (int i = 0; i < Gm.Length; i++)
                Gm[i].material.color = Color.magenta;
            if (Input.GetKeyDown(KeyCode.F))
            {
                isTeleporting = true;

				gameman.GetComponent<SaveInputManager>().SaveGame();

                LostCityUI.SetActive(false);
                FireLevelUI.SetActive(false);
                LightningLevelUI.SetActive(false);
                EarthLevelUI.SetActive(false);
                WorldUI.SetActive(false);
                //World buttons
                //Water
                if (cm.hasFoundWaterLevel == false)
                {
                    WaterTPButton.SetActive(false);
                }
                else
                {
                    WaterTPButton.SetActive(true);
                }
                //Fire
                if (cm.hasFoundFireLevel == false)
                {
                    FireTPButton.SetActive(false);
                }
                else
                {
                    FireTPButton.SetActive(true);
                }
                //Earth
                if (cm.hasFoundEarthLevel == false)
                {
                    EarthTPButton.SetActive(false);
                }
                else
                {
                    EarthTPButton.SetActive(true);
                }
                //Lightning
                if (cm.hasFoundLtLevel == false)
                {
                    LightningTPButton.SetActive(false);
                }
                else
                {
                    LightningTPButton.SetActive(true);
                }
                if(cm.cangotoLevel1 == false)
                {
                    LevelButton1.SetActive(false);
                }
                else
                {
                    LevelButton1.SetActive(true);
                }
                if (cm.cangotoLevel2 == false)
                {
                    LevelButton2.SetActive(false);
                }
                else
                {
                    LevelButton2.SetActive(true);
                }
                if (cm.cangotoLevel3 == false)
                {
                    LevelButton3.SetActive(false);
                }
                else
                {
                    LevelButton3.SetActive(true);
                }

            }
        }
        if(isOnTeleporter == false)
        {
            for (int i = 0; i < Gm.Length; i++)
                Gm[i].material.color = Color.white;
        }
        if(isTeleporting == true)
        {
            TPUI.SetActive(true);
           
            isTeleporting = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isOnTeleporter = true;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isOnTeleporter = false;
            TPUI.SetActive(false);
        }
    }
    //Change UI buttons
    public void WaterLevelUIChange()
    {
        WorldUI.SetActive(false);
        LostCityUI.SetActive(true);
        if(cm.hasFoundWater1 == false)
        {
            WaterButton1.SetActive(false);       
        }
        else
        {
            WaterButton1.SetActive(true);
        }
        if (cm.hasFoundWater2 == false)
        {
            WaterButton2.SetActive(false);
        }
        else
        {
            WaterButton2.SetActive(true);
        }
        if (cm.hasFoundWater3 == false)
        {
            WaterButton3.SetActive(false);
        }
        else
        {
            WaterButton3.SetActive(true);
        }
    }
    public void FireLevelUIChange()
    {
        WorldUI.SetActive(false);
        FireLevelUI.SetActive(true);
        if (cm.hasFoundFire1 == false)
        {
            FireButton1.SetActive(false);
        }
        else
        {
            FireButton1.SetActive(true);
        }
        if (cm.hasFoundFire2 == false)
        {
            FireButton2.SetActive(false);
        }
        else
        {
            FireButton2.SetActive(true);
        }
        if (cm.hasFoundFire3 == false)
        {
            FireButton3.SetActive(false);
        }
        else
        {
            FireButton3.SetActive(true);
        }
    }
    public void EarthLevelUIChange()
    {
        WorldUI.SetActive(false);
        EarthLevelUI.SetActive(true);
        if (cm.hasFoundEarth1 == false)
        {
            EarthButton1.SetActive(false);
        }
        else
        {
            EarthButton1.SetActive(true);
        }
        if (cm.hasFoundEarth2 == false)
        {
            EarthButton2.SetActive(false);
        }
        else
        {
            EarthButton2.SetActive(true);
        }
        if (cm.hasFoundEarth3 == false)
        {
            EarthButton3.SetActive(false);
        }
        else
        {
            EarthButton3.SetActive(true);
        }
    }
    public void LightningLevelUIChange()
    {
        WorldUI.SetActive(false);
        LightningLevelUI.SetActive(true);
        if (cm.hasFoundLt1 == false)
        {
            LtButton1.SetActive(false);
        }
        else
        {
            LtButton1.SetActive(true);
        }
        if (cm.hasFoundLt2 == false)
        {
            LtButton2.SetActive(false);
        }
        else
        {
            LtButton2.SetActive(true);
        }
        if (cm.hasFoundLt3 == false)
        {
            LtButton3.SetActive(false);
        }
        else
        {
            LtButton3.SetActive(true);
        }
    }
    //Hub Button
    public void HubLevel()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = HubLevelBuildLocation;
        activateTP = true;
    }
    //Water Level Buttons
    public void WaterLevel1()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = WaterLevelBuildLocation1;
        activateTP = true;
    }
    public void WaterLevel2()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = WaterLevelBuildLocation2;
        activateTP = true;
    }
    public void WaterLevel3()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = WaterLevelBuildLocation3;
        activateTP = true;
    }
    //Lightning Level Buttons
    public void LightningLevel1()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = LightningLevelBuildLocation1;
        activateTP = true;
    }
    public void LightningLevel2()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = LightningLevelBuildLocation2;
        activateTP = true;
    }
    public void LightningLevel3()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = LightningLevelBuildLocation3;
        activateTP = true;
    }
    //Earth Level Buttons
    public void EarthLevel1()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = EarthLevelBuildLocation1;
        activateTP = true;
    }
    public void EarthLevel2()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = EarthLevelBuildLocation2;
        activateTP = true;
    }
    public void EarthLevel3()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = EarthLevelBuildLocation3;
        activateTP = true;
    }

    //Fire Level Buttons
    public void FireLevel1()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = FireLevelBuildLocation1;
        activateTP = true;
    }
    public void FireLevel2()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = FireLevelBuildLocation2;
        activateTP = true;
    }
    public void FireLevel3()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = FireLevelBuildLocation3;
        activateTP = true;
    }
    public void Level1()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = Level1BN;
        activateTP = true;
    }
    public void Level2()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = Level2BN;
        activateTP = true;
    }
    public void Level3()
    {
        TPUI.SetActive(false);
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true;
        tpLocation = Level3BN;
        activateTP = true;
    }
    public void teleportEffect()
    {
        if(activateTP == true)
        {
            TPCover.SetActive(true);
            pse.enabled = true;

            if (TooBig == false)
            {
                TPCover.transform.localScale += new Vector3(.1f, .1f, .1f);
                if (TPCover.transform.localScale.x >= 9f)
                {
                    TooBig = true;
                }
            }
            else
            {
                if (TPCover.transform.localScale.x >= 0)
                {
                    TPCover.transform.localScale -= new Vector3(.1f, .1f, .1f);
                }
                if (TPCover.transform.localScale.x <= 2.5f)
                {
                    pse.enabled = false;
                }
                PlayerGO.SetActive(false);
            }
            loadSceneTime -= Time.deltaTime;
            if (loadSceneTime <= 0)
            {
                SceneManager.LoadScene(tpLocation);
            }
        }
    }
    public void backOut()
    {
        TPUI.SetActive(false);
    }
    public void backtoMain()
    {
        WorldUI.SetActive(true);
        LostCityUI.SetActive(false);
        FireLevelUI.SetActive(false);
        LightningLevelUI.SetActive(false);
        EarthLevelUI.SetActive(false);
    }
    private void TurnOnLevels()
    {
        switch (foundLevels)
        {
            case FoundLevels.Earth1:
                cm.hasFoundEarth1 = true;
                break;
            case FoundLevels.Earth2:
                cm.hasFoundEarth2 = true;
                break;
            case FoundLevels.Earth3:
                cm.hasFoundEarth3 = true;
                break;
            case FoundLevels.Fire1:
                cm.hasFoundFire1 = true;
                break;
            case FoundLevels.Fire2:
                cm.hasFoundFire2 = true;
                break;
            case FoundLevels.Fire3:
                cm.hasFoundFire3 = true;
                break;
            case FoundLevels.Water1:
                cm.hasFoundWater1 = true;
                break;
            case FoundLevels.Water2:
                cm.hasFoundWater2 = true;
                break;
            case FoundLevels.Water3:
                cm.hasFoundWater3 = true;
                break;
            case FoundLevels.Lt1:
                cm.hasFoundLt1 = true;
                break;
            case FoundLevels.Lt2:
                cm.hasFoundLt2 = true;
                break;
            case FoundLevels.Lt3:
                cm.hasFoundLt3 = true;
                break;
            case FoundLevels.Level1:
                cm.cangotoLevel1 = true;
                break;
            case FoundLevels.Level2:
                cm.cangotoLevel2 = true;
                break;
            case FoundLevels.Level3:
                cm.cangotoLevel3 = true;
                break;
            case FoundLevels.None:
                break;
            default:
                break;
        }
    }
    private void TurnOnWorlds()
    {
        switch (foundWorlds)
        {
            case FoundWorlds.Earth:
                cm.hasFoundEarthLevel = true;
                break;
            case FoundWorlds.Water:
                cm.hasFoundWaterLevel = true;
                break;
            case FoundWorlds.Fire:
                cm.hasFoundFireLevel = true;
                break;
            case FoundWorlds.Lt:
                cm.hasFoundLtLevel = true;
                break;
            case FoundWorlds.None:
                break;
            default:
                break;
        }        
    }
}

