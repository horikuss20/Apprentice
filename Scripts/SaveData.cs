using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
	public float health;
	public int heartnum;
	public int exp;
	public int gold;

	//test

	public string pside1;
	public string pside2;

	public int meteorXP;
	public int meteorshowerXP;
	public int greatswordXP;
	public int conflagrationXP;

	public int iceprismXP;
	public int iceballXP;
	public int frostblastXP;
	public int vortexXP;

	public int rockblastXP;
	public int beeXP;
	public int mekigneerXP;
	public int stoneskinXP;

	public int voltdaggerXP;
	public int lightlanceXP;
	public int chainlightXP;
	public int balllightXP;

	public string baseequip;
	public string slot1equip;
	public string slot2equip;
	public string slot3equip;

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


	public SaveData(SkillSystemNew ssm, Health Health, GameManager gm, CheckpointMaster cm, PendantSystemNew ps)
	{
		health = Health.health;
		heartnum = Health.numOfHearts;

		exp = ssm.exp;
		gold = gm.collectibleCounter;

		pside1 = ps.pSide1;
		pside2 = ps.pSide2;

		baseequip = ssm.baseequip;
		slot1equip = ssm.slot1equip;
		slot2equip = ssm.slot2equip;
		slot3equip = ssm.slot3equip;

		meteorXP = ssm.meteorXP;
		meteorshowerXP = ssm.meteorshowerXP;
		greatswordXP = ssm.greatswordXP;
		conflagrationXP = ssm.conflagrationXP;

		iceprismXP = ssm.iceprismXP;
		iceballXP = ssm.iceballXP;
		frostblastXP = ssm.frostblastXP;
		vortexXP = ssm.vortexXP;

		rockblastXP = ssm.rockblastXP;
		beeXP = ssm.beeXP;
		mekigneerXP = ssm.mekigneerXP;
		stoneskinXP = ssm.stoneskinXP;

		voltdaggerXP = ssm.voltdaggerXP;
		lightlanceXP = ssm.lightlanceXP;
		chainlightXP = ssm.chainlightXP;
		balllightXP = ssm.balllightXP;


		//Water
		hasFoundWater1 = cm.hasFoundWater1;
		hasFoundWater2 = cm.hasFoundWater2;
		hasFoundWater3 = cm.hasFoundWater3;
		//Fire
		hasFoundFire1 = cm.hasFoundFire1;
		hasFoundFire2 = cm.hasFoundFire2;
		hasFoundFire3 = cm.hasFoundFire3;
		//Earth
		hasFoundEarth1 = cm.hasFoundEarth1;
		hasFoundEarth2 = cm.hasFoundEarth2;
		hasFoundEarth3 = cm.hasFoundEarth3;
		//Lightning
		hasFoundLt1 = cm.hasFoundLt1;
		hasFoundLt2 = cm.hasFoundLt2;
		hasFoundLt3 = cm.hasFoundLt3;
		//Levels
		hasFoundWaterLevel = cm.hasFoundWaterLevel;
		hasFoundFireLevel = cm.hasFoundFireLevel;
		hasFoundEarthLevel = cm.hasFoundEarthLevel;
		hasFoundLtLevel = cm.hasFoundLtLevel;
}

}
