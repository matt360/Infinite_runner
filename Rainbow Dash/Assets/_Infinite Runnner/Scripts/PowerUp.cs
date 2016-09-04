using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	
	HUDScript hud;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			hud = GameObject.Find("Main Camera").GetComponent<HUDScript>();
			hud.ChangeScore(10);
			Destroy (this.gameObject);
		}
	}
}
