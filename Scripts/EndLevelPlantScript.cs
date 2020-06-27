using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelPlantScript : MonoBehaviour
{
    //for detecting if the player has enterd the trigger
    public float SceneChangeTime = .8f;
    public bool EnteredPlantTrigger = false;
    public int NextScene;
    public Vector3 nextTPstop;
    //checkpoint master 
    public bool Level1;
    public bool Level2;
    public bool Level3;

    //for the fade
    public Texture2D fadeOutTexture;
    public float fadeSpeed = .8f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

	private GameObject gm;

    private AudioSource plantSource;

    void Start()
    {
        plantSource = GetComponent<AudioSource>();
		gm = GameObject.Find("GameManager");
    }

    void Update()
    {
        if(EnteredPlantTrigger == true)
        {
            if (Level1 == true)
            {
                GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().cangotoLevel1 = true;
            }
            if (Level2 == true)
            {
                GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().cangotoLevel2 = true;
            }
            if (Level3 == true)
            {
                GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().cangotoLevel3 = true;
            }
            plantSource.Play();
            SceneChange();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().usingTP = true; 
        EnteredPlantTrigger = true;
    }

     void SceneChange()
    {
        float fadeTime = BeginFade(1);
        SceneChangeTime -= Time.deltaTime;

		gm.GetComponent<SaveInputManager>().SaveGame();

        if(SceneChangeTime <= 0)
        {
            SceneManager.LoadScene(NextScene);
            GameObject.Find("GameManager").GetComponent<GameManager>().collectibleCounter = 0;
        }
    }
    

    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }
    void OnLevelWasLoaded()
    {
        BeginFade(-1);
    }
}
