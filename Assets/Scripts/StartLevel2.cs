using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Changes alphalevel of gameobject
public class StartLevel2 : MonoBehaviour {

	public float targetTime = 4.0f;
	public float alphaLevel = 0f;
	public float alphaboost = 0f;
	public GameObject start;
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (targetTime > 0.0f) {
			targetTime -= Time.deltaTime;
			alphaLevel -= .0035f * alphaboost;
		} else {
		}
		GetComponent<Image> ().color = new Color (0, 0, 0, alphaLevel);
	}
}

