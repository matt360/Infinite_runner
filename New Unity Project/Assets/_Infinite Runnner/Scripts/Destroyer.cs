using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Player") {
			//Debug.Break ();
			Application.LoadLevel(1); //You can pass a name of the scene in quotes but it's fater to pass a number (yay!)
			return;
		}

  		if (other.gameObject.transform.parent) {
			//Find the game object, look at its transform, see what its parent is and grab that game object.
			Destroy (other.gameObject.transform.parent.gameObject);
		} else { //If it doesn't have a parent destroy the game object
			Destroy (other.gameObject);
		}
//	 	if ( (other.gameObject.transform.parent) ? (Destroy (other.gameObject.transform.parent.gameObject)) : (Destroy (other.gameObject)) );
	}
}
