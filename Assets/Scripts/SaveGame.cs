//
// Data structures and functions for saving and loading the game
//

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// TODO:
// Learn a lot more about this so that I can actually do it right
// Player Attributes is giving an exception when being serialized, despite being marked as serializable.
//    Only works with structs?

public class SaveGame : MonoBehaviour {

	[Serializable]
	public struct Vector3_serializable
	{
		public float x;
		public float y;
		public float z;
	}

	[Serializable]
	public class GameData
	{
		public Vector3_serializable m_playerPosition {get; set;}
		public Vector3_serializable m_playerRotation {get; set;}
		// public PlayerAttributes m_playerAttributes {get; set;}
		public TerrainControl.chunkData[] m_terrainList {get; set;}
	}

	public static Vector3_serializable playerPosition;
	public static Vector3_serializable playerRotation;
	// public static PlayerAttributes playerAttributes;
	public static TerrainControl.chunkData[] terrainList;

	// ---
	// --------------------
	// ---

	public static void save()
	{
		GameObject player = GameObject.Find("Player");

		terrainList = TerrainControl.terrainList;
		BinaryFormatter bf = new BinaryFormatter();
		GameData data = new GameData();

		Debug.Log(Application.persistentDataPath);
		FileStream file = File.Create(Application.persistentDataPath + "/GameInfo.dat"); // or whatever file name

		// DATA TO BE SAVED:
		playerPosition.x = player.transform.position.x;
		playerPosition.y = player.transform.position.y+0.001f; // potential rounding error causing problem?
		playerPosition.z = player.transform.position.z;
		data.m_playerPosition = playerPosition;

		playerRotation.x = player.transform.eulerAngles.x;
		playerRotation.y = player.transform.eulerAngles.y;
		playerRotation.z = player.transform.eulerAngles.z;
		data.m_playerRotation = playerRotation;

		// data.m_playerAttributes = player.GetComponent<PlayerAttributes>();

		data.m_terrainList = terrainList;
		// ----------

		bf.Serialize(file, data);
		file.Close();

		Debug.Log("Data Saved, hopefully");
	}

	public static void load()
	{
		if(File.Exists(Application.persistentDataPath + "/GameInfo.dat"))
		{
			GameObject player = GameObject.Find("Player");

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/GameInfo.dat", FileMode.Open);
			GameData data  = (GameData)bf.Deserialize(file); // check casting

			// DATA TO BE LOADED:
			player.transform.position = new Vector3(data.m_playerPosition.x,
			                                        data.m_playerPosition.y,
			                                        data.m_playerPosition.z);

			player.transform.eulerAngles = new Vector3(data.m_playerRotation.x,
			                                           data.m_playerRotation.y,
			                                           data.m_playerRotation.z);

			// setAttributes(data.m_playerAttributes, player.GetComponent<PlayerAttributes>());

			//TerrainControl.terrainList = data.m_terrainList;
			// ----------

			file.Close();
			Debug.Log("Successfully Loaded Save Data, I think");
		}
		else
		{
			Debug.Log("No saved data found");
		}
	}

	public static void setAttributes(PlayerAttributes from, PlayerAttributes to)
	{
		to.health = from.health;			// Player max health.
		to.curHealth = from.curHealth;			// Current health of player.
		to.moveSpeed = from.moveSpeed;			// Base movement speed of character.
		to.strength = from.strength;			// Base damage dealt by abilities.
		to.attRange = from.attRange;			// Max distance abilities can be used from the player.

		to.endurance = from.endurance;			// Base resource pool for sprinting, abilities, etc.
		to.curEndurance = from.curEndurance;		// Current endurance level.
		to.regeneration = from.regeneration;		// Energy regeneration rate.
		
		to.curLevel = from.curLevel;			// Level of player.
		to.maxLevel = from.maxLevel;			// Maximum player level.
		to.curExp = from.curExp;			// Base amount of experience on current level.
		to.maxExp = from.maxExp;			// Experience needed to level.
		
		to.hungerLevel = from.hungerLevel;		// 1-3 levels of hungers
		to.curHungerLevel = from.curHungerLevel;
		to.hungerMod = from.hungerMod;			// Stat multiplier based on hunger level
		to.feedTime = from.feedTime;			// Time left before hunger increases (seconds).
		to.time = from.time;				// Value that feedTime resets to.
	}
}
