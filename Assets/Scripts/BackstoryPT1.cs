using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the way the BackstoryPT1 scene plays out.
/// Times changes in the cinematic scene, e.g. when subtitles appear,
/// which images the camera focuses on etc.
/// </summary>
public class BackstoryPT1 : MonoBehaviour
{
	// UI elements
	Image blackboard;
	Image shader;
	Image bottomPanel;
	Text title;
	Text crossdresser;
	Text japaneseIdol;
	Text averageCho;
	Text subtitles;
	Text karabaanarit;

	// Sprites
	GameObject ryokanRiver;
	GameObject lightbulb;
	GameObject speechBubble;
	GameObject submarine;
	GameObject approval1;
	GameObject approval2;
	GameObject approval3;

	// Sounds
	AudioSource cicadaSound;
	AudioSource punchSound;
	AudioSource slapSound;
	AudioSource bgMusic;
	AudioSource notif2;
	AudioSource notif3;

	// Timer
	float timer = 0.0f;
	public int seconds = 0;

	// Main camera
	Vector3 origPos;
	float origSize;

	// Bools that
	bool slideImageIn = false;
	bool fadeToBlack = false;
	bool saveCameraPosition = true;

	void Start ()
	{
		blackboard = GameObject.Find ("Blackboard").GetComponent<Image> ();
		shader = GameObject.Find ("Shader").GetComponent<Image> ();
		bottomPanel = GameObject.Find ("BottomPanel").GetComponent<Image> ();
		title = GameObject.Find ("Title").GetComponent<Text> ();
		crossdresser = GameObject.Find ("Crossdresser").GetComponent<Text> ();
		japaneseIdol = GameObject.Find ("JapaneseIdol").GetComponent<Text> ();
		averageCho = GameObject.Find ("AverageCho").GetComponent<Text> ();
		subtitles = GameObject.Find ("Subtitles").GetComponent<Text> ();
		karabaanarit = GameObject.Find ("Karabaanarit").GetComponent<Text> ();

		ryokanRiver = GameObject.Find ("RyokanRiver");
		lightbulb = GameObject.Find ("Lightbulb");
		speechBubble = GameObject.Find ("SpeechBubble");
		submarine = GameObject.Find ("Submarine");
		approval1 = GameObject.Find ("Approval1");
		approval2 = GameObject.Find ("Approval2");
		approval3 = GameObject.Find ("Approval3");

		cicadaSound = GameObject.Find ("CicadaSound").GetComponent<AudioSource> ();
		cicadaSound.volume = 0;
		cicadaSound.Play ();
		punchSound = GameObject.Find ("PunchSound").GetComponent<AudioSource> ();
		slapSound = GameObject.Find ("SlapSound").GetComponent<AudioSource> ();
		bgMusic = GameObject.Find ("BGMusic").GetComponent<AudioSource> ();
		bgMusic.volume = 0;
		bgMusic.Play ();
		notif2 = GameObject.Find ("NotificationSound2").GetComponent<AudioSource> ();
		notif3 = GameObject.Find ("NotificationSound3").GetComponent<AudioSource> ();

		bottomPanel.gameObject.SetActive (false);
		crossdresser.gameObject.SetActive (false);
		japaneseIdol.gameObject.SetActive (false);
		averageCho.gameObject.SetActive (false);
		karabaanarit.gameObject.SetActive (false);

		lightbulb.SetActive (false);
		speechBubble.SetActive (false);
		submarine.SetActive (false);
		approval1.SetActive (false);
		approval2.SetActive (false);
		approval3.SetActive (false);

		// Starts the intro to the cinematic scene
		StartCoroutine ("Intro");
	}

	void Update ()
	{
		timer += Time.deltaTime;
		seconds = Convert.ToInt32 (timer % 60);

		// For 4 seconds after the scene starts, sounds will gradually become louder
		if (seconds < 4) {
			FadeInAudio (cicadaSound);
			FadeInAudio (bgMusic);
		}

		// Main camera slides from up to down to reveal the setting of the scene
		if (slideImageIn && Camera.main.transform.position.y > -6f) {
			Camera.main.transform.position -= new Vector3 (0, .04f, 0);
		} else {
			slideImageIn = false;
		}

		// Current position and size of the main camera are saved after the camera has slid in
		if (saveCameraPosition && Camera.main.transform.position.y <= -6f) {
			origPos = Camera.main.transform.position;
			origSize = Camera.main.orthographicSize;
			StartCoroutine ("ShowText");
			saveCameraPosition = false;
		}

		// Scene fades to black before loading the next scene
		if (fadeToBlack) {
			FadeOutAudio (cicadaSound);
			FadeOutAudio (bgMusic);
		}
	}

