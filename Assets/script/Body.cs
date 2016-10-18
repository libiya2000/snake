﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Internal;
public class Body : MonoBehaviour {

	//public GameObject MyPlayer;
	private GameObject BodyOne;
	//private List<Transform> NextTransformList=new List<Transform>();
	private List<Vector2> NextPositionList=new List<Vector2>();
	private List<Vector2> NextEulerAnglesList=new List<Vector2>();
	private List<GameObject> BodyList = new List<GameObject> ();
	private Vector2 BodyWidth;
	private int linedistence=20;

	public void BodyInit(GameObject head)
	{
		BodyList.Clear ();
		NextPositionList.Clear ();
	
		BodyWidth=new Vector2(head.GetComponent<SpriteRenderer>().sprite.rect.width,0);
		//init first body
		BodyOne=(GameObject)Instantiate(Resources.Load ("Body001"),(Vector2)head.transform.position+BodyWidth,Quaternion.identity);

		BodyList.Add (BodyOne);

		NextPositionList.Add (head.transform.position);
		NextPositionList.Add (BodyOne.transform.position);
		Debug.Log ("ccccc list0"+ "="+ NextPositionList [0].x+
			"list1"+ "="+ NextPositionList [1].x);
	}

	 
	public	void BodyFollow(Transform FirstT,ref bool IsEating,UInt32  speed)
	{
		//NextTransformList.Add (FirstT);


		Vector2 XXXTrans=new Vector2(FirstT.position.x,FirstT.position.y);


		//XXXTrans.DetachChildren ();


		NextPositionList.Insert (0, XXXTrans);
		if (NextPositionList.Count < BodyList.Count * linedistence+2)
			return;
			//if (Vector2.Distance (FirstT.transform.position, BodyList [0].transform.position) < BodyWidth.x)
			//	return;
		Debug.Log ("list0"+ "="+ NextPositionList [0].x+
			"list1"+ "="+ NextPositionList [1].x+
			"list2"+ "="+ NextPositionList [2].x);

		for(int i=1;i<BodyList.Count+1;i++) {
			
			//BodyList [i].transform.Translate (NextTransformList [i].position*speed*Time.deltaTime);
			//BodyList [i-1].transform.position=Vector2.Lerp(BodyList [i-1].transform.position,NextPositionList [i*linedistence],0.5f);
			BodyList [i-1].transform.position=(Vector2)(NextPositionList [i*linedistence]);

		//	Debug.Log ("list"+ i+"="+ NextPositionList [i].x);
			//BodyList [i].transform.Rotate (NextTransformList [i].right*speed*Time.deltaTime);

		}
		if (IsEating == true) {


			Vector2 xxx = NextPositionList [NextPositionList.Count - 1];
			GameObject BodyTail=(GameObject)Instantiate(Resources.Load ("Body001"),	xxx,Quaternion.identity);
			BodyList.Add(BodyTail);//BodyAdd ();
			IsEating=false ;
		} else {	
			if (NextPositionList.Count < linedistence * 2)
				return;
			NextPositionList.RemoveAt(NextPositionList.Count-1);//delet first transform

		}
	}

	public void BodyAdd()
	{
		
	}

	public void BodyRemove()
	{
		
	}

}