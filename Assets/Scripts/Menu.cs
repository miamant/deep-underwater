using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Main menu
/// </summary>
public class Menu : MonoBehaviour
{
	public string nextscene;

	// Shaders for fade effects
	public Image shaderInner;
	public Image shaderOuter;

	public GameObject mainPanel;
	public GameObject subPanel;
	GameObject exitText;

	// Menu buttons
	Button btnStart;
	Button btnCreds;
	Button btnBack;
	Button btnExit;

	void Start ()
	{
		exitText = GameObject.Find ("ExitText");

		btnStart = GameObject.Find ("ButtonStart").GetComponent<Button> ();
		btnCreds = GameObject.Find ("ButtonCredits").GetComponent<Button> ();
		btnBack = GameObject.Find ("ButtonBack").GetComponent<Button> ();
		btnExit = GameObject.Find ("ButtonExit").GetComponent<Button> ();

		btnStart.onClick.AddListener (() => OnButtonClick ("start game"));
		btnCreds.onClick.AddListener (() => OnButtonClick ("see credits"));
		btnBack.onClick.AddListener (() => OnButtonClick ("go back"));
		btnExit.onClick.AddListener (() => OnButtonClick ("exit game"));

		shaderOuter.gameObject.SetActive (false);
		exitText.SetActive (false);
		subPanel.SetActive (false);

		StartCoroutine ("FadeIn");
	}

	/// <summary>
	/// Fades from black to the menu.
	/// </summary>
	/// <returns>The in.</returns>
	IEnumerator FadeIn ()
	{
		for (float f = 1f; f >= 0; f -= 0.05f) {
			Color c = shaderInner.color;
			c.a = f;
			shaderInner.color = c;
			yield return null;
		}

		shaderInner.gameObject.SetActive (false);
	}

	/// <summary>
	/// Manages what happens when a button with a certain direction attached to it is clicked.
	/// </summary>
	/// <param name="direction">Direction.</param>
	void OnButtonClick (string direction)
	{
		GameObject.Find ("ButtonSound").GetComponent<AudioSource> ().Play ();

		switch (direction) {

		case "start game":
			StartCoroutine ("StartGame");
			break;

		case "see credits":
			ShowCredits ();
			break;

		case "go back":
			ReturnToMenu ();
			break;

		case "exit game":
			StartCoroutine ("ExitGame");
			break;
		}
	}

	/// <summary>
	/// Starts the game.
	/// </summary>
	/// <returns>The game.</returns>
	IEnumerator StartGame ()
	{
		shaderInner.gameObject.SetActive (true);

		for (float f = 0f; f <= 1.0f; f += 0.05f) {
			Color c = shaderInner.color;
			c.a = f;
			shaderInner.color = c;
			yield return null;
		}

		// Miniscule time drag
		for (int i = 0; i <= 8; i++) {
			yield return null;
		}

		SceneManager.LoadScene (nextscene, LoadSceneMode.Single);
	}

	/// <summary>
	/// Shows the credits.
	/// </summary>
	void ShowCredits ()
	{
		subPanel.SetActive (true);
		mainPanel.SetActive (false);
	}

	/// <summary>
	/// Returns to menu.
	/// </summary>
	void ReturnToMenu ()
	{
		subPanel.SetActive (false);
		mainPanel.SetActive (true);
	}

	/// <summary>
	/// Exits the game.
	/// </summary>
	/// <returns>The game.</returns>
	IEnumerator ExitGame ()
	{
		shaderInner.gameObject.SetActive (true);

		for (float f = 0f; f <= 1.0f; f += 0.05f) {
			Color c = shaderInner.color;
			c.a = f;
			shaderInner.color = c;
			yield return null;
		}

		subPanel.SetActive (false);
		mainPanel.SetActive (false);

		// Miniscule time drag
		for (int i = 0; i <= 8; i++) {
			yield return null;
		}

		shaderOuter.gameObject.SetActive (true);
		exitText.SetActive (true);

		for (float f = 1f; f >= 0; f -= 0.05f) {
			Color c = shaderOuter.color;
			c.a = f;
			shaderOuter.color = c;
			yield return null;
		}

		// Time drag
		for (int i = 0; i <= 32; i++) {
			yield return null;
		}

		for (float f = 0f; f <= 1.0f; f += 0.05f) {
			Color c = shaderOuter.color;
			c.a = f;
			shaderOuter.color = c;
			yield return null;
		}

		Application.Quit ();
	}
}
