using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages how the BackstoryPT2 plays out.
/// Times changes in the cinematic scene, e.g. when subtitles appear,
/// which images the camera focuses on etc.
/// </summary>
public class BackstoryPT2 : MonoBehaviour
{
	// UI elements
	Image shader;
	Image blackboard;
	Image bottomPanel;
	Text title;
	Text subtitles;
	Text ulyssesText;

	// UI parents
	GameObject ruralLandscape;
	GameObject middleScene;
	GameObject ryokanRiver;
	GameObject development;
	GameObject constructionFinished;
	GameObject marianaTrench;

	// Sprites (from Scene1)
	GameObject thoughtBubble;
	GameObject banknotes;
	GameObject emoji;
	GameObject lightbulb;
	GameObject black;
	GameObject hardHats;

	// Sprites (Scene2)
	GameObject dev1;
	GameObject dev2;
	GameObject dev3;

	// The red dots that together form a route from one point on the map to another
	List<GameObject> redDots;

	// Audio
	AudioSource bgMusic;
	AudioSource notifSound;
	AudioSource dingSound;
	AudioSource kachingSound;
	AudioSource slapSound;
	AudioSource punchSound;

	// Scenes
	public string nextScene;

	// Timer
	float timer = 0.0f;
	public int seconds = 0;

	// Booleans that give permissions to progress the scene
	bool canStartScene1 = false;
	bool canStartScene2 = false;
	bool canMoveDev1 = false;
	bool canMoveDev2 = false;
	bool canMoveDev3 = false;
	bool canMoveUlysses = false;


	void Start ()
	{
		// UI elements
		blackboard = GameObject.Find ("Blackboard").GetComponent<Image> ();
		shader = GameObject.Find ("Shader").GetComponent<Image> ();
		bottomPanel = GameObject.Find ("BottomPanel").GetComponent<Image> ();
		title = GameObject.Find ("Title").GetComponent<Text> ();
		subtitles = GameObject.Find ("Subtitles").GetComponent<Text> ();
		ulyssesText = GameObject.Find ("UlyssesText").GetComponent<Text> ();

		// UI parents
		ruralLandscape = GameObject.Find ("RuralLandscape");
		middleScene = GameObject.Find ("MiddleScene");
		ryokanRiver = GameObject.Find ("RyokanRiver");
		development = GameObject.Find ("Development");
		constructionFinished = GameObject.Find ("ConstructionFinished");
		marianaTrench = GameObject.Find ("MarianaTrench");

		// Sprites (from Scene1)
		thoughtBubble = GameObject.Find ("ThoughtBubble");
		banknotes = GameObject.Find ("Banknotes");
		emoji = GameObject.Find ("Emoji");
		lightbulb = GameObject.Find ("Lightbulb");
		black = GameObject.Find ("JustBlack");
		hardHats = GameObject.Find ("HardHats");

		// Sprites (Scene2)
		dev1 = GameObject.Find ("Dev1");
		dev2 = GameObject.Find ("Dev2");
		dev3 = GameObject.Find ("Dev3");

		// The red dots that together form a route from one point on map to another
		redDots = new List<GameObject> ();
		redDots.Add (GameObject.Find ("RedDot"));
		redDots.Add (GameObject.Find ("RedDot (1)"));
		redDots.Add (GameObject.Find ("RedDot (2)"));
		redDots.Add (GameObject.Find ("RedDot (3)"));
		redDots.Add (GameObject.Find ("RedDot (4)"));
		redDots.Add (GameObject.Find ("RedDot (5)"));
		redDots.Add (GameObject.Find ("RedDot (6)"));
		redDots.Add (GameObject.Find ("RedDot (7)"));
		redDots.Add (GameObject.Find ("RedDot (8)"));
		redDots.Add (GameObject.Find ("RedDot (9)"));
		redDots.Add (GameObject.Find ("RedDot (10)"));
		redDots.Add (GameObject.Find ("RedDot (11)"));

		// Audio
		bgMusic = GameObject.Find ("BGMusic").GetComponent<AudioSource> ();
		notifSound = GameObject.Find ("NotificationSound1").GetComponent<AudioSource> ();
		dingSound = GameObject.Find ("DingSound1").GetComponent<AudioSource> ();
		kachingSound = GameObject.Find ("Ka-ching").GetComponent<AudioSource> ();
		slapSound = GameObject.Find ("SlapSound").GetComponent<AudioSource> ();
		punchSound = GameObject.Find ("PunchSound").GetComponent<AudioSource> ();

		ryokanRiver.SetActive (false);
		development.SetActive (false);
		bottomPanel.gameObject.SetActive (false);
		middleScene.SetActive (false);
		thoughtBubble.SetActive (false);
		banknotes.SetActive (false);
		emoji.SetActive (false);
		lightbulb.SetActive (false);
		black.SetActive (false);
		hardHats.SetActive (false);
		dev2.SetActive (false);
		dev3.SetActive (false);
		constructionFinished.SetActive (false);
		ulyssesText.gameObject.SetActive (false);
		marianaTrench.SetActive (false);

		foreach (GameObject obj in redDots)
			obj.SetActive (false);

		StartCoroutine ("Intro");
	}


