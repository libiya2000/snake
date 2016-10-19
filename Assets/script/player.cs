using UnityEngine;
using System.Collections;
using System; 
using System.Collections.Generic;

public class player : MonoBehaviour {

	public UInt32  Speed=500;
	public UInt32 BodyLenth = 3;
	private Vector2 SpownPoint = new Vector2 (0, 0);
	private Body MyBody;
	private bool IsEating=false;
	private GameObject Players;
	public float TimeMove=0.1f;

	Vector2 dir = Vector2.right;
	// Use this for initialization
	void Start () {
		
		Camera MainC = Camera.main;//(Camera)GameObject.FindGameObjectWithTag ("MainCamera");

		Players=(GameObject)Instantiate(Resources.Load ("player001"),SpownPoint,Quaternion.identity);
	
		MyBody = Players.AddComponent<Body> ();
		//MyBody = new Body ();
		MyBody.BodyInit (Players);

		MainC.transform.SetParent (Players.transform);
		//MainC.transform.LookAt (Players.transform);
		// Move the Snake every 300ms
	//	InvokeRepeating("Move", 0.3f, TimeMove);

	}
	// Update is called once per Frame
	void Update() {
		// Move in a new Direction?
		if (Input.GetKey (KeyCode.RightArrow))
			dir = Vector2.right;
		else if (Input.GetKey (KeyCode.DownArrow))
			dir = -Vector2.up;    // '-up' means 'down'
		else if (Input.GetKey (KeyCode.LeftArrow))
			dir = -Vector2.right; // '-right' means 'left'
		else if (Input.GetKey (KeyCode.UpArrow)) {
			dir = Vector2.up;
		}
	}
	// Update is called once per frame

	void FixedUpdate() {

		// Move head into new direction
		//Players.transform.Translate(dir*Speed);
		//Players.transform.position=Vector2.Lerp(Players.transform.position,(Vector2)Players.transform.position+dir*Speed,0.5f);
		Players.transform.position=(Vector2)Players.transform.position+dir;
	//	Debug.Log ("players=" + Players.transform.position.x);

		//yield return new  WaitForSeconds (TimeMove);
		//Players.transform.Rotate (NextTransformList [i].right);
		MyBody.BodyFollow (Players.transform,ref IsEating,Speed);


	}

	//OnCollisionEnter2D   
	//OnTriggerEnter2D   
//	void OnCollisionEnter2D(Collider2D coll) {
	void OnTriggerEnter(Collider coll) {
		Debug.Log ("ccccc started OnTriggerEnter2D");
		// Food?
		if (coll.name.StartsWith("trap")) {
			// Get longer in next Move call
			IsEating = true;

			// Remove the Food
			Destroy(coll.gameObject);
		}
		// Collided with Tail or Border
		else {
			// ToDo 'You lose' screen
		}
	}
	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log ("ccccc started OnTriggerEnter2D");
		// Food?
		if (coll.name.StartsWith("trap")) {
			// Get longer in next Move call
			IsEating = true;

			// Remove the Food
			Destroy(coll.gameObject);
		}
		// Collided with Tail or Border
		else {
			// ToDo 'You lose' screen
		}
	}
		


}
