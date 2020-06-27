using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInputManager : MonoBehaviour
{
	GameObject GM;
	SkillSystemMain ssm;
	Health health;
	GameManager gamem;
	CheckpointMaster cm;
	PendantSystemNew ps;

   public void SaveGame()
   {
		GameObject GM = GameObject.Find("GameManager");
		GameManager gamem = GM.GetComponent<GameManager>();
		SkillSystemNew ssm = GM.GetComponent<SkillSystemNew>();
		health = GameObject.Find("HealthUI").GetComponent<Health>();
		cm = GameObject.Find("CheckpointMaster").GetComponent<CheckpointMaster>();
		ps = GM.GetComponent<PendantSystemNew>();


		SaveSystem.SaveGame(ssm,health,gamem,cm,ps);
   }

   public void LoadGame()
   {
		GameObject GM = GameObject.Find("GameManager");
		GameManager gamem = GM.GetComponent<GameManager>();
		SkillSystemNew ssm = GM.GetComponent<SkillSystemNew>();
		health = GameObject.Find("HealthUI").GetComponent<Health>();
		cm = GameObject.Find("CheckpointMaster").GetComponent<CheckpointMaster>();
		ps = GM.GetComponent<PendantSystemNew>();

		SaveData data = SaveSystem.LoadGame();

		gamem.collectibleCounter = data.gold;
		health.health = data.health;
		ssm.exp = data.exp;
		health.numOfHearts = data.heartnum;

		ps.pSide1 = data.pside1;
		ps.pSide2 = data.pside2;

		ssm.baseequip = data.baseequip;
		ssm.slot1equip = data.slot1equip;
		ssm.slot2equip = data.slot2equip;
		ssm.slot3equip = data.slot3equip;

		ssm.meteorXP = data.meteorXP;
		ssm.meteorshowerXP = data.meteorshowerXP;
		ssm.greatswordXP = data.greatswordXP;
		ssm.conflagrationXP = data.conflagrationXP;

		ssm.iceprismXP = data.iceprismXP;
		ssm.iceballXP = data.iceballXP;
		ssm.frostblastXP = data.frostblastXP;
		ssm.vortexXP = data.vortexXP;

		ssm.rockblastXP = data.rockblastXP;
		ssm.beeXP = data.beeXP;
		ssm.mekigneerXP = data.mekigneerXP;
		ssm.stoneskinXP = data.stoneskinXP;

		ssm.voltdaggerXP = data.voltdaggerXP;
		ssm.lightlanceXP = data.lightlanceXP;
		ssm.chainlightXP = data.chainlightXP;
		ssm.balllightXP = data.balllightXP;


		//Water
		cm.hasFoundWater1 = data.hasFoundWater1;
		cm.hasFoundWater2 = data.hasFoundWater2;
		cm.hasFoundWater3 = data.hasFoundWater3;
		//Fire
		cm.hasFoundFire1 = data.hasFoundFire1;
		cm.hasFoundFire2 = data.hasFoundFire2;
		cm.hasFoundFire3 = data.hasFoundFire3;
		//Earth
		cm.hasFoundEarth1 = data.hasFoundEarth1;
		cm.hasFoundEarth2 = data.hasFoundEarth2;
		cm.hasFoundEarth3 = data.hasFoundEarth3;
		//Lightning
		cm.hasFoundLt1 = data.hasFoundLt1;
		cm.hasFoundLt2 = data.hasFoundLt2;
		cm.hasFoundLt3 = data.hasFoundLt3;
		//Levels
		cm.hasFoundWaterLevel = data.hasFoundWaterLevel;
		cm.hasFoundFireLevel = data.hasFoundFireLevel;
		cm.hasFoundEarthLevel = data.hasFoundEarthLevel;
		cm.hasFoundLtLevel = data.hasFoundLtLevel;

		if(data.health <= 3)
		{
			health.health = 3;
		}

		if (data.heartnum > 3)
		{
			if (data.heartnum == 4)
			{
				health.Heal();
			}
			if (data.heartnum == 5)
			{
				health.Heal();
				health.Heal();
			}
		}
	}
}
