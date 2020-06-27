using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables

    #region Audio Sources
    private AudioSource musicSource;
    private AudioSource optionbuttonSource;
    private AudioSource resumebuttonSource;
    private AudioSource playerSource;
    private AudioSource ballistaSource;
    private AudioSource gateSource;
    private AudioSource sfxSource;
    private AudioSource pendantSource;
    #endregion

    #region Game Objects
    private GameObject optionsCanvas;
    private GameObject mainMenu;
    private GameObject optionsButton;
    private GameObject resumeButton;
    private GameObject menuButton;
    private GameObject Player;
    private GameObject pathPillar;
    private GameObject killPillar;
    private GameObject goldPillar;
    private GameObject backgroundCamera;
    public GameObject pillarEnemy;
    private GameObject pickuptext;
    private GameObject pickuptextbg;
    private GameObject potiontext;
    private GameObject potiontextbg;
    private GameObject healthUI;
    private GameObject eventSystem;
    private GameObject Shop;
    #endregion

    #region Sliders
    public Slider musicSlider;
    public Slider sfxSlider;
    private Slider fauxSlider;
    #endregion

    #region Audio Clips
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip cityMusic;
    public AudioClip collectibleClip;
    public AudioClip pathClear;
    public AudioClip enemyHit;
    public AudioClip dungeonClip;
    public AudioClip healthClip;
    #endregion

    #region Bools, Ints, Texts, Misc.
    public static GameManager instance = null;
    public bool isPaused;
    public bool dead;
    public bool open;
    public bool off;
    public float healthCount;
    public string levelName;
    private Text collectibleText;
    private Text collectibleText2;
    private Text holderText;
    private Text holderText2;
    public Vector3 playerVector;
    public Vector3 playerZReset;
    public int collectibleCounter;
    public int oldCollectCount;
    public int oldKillCount = 0;
    public int killCount = 0;
    public int healthPotion = 0;
    #endregion

    #endregion

    // DATA
    bool bPlaying;
    Scene curScene;

    public bool Playing
    {
        get { return bPlaying; }
        set { bPlaying = value; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        #region Variable Definitions
        optionsCanvas = GameObject.Find("OptionsCanvas");
        backgroundCamera = GameObject.Find("Background Camera");
        //eventSystem = GameObject.Find("EventSystem");
        mainMenu = GameObject.Find("MainCanvas");
        healthUI = GameObject.Find("HealthUI");
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(optionsCanvas);
        optionsButton = GameObject.Find("OptionsButton");
        menuButton = GameObject.Find("MenuButton");
        Player = GameObject.Find("PlayerFunctionality");
        musicSource = GetComponent<AudioSource>();
        sfxSource = GameObject.Find("SoundEffectPlayer").GetComponent<AudioSource>();
        musicSource.volume = 1.0f;
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
        fauxSlider = GameObject.Find("FauxSlider").GetComponent<Slider>();
        Shop = GameObject.Find("Wares and Flairs from Ye Olde Odon");

        if (optionsCanvas != null)
        {
            optionsCanvas.SetActive(false);
        }

        if (mainMenu == null)
        {
            mainMenu = optionsCanvas;
        }

        if(mainMenu != null)
        {
            DontDestroyOnLoad(mainMenu);
        }

        #endregion
    }

    private void Start()
    {
        if (resumeButton == null)
        {
            resumeButton = optionsButton;
        }

        else
        {
            resumeButton.SetActive(false);
        }
    }

    private void Update()
    {
        pauseMenu();
        BackgroundSwitch();
        PotionConsumable();
        musicSource.volume = musicSlider.value;
        sfxSource.volume = sfxSlider.value;

		if(GameObject.Find("PickupPic"))
		{
			pickuptext = GameObject.Find("PickupText").GetComponent<Text>().gameObject;
			pickuptextbg = GameObject.Find("PickupTextBG").GetComponent<Text>().gameObject;
		}

		if(pickuptext != null)
		{
			pickuptext.GetComponent<Text>().text = collectibleCounter.ToString();
			pickuptextbg.GetComponent<Text>().text = collectibleCounter.ToString();
		}

        //if(eventSystem == null)
        //{
        //    eventSystem = GameObject.Find("EventSystem");
        //}

        if (GameObject.Find("healthpotion"))
        {
            potiontext = GameObject.Find("PotionText").GetComponent<Text>().gameObject;
            potiontextbg = GameObject.Find("PotionTextBg").GetComponent<Text>().gameObject;
        }

        if (potiontext != null)
        {
            potiontext.GetComponent<Text>().text = healthPotion.ToString();
            potiontextbg.GetComponent<Text>().text = healthPotion.ToString();
        }

        if (healthUI == null)
        {
            healthUI = GameObject.Find("HealthUI");
        }

        if (collectibleCounter > oldCollectCount)
        {
            sfxSource.PlayOneShot(collectibleClip, 1.0f);
            oldCollectCount = collectibleCounter;
        }

        if (killCount > oldKillCount)
        {
            oldKillCount = killCount;
        }

        collectibleCounter = oldCollectCount;


        
        //if (mainMenu.activeInHierarchy)
        //{
        //    eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().firstSelectedGameObject = GameObject.Find("Start");
        //}

        //if(SceneManager.GetActiveScene().name == "Options")
        //{
        //    eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().firstSelectedGameObject = GameObject.Find("ReturnButton");
        //}

        //if (SceneManager.GetActiveScene().name == "Level_Select")
        //{
        //    eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().firstSelectedGameObject = GameObject.Find("LostCity");
        //}

        //if (optionsCanvas.activeInHierarchy)
        //{
        //    eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().firstSelectedGameObject = GameObject.Find("SaveButton");
        //}

        #region Menu, Slider, & Scene Regulation 
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (mainMenu.activeInHierarchy != this.mainMenu)
            {
                Destroy(GameObject.Find("MainCanvas"));
            }

            mainMenu.SetActive(false);
        }
        else
        {
            if (mainMenu.activeInHierarchy != this.mainMenu)
            {
                Destroy(GameObject.Find("MainCanvas"));
            }

            mainMenu.SetActive(true);
        }

        if (GameObject.Find("PlayerFunctionality") != null)
        {
            playerSource = GameObject.Find("PlayerFunctionality").GetComponent<AudioSource>();
            playerSource.volume = sfxSlider.value;
        }

        if (GameObject.Find("PlayerFunctionality") == null)
        {
            playerSource = musicSource;
        }

        if (GameObject.Find("PendantSelector") != null)
        {
            pendantSource = GameObject.Find("PendantSelector").GetComponent<AudioSource>();
            pendantSource.volume = sfxSlider.value;
        }

        if (GameObject.Find("PendantSelector") == null)
        {
            pendantSource = musicSource;
        }

        if (SceneManager.GetActiveScene().name == "SoundOptions")
        {
            if (optionsCanvas.activeInHierarchy == true)
            {
                optionsButton.SetActive(false);
                musicSlider.gameObject.SetActive(true);
                sfxSlider.gameObject.SetActive(true);
                menuButton.SetActive(false);
            }

            else
            {
                optionsButton.SetActive(true);
            }
            isPaused = true;
        }


        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            isPaused = false;
            optionsCanvas.SetActive(false);

			if(musicSource.clip != menuMusic)
			{
				musicSource.clip = menuMusic;
				musicSource.Play();
			}

            if(musicSlider != null)
            {
                musicSlider.gameObject.SetActive(true);
            }

            if (sfxSlider != null)
            {
                sfxSlider.gameObject.SetActive(true);
            }

        }

		if(SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Options" && SceneManager.GetActiveScene().name != "SoundOptions" && SceneManager.GetActiveScene().name != "Level_Select" && 
            SceneManager.GetActiveScene().name != "LostCity_Scene" && SceneManager.GetActiveScene().name != "Dungeon_Sceene" && SceneManager.GetActiveScene().name != "LostCity2_Scene" && SceneManager.GetActiveScene().name != "Dungeon2_Scene")
		{
			if (musicSource.clip != gameMusic)
			{
				musicSource.clip = gameMusic;
				musicSource.Play();
			}
		}
        
        if(SceneManager.GetActiveScene().name == "LostCity_Scene" || SceneManager.GetActiveScene().name == "LostCity2_Scene")
        {
            if (musicSource.isPlaying && musicSource.clip != cityMusic)
            {
                musicSource.Stop();
            }
            if(!musicSource.isPlaying)
            {
                musicSource.clip = cityMusic;
                musicSource.Play();
            }
        }

        if (SceneManager.GetActiveScene().name == "Dungeon_Sceene" || SceneManager.GetActiveScene().name == "Dungeon2_Scene")
        {
            if (musicSource.isPlaying && musicSource.clip != dungeonClip)
            {
                musicSource.Stop();
            }
            if (!musicSource.isPlaying)
            {
                musicSource.clip = dungeonClip;
                musicSource.Play();
            }
        }

        if (SceneManager.GetActiveScene().name == "Credits")
        {
            isPaused = false;
            optionsCanvas.SetActive(false);
            if (musicSlider != null)
            {
                musicSlider.gameObject.SetActive(true);
            }

            if (sfxSlider != null)
            {
                sfxSlider.gameObject.SetActive(true);
            }
        }

        if(SceneManager.GetActiveScene().name == "Options")
        {
            isPaused = false;
            optionsCanvas.SetActive(false);
        }

        if(backgroundCamera != null)
        {
            DontDestroyOnLoad(backgroundCamera);
        }

        if (optionsCanvas.activeInHierarchy != this.optionsCanvas)
        {
            Destroy(GameObject.Find("OptionsCanvas"));
        }


        #region Hub Scene
        if (SceneManager.GetActiveScene().name == "Hub_Scene" || SceneManager.GetActiveScene().name == "Hub2_Scene" || SceneManager.GetActiveScene().name == "Hub3_Scene")
        {
            sfxSlider.gameObject.SetActive(false);
            musicSlider.gameObject.SetActive(false);
            optionsButton.SetActive(false);
            menuButton.SetActive(true);
            healthUI = GameObject.Find("HealthUI");
        }
        #endregion

        #region Tutorial Scene
        if (SceneManager.GetActiveScene().name == "Tutorial_Scene")
        {
            sfxSlider.gameObject.SetActive(false);
            musicSlider.gameObject.SetActive(false);
            optionsButton.SetActive(false);
            menuButton.SetActive(true);
            healthUI = GameObject.Find("HealthUI");
        }
        #endregion

        #region Tanglewood Scene
        if (SceneManager.GetActiveScene().name == "Tanglewood1_Scene" || SceneManager.GetActiveScene().name == "Tanglewood2_Scene")
        {
            sfxSlider.gameObject.SetActive(false);
            musicSlider.gameObject.SetActive(false);
            optionsButton.SetActive(false);
            menuButton.SetActive(true);
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);


            #region Scene Specific Audio
            //Ballista
            if (GameObject.Find("Ballista") != null)
            {
                ballistaSource = GameObject.Find("Ballista").GetComponent<AudioSource>();
                ballistaSource.volume = sfxSlider.value;
            }

            if(GameObject.Find("Ballista") == null)
            {
                ballistaSource = musicSource;
            }

            //Player


            //Jump Gate
            if (GameObject.Find("JumpGate") != null)
            {
                gateSource = GameObject.Find("JumpGate").GetComponent<AudioSource>();
                gateSource.volume = sfxSlider.value;
            }

            if (GameObject.Find("JumpGate") == null)
            {
                gateSource = musicSource;
            }
            #endregion
        }
        #endregion

        #region Lost City

        #region 1st Level
        if (SceneManager.GetActiveScene().name == "LostCity_Scene")
        {

            sfxSlider.gameObject.SetActive(false);
            musicSlider.gameObject.SetActive(false);
            optionsButton.SetActive(false);
            menuButton.SetActive(true);
            killPillar = GameObject.Find("KillPillar2");
            goldPillar = GameObject.Find("GoldPillar");
            pillarEnemy = GameObject.Find("Shield_Enemy");
            //playerSource = GameObject.Find("PlayerFunctionality").GetComponent<AudioSource>();
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
            GameObject.Find("PlayerFunctionality").transform.position = new Vector3 (GameObject.Find("PlayerFunctionality").transform.position.x, GameObject.Find("PlayerFunctionality").transform.position.y, -0.5f);
            #region Scene Specific Interactions
            //GOLD ROOM
            if (GameObject.Find("Enemy1 (1)") == null && GameObject.Find("Enemy1 (2)") == null && GameObject.Find("Enemy1 (3)") == null)
            {
                if (dead == false && killCount == 0)
                {
                    sfxSource.clip = GameObject.Find("GameManager").GetComponent<GameManager>().pathClear;
                    sfxSource.Play();
                    killCount++;
                }
                if (killCount == 1)
                {
                    Destroy(goldPillar);
                }
            }

            //FIRST SHIELD MINI BOSS
            if (pillarEnemy == null)
            {
                if (GameObject.Find("Shield_Enemy") == null)
                {
                    Destroy(killPillar);
                    if(killCount == 0)
                    {
                        killCount = 1;
                    }
                    if (killCount == 1)
                    {
                        killCount++;
                        sfxSource.clip = GameObject.Find("GameManager").GetComponent<GameManager>().pathClear;
                        sfxSource.Play();
                    }
                }
            }

            #endregion

            #region Scene Audio
            //Ballista
            if (GameObject.Find("Ballista") != null)
            {
                ballistaSource = GameObject.Find("Ballista").GetComponent<AudioSource>();
                ballistaSource.volume = sfxSlider.value;
            }

            if (GameObject.Find("Ballista") == null)
            {
                ballistaSource = musicSource;
            }

            //Player
            //if (GameObject.Find("PlayerFunctionality") != null)
            //{
            //    playerSource = GameObject.Find("PlayerFunctionality").GetComponent<AudioSource>();
            //    playerSource.volume = sfxSlider.value;
            //}

            //if (GameObject.Find("PlayerFunctionality") == null)
            //{
            //    playerSource = musicSource;
            //}

            //Jump Gate
            if (GameObject.Find("JumpGate") != null)
            {
                gateSource = GameObject.Find("JumpGate").GetComponent<AudioSource>();
                gateSource.volume = sfxSlider.value;
            }

            if (GameObject.Find("JumpGate") == null)
            {
                gateSource = musicSource;
            }
            #endregion
        }
        #endregion

        #region 2nd Level

        if(SceneManager.GetActiveScene().name == "LostCity2_Scene")
        {
            GameObject enemy1 = GameObject.Find("Enemy1 (1)");
            GameObject enemy2 = GameObject.Find("Enemy1 (2)");
            GameObject goldEnemy1 = GameObject.Find("1");
            GameObject goldEnemy2 = GameObject.Find("2");
            GameObject gumb1 = GameObject.Find("Gumbis (1)");
            GameObject gumb2 = GameObject.Find("Gumbis (2)");
            sfxSlider.gameObject.SetActive(false);
            musicSlider.gameObject.SetActive(false);
            optionsButton.SetActive(false);
            menuButton.SetActive(true);
            //playerSource = GameObject.Find("PlayerFunctionality").GetComponent<AudioSource>();
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);


            if (enemy1 == null && enemy2 == null)
            {
                if(!sfxSource.isPlaying && dead == false)
                {
                    sfxSource.clip = GameObject.Find("GameManager").GetComponent<GameManager>().pathClear;
                    sfxSource.Play();
                    killCount++;
                }
            }

            if(killCount == 1)
            {
                dead = true;
                Destroy(GameObject.Find("FirstDoor"));
            }

            if (goldEnemy1 == null && goldEnemy2 == null)
            {
                if (killCount == 0)
                {
                    killCount = 1;
                }

                if (!sfxSource.isPlaying && killCount == 1)
                {
                    sfxSource.clip = GameObject.Find("GameManager").GetComponent<GameManager>().pathClear;
                    sfxSource.Play();
                    killCount++;
                }
            }

            if(killCount == 2)
            {
                Destroy(GameObject.Find("GoldWall"));
                Destroy(GameObject.Find("GoldWall2"));
            }

            if (gumb1 == null && gumb2 == null)
            {
                if (!sfxSource.isPlaying && killCount == 2)
                {
                    sfxSource.clip = GameObject.Find("GameManager").GetComponent<GameManager>().pathClear;
                    sfxSource.Play();
                    killCount++;
                }
            }

            if (killCount == 3)
            {
                Destroy(GameObject.Find("TreasureWall"));
            }
        }



        #endregion

        #endregion

        #region Dungeon

        #region 1st Level
        if (SceneManager.GetActiveScene().name == "Dungeon_Sceene" )
        {
            sfxSlider.gameObject.SetActive(false);
            musicSlider.gameObject.SetActive(false);
            optionsButton.SetActive(false);
            menuButton.SetActive(true);
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);

            #region Scene Interactions
            if (isPaused == true)
            {
                optionsCanvas.SetActive(true);
            }
            else
            {
                optionsCanvas.SetActive(false);
            }

            if(GameObject.Find("Shield_Enemy (1)") == null && killCount == 0)
            {
                killCount++;
                GameObject.Find("SHIELDWALL").SetActive(false);
            }
            #endregion

        }
        #endregion

        #region 2nd Level
        if(SceneManager.GetActiveScene().name == "Dungeon2_Scene")
        {
            sfxSlider.gameObject.SetActive(false);
            musicSlider.gameObject.SetActive(false);
            optionsButton.SetActive(false);
            menuButton.SetActive(true);
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
            #region Scene Specific Variables
            GameObject enemy1 = GameObject.Find("Enemy1 (4)");
            GameObject enemy2 = GameObject.Find("ZerkerHolder (1)");
            GameObject enemy3 = GameObject.Find("RangedEnemy (4)");
            GameObject enemy4 = GameObject.Find("Shield_Enemy (2)");
            GameObject enemy5 = GameObject.Find("RangedEnemy (6)");
            //GameObject gumbEnemy1 = GameObject.Find("RangedEnemy (9)");
            //GameObject gumbEnemy2 = GameObject.Find("Shield_Enemy (5)");
            //GameObject gumbEnemy3 = GameObject.Find("ZerkerEnemy (5)");
            //GameObject gumbEnemy4 = GameObject.Find("ZerkerEnemy (4)");
            //GameObject gumbEnemy5 = GameObject.Find("Shield_Enemy (4)");
            //GameObject gumbEnemy6 = GameObject.Find("RangedEnemy (8)");
            //GameObject rwall = GameObject.Find("RightRoomWall");
            //GameObject gumbis = GameObject.Find("Gumbis (1)");
            #endregion

            if (enemy1 == null && enemy2 == null && enemy3 == null)
            {
                if (killCount == 0)
                {
                    sfxSource.clip = GameObject.Find("GameManager").GetComponent<GameManager>().pathClear;
                    sfxSource.Play();
                    killCount++;
                }
            }

            if(killCount == 1)
            {
                Destroy(GameObject.Find("GoldRoof"));
            }

            if (enemy4 == null && enemy5 == null)
            {
                if (killCount == 0)
                {
                    killCount = 1;
                }

                if (killCount == 1)
                {
                    sfxSource.clip = GameObject.Find("GameManager").GetComponent<GameManager>().pathClear;
                    sfxSource.Play();
                    killCount++;
                }

                if (killCount == 2)
                {
                    Destroy(GameObject.Find("TreasureDoor (1)"));
                }
            }

            //if(gumbis == null && gumbEnemy1 == null && gumbEnemy2 == null && gumbEnemy3 == null && gumbEnemy4 == null && gumbEnemy5 == null && gumbEnemy6 == null)
            //{
            //    if(killCount != 3)
            //    {
            //        killCount = 3;
            //    }

            //    if(killCount == 3)
            //    {
            //        sfxSource.clip = GameObject.Find("GameManager").GetComponent<GameManager>().pathClear;
            //        sfxSource.Play();
            //        killCount++;
            //    }

            //    if(killCount == 4)
            //    {
            //        Destroy(rwall);
            //    }
            //}
        }



        #endregion

        #endregion

        if (sfxSlider == null)
        {
            sfxSlider = fauxSlider;
        }

        if (musicSlider == null)
        {
            musicSlider = fauxSlider;
        }

        #endregion

    }

    // SCENE MANAGEMENT
    #region Scene Management Functions
    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isPaused = false;
        curScene = scene;
        optionsCanvas.SetActive(false);
        bPlaying = false;
        killCount = 0;
        oldKillCount = 0;
        dead = false;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(curScene.buildIndex + 1);
    }

    public void StartScene(string sLevel)
    {
		GetComponent<SkillSystemNew>().slot1equip = null;
		GetComponent<SkillSystemNew>().slot2equip = null;
		GetComponent<SkillSystemNew>().slot3equip = null;
		GetComponent<SkillSystemNew>().baseequip = null;
		SceneManager.LoadScene("Tutorial_Scene");
        GameObject.Find("CheckpointMaster").GetComponent<CheckpointMaster>().usingTP = false;
        GameObject.Find("CheckpointMaster").GetComponent<CheckpointMaster>().lastCheckPointPos = new Vector3(-10,2.5f,-.5f);
        isPaused = false;
    }

    public void LoadScene(string sLevel)
    {
        SceneManager.LoadScene(sLevel);
        GameObject.Find("CheckpointMaster").GetComponent<CheckpointMaster>().usingTP = true;
        isPaused = false;
    }
    public void LoadScene1(string sLevel)
    {
        SceneManager.LoadScene(sLevel);
        isPaused = false;
    }

    public void LoadLast()
    {
        string levelName = PlayerPrefs.GetString("LastScene");
        SceneManager.LoadScene(levelName);
    }

    public void ResumeGame()
    {
        isPaused = false;
     
    }

    public void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    #endregion

    //FUNCTIONS
    #region Functions
    private void pauseMenu()
    {
        if(isPaused == false)
        {
            Time.timeScale = 1;
            optionsCanvas.SetActive(false);
        }

        if(isPaused == true)
        {
            Time.timeScale = 0;
            optionsCanvas.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelName = SceneManager.GetActiveScene().name;
            if (isPaused == false)
            {
                isPaused = true;
            }

            else if (isPaused == true)
            {
                isPaused = false;
            }
        }
    }

    private void BackgroundSwitch()
    {
        if (SceneManager.GetActiveScene().name == "LostCity_Scene" || SceneManager.GetActiveScene().name == "Dungeon_Sceene" || SceneManager.GetActiveScene().name == "LostCity2_Scene" || SceneManager.GetActiveScene().name == "Dungeon2_Scene"
        || SceneManager.GetActiveScene().name == "Tanglewood1_Scene" || SceneManager.GetActiveScene().name == "Tanglewood2_Scene" || SceneManager.GetActiveScene().name == "Hub_Scene" || SceneManager.GetActiveScene().name == "Nick_Scene"
        || SceneManager.GetActiveScene().name == "Tutorial_Scene" || SceneManager.GetActiveScene().name == "Hub2_Scene" || SceneManager.GetActiveScene().name == "Hub3_Scene")
        {
            off = true;
        }
        else
        {
            off = false;
        }

        if (off == true)
        {
            backgroundCamera.SetActive(false);
        }

        if(off == false)
        {
            backgroundCamera.SetActive(true);
        }
    }

    private void PotionConsumable()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(healthPotion != 0 && healthUI.GetComponent<Health>().health != healthUI.GetComponent<Health>().numOfHearts)
            {
                healthUI.GetComponent<Health>().health = healthUI.GetComponent<Health>().numOfHearts;
                healthPotion -= 1;
            }
        }
    }
    #endregion
}
