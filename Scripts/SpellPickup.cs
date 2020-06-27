using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPickup : MonoBehaviour
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
        Gamemanager.GetComponent<SkillSystemNew>().baseequip = Gamemanager.GetComponent<SkillSystemNew>().firegreatsword;
        Player.GetComponent<MagicSpells>().basespell = MagicSpells.BaseSpells.fireSword;
        Destroy(gameObject);
    }


}
