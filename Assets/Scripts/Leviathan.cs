using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leviathan : MonoBehaviour {

	public float targetTime = 2.0f;
	public float alphaLevel = 0f;
	public float alkuaika = 2.0f;
	public Object level;
	public GameObject sound;
	public GameObject player;
	public GameObject scenechange;
	private int number = 0;
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (number < 1) {
			if (player.transform.position.y < -180) {
				sound.SetActive(true);
				number++;
			}
		}
		if (player.transform.position.y < -200) {
				if (targetTime > 0.0f) {
					alphaLevel += .0015f;
					targetTime -= Time.deltaTime;
				} else {
					if (alphaLevel > 0) {
						alphaLevel -= .0015f;
					} else {
						targetTime = alkuaika + 2;
						alkuaika = targetTime * 2;
					}
				}
				GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, alphaLevel);
			}
		if (player.transform.position.y < -250) {
			scenechange.SetActive (true);
		}
	}
}
