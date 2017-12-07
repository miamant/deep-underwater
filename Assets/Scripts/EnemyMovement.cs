using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

/// <summary>
/// Enemy movement.
/// </summary>
public class EnemyMovement : MonoBehaviour
{
	public float speed = 2f;
	Transform target;
	Animator anim;

	void Start ()
	{
		target = GameObject.Find ("Player").GetComponent<Transform> ();
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate ()
	{
		//if (GameObject.Find ("Trigger")
		//	.GetComponent<PlayerDetector> ().playerDetected) {
		if (target != null) {
			FollowPlayer ();
			FlipGraphics ();
		}
		//} else {
		//	anim.Play ("Idle");
		//}
	}

	/// <summary>
	/// Follows the player.
	/// </summary>
	void FollowPlayer ()
	{
		anim.Play ("Active");

		transform.position = Vector2.MoveTowards (transform.position, target.position,
			speed * Time.deltaTime);

		transform.up = target.position - transform.position;
	}

	/// <summary>
	/// Flips the graphics fo the enemy when changing direction.
	/// </summary>
	void FlipGraphics ()
	{
		if (target.position.x > transform.position.x) {
			transform.localScale = new Vector3 (1.0f,
				transform.localScale.y,
				transform.localScale.z);
		} else {
			transform.localScale = new Vector3 (-1.0f,
				transform.localScale.y,
				transform.localScale.z);
		}
	}
}
