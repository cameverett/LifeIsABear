using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Attach to empty game object
public class LootItem : MonoBehaviour
{
  private Rect inventoryWindowRect;
  private bool inventoryWindowShow;
  private LootTable lootTable;

  //Changed to private
  private Dictionary<int, string> lootDictionary = new Dictionary<int, string>()
  {
    {0, string.Empty}
  };

  private Ray mouseRay;
  private RaycastHit rayHit;

  // Use this for initialization
  void Awake()
  {
    inventoryWindowRect = new Rect (300, 100, 175, 150);
    inventoryWindowShow = false;
  }
	
  void Update()
  {

    int layerMask = 1 << 9;
    layerMask = ~layerMask; // Ray touches everything but what is on layer 9.
    Transform cameraTransform = Camera.main.transform;
    mouseRay = new Ray (cameraTransform.position, cameraTransform.forward);
    RaycastHit hit;	

    if (Input.GetMouseButton(1) && 
        Physics.Raycast(mouseRay, out hit, Mathf.Infinity, layerMask))
    {
      lootTable = hit.collider.gameObject.GetComponent<LootTable>();
      lootDictionary = lootTable.lootDictionary;
      // Close player's inventory and other inventory windows.
      LootWindowControl();
      Destroy(hit.collider.gameObject);
    }

  }

  void OnGUI()
  {
    if (inventoryWindowShow)
    {
      inventoryWindowRect = GUI.Window(0, inventoryWindowRect,
                                       showInvSlots, "Press Q to Loot");
    }
  }

  void showInvSlots(int windowId)
  {
    // Inventory button layout
    GUILayout.BeginArea (new Rect(0, 50, 200, 100));
    GUILayout.BeginHorizontal ();
	
    // Iterates through inventory slots until it finds an empty one.
    // If the inventory is "full" then don't place it any slot.
    if(GUILayout.Button (lootDictionary[0], GUILayout.Height (50))
       || Input.GetKeyDown("q"))
    {

      if(lootDictionary[0] != string.Empty)
      {
        for(int i=0; i<3 && (lootDictionary[0] != string.Empty); i++)
	      {
          if(PlayerInventoryGUI.inventoryNameDictionary[i] == string.Empty)
          {
            PlayerInventoryGUI.inventoryNameDictionary[i] = lootDictionary[0];
            lootDictionary[0] = string.Empty;
          }
        }

        inventoryWindowShow = false;
      } 

      else
      {
        inventoryWindowShow = false;	
      }

    }
	
    GUILayout.EndHorizontal ();
    GUILayout.EndArea ();	
  }

  //------------------------------------------------------
  // void LootWindowControl()
  // 
  // Ensures GUI.Window instances are deleted when opening
  // new windows serving as the player's inventory menus.
  //------------------------------------------------------
  void LootWindowControl()
  {
    if(PlayerInventoryGUI.isOpen)
    {
      PlayerInventoryGUI.isOpen = false;
      // Make sure to close windows the next. 
      inventoryWindowShow = false;
      inventoryWindowShow = !inventoryWindowShow;
    }
    else
    {
      inventoryWindowShow = true;
    }
  }

}
