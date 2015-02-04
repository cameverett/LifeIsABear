using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Attach to loot prefabs
public class LootTable : MonoBehaviour
{
  public Dictionary<int, string> lootDictionary = new Dictionary<int, string>();

  void Start()
  {
    lootDictionary.Add(0, gameObject.tag);
  }
	
}
