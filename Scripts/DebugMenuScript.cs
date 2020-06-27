using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenuScript : MonoBehaviour
{
    public GameObject DebugCanvas;
    private bool Off = true;
    private bool willDie = false;
    public int NextScene;
    public void Awake()
    {
        DebugCanvas = GameObject.Find("DebugUI");
        DebugCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            DebugCanvas.SetActive(Off);
            Off = !Off;
        }

    }
    public void Immortality()
    {
        GameObject.Find("HealthUI").GetComponent<Health>().noDamage = !willDie;
        willDie = !willDie;
    }
    public void DamagePlayer()
    {
        GameObject.Find("HealthUI").GetComponent<Health>().Damage(1);
    }
    public void KillPlayer()
    {
        GameObject.Find("HealthUI").GetComponent<Health>().health = 0;
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void HealPlayer()
    {
        GameObject.Find("HealthUI").GetComponent<Health>().Heal();
    }
    public void AddHeart()
    {
        GameObject.Find("HealthUI").GetComponent<Health>().numOfHearts++;
    }
    public void RemoveHeart()
    {
        GameObject.Find("HealthUI").GetComponent<Health>().numOfHearts--;
    }
    public void DamagePlayerHalf()
    {
        GameObject.Find("HealthUI").GetComponent<Health>().DamageHalf();
    }
    public void HealPlayerHalf()
    {
        GameObject.Find("HealthUI").GetComponent<Health>().HealHalf();
    }
    public void KillAllEnemys()
    {
            foreach (GameObject Rot in GameObject.FindGameObjectsWithTag("PatrolEnemy"))
            {
                Destroy(Rot);
            }
    }
    public void NextLevel()
    {
        GameObject.FindGameObjectWithTag("cm").GetComponent<CheckpointMaster>().lastCheckPointPos = new Vector3(0, 0, 0);
        NextScene = GameObject.Find("EndLevelPlant").GetComponent<EndLevelPlantScript>().NextScene;
        SceneManager.LoadScene(NextScene);
    }
}


