using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour {

	// Use this for initialization
	//How much time before next bubble spawn
	public float targetTime = 4.0f;
	//Resets targetTime (should be the same as targettime)
	public float startTime = 4.0f;
	public Transform bubbleSpawn;
	public GameObject bubblePrefab;
	void Start () {
		var bubble = (GameObject)Instantiate (
			bubblePrefab,
			bubbleSpawn.transform.position,
			bubbleSpawn.transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		if (targetTime > 0.0f) {
			targetTime -= Time.deltaTime;
			} else {
			var bubble = (GameObject)Instantiate (
				bubblePrefab,
				bubbleSpawn.transform.position,
				bubbleSpawn.transform.rotation);
				targetTime = startTime;
			}
		}
}
