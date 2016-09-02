using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {

	public float scrollSpeed = 0;
//	private Vector2 savedOffset;
	private Renderer rend;

	void Start () {
		rend = GetComponent<Renderer> ();
	}

	void Update () {
		float offset = Time.time * scrollSpeed;
		rend.material.mainTextureOffset = new Vector2 (offset, 0);
	}
}
