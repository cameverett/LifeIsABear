using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Attach to Player in Unity
public class PlayerInventoryGUI : MonoBehaviour {
  static public bool isOpen;  // Player inventory is open on screen
  public bool isCarrying;     // TODO: Prep for item equips 
  private Rect inventoryWindowRect;

  // The player's inventory starts empty and can hold up to three items. 
  static public Dictionary<int, string> 
    inventoryNameDictionary = new Dictionary<int, string> ()
    {	
      {0, string.Empty},
      {1, string.Empty},
      {2, string.Empty}
    };

  // Use this for initialization
  void Awake () {
    isOpen = false;
    inventoryWindowRect = new Rect(Screen.width * 0.6f, 
                                   Screen.height * 0.45f, 110, 200);
    //isCarrying = false;
  }

  // Update is called once per frame
  void Update ()
  {
    InvToggle();
  }

  void OnGUI()
  {
    // When inventory isOpen call showInvSlots()
    if (isOpen)
      inventoryWindowRect = GUI.Window (0, inventoryWindowRect,
                                        showInvSlots, "Inventory");	
    
    // Targeting reticule for selecting items/targets 
    GUI.Box (new Rect(Screen.width/2,Screen.height/2, 5, 5), "");
  }

  //-----------------------------------------------------
  // showInvSlots
  //
  // Displays the players inventory on to screen.
  //-----------------------------------------------------
  void showInvSlots (int windowId)
  {

    GUILayout.BeginArea (new Rect(5, 25, 95, 160));

    GUILayout.BeginHorizontal ();
    GUILayout.Button ("1: " + inventoryNameDictionary[0],GUILayout.Height (50));
    GUILayout.EndHorizontal ();

    GUILayout.BeginHorizontal ();
    GUILayout.Button ("2: " + inventoryNameDictionary[1],GUILayout.Height (50));
    GUILayout.EndHorizontal ();

    GUILayout.BeginHorizontal ();
    GUILayout.Button ("3: " + inventoryNameDictionary[2],GUILayout.Height (50));
    GUILayout.EndHorizontal ();

    GUILayout.EndArea ();

  }

  //-----------------------------------------------------
  // InvToggle()
  //
  // Show inventory on keypress.
  //-----------------------------------------------------
  void InvToggle()
  {
    if(Input.GetKeyDown("i") && isOpen == false)
      isOpen = true;

    else if(Input.GetKeyDown("i") && isOpen != false)
      isOpen = false;
  }

}
