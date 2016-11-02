using UnityEngine;
using System.Collections;
using System;
public class Enermy_level2 : MonoBehaviour {

	public int Health ;
	public int full=500;
	private int defense=1;
	private  int SaveHealth=0;

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
			string[] strs = coll.name.Split(new string[]{"_"},StringSplitOptions.None);
			calculate (strs [2]);
			int Damage = power - defense;
			Health = Math.Min (SaveHealth * power + Health, full);

			Health = Math.Min (Health - Damage, 0);

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

	}

	private  void calculate (string type)
	{
		SaveHealth=0;
		if(type =="red")			
			defense=1;
		else if (type == "blue")
			defense+=10;
		else if (type == "green")
			SaveHealth=1;
		else
			return;
	}
}
