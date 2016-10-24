using UnityEngine;
using System.Collections;

public class Skill001 : MonoBehaviour {

	public	GameObject SkillMaster;
	public	GameObject Bullet;
	public 	int speed=50;

	// Use this for initialization
	void Start () {
	
		InvokeRepeating ("Shoot",5f,3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Shoot()
	{
		GameObject shootOne =(GameObject)Instantiate (Resources.Load (Bullet.name), SkillMaster.transform.position, Quaternion.identity);
		//shootOne.transform.eulerAngles = 45;

		Vector2 VV=new Vector2((float)Random.Range(-10000,10000),(float)Random.Range(-10000,10000));
	
		//Debug.Log (VV.x + ","+VV.y);

		shootOne.GetComponent<Rigidbody2D> ().velocity= (Vector2)VV.normalized * speed;
		//Vector2 VV=new Vector2(0,1);
		//shootOne.transform.Translate (VV.normalized * speed*Time.deltaTime,Space.World);

	}
}
