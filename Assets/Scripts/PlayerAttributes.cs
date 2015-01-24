using UnityEngine;
using System;
using System.Collections;

// Attach to Player in Unity
[Serializable]
public class PlayerAttributes : MonoBehaviour {

  public float health;       // Player max health.
  public float curHealth;    // Current health of player.
  public float moveSpeed;    // Base movement speed of character.
  public float strength;     // Base damage dealt by abilities.
  public float attRange;     // Max distance abilities can be used from player.

  public float endurance;
  public float curEndurance;
  public float regeneration; // Regen rate of player's energy resource. 

  public float curLevel;
  public float maxLevel;
  public float curExp;       // Base amount of experience on current level.
  public float maxExp;	     // Experience needed to level up.

  public float hungerLevel;
  public float curHungerLevel;
  public float hungerMod;
  public float feedTime;     // Time left before hunger increases (seconds).
  public float time;         // Value that feedTime resets to.
  public int sec = 60;
	
	
  void Awake()
  {
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
    feedTime = 60.0f;
    time = 60.0f;

    InvokeRepeating ("Stopwatch", 1, 1);
  }
	
  void Update()
  {
    LevelUp();
    EnergyRegen();
    Health();
    HungerLevel();
    HungerModifier();
    Eat();

  }


  void OnGUI()
  {

    //-----------------------------------------------------
    // Displays player statistics on screen.
    // GUI.Box(pos from left, from top, width, height)
    //-----------------------------------------------------
    GUI.Box(new Rect(0, 0, Screen.width/4/(health / curHealth), 20),
		     "HP: " + curHealth + " / " + health);

    GUI.Box(new Rect(0, 20, Screen.width/4/(endurance / curEndurance), 20),
		     "Energy: " + curEndurance + " / " + endurance);

    GUI.Box(new Rect(0, 40, Screen.width/4/(curHungerLevel / hungerLevel), 20), 
		     "Hunger: " + hungerLevel + " / 3");

    GUI.Box(new Rect(0, 60, Screen.width/4/(maxExp/curExp), 20),
		     "LvL: " + curLevel);

    GUI.Box (new Rect (Screen.width - Screen.width * 0.25f, 5,
                       Screen.width * 0.15f, Screen.height * 0.1f),
	        		   "Hungrier in " + sec + " !!!");

  }



  //-----------------------------------------------------
  // LevelUp()
  //
  // Increment level after curExp reaches maxExp in value
  // resets curExp to 1
  // Increase base stats on level up.
  //-----------------------------------------------------
  void LevelUp()
  {
    if (curExp >= maxExp)
    {
      curExp = 1.0f;
      if(curLevel < maxLevel)
      {
	curLevel++;
	strength += curLevel;
	health += 5.0f;
	curHealth = health;
	endurance += 2.0f;
	curEndurance = endurance;
      }
    }

    return;
  }


  //-----------------------------------------------------
  // EnergyRegen()
  //
  // curEndurance values are always greater than or equal
  // to 0 or less than or equal to endurance.
  //-----------------------------------------------------
  void EnergyRegen()
  {
    if (curEndurance > endurance)
    {
      curEndurance = endurance;
    }

      if (curEndurance < endurance)
      {
        curEndurance += regeneration * Time.deltaTime;
	if (curEndurance > endurance)
	{
          curEndurance = endurance;
        }
      }

    return;
  }


  //-----------------------------------------------------
  // Health()
  //
  // Checks if curHealth is within 0 and the max
  // health limit.
  //-----------------------------------------------------
  void Health()
  {
    if (curHealth > health)
    {
      curHealth = health;
    }
    if (curHealth < 0)
    {
      curHealth = 0.0f;
    }

    return;
  }

	
  //-----------------------------------------------------
  // HungerLevel()
  //
  // Adjusts hungerLevel based on time passed
  //-----------------------------------------------------
  void HungerLevel()
  {
    feedTime -= Time.deltaTime;
    if(feedTime < 0 && hungerLevel <= 2)
    {
      hungerLevel++;
      Debug.Log("You've gotten hungrier!");
      feedTime = time;
    }

    return;
  }




  //-----------------------------------------------------
  // HungerModifier()
  //
  // Adjusts stats in void Update() based on hungerLevel.
  //-----------------------------------------------------
  void HungerModifier()
  {
    if(curHungerLevel != hungerLevel)
    {

      if (hungerLevel == 2)
      {
	hungerMod = 2.0f;
	curHungerLevel = hungerLevel;

	health -= hungerMod;
	endurance -= hungerMod;				
	strength -= hungerMod;
	attRange -= hungerMod;
      }
      else if(hungerLevel == 3)
      {
	hungerMod = 3.0f;
	curHungerLevel = hungerLevel;

	health -= hungerMod;
	endurance -= hungerMod;		
	strength -= hungerMod;
	attRange -= hungerMod;
      }

    }

    return;
  }

  //-----------------------------------------------------
  // Eat()
  //
  // If the selected item in the player's inventory is
  // edible "eat" it and lower curHungerLevel by 1
  // Also resets the value of feedTime.
  //-----------------------------------------------------
  void Eat()
  {
    if(Input.GetKeyDown ("1") && 
       PlayerInventoryGUI.inventoryNameDictionary[0] != string.Empty)
    {

      if(PlayerInventoryGUI.inventoryNameDictionary[0] == "Picnic"
         || PlayerInventoryGUI.inventoryNameDictionary[0] == "Berry")
      {

	if(PlayerInventoryGUI.inventoryNameDictionary[0] == "Berry")
        {
          curHealth += hungerMod;
        }

        else
        {
          health += hungerMod;
          curHealth = health;
        }
      
        PlayerInventoryGUI.inventoryNameDictionary[0] = string.Empty;
        curExp++;
        if(hungerLevel > 1)
        {
          hungerLevel = curHungerLevel - 1;
	}
      
        feedTime = time;
        sec = 60;
      }

    }

    else if(Input.GetKeyDown ("2") &&
            PlayerInventoryGUI.inventoryNameDictionary[1] != string.Empty)
    {

      if(PlayerInventoryGUI.inventoryNameDictionary[1] == "Picnic" 
         || PlayerInventoryGUI.inventoryNameDictionary[1] == "Berry")
      {

        if(PlayerInventoryGUI.inventoryNameDictionary[1] == "Berry")
	{
          curHealth += hungerMod;
	}

        else
	{
          health += hungerMod;
          curHealth = health;
        }

        PlayerInventoryGUI.inventoryNameDictionary[1] = string.Empty;
	curExp++;

        if(hungerLevel > 1)
	{
          hungerLevel = curHungerLevel - 1;
	}

        feedTime = time;
        sec = 60;

      }

    }

    else if(Input.GetKeyDown ("3") && 
            PlayerInventoryGUI.inventoryNameDictionary[2] != string.Empty)
    {

      if(PlayerInventoryGUI.inventoryNameDictionary[2] == "Picnic" 
         || PlayerInventoryGUI.inventoryNameDictionary[2] == "Berry")
      {

        if(PlayerInventoryGUI.inventoryNameDictionary[2] == "Berry")
	{
          curHealth += hungerMod;
	}
	
        else
	{
          health += hungerMod;
          curHealth = health;
        }

        PlayerInventoryGUI.inventoryNameDictionary[2] = string.Empty;
        curExp++;
        if(hungerLevel > 1)
	{
          hungerLevel = curHungerLevel - 1;
	}

        feedTime = time;
        sec = 60;

      }
    }

  }
	
  void Stopwatch()
  { 
    sec--; if(sec < 0){ sec = 60;}
  }
	
}
