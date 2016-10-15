using UnityEngine;
using System.Collections;
using System; 
using System.Collections.Generic;

public class player : MonoBehaviour {

	public UInt32  Speed=10;
	public UInt32 BodyLenth = 3;
	private Vector2 SpownPoint = new Vector2 (0, 0);
	Vector2 dir = Vector2.right;
	GameObject Players;
	// Use this for initialization
	void Start () {
		Players=(GameObject)Instantiate(Resources.Load ("player001"),SpownPoint,Quaternion.identity);
		// Move the Snake every 300ms
		InvokeRepeating("Move", 0.3f, 0.3f);
	}
	// Update is called once per Frame
	void Update() {
		// Move in a new Direction?
		if (Input.GetKey(KeyCode.RightArrow))
			dir = Vector2.right;
		else if (Input.GetKey(KeyCode.DownArrow))
			dir = -Vector2.up;    // '-up' means 'down'
		else if (Input.GetKey(KeyCode.LeftArrow))
			dir = -Vector2.right; // '-right' means 'left'
		else if (Input.GetKey(KeyCode.UpArrow))
			dir = Vector2.up;
	}
	// Update is called once per frame

	void Move() {
		// Move head into new direction
		Players.transform.Translate(dir);
	}
}
