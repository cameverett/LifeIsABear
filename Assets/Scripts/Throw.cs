using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour {

  public Rigidbody ThrowBoulder;
  public Rigidbody ThrowBeehive;
  public Transform throwPos;
  public Transform turretPos;
  public float throwForce = 4000f;

  void Update () {
    if(Input.GetKeyDown ("1") &&
       PlayerInventoryGUI.inventoryNameDictionary[0] != string.Empty)
    {

      if(PlayerInventoryGUI.inventoryNameDictionary[0] == "Beehive")
      {
        PlayerInventoryGUI.inventoryNameDictionary[0] = string.Empty;
        Rigidbody thrown = Instantiate (ThrowBeehive, turretPos.position,
                           turretPos.rotation) as Rigidbody;
        thrown.AddForce (turretPos.forward * throwForce);
        Destroy (thrown.gameObject, 4.0f);
      }

      else if(PlayerInventoryGUI.inventoryNameDictionary[0] == "Boulder")
      {
        PlayerInventoryGUI.inventoryNameDictionary[0] = string.Empty;
        Rigidbody toss = Instantiate (ThrowBoulder, throwPos.position,
                         throwPos.rotation) as Rigidbody;
        toss.AddForce (throwPos.forward * throwForce);
        Destroy (toss.gameObject, 10.0f);
      }

    }

    else if(Input.GetKeyDown ("2") &&
            PlayerInventoryGUI.inventoryNameDictionary[1] != string.Empty)
    {

      if(PlayerInventoryGUI.inventoryNameDictionary[1] == "Beehive")
      {
        PlayerInventoryGUI.inventoryNameDictionary[1] = string.Empty;
        Rigidbody thrown = Instantiate (ThrowBeehive, turretPos.position,
                           turretPos.rotation) as Rigidbody;
        thrown.AddForce (turretPos.forward * throwForce);
        Destroy (thrown.gameObject, 4.0f);
      }

      else if(PlayerInventoryGUI.inventoryNameDictionary[1] == "Boulder")
      {
        PlayerInventoryGUI.inventoryNameDictionary[1] = string.Empty;
        Rigidbody toss = Instantiate (ThrowBoulder, throwPos.position,
                         throwPos.rotation) as Rigidbody;
        toss.AddForce (throwPos.forward * throwForce);
        Destroy (toss.gameObject, 10.0f);
      }

    }

      else if(Input.GetKeyDown ("3") &&
              PlayerInventoryGUI.inventoryNameDictionary[2] != string.Empty)
      {

        if(PlayerInventoryGUI.inventoryNameDictionary[2] == "Beehive")
	{
          PlayerInventoryGUI.inventoryNameDictionary[2] = string.Empty;
          Rigidbody thrown = Instantiate (ThrowBeehive, turretPos.position,
                             turretPos.rotation) as Rigidbody;
          thrown.AddForce (turretPos.forward * throwForce);
          Destroy (thrown.gameObject, 4.0f);
        }

        else if(PlayerInventoryGUI.inventoryNameDictionary[2] == "Boulder")
	{
          PlayerInventoryGUI.inventoryNameDictionary[2] = string.Empty;
          Rigidbody toss = Instantiate (ThrowBoulder, throwPos.position,
                           throwPos.rotation) as Rigidbody;
          toss.AddForce (throwPos.forward * throwForce);
          Destroy (toss.gameObject, 10.0f);
        }

      }
  }

}
