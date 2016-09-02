using UnityEngine;
using UnityEngine.UI;
using System.Collections;


	public class HUDScript : MonoBehaviour {

		float playerScore = 0f;
		[SerializeField] private Text scoreText;
		private PlatformerCharacter2D m_Character;

		void Update () {
			m_Character = GetComponent<PlatformerCharacter2D> ();
			playerScore += Time.deltaTime;
			scoreText.text = "Score: " + ((int)(playerScore * 100)).ToString();
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