	/// <summary>
	/// Intro to the scene.
	/// </summary>
	IEnumerator Intro ()
	{
		// Fade out Blackboard
		for (float f = 1.0f; f >= 0; f -= 0.01f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		// Time drag
		for (int i = 0; i <= 28; i++) {
			yield return null;
		}

		// Fade in Shader
		for (float f = 0; f <= 1.0f; f += 0.02f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		title.gameObject.SetActive (false);
		blackboard.gameObject.SetActive (false);

		slideImageIn = true;

		// Fade out Shader
		for (float f = 1.0f; f >= 0; f -= 0.01f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}
	}

	/// <summary>
	/// Starts part of the scene where subtitles are displayed.
	/// </summary>
	/// <returns>null</returns>
	IEnumerator ShowText ()
	{
		// Time drag
		for (int i = 0; i <= 70; i++) {
			yield return null;
		}

		bottomPanel.gameObject.SetActive (true);
		subtitles.text = "Our story begins with three scientists.";

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		subtitles.text = "";
		bottomPanel.gameObject.SetActive (false);

		// Time drag
		for (int i = 0; i <= 40; i++) {
			yield return null;
		}
			
		SeeIdol ();
		punchSound.Play ();
		japaneseIdol.gameObject.SetActive (true);


		// Time drag
		for (int i = 0; i <= 60; i++) {
			yield return null;
		}

		japaneseIdol.gameObject.SetActive (false);
		SeeDresser ();
		punchSound.Play ();
		crossdresser.gameObject.SetActive (true);

		// Time drag
		for (int i = 0; i <= 60; i++) {
			yield return null;
		}

		crossdresser.gameObject.SetActive (false);
		SeeCho ();
		punchSound.Play ();
		averageCho.gameObject.SetActive (true);

		// Time drag
		for (int i = 0; i <= 70; i++) {
			yield return null;
		}

		averageCho.gameObject.SetActive (false);
		subtitles.text = "";

		Camera.main.transform.position = origPos;
		Camera.main.orthographicSize = origSize;

		// Time drag
		for (int i = 0; i <= 50; i++) {
			yield return null;
		}

		bottomPanel.gameObject.SetActive (true);
		subtitles.text = "Together, they are";

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}

		subtitles.text = "";
		bottomPanel.gameObject.SetActive (false);

		// Time drag
		for (int i = 0; i <= 10; i++) {
			yield return null;
		}

		karabaanarit.gameObject.SetActive (true);
		slapSound.Play ();

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}

		karabaanarit.gameObject.SetActive (false);

		// Time drag
		for (int i = 0; i <= 80; i++) {
			yield return null;
		}

		bottomPanel.gameObject.SetActive (true);
		subtitles.text = "One day, Average Chow had a brilliant idea.";

		// Time drag
		for (int i = 0; i <= 110; i++) {
			yield return null;
		}

		subtitles.text = "";
		bottomPanel.gameObject.SetActive (false);

		// Time drag
		for (int i = 0; i <= 30; i++) {
			yield return null;
		}

		lightbulb.SetActive (true);
		notif2.Play ();

		// Time drag
		for (int i = 0; i <= 50; i++) {
			yield return null;
		}

		lightbulb.SetActive (false);

		// Time drag
		for (int i = 0; i <= 30; i++) {
			yield return null;
		}

		Camera.main.transform.position = new Vector3 (1.09f, -2.84f, -10f);
		Camera.main.orthographicSize = 4f;
		notif3.Play ();
		speechBubble.SetActive (true);
		submarine.SetActive (true);

		bottomPanel.gameObject.SetActive (true);
		subtitles.text = "Let's explore the bottom of the ocean, he mumbled to himself.";

		// Time drag
		for (int i = 0; i <= 160; i++) {
			yield return null;
		}

		subtitles.text = "";
		bottomPanel.gameObject.SetActive (false);
		speechBubble.SetActive (false);
		submarine.SetActive (false);
		Camera.main.transform.position = origPos;
		Camera.main.orthographicSize = origSize;

		// Time drag
		for (int i = 0; i <= 40; i++) {
			yield return null;
		}

		bottomPanel.gameObject.SetActive (true);
		subtitles.text = "Both Crossdresser and J-Idol approved his mumbling.";

		// Time drag
		for (int i = 0; i <= 70; i++) {
			yield return null;
		}

		approval1.gameObject.SetActive (true);
		punchSound.Play ();
		approval2.gameObject.SetActive (true);
		punchSound.Play ();

		// Time drag
		for (int i = 0; i <= 50; i++) {
			yield return null;
		}

		subtitles.text = "";
		bottomPanel.gameObject.SetActive (false);

		// Time drag
		for (int i = 0; i <= 30; i++) {
			yield return null;
		}

		approval1.gameObject.SetActive (false);
		approval2.gameObject.SetActive (false);
		Camera.main.transform.position = new Vector3 (1.09f, -2.84f, -10f);
		Camera.main.orthographicSize = 4f;
		speechBubble.SetActive (true);
		submarine.SetActive (true);
		approval3.gameObject.SetActive (true);
		slapSound.Play ();

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}

		fadeToBlack = true;

		// Fade in Shader
		for (float f = 0; f <= 1.0f; f += 0.02f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		SceneManager.LoadScene ("BackstoryPT2", LoadSceneMode.Single);
	}

	/// <summary>
	/// Camera focuses on a character's (Japanese Idol) face.
	/// </summary>
	void SeeIdol ()
	{
		Camera.main.orthographicSize = 1f;

		Camera.main.transform.position
		= new Vector3 (-4.49f, -5.85f, -10f);
	}

	/// <summary>
	/// Camera focuses on a character's (Crossdresser) face
	/// </summary>
	void SeeDresser ()
	{
		Camera.main.transform.position
		= new Vector3 (0.02f, -5.3f, -10f);
	}

	/// <summary>
	/// Camera focuses on a character's (Average Chow) face
	/// </summary>
	void SeeCho ()
	{
		Camera.main.transform.position
		= new Vector3 (4.38f, -5.16f, -10f);
	}

	/// <summary>
	/// Fades in audio.
	/// </summary>
	/// <param name="audio">Audio.</param>
	void FadeInAudio (AudioSource audio)
	{
		if (audio.volume < 1f) {
			audio.volume += .05f * Time.deltaTime;
		}
	}

	/// <summary>
	/// Fades out audio.
	/// </summary>
	/// <param name="audio">Audio.</param>
	void FadeOutAudio (AudioSource audio)
	{
		if (audio.volume > 0) {
			audio.volume -= .02f * Time.deltaTime;
		}
	}
}
