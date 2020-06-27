using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;


public class CheckpointMaster :MonoBehaviour
{
    private static CheckpointMaster instance;
    public  Vector3 lastCheckPointPos;
    public bool usingTP = false;
    public GameObject Player;

    //Bools for finding the teleporters
    //Water
    public bool hasFoundWater1;
    public bool hasFoundWater2;
    public bool hasFoundWater3;
    //Fire
    public bool hasFoundFire1;
    public bool hasFoundFire2;
    public bool hasFoundFire3;
    //Earth
    public bool hasFoundEarth1;
    public bool hasFoundEarth2;
    public bool hasFoundEarth3;
    //Lightning
    public bool hasFoundLt1;
    public bool hasFoundLt2;
    public bool hasFoundLt3;
    //Levels
    public bool hasFoundWaterLevel;
    public bool hasFoundFireLevel;
    public bool hasFoundEarthLevel;
    public bool hasFoundLtLevel;
    //Levels
    public bool cangotoLevel1;
    public bool cangotoLevel2;
    public bool cangotoLevel3;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
      

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += Sceneloadset;
    }
    void Sceneloadset(Scene scene, LoadSceneMode mode)
    {
        if (usingTP == true)
        {
            lastCheckPointPos = GameObject.Find("Teleporter").GetComponent<Transform>().position;
            usingTP = false;
        }
        
    }
    
}
