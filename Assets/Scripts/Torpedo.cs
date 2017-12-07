using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

/// <summary>
/// Torpedo.
/// Makes the torpedo move and rotate accordingly to player movement.
/// When right joystick is held down a certain time,
/// launches the torpedo from its position.
/// </summary>
public class Torpedo : MonoBehaviour
{
	public float speed = 10f;

	Transform target;
	Vector3 offset;
	Vector2 moveVec;

	GameObject explosion;
	AudioSource explosionSound;
	AudioSource whooshSound;
	Rigidbody2D rb;

	LeftJoystick leftJoystick;
	RightJoystick rightJoystick;

	bool stopFollow = false;

	int torpedoCountdown = 0;

	void Start ()
	{
		target = GameObject.Find ("TorpedoSpawn").GetComponent<Transform> ();
		offset = transform.position - target.position;

		explosion = transform.GetChild (0).gameObject;
		explosionSound = transform.GetChild (1).gameObject.GetComponent<AudioSource> ();
		whooshSound = transform.GetChild (2).gameObject.GetComponent<AudioSource> ();
		explosion.SetActive (false);
		rb = GetComponent<Rigidbody2D> ();

		// Joysticks
		if (GameObject.Find ("LeftJoystick") != null) {
			leftJoystick = GameObject.Find ("LeftJoystick").transform.GetChild (0)
			.GetComponent<LeftJoystick> ();
		}
		if (GameObject.Find ("RightJoystick") != null) {
			rightJoystick = GameObject.Find ("RightJoystick").transform.GetChild (0)
			.GetComponent<RightJoystick> ();
		}

		// Ignore collisions between Player
		Physics2D.IgnoreCollision (GetComponent<Collider2D> ()
			, GameObject.Find ("Player").GetComponent<Collider2D> ());

		if ((GameObject.Find ("LeftJoystick") != null) || (GameObject.Find ("RightJoystick") != null))
			moveVec = new Vector2 (rightJoystick.inputVec.x, rightJoystick.inputVec.y);

		StartCoroutine ("Scale");
	}

	void FixedUpdate ()
	{
		if ((GameObject.Find ("LeftJoystick") == null) || (GameObject.Find ("RightJoystick") == null)) {
			Destroy (gameObject);
		} else {

			// If Player turns left, so does Torpedo
			if (GameObject.Find ("Player").GetComponent<PlayerMovement> ().turnedLeft) {
				transform.localScale = new Vector3 (-1.0f,
					transform.localScale.y,
					transform.localScale.z);
			} else {
				// If Player turns right, so does Torpedo
				transform.localScale = new Vector3 (1.0f,
					transform.localScale.y,
					transform.localScale.z);
			}

			// Follow target
			if (!stopFollow)
				transform.position = target.position + offset;

			// Player rotation direction (left joystick)
			Vector3 lookVec = new Vector3 (leftJoystick.Horizontal ()
			, leftJoystick.Vertical (), 4000);

			// Player rotation direction (right joystick)
			Vector3 lookVec2 = new Vector3 (rightJoystick.inputVec.x
			, rightJoystick.inputVec.y, 4000);


			if (!stopFollow) {
				// Right Joystick dictates Torpedo rotation
				if (lookVec2.x != 0 && lookVec2.y != 0) {
			
					GetComponentInParent <Transform> ().rotation = 
				Quaternion.LookRotation (lookVec2, Vector3.back);

				} else {

					// Left Joystick dictates Torpedo rotation
					if (lookVec.x != 0 && lookVec.y != 0) {

						GetComponentInParent <Transform> ().rotation = 
					Quaternion.LookRotation (lookVec, Vector3.back);

					} else if (transform.localScale == new Vector3 (1.0f,
						           transform.localScale.y,
						           transform.localScale.z)) {
						GetComponentInParent <Transform> ().rotation = 
					Quaternion.LookRotation (lookVec, Vector3.right);
					} else {
						GetComponentInParent <Transform> ().rotation = 
					Quaternion.LookRotation (lookVec, Vector3.left);
					}
				}
			}
		}
	}

	void Update ()
	{
		if (GameObject.Find ("LeftJoystick") == null || GameObject.Find ("RightJoystick") == null) {
			Destroy (gameObject);

		} else {
			
			if (!stopFollow) {
				moveVec = new Vector2 (rightJoystick.inputVec.x, rightJoystick.inputVec.y);
			}

			if (rightJoystick.isDragged) {
				torpedoCountdown++;
	
				if (torpedoCountdown == 80) {
					stopFollow = true;
					whooshSound.Play ();
				}

			} else {
				torpedoCountdown = 0;
			}

			if (stopFollow)
				rb.AddForce (moveVec * speed);

			if (GameObject.Find ("Torpedo(Clone)") != null) {
				Physics2D.IgnoreCollision (GetComponent<Collider2D> ()
				, GameObject.Find ("Torpedo(Clone)").GetComponent<Collider2D> ());
			}
		}
	}

	/// <summary>
	/// Scales torpedo at start.
	/// </summary>
	IEnumerator Scale ()
	{
		for (int i = 0; i < 20; i++) {
			transform.localScale += new Vector3 (.022f, .022f, .022f);
			yield return new WaitForSeconds (.01f);
		}
	}

	/// <summary>
	/// Triggers an explosion when colliding with an object that is not another Torpedo.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.name.Equals ("Torpedo(Clone)")) {
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (), col.collider);
		} else {
			Explode ();
		}
	}

	/// <summary>
	/// Explodes.
	/// </summary>
	void Explode ()
	{
		explosion.SetActive (true);
		explosionSound.Play ();
		GetComponent<SpriteRenderer> ().color = Color.clear;
		GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
		
		StartCoroutine ("Disappear");
	}

	/// <summary>
	/// Destroys the torpedo.
	/// </summary>
	IEnumerator Disappear ()
	{
		// Time drag
		for (int i = 0; i <= 115; i++) {
			yield return null;
		}

		Destroy (gameObject);
	}
}
