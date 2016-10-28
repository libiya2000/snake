using UnityEngine;
using System.Collections;
using System; 
using System.Collections.Generic;

public class Snakes : MonoBehaviour {

	public GameObject SnakeHead;
	public float  Speed=1.5f;
	public UInt32 BodyLenth = 3;
	public bool  GameStop=false;

	private Body MyBody;
	private bool IsEating=false;
	private bool IsDead=false ;
	public float TimeMove=0.1f;
	private Vector2 SpownPoint = new Vector2 (0, 0);
	private UInt64 EnergyNeed = 5;
	private Vector2 dir = Vector2.right;


	public delegate void DeadHandler(bool IsDead_);
	public  event DeadHandler ONDead;
	// Use this for initialization
	void Start () {
		//EasyButton.Dead += Dead;	
		ONDead+=ONDeadtoBorn;
		Camera MainC = Camera.main;//(Camera)GameObject.FindGameObjectWithTag ("MainCamera");

		//SnakeHead=(GameObject)Instantiate(Resources.Load ("player001"),SpownPoint,Quaternion.identity);

		//SnakeHead = transform.GetComponent<GameObject> ();
		//SnakeHead.transform.position = SpownPoint;
		MyBody = SnakeHead.AddComponent<Body> ();
		//MyBody = new Body ();
		MyBody.BodyInit (SnakeHead);

		MainC.transform.SetParent (SnakeHead.transform);
		//MyUI = UI_Data.UI_DataInstance;

		//MainC.transform.LookAt (Players.transform);
		// Move the Snake every 300ms
		//	InvokeRepeating("Move", 0.3f, TimeMove);
		UI_Data.UI_DataInstance.ShootButton.onClick.AddListener(delegate {
			Shoot();
		});
	}
	// Update is called once per Frame
	void Update() {
		
	}
	// Update is called once per frame

	void FixedUpdate() {
		if (GameStop)
			return;
		SnakeHead.transform.position=(Vector2)SnakeHead.transform.position+Speed*dir.normalized;
		MyBody.BodyFollow (SnakeHead.transform,ref IsEating,Speed);


	}
	public void BodyMessageDead(bool IsDead_)
	{
		ONDeadtoBorn (IsDead_);
	}

	public  void ReLive()
	{
		ONDeadtoBorn (true);
	}
	void 	ONDeadtoBorn(bool IsDead_)
	{
		//Debug.Log ("ccccc reBorn"); //dead punish
		if (UI_Data.UI_DataInstance.Scores.Red > EnergyNeed)
			UI_Data.UI_DataInstance.Scores.Red -= EnergyNeed;
		else
			UI_Data.UI_DataInstance.Scores.Red=0;
		UI_Data.UI_DataInstance.setbuttonText ((UI_Data.UI_DataInstance.Scores.Red/EnergyNeed).ToString());

		UI_Data.UI_DataInstance.setStates ();

		MyBody.BodyRemoveALL ();
		SnakeHead.transform.position = SpownPoint;
		MyBody.BodyInit (SnakeHead);
		IsDead = false;
	}
	//OnCollisionEnter2D   
	void OnTriggerEnter2D(Collider2D coll) {
	//	Debug.Log ("ccccc started OnTriggerEnter2D");
		// Food?
		if (GameStop)
			return;
		if (coll.name.StartsWith("Food")) {
			// Get longer in next Move call
			IsEating = true;
			UI_Data.UI_DataInstance.setStates();
			//	UI_Data.UI_DataInstance.Scores.All += 1;
			// Remove the Food
			Destroy(coll.gameObject);
			UI_Data.UI_DataInstance.setbuttonText ((UI_Data.UI_DataInstance.Scores.Red/EnergyNeed).ToString());
		}
		if (coll.name.StartsWith("edge")) {
			
			ONDead(true);

		}
		if (coll.name.StartsWith("Enermy")) {

			ONDead(true);

		}
	//	if (coll.name.StartsWith("Enermy_Bullet")) {

	//		ONDead(true);

//		}
	
	}
	public void  Shoot()
	{
		if (UI_Data.UI_DataInstance.Scores.Red >= EnergyNeed) {
			UI_Data.UI_DataInstance.Scores.Red -= EnergyNeed;
			UI_Data.UI_DataInstance.setbuttonText ((UI_Data.UI_DataInstance.Scores.Red/EnergyNeed).ToString());

			GameObject shootOne = (GameObject)Instantiate (Resources.Load ("Snake_Bullet_001"), this.transform.position, Quaternion.identity);
			//set bollet power
			int power=MyBody.BodyList.Count+1;
			shootOne.GetComponent<BulletEffect>().ThePower = power;
			shootOne.GetComponent<Rigidbody2D> ().velocity = (Vector2)dir.normalized * 300;
		}
	}
	void OnEnable()  

	{    
		EasyJoystick.On_JoystickMove += OnJoystickMove;  
	}  
	//  此函数是摇杆移动中所要处理的事
	void OnJoystickMove(MovingJoystick move)  
	{    
		if (move.joystickName != "Myjoystick")       //  在这里的名字new joystick 就是上面所说的很重要的名字，在上面图片中joystickName的你修改了什么名字，这里就要写你修改的好的名字（不然脚本不起作用）。
		{  
			return;  
		}  

		float PositionX = move.joystickAxis.x;       //   获取摇杆偏移摇杆中心的x坐标
		float PositionY = move.joystickAxis.y;      //    获取摇杆偏移摇杆中心的y坐标

		if (PositionY != 0 || PositionX != 0)  
		{   
			dir.x = PositionX;
			dir.y = PositionY;
			//ONDead(true);
		}  
	} 

	void OnDestroy(){
		ONDead-=ONDeadtoBorn;
	}
}
