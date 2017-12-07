using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplosionScene : MonoBehaviour {
	//textbox
	public GameObject bar;
	public Text text;
	public Text status;
	//player
	public GameObject uly;
	//sceneswitch
	public string nextScene;
	//explosion gameobjects
	public GameObject exp1;
	public GameObject exp2;
	public GameObject exp3;
	public GameObject exp4;
	public GameObject exp5;



	// Use this for initialization
	void Start ()
	{
		StartCoroutine ("Intro");
	}

	// Update is called once per frame
	void Update ()
	{

	}

	IEnumerator Intro ()
	{

		string temp = "";

		text.text = "";
		temp = "What is that noise?";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			text.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}
		for (int i = 0; i <= 40; i++) {
			yield return null;
		}
		temp = "";

		text.text = "";
		// Time drag
		for (int i = 0; i <= 60; i++) {
			yield return null;
		}
		//activates gameobject
		exp1.SetActive (true);
		for (int i = 0; i <= 20; i++) {
			yield return null;
		}
		temp = "";

		text.text = "";
		temp = "HOLY SHIT!";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			text.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}
		for (int i = 0; i <= 20; i++) {
			yield return null;
		}
		temp = "";

		text.text = "";
		temp = "Get to the subs!";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			text.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}
		for (int i = 0; i <= 40; i++) {
			yield return null;
		}
		temp = "";

		text.text = "";
		temp = "ARRRRR!!";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			text.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}
		for (int i = 0; i <= 70; i++) {
			yield return null;
		}
		exp2.SetActive (true);
		exp3.SetActive (true);
		exp4.SetActive (true);
		exp5.SetActive (true);
		bar.SetActive (false);
	}
}
