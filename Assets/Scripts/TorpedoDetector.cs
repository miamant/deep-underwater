using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Torpedo detector. Its bool value will decide when the player's gun is loaded.
/// If an object tagged as Torpedo exits the Trigger area (spawnpoint) the bool triggers
/// a counter that times when the gun is loaded.
/// </summary>
public class TorpedoDetector : MonoBehaviour
{
	public bool torpedoDetected;

	void Update ()
	{

	}

	/// <summary>
	/// Bool is set to false when torpedo is detected.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag.Equals ("Torpedo"))
			torpedoDetected = false;
	}

	/// <summary>
	/// Bool is set to true when torpedo is detected.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag.Equals ("Torpedo"))
			torpedoDetected = true;
	}
}
