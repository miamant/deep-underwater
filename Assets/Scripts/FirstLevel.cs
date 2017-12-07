using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstLevel : MonoBehaviour
{
	//Textbox
	public GameObject bar;
	public Text text;
	public Text status;
	//player
	public GameObject uly;
	//Depth meter
	public Slider ulius;
	//sceneswitch
	public string nextScene;



	// Use this for initialization
	void Start ()
	{
		StartCoroutine ("Intro");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		//Gets player y axis and tranforms it to int and prints it.
		status.text = "Depth: " + Mathf.RoundToInt (uly.transform.position.y);
		//Sets slider value
		ulius.value = uly.transform.position.y;
	}

	IEnumerator Intro ()
	{
		// Time drag
		for (int i = 0; i <= 110; i++) {
			yield return null;
		}

		string temp = "";

		text.text = "";
		temp = "Let's go deep underwater!!!";
		for (int i = 0; i < temp.Length; i++) {
			char[] charArr = temp.ToCharArray ();
			text.text += charArr [i];
			yield return new WaitForSeconds (.05f);
		}
		// Time drag
		for (int i = 0; i <= 130; i++) {
			yield return null;
		}
		bar.SetActive (false);
	}
}
