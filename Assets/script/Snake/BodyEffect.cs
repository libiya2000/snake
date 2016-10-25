using UnityEngine;
using System.Collections;

public class BodyEffect : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D coll) {
		//	Debug.Log ("ccccc started OnTriggerEnter2D");
		// Food?


		if (coll.name.StartsWith("Enermy")) {

			gameObject.SendMessageUpwards("BodyMessageDead", true);

		}
		//	if (coll.name.StartsWith("Enermy_Bullet")) {

		//		ONDead(true);

		//		}

	}
}
