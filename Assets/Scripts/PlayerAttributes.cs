using UnityEngine;
using System;
using System.Collections;

// Attach to Player in Unity
[Serializable]
public class PlayerAttributes : MonoBehaviour {

	public float health;			// Player max health.
	public float curHealth;			// Current health of player.
	public float moveSpeed;			// Base movement speed of character.
	public float strength;			// Base damage dealt by abilities.
	public float attRange;			// Max distance abilities can be used from the player.

	public float endurance;			// Base resource pool for sprinting, abilities, etc.
	public float curEndurance;		// Current endurance level.
	public float regeneration;		// Energy regeneration rate.

	public float curLevel;			// Level of player.
	public float maxLevel;			// Maximum player level.
	public float curExp;			// Base amount of experience on current level.
	public float maxExp;			// Experience needed to level.

	public float hungerLevel;		// 1-3 levels of hungers
	public float curHungerLevel;
	public float hungerMod;			// Stat multiplier based on hunger level
	public float feedTime;			// Time left before hunger increases (seconds).
	public float time;				// Value that feedTime resets to.
	
	
	void Awake() {
		health = 20.0f;
		curHealth = health;
		endurance = 10.0f;
		curEndurance = endurance;
		regeneration = 1.0f;

		strength = 2.0f;
		attRange = 2.0f;

		curLevel = 1.0f;
		maxLevel = 3.0f;
		curExp = 1.0f;
		maxExp = 10.0f;

		hungerLevel = 1.0f;
		curHungerLevel = hungerLevel;
		feedTime = time = 60.0f;
	}
	
	void Update() {

		LevelUp();
		EnergyRegen();
		Health();
		HungerLevel();
		HungerModifier();
		Eat();

		/*if(Input.GetKeyDown ("h")){
			curHealth -= 1;
			Debug.Log("");
		}
		if(Input.GetKeyDown ("e")){
			curEndurance -= 3;
		}
		if(Input.GetKeyDown ("l")){
			curExp += 1;
		}*/
	}


	void OnGUI(){
	//-----------------------------------------------------
	// Displays player statistics on screen.
	//-----------------------------------------------------
		//GUI.Box(pos from left, from top, width, height)
		GUI.Box(new Rect(0, 0, Screen.width/4/(health / curHealth), 20), "HP: " + curHealth + " / " + health);
		GUI.Box(new Rect(0, 20, Screen.width/4/(endurance / curEndurance), 20), "Energy: " + curEndurance + " / " + endurance);
		GUI.Box(new Rect(0, 40, Screen.width/4/(curHungerLevel / hungerLevel), 20), "Hunger: " + hungerLevel + " / 3");
		GUI.Box(new Rect(0, 60, Screen.width/4/(maxExp/curExp), 20), "LvL: " + curLevel);
	}



	//-----------------------------------------------------
	// LevelUp()
	//
	// Increment level after curExp reaches maxExp in value
	// resets curExp to 1
	// Increase base stats on level up.
	//-----------------------------------------------------
	void LevelUp(){
		if (curExp >= maxExp) {
			curExp = 1.0f;
			if(curLevel < maxLevel){
				curLevel++;
				strength += curLevel;
				health += 5.0f;
				curHealth = health;
				endurance += 2.0f;
				curEndurance = endurance;
			}
		}
	}


	//-----------------------------------------------------
	// EnergyRegen()
	//
	// curEndurance values are always greater than or equal
	// to 0 or less than or equal to endurance.
	//-----------------------------------------------------
	void EnergyRegen(){
		if (curEndurance > endurance) {
			curEndurance = endurance;
		}
		if (curEndurance < endurance) {
			curEndurance += regeneration * Time.deltaTime;
			if (curEndurance > endurance) {
				curEndurance = endurance;
			}
			if (curEndurance < 0) {
				curEndurance = 0.0f;
			}	
		}
	}


	//-----------------------------------------------------
	// Health()
	//
	// Checks if curHealth is within 0 and the max
	// health limit.
	//-----------------------------------------------------
	void Health(){
		if (curHealth > health) {
			curHealth = health;
		}
		if (curHealth < 0) {
			curHealth = 0.0f;
		}
	}


	//-----------------------------------------------------
	// HungerLevel()
	//
	// Adjusts hungerLevel based on time passed
	// TODO goes down based on items eaten/mauled.
	//-----------------------------------------------------
	void HungerLevel(){
		feedTime -= Time.deltaTime;
		if(feedTime < 0 && !(hungerLevel > 2) ){
			hungerLevel++;
			feedTime = time;
		}
	}



	//-----------------------------------------------------
	// HungerModifier()
	//
	// Adjusts stats in void Update() based on hungerLevel.
	//-----------------------------------------------------
	void HungerModifier(){
		if(curHungerLevel != hungerLevel){

			if (hungerLevel == 2){
				hungerMod = 2.0f;
				curHungerLevel = hungerLevel;

				health -= hungerMod;
				endurance -= hungerMod;				
				strength -= hungerMod;
				attRange -= hungerMod;
			}
			else if(hungerLevel == 3){
				hungerMod = 3.0f;
				curHungerLevel = hungerLevel;

				health -= hungerMod;
				endurance -= hungerMod;		
				strength -= hungerMod;
				attRange -= hungerMod;
			}

		}
	}

	//-----------------------------------------------------
	// Eat()
	//
	// If the selected item in the player's inventory is
	// edible "eat" it and lower curHungerLevel by 1
	// Also resets the value of feedTime.
	//-----------------------------------------------------
	void Eat(){
		if(Input.GetKeyDown ("1") && PlayerInventoryGUI.inventoryNameDictionary[0] != string.Empty){
			if(PlayerInventoryGUI.inventoryNameDictionary[0] == "Basket" || PlayerInventoryGUI.inventoryNameDictionary[0] == "Berries"){
				Debug.Log ("You've eaten " + PlayerInventoryGUI.inventoryNameDictionary[0]);
				PlayerInventoryGUI.inventoryNameDictionary[0] = string.Empty;
				curHealth += 5;
				curExp++;
				hungerLevel = 1;
				feedTime = time;
			}
		}
		else if(Input.GetKeyDown ("2") && PlayerInventoryGUI.inventoryNameDictionary[1] != string.Empty){
			if(PlayerInventoryGUI.inventoryNameDictionary[1] == "Basket" || PlayerInventoryGUI.inventoryNameDictionary[1] == "Berries"){
				Debug.Log ("You've eaten " + PlayerInventoryGUI.inventoryNameDictionary[1]);
				PlayerInventoryGUI.inventoryNameDictionary[1] = string.Empty;
				curHealth += 5;
				curExp++;
				hungerLevel = 1;
				feedTime = time;
			}
		}
		else if(Input.GetKeyDown ("3") && PlayerInventoryGUI.inventoryNameDictionary[2] != string.Empty){
			if(PlayerInventoryGUI.inventoryNameDictionary[2] == "Basket" || PlayerInventoryGUI.inventoryNameDictionary[2] == "Berries"){
				Debug.Log ("You've eaten " + PlayerInventoryGUI.inventoryNameDictionary[0]);
				PlayerInventoryGUI.inventoryNameDictionary[2] = string.Empty;
				curHealth += 5;
				curExp++;
				hungerLevel = 1;
				feedTime = time;
			}
		}

	}


	
}