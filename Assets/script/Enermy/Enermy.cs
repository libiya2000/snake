using UnityEngine;
using System.Collections;
using System;
public class Enermy : MonoBehaviour {

	public int Health ;
	public int full=50;

	// Use this for initialization
	void Start () {
		Health = full;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D coll) {
		//	Debug.Log ("ccccc started OnTriggerEnter2D");
		// Food?

		if (coll.name.StartsWith("Snake_bullet")) {
			
			int power = coll.gameObject.GetComponent<BulletEffect> ().ThePower;
			if (power > Health)
				Health = 0;
			else
				Health -= power;
			
			Destroy (coll.gameObject);
			UI_Data.UI_DataInstance.setEnermyStates(full,Health);
			if (Health == 0) {
				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MainGame> ().GamePass (1);
				Destroy (this.transform.gameObject);
			}
		}
		if (coll.name.StartsWith("edge")) {

			Destroy (this.transform.gameObject);

		}
		//	if (coll.name.StartsWith("Enermy_Bullet")) {

		//		ONDead(true);

		//		}

	}
}
