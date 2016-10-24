using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public struct ScoreType
{
	public	UInt64 All;
	public	UInt64 Red ;
	public	UInt64 Bule;
	public	UInt64 Yellow;
}

public class UI_Data : MonoBehaviour {

	//单例模式
	public static UI_Data UI_DataInstance;
	//public UInt64[] ScoreList= new UInt64[sizeof (ScoreType)] ;

	public ScoreType  Scores= new ScoreType();

//	public GameObject MyCanvas =GameObject.FindGameObjectWithTag("Canvas");
//	public Image ToolBar;
	public Slider SkillSlieder;
	public Button ShootButton;


	public delegate void UIStatesHandler(float V);
	public  event UIStatesHandler ONStatesChange;

	static UI_Data()
	{
		UnityEngine.GameObject go = new UnityEngine.GameObject("UI");
		//DontDestroyOnLoad(go);
		UI_DataInstance = go.AddComponent<UI_Data>();
	}
	public void init()
	{
		SkillSlieder = GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("Slider").gameObject.GetComponent<Slider>();
		ShootButton = GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("Button").gameObject.GetComponent<Button>();
	}
	// Use this for initialization
	void Start () {
		 
		ONStatesChange += UpdataSlieder;
		Scores.All = 0;
		Scores.Bule = 0;
		Scores.Red = 0;
		Scores.Yellow = 0;

	
	//	ToolBar.fillAmount = 1;
		SkillSlieder.interactable = false;
		SkillSlieder.value = 0.0f;
	}
	public void setStates()
	{
		Scores.Red++;
		float xxx = (float)((decimal)Scores.Red / 5);
		ONStatesChange (xxx);
	}

	void  UpdataSlieder(float NewValue)
	{
		SkillSlieder.value = NewValue;
		Debug.Log ("newwwwwwwwwww"+NewValue);
	}
	// Update is called once per frame
	void Update () {
	//	ToolBar.fillAmount -= 1.0f/30 * Time.deltaTime;
		//ToolBar.fillAmount =Scores.All;

	}
}
