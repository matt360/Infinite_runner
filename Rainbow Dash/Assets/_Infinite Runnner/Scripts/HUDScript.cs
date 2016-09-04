using UnityEngine;
using UnityEngine.UI;
using System.Collections;


	public class HUDScript : MonoBehaviour {

		public Rigidbody2D player;
		float playerScore = 0f;

		public Text scoreText;
		
		void Update () {
		if (player.velocity.x != 0) {
				playerScore += Time.deltaTime;
				scoreText.text = "Score: " + ((int)(playerScore * 100)).ToString ();
			}
		}

		public void ChangeScore(int amount) { // to have bad blocks just pass a negative ammount
			playerScore += amount;
		}

		void OnDisable() {
			PlayerPrefs.SetInt ("Score", (int)(playerScore * 100) );
		}

	//	void OnGUI () {//create a rectangle to display a score, times 100 to not have decimals and cast to an integer
	//		GUI.Label (new Rect (10, 10, 100, 30), "Score: " + (int)(playerScore * 100));
	//	}
	}

