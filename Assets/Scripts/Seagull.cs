using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour {

	public float targetTime = 4.0f;
	public float size = 3.0f;
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (targetTime > 0.0f) {
			targetTime -= Time.deltaTime;
			if (size > 0) {
				size -= 0.01f;
				transform.localScale -= new Vector3 (0.01f, 0.01f, 0);
			} else {
				gameObject.SetActive (false);
			}
		}
	}
}
