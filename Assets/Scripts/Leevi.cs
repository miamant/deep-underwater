using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leevi : MonoBehaviour {
	// Use this for initialization
	//speed factor
	public float speedboost = 1f;
	// left or right side of screen
	public bool RLside = true;

	void Start () {
		
	}

	
	// Update is called once per frame
	void Update () {
		if (RLside == true) {
			if (transform.localPosition.x < 22.2) {
				//moving Gameobject
				transform.localPosition += new Vector3 (0.01f * speedboost, 0, 0);
			} else {
				RLside = false;
				transform.localScale = new Vector3 (-6.5f, 6.5f, 6.5f);
				//tranforms gameobject to other way and sets it to random y range in screen
				transform.localPosition = new Vector3 (22.2f, Random.Range (-6.15f, 6.15f), 0);
			}
		} 
		if (RLside == false){
			if (transform.localPosition.x > -22.2) {
				transform.localPosition -= new Vector3 (0.01f * speedboost, 0, 0);
			} else {
				RLside = true;
				transform.localScale = new Vector3 (6.5f, 6.5f, 6.5f);
				transform.localPosition = new Vector3 (-22.2f, Random.Range (-6.15f, 6.15f), 0);
			}
		}
	}
}
