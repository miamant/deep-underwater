using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Acts as a general manager for the FirstCaveMap scene.
/// Plays out the intro to the scene,
/// manages what happens when player dies.
/// </summary>
public class FirstCaveMap : MonoBehaviour
{
	public Image shaderInner;
	public Image dialogueFaceShader;
	public Image shaderOuter;

	public AudioSource ambience;

	public GameObject HUD;
	public GameObject dialogueBar;
	public GameObject ingameMenu;
	public GameObject dialogueMiniBar;
	public GameObject player;
	public GameObject healthBar;
	public GameObject leftJoystick;
	public GameObject rightJoystick;
	public GameObject title;
	GameObject enemy;

	public Text subtitles;
	public Text miniText;

	public AudioMaster audioMaster;

	public bool canContinue;
	bool temp = true;
	bool scaleCamera = false;


	void Start ()
	{
		enemy = GameObject.Find ("Enemy");

		HUD.SetActive (false);
		ingameMenu.SetActive (false);
		dialogueBar.SetActive (false);
		dialogueMiniBar.SetActive (false);
		healthBar.SetActive (false);
		shaderOuter.gameObject.SetActive (false);
		title.SetActive (false);
		enemy.GetComponent<EnemyMovement> ().enabled = false;

		StartCoroutine ("Intro");
	}

	void Update ()
	{
		if (ambience.volume != 1f) {
			ambience.volume += .2f * Time.deltaTime;
		}

		if (audioMaster.submarineEngine.volume != 1f) {
			audioMaster.submarineEngine.volume += .2f * Time.deltaTime;
		}

		if (temp && (enemy == null)) {
			StartCoroutine ("PostFight");
			temp = false;
		}

		if ((enemy == null) && (audioMaster.unseenHorrors.volume > 0)) {
			audioMaster.unseenHorrors.volume -= .7f * Time.deltaTime;
		}

		if (scaleCamera && (Camera.main.orthographicSize > 7f)) {
			Camera.main.orthographicSize -= 2f * Time.deltaTime;
		}

		if (player == null) {
			StartCoroutine ("PlayerDies");
		}
	}

	/// <summary>
	/// Intro to the scene.
	/// </summary>
	IEnumerator Intro ()
	{
		string temp = "";

		// Time drag
		for (int i = 0; i <= 200; i++) {
			yield return null;
		}

		dialogueBar.SetActive (true);

		subtitles.text = "";
		temp = "Ugh...";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 110; i++) {
			yield return null;
		}
			
		subtitles.text = "";
		temp = "Can't see anything in here.";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 120; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "The light's must've gone off during the crash.";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "If I could get them back on...";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 60; i++) {
			yield return null;
		}

		audioMaster.light2.Play ();

		// Time drag
		for (int i = 0; i <= 80; i++) {
			yield return null;
		}

		audioMaster.light2.Play ();

		// Time drag
		for (int i = 0; i <= 80; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "...";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 50; i++) {
			yield return null;
		}

		audioMaster.light2.Play ();

		// Time drag
		for (int i = 0; i <= 50; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "This one?";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 80; i++) {
			yield return null;
		}

		audioMaster.light2.Play ();

		// Time drag
		for (int i = 0; i <= 20; i++) {
			yield return null;
		}

		audioMaster.flickeringLights.Play ();

		// Fade out Shader
		for (float f = 1f; f > 0f; f -= .008f) {
			Color c = shaderInner.color;
			c.a = f;
			shaderInner.color = c;
			Color c2 = dialogueFaceShader.color;
			c2.a = f;
			dialogueFaceShader.color = c2;

			if (f < 0.6f)
				subtitles.text = "";

			yield return null;
		}

		// Time drag
		for (int i = 0; i <= 30; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "Where the hell am I?";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "Where are the others?";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 130; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "They couldn't have died, could they?";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "...";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "I need to contact them.";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}

		audioMaster.policeScanner.Play ();

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}
			
		subtitles.text = "";
		temp = "<Crossdresser?>";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 60; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "<Are you there?>";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		audioMaster.policeScanner.Play ();

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "<J-Idol?>";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}
			
		subtitles.text = "";
		temp = "...";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "What the hell is going on?";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "What was that creature?";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 120; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "...";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		subtitles.text = "";
		temp = "I need to get out of here.";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 110; i++) {
			yield return null;
		}

		dialogueBar.SetActive (false);
		player.GetComponent<PlayerShooting> ().enabled = false;
		HUD.SetActive (true);
		ingameMenu.SetActive (true);
		shaderInner.gameObject.SetActive (false);
	}

	/// <summary>
	/// Plays out a cinematic scene after the player's fight with an enemy.
	/// </summary>
	/// <returns>The fight.</returns>
	IEnumerator PostFight ()
	{
		string temp = "";

		audioMaster.heartbeatSound.Stop ();

		// Time drag
		for (int i = 0; i <= 50; i++) {
			yield return null;
		}

		leftJoystick.SetActive (false);
		rightJoystick.SetActive (false);
		shaderInner.gameObject.SetActive (true);

		// Time drag
		for (int i = 0; i <= 40; i++) {
			yield return null;
		}

		scaleCamera = true;

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}

		dialogueBar.SetActive (true);

		subtitles.text = "";
		temp = "That was close...";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 120; i++) {
			yield return null;
		}

		if (player.GetComponent<PlayerHealth> ().currentHealth < 15) {
			subtitles.text = "";
			temp = "It damaged my ship. I need to be careful.";
			for (int i = 0; i < temp.Length; i++) {
				char[] charArr = temp.ToCharArray ();
				subtitles.text += charArr [i];
				yield return new WaitForSeconds (.05f);
			}

			// Time drag
			for (int i = 0; i <= 120; i++) {
				yield return null;
			}

		} else {
			
			subtitles.text = "";
			temp = "It could've damaged my ship. I need to be careful.";
			for (int i = 0; i < temp.Length; i++) {
				char[] charArr = temp.ToCharArray ();
				subtitles.text += charArr [i];
				yield return new WaitForSeconds (.05f);
			}

			// Time drag
			for (int i = 0; i <= 120; i++) {
				yield return null;
			}
		}

		dialogueBar.SetActive (false);
		leftJoystick.SetActive (true);
		rightJoystick.SetActive (true);
		ingameMenu.SetActive (true);
		shaderInner.gameObject.SetActive (false);

		canContinue = true;
	}

	/// <summary>
	/// Plays the scene again if the player dies.
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
		title.SetActive (true);

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

		SceneManager.LoadScene ("1stCaveMap", LoadSceneMode.Single);
	}
}
