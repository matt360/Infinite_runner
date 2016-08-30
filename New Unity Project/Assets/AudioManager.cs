using UnityEngine;
using System.Collections;


public class AudioManager : MonoBehaviour {

	public AudioClip jumpSound;
	public AudioClip deadSound;
	public AudioClip slideSound;
	private AudioSource audioSource;

	void Awake () {
		audioSource = GetComponent<AudioSource> ();
	}

	public AudioSource GetAudioSource() {
		return audioSource;
	}
}
