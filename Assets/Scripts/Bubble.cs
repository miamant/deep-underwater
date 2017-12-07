using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame

	void Update ()
	{
		//Transform size until reach 2f.
		if (transform.localScale.magnitude < 2f) {
			transform.localScale += new Vector3 (0.007f, 0.007f, 0);
		}
		//Moving y axis
		transform.localPosition += new Vector3 (0, 0.01f, 0);

	}
// Destroys this gameobject.
	void OnCollisionEnter2D (Collision2D col)
	{
		Destroy (gameObject);
	}
}
