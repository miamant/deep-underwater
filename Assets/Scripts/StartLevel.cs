using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevel : MonoBehaviour {

	public float targetTime = 4.0f;
	public float alphaLevel = .5f;
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
		if (targetTime > 0.0f) {
			targetTime -= Time.deltaTime;
			alphaLevel -= .0035f;
		}
		GetComponent<RawImage> ().color = new Color (0, 0, 0, alphaLevel);
	}
}
