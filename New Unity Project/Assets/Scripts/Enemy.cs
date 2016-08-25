using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private BoxCollider2D collider;
	public float lifetime = 3f;

	void Start() {
		Destroy (this.gameObject, lifetime);
	}

	private void Awake() {
		collider = GetComponent<BoxCollider2D> ();
	}

	private void FixedUpdate() {
		bool dodge = Input.GetKey (KeyCode.LeftAlt); 
		SetCollider (dodge);
	}

	private void SetCollider(bool dodge) {
		if (dodge) {
			collider.enabled = false;
		} else {
			collider.enabled = true;
		}
	}
}
