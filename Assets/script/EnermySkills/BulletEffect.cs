﻿using UnityEngine;
using System.Collections;

public class BulletEffect : MonoBehaviour {
	public int ThePower=1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D coll) {
		//	Debug.Log ("ccccc started OnTriggerEnter2D");
		// Food?

		if (coll.name.StartsWith("edge")) {

			Destroy (this.transform.gameObject);

		}

	}
}