	void Update ()
	{
		timer += Time.deltaTime;
		seconds = Convert.ToInt32 (timer % 60);

		// Background music fades in
		if (seconds < 4)
			FadeInAudio (bgMusic);

		// A picture is moved down
		if (canMoveDev1)
			MoveGameObject (dev1, "down", .02f);
		// A picture is scaled smaller
		if (canMoveDev2)
			ScaleGameObject (dev2, "smaller", 15f);

		// A picture is moved down
		if (canMoveDev3)
			MoveGameObject (dev3, "down", .02f);

		// A picture is scaled bigger
		if (canMoveUlysses)
			ScaleGameObject (constructionFinished, "bigger", 1f);
	}

	void FixedUpdate ()
	{
		// Starts the first part of the scene
		if (canStartScene1) {
			StartCoroutine ("Scene1");
			canStartScene1 = false;
		}

		// Starts the second part of the scene
		if (canStartScene2) {
			StartCoroutine ("Scene2");
			canStartScene2 = false;
		}
	}

	/// <summary>
	/// Intro to the scene.
	/// </summary>
	IEnumerator Intro ()
	{
		// Fade out Blackboard
		for (float f = 1f; f >= 0; f -= .01f) {
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
		for (float f = 0; f <= 1f; f += .02f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		title.gameObject.SetActive (false);
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

		canStartScene1 = true;
	}

	/// <summary>
	/// First part of the scene.
	/// </summary>
	IEnumerator Scene1 ()
	{
		bottomPanel.gameObject.SetActive (true);
		subtitles.text = "Average Chow knew they needed money.";

		// Time drag
		for (int i = 0; i <= 110; i++) {
			yield return null;
		}

		SubtitlesOff ();

		// Time drag
		for (int i = 0; i <= 40; i++) {
			yield return null;
		}

		notifSound.Play ();
		thoughtBubble.SetActive (true);
		banknotes.SetActive (true);
		emoji.SetActive (true);
		GameObject.Find ("RuralLandscape").transform.Translate (Vector2.down * 4f);
		GameObject.Find ("RuralLandscape").transform.Translate (Vector2.right * 1f);
		GameObject.Find ("RuralLandscape").transform.localScale += new Vector3 (.3f, .3f, 0);

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}

		dingSound.Play ();
		lightbulb.SetActive (true);

		// Time drag
		for (int i = 0; i <= 110; i++) {
			yield return null;
		}

		kachingSound.Play ();
		black.SetActive (true);
		middleScene.SetActive (true);

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		bottomPanel.gameObject.SetActive (true);
		subtitles.text = "We're not sure how he did it";

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}

		SubtitlesOff ();

		// Time drag
		for (int i = 0; i <= 30; i++) {
			yield return null;
		}

		bottomPanel.gameObject.SetActive (true);
		subtitles.text = "We don't even know what he did, but";

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}

		subtitles.text = "they had money now.";

		// Time drag
		for (int i = 0; i <= 110; i++) {
			yield return null;
		}

		SubtitlesOff ();

		ryokanRiver.SetActive (true);
		ruralLandscape.SetActive (false);
		middleScene.SetActive (false);
		black.SetActive (false);

		// Time drag
		for (int i = 0; i <= 50; i++) {
			yield return null;
		}

		bottomPanel.gameObject.SetActive (true);
		subtitles.text = "With the money, they could start the first part of their project:";

		// Time drag
		for (int i = 0; i <= 130; i++) {
			yield return null;
		}

		notifSound.Play ();
		notifSound.Play ();
		notifSound.Play ();
		hardHats.SetActive (true);
		subtitles.text = "Development and construction of the main ship.";

		// Time drag
		for (int i = 0; i <= 120; i++) {
			yield return null;
		}

		SubtitlesOff ();

		// Fade in Shader
		for (float f = 0; f <= 1f; f += .02f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		ryokanRiver.SetActive (false);
		development.gameObject.SetActive (true);

		canStartScene2 = true;
	}

