using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//switchs scene after enemy death
using UnityEngine.UI;


public class SceneEnemyDead : MonoBehaviour {

	// Use this for initialization
	public string nextscene;
	public float targetTime = 60.0f;
	public float alphaLevel = 0f;
	public float alphaboost = 0f;
	public float waittime = 0f;
	public GameObject enemy;
	void Start () {

		}

		// Update is called once per frame
		void Update () {
		if (enemy == null) {
			if (waittime > 0.0f) {
				waittime -= Time.deltaTime;
			} else {
				if (targetTime > 0.0f) {
					targetTime -= Time.deltaTime;
					alphaLevel += .005f * alphaboost;
				} else {
					SceneManager.LoadScene (nextscene, LoadSceneMode.Single);
				}
				GetComponent<Image> ().color = new Color (0, 0, 0, alphaLevel);
			}
		}
	}
}
