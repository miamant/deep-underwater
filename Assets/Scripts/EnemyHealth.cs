using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy health.
/// </summary>
public class EnemyHealth : MonoBehaviour
{

	public int startingHealth;
	public int currentHealth;
	public float flashSpeed = 5f;
	public Color flashColour = new Color (1f, 0f, 0f, 0.8f);

	public bool isDead;
	bool damaged;

	void Start ()
	{
		currentHealth = startingHealth;
	}

	void Update ()
	{
		
		if (damaged) {
			GetComponent<SpriteRenderer> ().color = flashColour;
		} else {
			GetComponent<SpriteRenderer> ().color = Color.Lerp (
				GetComponent<SpriteRenderer> ().color, Color.white, flashSpeed * Time.deltaTime);
		}
		damaged = false;

		if (currentHealth == 0)
			Die ();
	}

	/// <summary>
	/// When colliding with Torpedo enemy loses health.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionEnter2D (Collision2D col)
	{
		// Loses health when colliding with Torpedo
		if (col.gameObject.tag.Equals ("Torpedo")) {
			currentHealth--;
			damaged = true;
		}
	}

	/// <summary>
	/// Enemy dies.
	/// </summary>
	void Die ()
	{
		isDead = true; 
		Destroy (transform.parent.gameObject);
	}
}
