using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class UlysseusMovement : MonoBehaviour
{
	public float playerSpeed = 10f, boostMultiplier = 2f;
	public LeftJoystick leftJoystick;
	public RightJoystick rightJoystick;

	Rigidbody2D playerBody;

	void Start ()
	{
		playerBody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate ()
	{
		/*** VECTORS USED ***/

		// Player movement direction (left joystick)
		Vector2 moveVec = new Vector2 (leftJoystick.Horizontal ()
			, leftJoystick.Vertical ()) * playerSpeed;

		// Player rotation direction (left joystick)
		Vector3 lookVec = new Vector3 (leftJoystick.Horizontal ()
			, leftJoystick.Vertical (), 4000);

		// Player rotation direction (right joystick)
		Vector3 lookVec2 = new Vector3 (rightJoystick.inputVec.x
			, rightJoystick.inputVec.y, 4000);


		/*** PLAYER MOVEMENT & ROTATION ***/

		// Right joystick dictates player rotation
		if (lookVec2.x != 0 && lookVec2.y != 0) {

			/*GetComponentInParent <Transform> ().rotation = 
				Quaternion.LookRotation (lookVec2, Vector3.back);*/

			// Flips player graphics when going right
			if (lookVec2.x > 0) {
				transform.localScale = new Vector3 (1.0f,
					transform.localScale.y,
					transform.localScale.z);
			}

			// Flips player graphics when going left
			if (lookVec2.x < 0) {
				transform.localScale = new Vector3 (-1.0f,
					transform.localScale.y,
					transform.localScale.z);
			}

		} else {

			// Left joystick dictates player rotation
			if (lookVec.x != 0 && lookVec.y != 0) {
				/*GetComponentInParent <Transform> ().rotation = 
					Quaternion.LookRotation (lookVec, Vector3.back);*/
			} else if (transform.localScale == new Vector3 (1.0f,
				           transform.localScale.y,
				           transform.localScale.z)) {
				/*GetComponentInParent <Transform> ().rotation = 
					Quaternion.LookRotation (lookVec, Vector3.right);*/
			} else {
				/*GetComponentInParent <Transform> ().rotation = 
					Quaternion.LookRotation (lookVec, Vector3.left);*/
			}

			// Flips player graphics when going right
			if (leftJoystick.Horizontal () > 0) {
				transform.localScale = new Vector3 (1.0f,
					transform.localScale.y,
					transform.localScale.z);
			}

			// Flips player graphics when going left
			if (leftJoystick.Horizontal () < 0) {
				transform.localScale = new Vector3 (-1.0f,
					transform.localScale.y,
					transform.localScale.z);
			}
		}

		// Boost feature (not in use)
		bool isBoosting = CrossPlatformInputManager.GetButton ("BoostButton");
		playerBody.AddForce (moveVec * (isBoosting ? boostMultiplier : 1));
	}
}
