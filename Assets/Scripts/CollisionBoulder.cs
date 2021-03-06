﻿using UnityEngine;
using System.Collections;

// Attach to physics.Boulder rigidbody in Unity
public class CollisionBoulder : MonoBehaviour
{
  private void OnCollisionEnter(Collision collision)
  {
    if(collision.gameObject.tag == "Fox")
    {
      Vector3 s = collision.gameObject.transform.localScale;
      s.y *= 0.1f;
      collision.gameObject.transform.localScale = s;
      Destroy (collision.gameObject, 3.0f);
    }
  }

  void Awake()
  {
    Screen.lockCursor = true;
    Screen.showCursor = false;
  }

}
