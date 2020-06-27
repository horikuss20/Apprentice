using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpellPickup1 : MonoBehaviour
{
    GameObject Player;
    GameObject Gamemanager;

    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player");
        Gamemanager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Gamemanager.GetComponent<SaveInputManager>().SaveGame();
        Gamemanager.GetComponent<SkillSystemNew>().slot1equip = Gamemanager.GetComponent<SkillSystemNew>().meteor;
        Player.GetComponent<MagicSpells>().KSpells = MagicSpells.MainSpells.meteor;
        Destroy(gameObject);
    }
}
