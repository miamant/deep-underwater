using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour {

	public float size = 10.0f;
	public float rotated = 10.0f;
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (size > 0) {
			size -= 0.05f;
			transform.Rotate (0, 0, +0.05f);
		} else if (rotated > 0) {
			rotated -= 0.05f;
			transform.Rotate (0, 0, -0.05f);
		} else {
			size = 10.0f;
			rotated = 10.0f;
		}
	}
}
