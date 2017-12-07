using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiMovement : MonoBehaviour
{

	public Transform emoji;
	public int countdown = 100;
	public bool goLeft;

	void Start ()
	{
		
	}

	void Update ()
	{
		if (countdown == 100) {
			goLeft = true;
		} else if (countdown == 50) {
			goLeft = false;
		} else if (countdown == 0) {
			countdown = 101;
		}

		if (goLeft) {
			emoji.position += new Vector3 (-0.8f, 0, 0);
		} else {
			emoji.position += new Vector3 (0.8f, 0, 0);
		}

		countdown--;
	}
}
