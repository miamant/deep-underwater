using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Acts as a store for audio files.
/// </summary>
public class AudioMaster : MonoBehaviour
{
	// Lists
	public List<AudioSource> keyboardSounds;

	// Music
	public AudioSource anxiety;
	public AudioSource unseenHorrors;

	// Ambience
	public AudioSource submarineEngine;
	public AudioSource underwater;
	public AudioSource underwaterBubbles;
	public AudioSource horrorLand;
	public AudioSource scaryTitusCalen;
	public AudioSource horrorAmbiance;

	// Sound effects
	public AudioSource policeScanner;
	public AudioSource light2;
	public AudioSource flickeringLights;
	public AudioSource torpedoSound;
	public AudioSource torpedoExplosion;
	public AudioSource heartbeatSound;
	public AudioSource oneBeat;
	public AudioSource raptorCall;
	public AudioSource hatchOpen;

	void Start ()
	{
		keyboardSounds.Add (GameObject.Find ("key1").GetComponent<AudioSource> ());
		keyboardSounds.Add (GameObject.Find ("key2").GetComponent<AudioSource> ());
		keyboardSounds.Add (GameObject.Find ("key3").GetComponent<AudioSource> ());
		keyboardSounds.Add (GameObject.Find ("key4").GetComponent<AudioSource> ());
	}
}
