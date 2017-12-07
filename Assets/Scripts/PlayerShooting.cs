using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages player's shooting.
/// Loads gun, launches a torpedo if the right joystick is held down long enough,
/// loads the gun after a certain time etc.
/// </summary>
public class PlayerShooting : MonoBehaviour
{
	public RightJoystick rightJoystick;
	public Transform torpedoSpawn;
	public GameObject torpedoPrefab;
	public GameObject playerGun;

	AudioSource gunLoadSound;

	int torpedoCountdown = 0;

	void Start ()
	{
		gunLoadSound = transform.GetChild (3).GetComponent<AudioSource> ();

		if (GameObject.Find ("GameController") == null)
			LoadGun ();
	}

	void Update ()
	{
		if (torpedoSpawn.GetComponent<TorpedoDetector> ().torpedoDetected) {
			torpedoCountdown++;
			if (torpedoCountdown == 80) {
				LoadGun ();
			}
		} else {
			torpedoCountdown = 0;
		}
	}

	/// <summary>
	/// Loads the gun.
	/// </summary>
	public void LoadGun ()
	{
		gunLoadSound.Play ();

		// Create Torpedo from the Torpedo Prefab
		var torpedo = (GameObject)Instantiate (
			              torpedoPrefab,
			              torpedoSpawn.transform.position,
			              torpedoSpawn.transform.rotation);
	}
}
