using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Acts as a general manager for the 2ndCaveMap scene.
/// Loads the scene again if the player dies.
/// </summary>
public class SecondCaveMap : MonoBehaviour
{
	// Player
	public GameObject player;

	// Audio
	public AudioSource bgMusic;
	public AudioSource pressureSound;
	public AudioSource engineSound;

	// UI
	public Image shaderOuter;
	public Image shaderInner;
	public Text title;
	public Text monologue;
	public Text minilogue;

	// Dialogue
	public GameObject textBar;
	public GameObject miniTextBar;

	// Joysticks & healtbar
	public GameObject HUD;

	// In-game menu
	public GameObject ingameMenu;

	bool playerLostHealth = true;


	void Start ()
	{
		title.gameObject.SetActive (false);
		textBar.SetActive (false);

		miniTextBar.transform.localPosition = new Vector3 (300f, 0, 0);
		miniTextBar.SetActive (false);

		StartCoroutine ("Intro");
	}

	void Update ()
	{
		if (player == null) {
			StartCoroutine ("PlayerDies");
		}

		if (bgMusic.volume != 1f) {
			bgMusic.volume += .2f * Time.deltaTime;
			pressureSound.volume += .2f * Time.deltaTime;
			engineSound.volume += .2f * Time.deltaTime;
		}

		if (playerLostHealth && GameObject.Find ("Player")
			.GetComponent<PlayerHealth> ().currentHealth < 15) {
			StartCoroutine ("Warning");
			playerLostHealth = false;
		}
	}

	/// <summary>
	/// Fades from black to the scene.
	/// </summary>
	IEnumerator Intro ()
	{
		// Fade out ShaderOuter
		for (float f = 1f; f > 0f; f -= .008f) {
			Color c = shaderOuter.color;
			c.a = f;
			shaderOuter.color = c;
			yield return null;
		}

		shaderOuter.gameObject.SetActive (false);
	}

	/// <summary>
	/// Protagonist gives a warning to the player after losing health.
	/// </summary>
	IEnumerator Warning ()
	{
		string temp = "";

		miniTextBar.SetActive (true);

		minilogue.text = "";
		temp = "Oh damn!";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			minilogue.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 70; i++) {
			yield return null;
		}

		minilogue.text = "";
		temp = "Those bubbles actually damaged my ship.";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			minilogue.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 120; i++) {
			yield return null;
		}

		miniTextBar.SetActive (false);
	}

	/// <summary>
	/// Loads the scene again when player dies.
	/// </summary>
	/// <returns>The dies.</returns>
	IEnumerator PlayerDies ()
	{
		ingameMenu.SetActive (false);
		HUD.SetActive (false);
		shaderInner.gameObject.SetActive (true);

		// Fade in ShaderInner
		for (float f = 0; f <= 1f; f += .02f) {
			Color c = shaderInner.GetComponent<Image> ().color;
			c.a = f;
			shaderInner.GetComponent<Image> ().color = c;
			yield return null;
		}

		shaderOuter.gameObject.SetActive (true);
		title.gameObject.SetActive (true);

		// Fade out ShaderOuter
		for (float f = 1f; f >= 0; f -= .01f) {
			Color c = shaderOuter.color;
			c.a = f;
			shaderOuter.color = c;
			yield return null;
		}

		// Time drag
		for (int i = 0; i <= 160; i++) {
			yield return null;
		}

		// Fade in ShaderOuter
		for (float f = 0; f <= 1f; f += .02f) {
			Color c = shaderOuter.GetComponent<Image> ().color;
			c.a = f;
			shaderOuter.GetComponent<Image> ().color = c;
			yield return null;
		}

		SceneManager.LoadScene ("2ndCaveMap", LoadSceneMode.Single);
	}
}
