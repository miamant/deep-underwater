using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Triggers events during the scene accoring to player's position on the map.
/// If the player enters a trigger area, that area will signal that an event is to be started.
/// </summary>
public class TriggerEventMaster : MonoBehaviour
{
	public FirstCaveMap gameController;

	public Collider2D playerCol;
	public Collider2D triggerCol1;
	public Collider2D triggerCol2;
	public Collider2D triggerCol3;
	public Collider2D triggerCol4;

	public GameObject miniDialogue;
	public GameObject HUD;
	public GameObject healthBar;
	public GameObject shaderInner;
	public GameObject ingameMenu;
	public GameObject dialogueBar;
	public GameObject player;
	GameObject enemy;

	public Text monologue;
	public Text subtitles;

	public Image shaderOuter;

	public AudioMaster audioMaster;

	public string nextScene;

	bool event1 = true;
	bool event2 = true;
	bool event3 = true;

	bool lowerNoise = false;
	bool scaleCamera = false;
	bool notNeeded = false;

	void Start ()
	{
		enemy = GameObject.Find ("Enemy");
	}

	void Update ()
	{
		if (event1 && (triggerCol1.IsTouching (playerCol))) {
			StartCoroutine ("Event1");
			event1 = false;
		}

		if (!scaleCamera && !notNeeded) {
			if (triggerCol2.IsTouching (playerCol)) {
				if (Camera.main.orthographicSize < 13f) {
					Camera.main.orthographicSize += 2f * Time.deltaTime;
					if (event2 && (Camera.main.orthographicSize > 11f)) {
						StartCoroutine ("Event2");
						event2 = false;
					}
				}
			} else {
				if (Camera.main.orthographicSize > 5f)
					Camera.main.orthographicSize -= 2f * Time.deltaTime;
			}
		}

		if (event3 && (triggerCol3.IsTouching (playerCol))) {
			StartCoroutine ("Event3");
			lowerNoise = true;
			event3 = false;
		}

		if (lowerNoise && (GameObject.Find ("MovementSound")
			.GetComponent<AudioSource> ().volume > 0)) {
			GameObject.Find ("MovementSound")
				.GetComponent<AudioSource> ().volume -= .7f * Time.deltaTime;
		}

		if (scaleCamera && (Camera.main.orthographicSize < 8f)) {
			Camera.main.orthographicSize += 2f * Time.deltaTime;
		}

		if (gameController.canContinue && triggerCol4.IsTouching (playerCol)) {
			StartCoroutine ("Event4");
			gameController.canContinue = false;
		}
	}

	/// <summary>
	/// Plays out the first event.
	/// </summary>
	IEnumerator Event1 ()
	{
		string temp = "";

		miniDialogue.SetActive (true);

		monologue.text = "";
		temp = "Seems like some underwater cave system.";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			monologue.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 130; i++) {
			yield return null;
		}

		miniDialogue.SetActive (false);
	}

	/// <summary>
	/// Plays out the second event.
	/// </summary>
	IEnumerator Event2 ()
	{
		string temp = "";

		miniDialogue.SetActive (true);

		monologue.text = "";
		temp = "This place is huge!";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			monologue.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 110; i++) {
			yield return null;
		}

		miniDialogue.SetActive (false);
	}

	/// <summary>
	/// Plays out the third event.
	/// </summary>
	IEnumerator Event3 ()
	{
		string temp = "";

		miniDialogue.SetActive (true);
		HUD.SetActive (false);
		ingameMenu.SetActive (false);
		shaderInner.gameObject.SetActive (true);
		player.GetComponent<PlayerMovement> ().enabled = false;

		monologue.text = "";
		temp = "!";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			monologue.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		miniDialogue.SetActive (false);

		Camera.main.GetComponent<CameraFollow> ().smoothing = 2f;
		Camera.main.GetComponent<CameraFollow> ()
			.target = enemy.GetComponent<Transform> ();

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		subtitles.text = "";
		dialogueBar.SetActive (true);

		temp = "What the hell is that?";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 80; i++) {
			yield return null;
		}

		enemy.GetComponent<Transform> ()
			.localScale = new Vector3 (transform.localScale.x, -1f, transform.localScale.z);

		audioMaster.heartbeatSound.Play ();
		audioMaster.scaryTitusCalen.Play ();

		// Time drag
		for (int i = 0; i <= 70; i++) {
			yield return null;
		}

		subtitles.text = "";
		dialogueBar.SetActive (false);
		audioMaster.raptorCall.Play ();

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}

		lowerNoise = false;
		audioMaster.unseenHorrors.Play ();
		enemy.GetComponent<EnemyMovement> ().enabled = true;
		enemy.GetComponent<Transform> ()
			.localScale = new Vector3 (transform.localScale.x, 1f, transform.localScale.z);

		// Time drag
		for (int i = 0; i <= 50; i++) {
			yield return null;
		}

		scaleCamera = true;
		notNeeded = true;
		triggerCol2.gameObject.SetActive (false);

		HUD.SetActive (true);
		healthBar.SetActive (true);
		shaderInner.gameObject.SetActive (false);
		Camera.main.GetComponent<CameraFollow> ()
			.target = player.GetComponent<Transform> ();
		miniDialogue.transform.localPosition = new Vector3 (300f, 0, 0);

		// Time drag
		for (int i = 0; i <= 30; i++) {
			yield return null;
		}

		Camera.main.GetComponent<CameraFollow> ().smoothing = 5f;
		player.GetComponent<PlayerMovement> ().enabled = true;

		// Time drag
		for (int i = 0; i <= 50; i++) {
			yield return null;
		}

		miniDialogue.SetActive (true);

		monologue.text = "";
		temp = "Damn! What do I do?";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			monologue.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 120; i++) {
			yield return null;
		}

		monologue.text = "";
		temp = "The torpedos... I could use them!";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			monologue.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 120; i++) {
			yield return null;
		}

		audioMaster.hatchOpen.Play ();

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}

		player.GetComponent<PlayerShooting> ().enabled = true;
		player.GetComponent<PlayerShooting> ().LoadGun ();

		monologue.text = "";
		temp = "Ready to fire!";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			monologue.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		player.GetComponent<PlayerShooting> ().enabled = true;

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		monologue.text = "";
		temp = "I have to direct myself towards the target!";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			monologue.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 150; i++) {
			yield return null;
		}

		miniDialogue.SetActive (false);
		scaleCamera = false;
	}

	/// <summary>
	/// Plays out the fourth event.
	/// </summary>
	IEnumerator Event4 ()
	{
		string temp = "";

		lowerNoise = true;
		ingameMenu.SetActive (false);
		HUD.SetActive (false);
		shaderInner.gameObject.SetActive (true);
		player.GetComponent<PlayerMovement> ().enabled = false;

		// Fade in ShaderInner
		for (float f = 0; f <= 1f; f += .02f) {
			Color c = shaderInner.GetComponent<Image> ().color;
			c.a = f;
			shaderInner.GetComponent<Image> ().color = c;
			yield return null;
		}

		// Time drag
		for (int i = 0; i <= 40; i++) {
			yield return null;
		}

		dialogueBar.SetActive (true);
		subtitles.text = "";
		temp = "I wonder when this cave will end...";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			subtitles.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 130; i++) {
			yield return null;
		}

		shaderOuter.gameObject.SetActive (true);

		// Fade in ShaderOuter
		for (float f = 0; f <= 1f; f += .02f) {
			Color c = shaderOuter.color;
			c.a = f;
			shaderOuter.color = c;
			yield return null;
		}

		SceneManager.LoadScene (nextScene, LoadSceneMode.Single);
	}
}
