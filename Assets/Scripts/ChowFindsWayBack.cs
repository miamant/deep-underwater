using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the way the ChowFindsWayBack scene plays out.
/// Times changes for the subtitles, images etc.
/// </summary>
public class ChowFindsWayBack : MonoBehaviour
{
	// UI elements
	public Image shader;
	public Image blackboard;
	public Image whiteBurst;
	public Image whiteboard;
	public Text title;
	public Text subtitles;
	public Text monologueText;
	public GameObject dialogueBar;
	public GameObject monologueBar;

	// Average Chow
	public GameObject chow;
	public GameObject chowWrapper;

	// Ice cracks
	public GameObject crack1;
	public GameObject crack2;
	public GameObject crack3;
	public GameObject crack4;

	// Audio
	public AudioSource windyTransition;
	public AudioSource bgMusic;
	public AudioSource seagullSound;

	// Scene
	public string nextScene;
	public string nextNextScene;

	// Canvas
	public Canvas canvas;

	bool scale = false;
	bool seagulls = false;

	void Start ()
	{
		crack1.SetActive (false);
		crack2.SetActive (false);
		crack3.SetActive (false);
		crack4.SetActive (false);
		dialogueBar.SetActive (false);
		monologueBar.SetActive (false);
		whiteboard.gameObject.SetActive (false);

		StartCoroutine ("Chow");
	}

	void Update ()
	{
		if (bgMusic.volume < 1f)
			bgMusic.volume += 0.5f * Time.deltaTime;

		if (scale && whiteBurst != null)
			whiteBurst.transform.localScale += new Vector3 (0.06f, 0.06f, 0.06f);	

		if (seagulls && seagullSound.volume < 1f)
			seagullSound.volume += 0.5f * Time.deltaTime;
	}

	/// <summary>
	/// Manages how the whole scene plays out.
	/// </summary>
	IEnumerator Chow ()
	{
		title.text = "ONE WEEK LATER";

		// Fade out Shader
		for (float f = 1f; f >= 0; f -= .01f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		// Time drag
		for (int i = 0; i <= 120; i++) {
			yield return null;
		}

		// Fade in Shader
		for (float f = 0; f <= 1.0f; f += 0.02f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		// Time drag
		for (int i = 0; i <= 60; i++) {
			yield return null;
		}

		title.gameObject.SetActive (false);
		dialogueBar.SetActive (true);
		subtitles.gameObject.SetActive (true);

		// Time drag
		for (int i = 0; i <= 160; i++) {
			yield return null;
		}

		dialogueBar.SetActive (false);

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		blackboard.gameObject.SetActive (false);

		// Fade out Shader
		for (float f = 1f; f >= 0; f -= .01f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		string temp = "";

		monologueBar.SetActive (true);

		monologueText.text = "";
		temp = "Is that light I see?";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			monologueText.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}

		// Time drag
		for (int i = 0; i <= 120; i++) {
			yield return null;
		}

		monologueBar.SetActive (false);
		chow.SetActive (false);

		crack1.SetActive (true);

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}

		crack2.SetActive (true);

		// Time drag
		for (int i = 0; i <= 30; i++) {
			yield return null;
		}

		crack3.SetActive (true);

		// Time drag
		for (int i = 0; i <= 60; i++) {
			yield return null;
		}

		crack4.SetActive (true);

		// Time drag
		for (int i = 0; i <= 30; i++) {
			yield return null;
		}

		scale = true;

		windyTransition.Play ();

		// Time drag
		for (int i = 0; i <= 120; i++) {
			yield return null;
		}

		SceneManager.LoadScene (nextScene, LoadSceneMode.Additive);

		// Time drag
		for (int i = 0; i <= 40; i++) {
			yield return null;
		}

		whiteboard.gameObject.SetActive (true);
		whiteBurst.gameObject.SetActive (false);
		chowWrapper.SetActive (false);

		seagulls = true;
		seagullSound.Play ();

		// Fade out Whiteboard
		for (float f = 1.0f; f >= 0; f -= 0.01f) {
			Color c = whiteboard.color;
			c.a = f;
			whiteboard.color = c;
			yield return null;
		}
	}
}
