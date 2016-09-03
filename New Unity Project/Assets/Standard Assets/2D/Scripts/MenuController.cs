using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets {
//	[RequireComponent(typeof (PlatformerCharacter2D))]
public class MenuController : MonoBehaviour {
	public GameObject playerControllerObject;
	private Platformer2DUserControl playerController;

	public Canvas MainMenuCanvas;
	public Text StarGameText;
	public Text InstructionsText;
	public Text CreditsText;
	public Text ExitGameText;

	public Canvas InstructionsCanvas;
	public Text InstructionsChildText;
	public Text CreditsChildText;

	void Start () {
		
		playerController = GetComponent<Platformer2DUserControl> ();

		MainMenuCanvas = MainMenuCanvas.GetComponent<Canvas> ();
		StarGameText = StarGameText.GetComponent<Text> ();
		InstructionsText = InstructionsText.GetComponent<Text> ();
		CreditsText = CreditsText.GetComponent<Text> ();
		ExitGameText = ExitGameText.GetComponent<Text> ();

		InstructionsCanvas = InstructionsCanvas.GetComponent<Canvas> ();
		InstructionsChildText = InstructionsChildText.GetComponent<Text> ();
		CreditsChildText = CreditsChildText.GetComponent<Text> ();
		
		MainMenuCanvas.enabled = true;
		InstructionsCanvas.enabled = false;
	}
	

	void Update () {
		if (Input.GetKey ("escape")) { // Go back to main menu
			MainMenuCanvas.enabled = true;
			InstructionsCanvas.enabled = false;
		}	// Start Game
			if (MainMenuCanvas.isActiveAndEnabled && playerController.GetJump()) {
			MainMenuCanvas.enabled = false;
			InstructionsCanvas.enabled = false;
		}   // Display Instructions
		if (MainMenuCanvas.isActiveAndEnabled && Input.GetKey (KeyCode.I)) {
			MainMenuCanvas.enabled = false;
			InstructionsCanvas.enabled = true;
			InstructionsText.enabled = true;
		}	// Display Credits
		if (MainMenuCanvas.isActiveAndEnabled && Input.GetKey (KeyCode.C)) {
			MainMenuCanvas.enabled = false;
			InstructionsCanvas.enabled = true;
			InstructionsChildText.enabled = false;
			CreditsChildText.enabled = true;
		}




	}
}
}
