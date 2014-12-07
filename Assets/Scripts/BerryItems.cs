using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// attach to berry prefab
public class BerryItems : MonoBehaviour {

	public Dictionary<int, string> lootDictionary = new Dictionary<int, string>()
	{
		{0, string.Empty}
	};
	
	// Use this for initialization
	void Start () {		
		lootDictionary [0] = "Berries";
	}
	
}
