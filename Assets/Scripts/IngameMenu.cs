using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// In-game menu.
/// </summary>
public class IngameMenu : MonoBehaviour, IPointerDownHandler
{
	public int count = 0;

	GameObject underMenuCover;
	GameObject controls;
	GameObject verification;

	// In-game menu buttons
	Button btnContinue;
	Button btnControls;
	Button btnBack;
	Button btnQuit;
	Button btnYes;
	Button btnNo;

	void Start ()
	{
		underMenuCover = GameObject.Find ("UnderMenuCover");
		controls = GameObject.Find ("Controls");
		verification = GameObject.Find ("Verification");

		btnContinue = GameObject.Find ("ButtonContinue").GetComponent<Button> ();
		btnControls = GameObject.Find ("ButtonControls").GetComponent<Button> ();
		btnBack = GameObject.Find ("ButtonBack").GetComponent<Button> ();
		btnQuit = GameObject.Find ("ButtonQuit").GetComponent<Button> ();
		btnYes = GameObject.Find ("ButtonYes").GetComponent<Button> ();
		btnNo = GameObject.Find ("ButtonNo").GetComponent<Button> ();

		btnContinue.onClick.AddListener (() => OnButtonClick ("continue game"));
		btnControls.onClick.AddListener (() => OnButtonClick ("see controls"));
		btnBack.onClick.AddListener (() => OnButtonClick ("go back"));
		btnQuit.onClick.AddListener (() => OnButtonClick ("check for verification"));
		btnYes.onClick.AddListener (() => OnButtonClick ("quit game"));
		btnNo.onClick.AddListener (() => OnButtonClick ("go back"));

		verification.SetActive (false);
		controls.SetActive (false);
		underMenuCover.SetActive (false);
	}

	/// <summary>
	/// Manages what happens when a button is clicked 
	/// depending on the direction that has been set to the button.
	/// </summary>
	/// <param name="direction">Direction.</param>
	void OnButtonClick (string direction)
	{
		GameObject.Find ("ButtonSound").GetComponent<AudioSource> ().Play ();

		switch (direction) {

		case "continue game":
			ContinueGame ();
			break;

		case "see controls":
			ShowControls ();
			break;

		case "go back":
			ReturnToIngameMenu ();
			break;

		case "check for verification":
			VerifyCheck ();
			break;

		case "quit game":
			SceneManager.LoadScene ("Menu", LoadSceneMode.Single);
			break;
		}
	}

	/// <summary>
	/// Continues the game.
	/// </summary>
	void ContinueGame ()
	{
		count = 0;

		underMenuCover.SetActive (false);

		if (GameObject.Find ("Player") != null) {
			GameObject.Find ("Player").GetComponent<PlayerMovement> ().enabled = true;
			GameObject.Find ("Player").GetComponent<PlayerHealth> ().enabled = true;
			GameObject.Find ("Player").GetComponent<Rigidbody2D> ()
				.constraints = RigidbodyConstraints2D.None;
			GameObject.Find ("Player").GetComponent<Rigidbody2D> ()
				.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}

	/// <summary>
	/// Shows game controls.
	/// </summary>
	void ShowControls ()
	{
		btnContinue.gameObject.SetActive (false);
		btnControls.gameObject.SetActive (false);
		btnQuit.gameObject.SetActive (false);

		controls.SetActive (true);
	}

	/// <summary>
	/// Returns to in-game menu.
	/// </summary>
	void ReturnToIngameMenu ()
	{
		controls.SetActive (false);
		verification.SetActive (false);

		btnContinue.gameObject.SetActive (true);
		btnControls.gameObject.SetActive (true);
		btnQuit.gameObject.SetActive (true);
	}

	/// <summary>
	/// Verifies player's will to quit the game.
	/// </summary>
	void VerifyCheck ()
	{
		btnContinue.gameObject.SetActive (false);
		btnControls.gameObject.SetActive (false);
		btnQuit.gameObject.SetActive (false);

		verification.SetActive (true);
	}

	/// <summary>
	/// Stops player movement when in-game menu is displayed.
	/// </summary>
	/// <param name="ped">Ped.</param>
	public virtual void OnPointerDown (PointerEventData ped)
	{
		GameObject.Find ("ButtonSound").GetComponent<AudioSource> ().Play ();

		count++;

		if (count < 2) {
			
			underMenuCover.SetActive (true);

			if (GameObject.Find ("Player") != null) {
				GameObject.Find ("Player").GetComponent<Rigidbody2D> ()
				.constraints = RigidbodyConstraints2D.FreezeAll;
				GameObject.Find ("Player").GetComponent<PlayerMovement> ().enabled = false;
				GameObject.Find ("Player").GetComponent<PlayerHealth> ().enabled = false;
			}

			/*GameObject.Find ("Enemy").GetComponent<Rigidbody2D> ()
				.constraints = RigidbodyConstraints2D.FreezeAll;
			GameObject.Find ("Enemy").GetComponent<Animator> ().Play ("Idle");
			GameObject.Find ("Enemy").GetComponent<EnemyMovement> ().enabled = false;
			GameObject.Find ("Enemy").GetComponent<EnemyHealth> ().enabled = false;*/
			
		} else {
			
			underMenuCover.SetActive (false);

			if (GameObject.Find ("Player") != null) {
				GameObject.Find ("Player").GetComponent<PlayerMovement> ().enabled = true;
				GameObject.Find ("Player").GetComponent<PlayerHealth> ().enabled = true;
				GameObject.Find ("Player").GetComponent<Rigidbody2D> ()
				.constraints = RigidbodyConstraints2D.None;
				GameObject.Find ("Player").GetComponent<Rigidbody2D> ()
				.constraints = RigidbodyConstraints2D.FreezeRotation;
			}

			/*GameObject.Find ("Enemy").GetComponent<EnemyMovement> ().enabled = true;
			GameObject.Find ("Enemy").GetComponent<EnemyHealth> ().enabled = true;
			GameObject.Find ("Enemy").GetComponent<Rigidbody2D> ()
				.constraints = RigidbodyConstraints2D.None;
			GameObject.Find ("Enemy").GetComponent<Rigidbody2D> ()
				.constraints = RigidbodyConstraints2D.FreezeRotation;*/
			
			ReturnToIngameMenu ();

			count = 0;
		}
	}
}
