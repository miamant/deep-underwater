using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Plays out the Intro scene.
/// </summary>
public class Intro : MonoBehaviour
{
	public Image shader;
	public Text title;
	public string nextScene;

	void Start ()
	{
		StartCoroutine ("Fade"); // Starts the fade-in-out functionality
	}

	/// <summary>
	/// Manages the fade effects and text appearances.
	/// </summary>
	IEnumerator Fade ()
	{
		// 1. Fade in
		for (float f = 1f; f >= 0; f -= 0.01f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		// Time drag
		for (int i = 0; i <= 12; i++) {
			yield return null;
		}

		// 2. Fade out
		for (float f = 0f; f <= 1.0f; f += 0.01f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		// Miniscule time drag
		for (int i = 0; i <= 8; i++) {
			yield return null;
		}

		title.fontSize += 20;
		title.text = "DEEP UNDERWATER";

		// 3. Fade in to new text
		for (float f = 1f; f >= 0; f -= 0.01f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		// Longer time drag
		for (int i = 0; i <= 30; i++) {
			yield return null;
		}
			
		// 4. Fade out
		for (float f = 0f; f <= 1.0f; f += 0.01f) {
			Color c = shader.color;
			c.a = f;
			shader.color = c;
			yield return null;
		}

		// Miniscule time drag
		for (int i = 0; i <= 8; i++) {
			yield return null;
		}

		SceneManager.LoadScene (nextScene, LoadSceneMode.Additive);
	}
}
