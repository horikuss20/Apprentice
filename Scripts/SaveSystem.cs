using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
	    public static void SaveGame(SkillSystemNew ssm, Health Health, GameManager gm, CheckpointMaster cm, PendantSystemNew ps)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			string path = Application.persistentDataPath + "/Game.oof";
			FileStream stream = new FileStream(path, FileMode.Create);

			SaveData data = new SaveData(ssm, Health, gm,cm,ps);

			formatter.Serialize(stream, data);
			stream.Close();
		}

		public static SaveData LoadGame()
		{
			string path = Application.persistentDataPath + "/Game.oof";
			if(File.Exists(path))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream stream = new FileStream(path, FileMode.Open);

				SaveData data = formatter.Deserialize(stream) as SaveData;
				stream.Close();

				return data;
			}
			else
			{
				Debug.LogError("No save file at " + path);
				return null;
			}
		}
}
