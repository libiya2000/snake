using UnityEngine;
using System.Collections;
using System;

public class Enermy_level4 : MonoBehaviour {



	public int Health ;
	public int full=700;
	private int defense=1;
	private  int SaveHealth=0;
	private Animator A1;
	public  float A1speed = 0.7f;
	public  float Movespeed=40f;
	public int AnimaStatus = 0;

	private GameObject MainP;
	private  GameObject effectOBJ;
	private Transform childt;
	// Use this for initialization
	void Start () {
		Health = full;
		A1 = this.transform.gameObject.GetComponent<Animator> ();
		A1.speed = A1speed;
		MainP = GameObject.FindGameObjectWithTag ("MainCamera");
		InvokeRepeating ("MoveToPlayer",2f,15f);
		InvokeRepeating ("skillCrash",3f,8f);

		Transform childt =	transform.FindChild ("FireBall");
		childt.gameObject.SetActive (false);
	//	effectOBJ=transform.FindChild ("霸王龙_3").gameObject;
	//	effectOBJ.SetActive( false);
	}

	// Update is called once per frame
	void Update () {
		A1.speed = A1speed;
	
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

			if ((float)(decimal)Health / full < 0.3f)
				Do_effect ();
			Destroy (coll.gameObject);
			UI_Data.UI_DataInstance.setEnermyStates(full,Health);
			if (Health == 0) {
				MainP.GetComponent<MainGame> ().GamePass (4);
				Destroy (this.transform.gameObject);
			}
		}
		if (coll.name.StartsWith("edge")) {
			StartCoroutine_Auto( Do_effect_crash ());

			transform.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(0,0);
			Invoke ("MoveToPlayer",0.5f);

			//Destroy (this.transform.gameObject);

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
	private IEnumerator Do_effect(){
		effectOBJ.SetActive (true);
		yield return new WaitForSeconds(0.5f);
		effectOBJ.SetActive (false);
	}
	IEnumerator Do_effect_crash(){
		effectOBJ.SetActive (true);
		yield return new WaitForSeconds(0.5f);
		effectOBJ.SetActive (false);
	}
	void MoveToPlayer()
	{
		Vector3	Tar = MainP.GetComponent<MainGame>().MainPlayer.transform.position;
		//Vector2 TarP = new Vector2 (Tar.x, Tar.y);
		A1.speed = A1speed;
		walkRandom (Tar);
	}

	private void  walkRandom(Vector3 TragetPoint)
	{
		Vector3 V;
		V =TragetPoint-transform.position;
		//rotation = Quaternion.LookRotation(target - transform.position);
		//transform.rotation = rotation;
		//float angle;
		//angle = Vector2.Angle (new Vector2( transform.position.x,transform.position.y), TragetPoint);
		//transform.eulerAngles=new Vector3(0,0 ,angle);
		//Debug.Log(V +"++"+Quaternion.FromToRotation(Vector3.zero,V));
		transform.rotation=Quaternion.FromToRotation(Vector3.right,V);//.SetLookRotation(V);//SetFromToRotation(transform.position,TragetPoint);// Quaternion.LookRotation(V);
		//Debug.Log(transform.rotation.eulerAngles);
		transform.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(V.x,V.y).normalized * Movespeed;
		//transform .position 
	}

	private void skillCrash()
	{
		AnimaStatus = 1;
		//AnimaStatus=A1.GetInteger ("Status");
		transform.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(0,0);
		A1.Play ("FireSkill");

		//Debug.Log (AnimaStatus + "ooooooooooooooooo");
		//A1.SetInteger ("Status", AnimaStatus);
		//transform.gameObject.GetComponent<Rigidbody2D> ().velocity *= 5f;
		//A1.speed = A1speed*3;
	}
	public void fire()
	{
		StartCoroutine_Auto( Do_fire ());

	}
	public IEnumerator Do_fire()
	{
		Transform childt =	transform.FindChild ("FireBall");
		childt.gameObject.SetActive (true);
		childt.GetComponent<ParticleSystem> ().Play ();
		//childt.rotation = Quaternion.FromToRotation (Vector3.right, V);
		while(childt.GetComponent<ParticleSystem> ().isPlaying)
			yield return null;
		childt.gameObject.SetActive (false);
		A1.Play ("walk");
		MoveToPlayer ();
	}
}

