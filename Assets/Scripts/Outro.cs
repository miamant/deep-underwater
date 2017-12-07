using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Outro for the game.
/// </summary>
public class Outro : MonoBehaviour
{
	public GameObject textBar;

	void Start ()
	{
		textBar.SetActive (false);

		StartCoroutine ("OutroScene");
	}

	void Update ()
	{
		
	}

	/// <summary>
	/// Manages how the scene plays out. Loads Main Menu afterwards.
	/// </summary>
	/// <returns>null</returns>
	IEnumerator OutroScene ()
	{
		textBar.SetActive (true);

		// Time drag
		for (int i = 0; i <= 200; i++) {
			yield return null;
		}

		textBar.SetActive (false);

		SceneManager.LoadScene ("Menu", LoadSceneMode.Single);
	}
}
