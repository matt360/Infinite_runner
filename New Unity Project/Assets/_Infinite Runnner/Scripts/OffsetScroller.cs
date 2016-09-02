using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour {

	public float scrollSpeed;
	private Vector2 savedOffset;
	private Renderer rend;

	void Start () {
		rend = GetComponent<Renderer> ();
		//savedOffset = rend.sharedMaterial.GetTextureOffset ("_MainTex");
		savedOffset = rend.material.mainTextureOffset;
	}

	void Update () {
		float y = Time.time * scrollSpeed;
		//Vector2 offset = new Vector2 (savedOffset.x, y);
		rend.material.mainTextureOffset = new Vector2 (savedOffset.x, y);
	}

	void OnDisable() {
		//savedOffset = rend.material.mainTextureOffset;
		 rend.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}

/*using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour {

	public float scrollSpeed;
	private Vector2 savedOffset;
	public Renderer rend;

	void Start () {
		rend = GetComponent<Renderer> ();
		savedOffset = rend.sharedMaterial.GetTextureOffset ("_MainTex");
	}

	void Update () {
		//float y = Mathf.Repeat (Time.time * scrollSpeed, 1);
		float y = Time.time * scrollSpeed;
		Vector2 offset = new Vector2 (savedOffset.x, y);
		//rend.sharedMaterial.SetTextureOffset ("_MainTex", offset);
		rend.material.mainTextureOffset = offset;
	}

	void OnDisable () {
		rend.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}*/
