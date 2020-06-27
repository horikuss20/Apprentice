using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellClear : MonoBehaviour
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
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Gamemanager.GetComponent<GameManager>().collectibleCounter = 0;
            Gamemanager.GetComponent<GameManager>().oldCollectCount = 0;

            Player.GetComponent<MagicSpells>().basespell = MagicSpells.BaseSpells.none;
            Player.GetComponent<MagicSpells>().KSpells = MagicSpells.MainSpells.none;
            Player.GetComponent<MagicSpells>().LSpells = MagicSpells.MainSpells.none;
            Player.GetComponent<MagicSpells>().SemiSpells = MagicSpells.MainSpells.none;
        }
    }
}
