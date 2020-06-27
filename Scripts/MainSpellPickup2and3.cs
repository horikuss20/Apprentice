using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpellPickup2and3 : MonoBehaviour
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
        Gamemanager.GetComponent<SkillSystemNew>().slot2equip = Gamemanager.GetComponent<SkillSystemNew>().iceprism;
        Player.GetComponent<MagicSpells>().LSpells = MagicSpells.MainSpells.iceprism;
        Gamemanager.GetComponent<SkillSystemNew>().slot3equip = Gamemanager.GetComponent<SkillSystemNew>().iceball;
        Player.GetComponent<MagicSpells>().SemiSpells = MagicSpells.MainSpells.iceball;
        Destroy(gameObject);
    }
}
