using UnityEngine;
using System.Collections;

public class CollisionBoulder : MonoBehaviour {
  public Vector3 decrementAmount;
  
  private void OnCollisionEnter(Collision collision){
    if(collision.gameObject.tag == "Fox"){
	  Vector3 s = collision.gameObject.transform.localScale;
	  s.y -= 0.75f;
	  collision.gameObject.transform.localScale = s;
	  Destroy (collision.gameObject, 2.0f);
	  Debug.Log ("Strike!!!");
	}
  }

  void Awake(){
	Screen.lockCursor = true;
	Screen.showCursor = false;
  }

}
