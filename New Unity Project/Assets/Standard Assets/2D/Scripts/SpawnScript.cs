using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets {

	public class SpawnScript : MonoBehaviour {
		public Rigidbody2D player;
		public Canvas MainMenu ;
		float playerScore = 0f;

		// colection of the objects to spawn
		public GameObject[] obj;
		public float spawnMin = 1f;
		// [Range(1, 3)][SerializeField] private float spawnMin = 1f;
		public float spawnMax = 2f;

		// Use this for initialization
		void Start () {
			Spawn ();
		}

		void Spawn() {
			Instantiate (obj [Random.Range (0, obj.Length)], transform.position, Quaternion.identity);
				Invoke ("Spawn", Random.Range (spawnMin, spawnMax)); // Invoke the function Spawn() by itself (Spawn() recursion);
		}
	}
}
