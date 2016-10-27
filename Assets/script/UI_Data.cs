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
	public Slider EnermyBlood;
	public Slider PoweSlieder;
	public Button ShootButton;
	public Button QuitButton;
	public Canvas CanvasOne;
	public Canvas GameOverCanvas;
	public Text TimeLimite;

	public Canvas NextCanvas;
	public int TheLevel=1;

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
		CanvasOne = (UnityEngine.Canvas)GameObject.FindGameObjectWithTag ("Canvas").gameObject.GetComponent<Canvas>();
		SkillSlieder = GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("Slider").gameObject.GetComponent<Slider>();
		ShootButton = GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("Button").gameObject.GetComponent<Button>();
		EnermyBlood=GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("SliderEnermy").gameObject.GetComponent<Slider>();
		PoweSlieder=GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("SliderPower").gameObject.GetComponent<Slider>();
		TimeLimite=GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("TextTimeLimit").gameObject.GetComponent<Text>();

		QuitButton=GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("QuitButton").gameObject.GetComponent<Button>();
		QuitButton.onClick.AddListener (delegate {
			Quit ();
		});
		PoweSlieder.value = 0;
		EnermyBlood.value = 1f;
		NextCanvas = (UnityEngine.Canvas)GameObject.FindGameObjectWithTag ("NextCanvas").gameObject.GetComponent<Canvas>();
		GameOverCanvas = (UnityEngine.Canvas)GameObject.FindGameObjectWithTag ("GameOverCanvas").gameObject.GetComponent<Canvas>();
		NextCanvas.transform.FindChild ("ButtonNext").gameObject.GetComponent<Button> ().onClick.AddListener (delegate {
			LoadNextLevel();	
		});
		NextCanvas.transform.FindChild ("ButtonQuit").gameObject.GetComponent<Button> ().onClick.AddListener (delegate {
			Quit();	
		});
		GameOverCanvas.transform.FindChild ("Quit").gameObject.GetComponent<Button> ().onClick.AddListener (delegate {
			Quit();	
		});
		GameOverCanvas.transform.FindChild ("Restart").gameObject.GetComponent<Button> ().onClick.AddListener (delegate {
			LoadNextLevel();	
		});
		GameOverCanvas.gameObject.SetActive (false);
		NextCanvas.gameObject.SetActive (false);

		//	ToolBar.fillAmount = 1;
		SkillSlieder.interactable = false;
		SkillSlieder.value = 0.0f;
	}
	// Use this for initialization
	void Start () {
		 
		ONStatesChange += UpdataSlieder;
		Scores.All = 0;
		Scores.Bule = 0;
		Scores.Red = 0;
		Scores.Yellow = 0;

	

	}
	public void setStates()
	{
		Scores.Red++;
		float xxx = (float)((decimal)Scores.Red / 20);
		ONStatesChange (xxx);
	}

	public void setPowerStates(int full,int current)
	{
		int cc = current;
		if (current > full)
			cc = full;
		PoweSlieder.value= (float)((decimal)cc/ full);

	}
	public void setEnermyStates(int full,int current)
	{
		
		EnermyBlood.value= (float)((decimal)current/ full);

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

	public void setbuttonText(string SS)
	{
		Text  tt=	(UnityEngine.UI.Text)ShootButton.transform.FindChild ("Text").gameObject.GetComponent<Text>();
		tt.text = SS;
	}

	public void ShowChooseNextLevel(int Level)
	{
		CanvasOne.gameObject.SetActive (false);
		NextCanvas.gameObject.SetActive (true);
		TheLevel = 1 +	Level;
	}
	public void LoadNextLevel()
	{
		Map.MyInstance.LoadNewMap (TheLevel);
	}

	public void GameOver()
	{
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MainGame> ().GamePass (0);
		CanvasOne.gameObject.SetActive (false);
		GameOverCanvas.gameObject.SetActive (true);
	}
	public void SetTimeLimit(string T)
	{
		TimeLimite.text = T;

	}
	private void  Quit()
	{
		Debug.Log ("quit");
		Application.Quit ();
	}


}
