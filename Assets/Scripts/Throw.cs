using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour
{

  public Rigidbody Boulder;
  public Rigidbody Beehive;
  public Transform throwPos;
  public Transform turretPos;
  public float throwForce = 4000f;

  void Update ()
  {

    if(Input.GetKeyDown ("1") 
       && PlayerInventoryGUI.inventoryNameDictionary[0] != string.Empty)
    {
      ThrowItem(0, PlayerInventoryGUI.inventoryNameDictionary[0]);
    }

    if(Input.GetKeyDown ("2")
       && PlayerInventoryGUI.inventoryNameDictionary[1] != string.Empty)
    {
      ThrowItem (1, PlayerInventoryGUI.inventoryNameDictionary[1]);
    }

    if(Input.GetKeyDown ("3")
       && PlayerInventoryGUI.inventoryNameDictionary[2] != string.Empty)
      {
        ThrowItem (2, PlayerInventoryGUI.inventoryNameDictionary[2]);
      }

  }



  void ThrowItem(int slot, string item)
  {
    PlayerInventoryGUI.inventoryNameDictionary[slot] = string.Empty;
    if(item == "Beehive")
    {
      Rigidbody thrown = Instantiate (Beehive, turretPos.position,
                         turretPos.rotation) as Rigidbody;
      thrown.AddForce (turretPos.forward * throwForce);
      Destroy (thrown.gameObject, 4.0f);
    }
    if(item == "Boulder")
    {
      Rigidbody thrown = Instantiate (Boulder, turretPos.position,
                         turretPos.rotation) as Rigidbody;
      thrown.AddForce (turretPos.forward * throwForce);
      Destroy (thrown.gameObject, 10.0f);
    }
  }

}