	/// <summary>
	/// Second part of the scene.
	/// </summary>
	IEnumerator Scene2 ()
	{
		canMoveDev1 = true;

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

		dev1.SetActive (false);
		dev2.SetActive (true);
		canMoveDev2 = true;

		// Time drag
		for (int i = 0; i <= 180; i++) {
			yield return null;
		}

		dev2.SetActive (false);
		dev3.SetActive (true);
		canMoveDev3 = true;

		// Time drag
		for (int i = 0; i <= 170; i++) {
			yield return null;
		}

		// Fade in Shader
		for (float f = 0; f <= 1f; f += .02f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		dev3.SetActive (false);
		blackboard.gameObject.SetActive (true);
		title.text = "Japan, 1915";
		title.gameObject.SetActive (true);

		// Fade out Shader
		for (float f = 1f; f >= 0; f -= .01f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		// Time drag
		for (int i = 0; i <= 90; i++) {
			yield return null;
		}

		// Fade in Shader
		for (float f = 0; f <= 1f; f += .02f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		blackboard.gameObject.SetActive (false);
		title.gameObject.SetActive (false);

		StartCoroutine ("Scene3");
	}

	/// <summary>
	/// Third part of the scene.
	/// </summary>
	IEnumerator Scene3 ()
	{
		constructionFinished.SetActive (true);
		canMoveUlysses = true;

		// Fade out Shader
		for (float f = 1f; f >= 0; f -= .01f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		// Time drag
		for (int i = 0; i <= 60; i++) {
			yield return null;
		}

		bottomPanel.gameObject.SetActive (true);
		subtitles.text = "The construction of the main ship finished in 1915.";

		// Time drag
		for (int i = 0; i <= 110; i++) {
			yield return null;
		}

		SubtitlesOff ();

		// Time drag
		for (int i = 0; i <= 40; i++) {
			yield return null;
		}
			
		bottomPanel.gameObject.SetActive (true);
		slapSound.Play ();
		ulyssesText.gameObject.SetActive (true);
		subtitles.text = "The ship was named Ulysses.";

		// Time drag
		for (int i = 0; i <= 110; i++) {
			yield return null;
		}

		SubtitlesOff ();
		ulyssesText.gameObject.SetActive (false);

		// Time drag
		for (int i = 0; i <= 60; i++) {
			yield return null;
		}

		// Fade in Shader
		for (float f = 0; f <= 1f; f += .02f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		ryokanRiver.SetActive (true);
		constructionFinished.SetActive (false);

		// Fade out Shader
		for (float f = 1f; f >= 0; f -= .01f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		// Time drag
		for (int i = 0; i <= 60; i++) {
			yield return null;
		}

		bottomPanel.gameObject.SetActive (true);
		subtitles.text = "Karabaanarit were ready to start their voyage.";

		// Time drag
		for (int i = 0; i <= 60; i++) {
			yield return null;
		}
			
		notifSound.Play ();
		notifSound.Play ();
		notifSound.Play ();
		hardHats.SetActive (false);

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		SubtitlesOff ();
			
		marianaTrench.SetActive (true);
		ryokanRiver.SetActive (false);

		// Time drag
		for (int i = 0; i <= 100; i++) {
			yield return null;
		}

		foreach (GameObject obj in redDots) {
			punchSound.Play ();
			obj.SetActive (true);
			yield return new WaitForSeconds (.2f);
		}

		// Time drag
		for (int i = 0; i <= 60; i++) {
			yield return null;
		}

		// Fade in Shader
		for (float f = 0; f <= 1f; f += .02f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		SceneManager.LoadScene (nextScene, LoadSceneMode.Single);
	}

	/// <summary>
	/// Disables subtitles.
	/// </summary>
	void SubtitlesOff ()
	{
		subtitles.text = "";
		bottomPanel.gameObject.SetActive (false);
	}

	/// <summary>
	/// Moves the game object.
	/// </summary>
	/// <param name="obj">Object.</param>
	/// <param name="direction">Direction.</param>
	/// <param name="speed">Speed.</param>
	void MoveGameObject (GameObject obj, string direction, float speed)
	{
		switch (direction) {

		case "up":
			obj.transform.Translate (Vector2.up * speed);
			break;

		case "down":
			obj.transform.Translate (Vector2.down * speed);
			break;
			
		}
	}

	/// <summary>
	/// Scales the game object.
	/// </summary>
	/// <param name="obj">Object.</param>
	/// <param name="whichWay">Which way.</param>
	/// <param name="speed">Speed.</param>
	void ScaleGameObject (GameObject obj, string whichWay, float speed)
	{
		switch (whichWay) {

		case "bigger":
			obj.transform.localScale += new Vector3 ((.0005f * speed), (.0005f * speed), 0);
			break;

		case "smaller":
			obj.transform.localScale -= new Vector3 ((.0005f * speed), (.0005f * speed), 0);
			break;

		}
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
			audio.volume -= .1f * Time.deltaTime;
		}
	}
}
