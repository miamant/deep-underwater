using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Triggers events during the scene accoring to player's position on the map.
/// If the player enters a trigger area, that area will signal that an event is to be started.
/// </summary>
public class TriggerEventMasterSecond : MonoBehaviour
{
	public string nextScene;

	public Collider2D playerCol;
	public Collider2D triggerColEnd;
	public Collider2D triggerCol1;

	public GameObject HUD;
	public GameObject ingameMenu;

	// UI
	public Image shaderOuter;
	public Image shaderInner;

	// Dialogue
	public GameObject textBar;
	public Text textBarSentence;
	public Text miniTextBarSentence;
	public GameObject miniTextBar;

	bool temp = true;
	bool temp2 = true;
	bool lowerNoise = true;

	void Start ()
	{
		shaderInner.gameObject.SetActive (false);
	}

	void Update ()
	{
		if (temp2 && triggerColEnd.IsTouching (playerCol)) {
			StartCoroutine ("End");
			temp2 = false;
		}

		if (temp && triggerCol1.IsTouching (playerCol)) {
			StartCoroutine ("Thoughts");
			temp = false;
		}

		if (GameObject.Find ("Player") != null) {
			if (lowerNoise && (GameObject.Find ("MovementSound")
			.GetComponent<AudioSource> ().volume > 0)) {
				GameObject.Find ("MovementSound")
				.GetComponent<AudioSource> ().volume -= .7f * Time.deltaTime;
			}
		}
	}

	/// <summary>
	/// Plays out an event where the main characters thinks to himself.
	/// </summary>
	IEnumerator Thoughts ()
	{
		string temp = "";
		miniTextBarSentence.text = "";
		miniTextBar.SetActive (true);
		temp = "Will I ever see light again?";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			miniTextBarSentence.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 150; i++) {
			yield return null;
		}

		miniTextBar.SetActive (false);
	}

	/// <summary>
	/// Fades the scene to black.
	/// </summary>
	IEnumerator End ()
	{
		ingameMenu.SetActive (false);
		shaderInner.gameObject.SetActive (true);
		HUD.SetActive (false);

		lowerNoise = true;

		// Fade in ShaderInner
		for (float f = 0; f <= 1f; f += .02f) {
			Color c = shaderInner.color;
			c.a = f;
			shaderInner.color = c;
			yield return null;
		}

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		string temp = "";
		textBarSentence.text = "";
		textBar.SetActive (true);
		temp = "Does this cave have an end?";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			textBarSentence.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 120; i++) {
			yield return null;
		}

		textBar.SetActive (false);

		SceneManager.LoadScene (nextScene, LoadSceneMode.Single);
	}
}
