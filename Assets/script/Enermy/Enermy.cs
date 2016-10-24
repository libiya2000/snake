using UnityEngine;
using System.Collections;

public class Enermy : MonoBehaviour {

	public int Health = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D coll) {
		//	Debug.Log ("ccccc started OnTriggerEnter2D");
		// Food?

		if (coll.name.StartsWith("Snake_bullet")) {
			
			Health--;
			if(Health==0)
				Destroy (this.transform.gameObject);
		}
		if (coll.name.StartsWith("edge")) {

			Destroy (this.transform.gameObject);

		}
		//	if (coll.name.StartsWith("Enermy_Bullet")) {

		//		ONDead(true);

		//		}

	}
}
