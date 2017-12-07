using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Player health.
/// Reduces player health when colliding with Enemy.
/// Kills player when health drops to zero.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
	public int startingHealth;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColour = new Color (1f, 0f, 0f, 0.1f);

	int count = 0;
	bool isDead;
	bool damaged;

	void Start ()
	{
		currentHealth = startingHealth;
		healthSlider.maxValue = startingHealth;
	}

	void Update ()
	{
		if (damaged) {
			damageImage.color = flashColour;
			healthSlider.value--;
		} else {
			damageImage.color = Color.Lerp (damageImage.color
				, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;

		if (currentHealth == 0)
			Die ();


		// Acts as a timer for losing health steadily while touching Enemy
		if (count > 50)
			count = 0;
		count++;
	}

	/// <summary>
	/// Doesn't reduce player health when colliding with Torpedo.
	/// Reduced player health when colliding with Enemy.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionEnter2D (Collision2D col)
	{
		// Player cannot collide with Torpedo
		if (col.gameObject.tag.Equals ("Torpedo")) {
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (),
				col.collider);
		}

		// Player loses health when colliding with Enemy
		if (col.gameObject.tag.Equals ("Enemy")) {
			currentHealth--;
			damaged = true;
		}

		// Player loses health when colliding with Bubble
		if (col.gameObject.tag.Equals ("Bubble")) {
			currentHealth--;
			damaged = true;
		}
	}

	/// <summary>
	/// Steadily reduces player health when in contact with Enemy. 
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionStay2D (Collision2D col)
	{
		// Player loses health steadily while touching Enemy
		if (count == 50) {
			if (col.gameObject.tag.Equals ("Enemy")) {
				currentHealth--;
				damaged = true;
			}
		}
	}

	/// <summary>
	/// Kills player.
	/// </summary>
	void Die ()
	{
		Destroy (transform.parent.gameObject);
	}
}
