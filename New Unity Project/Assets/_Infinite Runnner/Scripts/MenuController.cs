using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets {
public class MenuController : MonoBehaviour {

	public Canvas MainMenuCanvas;
	public Text StarGameText;
	public Text InstructionsText;
	public Text CreditsText;
	public Text ExitGameText;

	public Canvas InstructionsCanvas;
	public Text InstructionsChildText;
	public Text CreditsChildText;

	void Awake () {
		MainMenuCanvas = GetComponent<Canvas> ();
		StarGameText = GetComponent<Text> ();
		InstructionsText = GetComponent<Text> ();
		CreditsText = GetComponent<Text> ();
		ExitGameText = GetComponent<Text> ();

		InstructionsCanvas = GetComponent<Canvas> ();
		InstructionsChildText = GetComponent<Text> ();
		CreditsChildText = GetComponent<Text> ();
	}
	

	void Update () {
		if (Input.GetKey ("escape")) {
			MainMenuCanvas.enabled = true;
			InstructionsCanvas.enabled = false;
		}
		if (MainMenuCanvas.isActiveAndEnabled && Input.GetKey("Jump")) {
			MainMenuCanvas.enabled = false;
			InstructionsCanvas.enabled = false;
		}
		if (MainMenuCanvas.isActiveAndEnabled && Input.GetKey (KeyCode.I)) {
			MainMenuCanvas.enabled = false;
			InstructionsCanvas.enabled = true;
			InstructionsText.enabled = true;
		}
		if (MainMenuCanvas.isActiveAndEnabled && Input.GetKey (KeyCode.C)) {
			MainMenuCanvas.enabled = false;
			InstructionsCanvas.enabled = true;
			InstructionsChildText.enabled = false;
			CreditsChildText.enabled = true;
		}





	}
}
}
