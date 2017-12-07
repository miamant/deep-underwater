using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//exceptions for colliding
public class MapCollider : MonoBehaviour {
	public GameObject enemy;
	public GameObject mapcol;
	public GameObject mapco2;
	public GameObject mapco3;
	public GameObject mapco4;
	// Use this for initialization
	void Start () {
		Physics2D.IgnoreCollision (enemy.GetComponent<Collider2D> (), mapcol.GetComponent<Collider2D> ());
		Physics2D.IgnoreCollision (enemy.GetComponent<Collider2D> (), mapco2.GetComponent<Collider2D> ());
		Physics2D.IgnoreCollision (enemy.GetComponent<Collider2D> (), mapco3.GetComponent<Collider2D> ());
		Physics2D.IgnoreCollision (enemy.GetComponent<Collider2D> (), mapco4.GetComponent<Collider2D> ());
	}
	
	// Update is called once per frame
	void Update () {
	}
}
