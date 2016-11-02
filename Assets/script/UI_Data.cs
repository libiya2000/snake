using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public struct ScoreType
{
	public	UInt64 All;
	public	UInt64 Red ;
	public	UInt64 Blue;
	public	UInt64 Green;
}

public class UI_Data : MonoBehaviour {

	//单例模式
	public static UI_Data UI_DataInstance;
	//public UInt64[] ScoreList= new UInt64[sizeof (ScoreType)] ;

	public ScoreType  Scores= new ScoreType();

//	public GameObject MyCanvas =GameObject.FindGameObjectWithTag("Canvas");
//	public Image ToolBar;
	public Slider SkillSliederGreen;
	public Slider SkillSliederBlue;
	public Slider SkillSliederRed;
	public Slider EnermyBlood;
	public Slider PoweSlieder;
	public Button ShootButtonGreen;
	public Button ShootButtonBlue;
	public Button ShootButtonRed;
	public Button QuitButton;
	public Canvas CanvasOne;
	public Canvas GameOverCanvas;
	public Text TimeLimite;

	public Canvas NextCanvas;
	public int TheLevel=1;

//	public delegate void UIStatesHandler(float V);
//	public  event UIStatesHandler ONStatesChange;

	public  UInt64 skillPowerMax=20;
	static UI_Data()
	{
		UnityEngine.GameObject go = new UnityEngine.GameObject("UI");
		//DontDestroyOnLoad(go);
		UI_DataInstance = go.AddComponent<UI_Data>();
	}
	public void init()
	{
		CanvasOne = (UnityEngine.Canvas)GameObject.FindGameObjectWithTag ("Canvas").gameObject.GetComponent<Canvas>();
		SkillSliederGreen = GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("SliderGreen").gameObject.GetComponent<Slider>();
		ShootButtonGreen=SkillSliederGreen.transform.FindChild ("Button").gameObject.GetComponent<Button> ();
		SkillSliederGreen.value = 0;
		SkillSliederBlue = GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("SliderBlue").gameObject.GetComponent<Slider>();
		ShootButtonBlue=SkillSliederBlue.transform.FindChild ("Button").gameObject.GetComponent<Button> ();
		SkillSliederBlue.value = 0;
		SkillSliederRed = GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("SliderRed").gameObject.GetComponent<Slider>();
		ShootButtonRed=SkillSliederRed.transform.FindChild ("Button").gameObject.GetComponent<Button> ();
		SkillSliederRed.value = 0;
		//ShootButton = GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("Button").gameObject.GetComponent<Button>();
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
			Restart();	
		});
		GameOverCanvas.gameObject.SetActive (false);
		NextCanvas.gameObject.SetActive (false);

		//	ToolBar.fillAmount = 1;

	}
	// Use this for initialization
	void Start () {
		 
		//ONStatesChange += UpdataSlieder;
		Scores.All = 0;
		Scores.Blue = 0;
		Scores.Red = 0;
		Scores.Green = 0;

	

	}
	public void setStates(string type)
	{	
		
		if(type =="red")			
			Scores.Red++;
		else if (type == "blue")
			Scores.Blue++;
		else if (type == "green")
			Scores.Green++;
		else
			return;
		Scores.Red = Math.Min (Scores.Red, skillPowerMax);
		Scores.Blue = Math.Min (Scores.Blue, skillPowerMax);
		Scores.Green = Math.Min (Scores.Green, skillPowerMax);

	//	Debug.Log (Scores.Green + " " + Scores.Blue + " " + Scores.Red);
		UpdateSkillSlieder ();

	}
	public void UpdateSkillSlieder()
	{

		SkillSliederRed.value = (float)((decimal)Scores.Red/ skillPowerMax);
		SkillSliederBlue.value = (float)((decimal)Scores.Blue/skillPowerMax);
		SkillSliederGreen.value = (float)((decimal)Scores.Green/skillPowerMax);
		//Debug.Log (SkillSliederRed.value +" "+  SkillSliederBlue.value +" "+ SkillSliederGreen.value);
		
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


	// Update is called once per frame
	void Update () {
	//	ToolBar.fillAmount -= 1.0f/30 * Time.deltaTime;
		//ToolBar.fillAmount =Scores.All;

	}

	public void setbuttonText(UInt64 EnergyNeed)
	{
		ShootButtonRed.transform.FindChild ("Text").gameObject.GetComponent<Text>().text=(Scores.Red/EnergyNeed).ToString();
		ShootButtonBlue.transform.FindChild ("Text").gameObject.GetComponent<Text>().text=(Scores.Blue/EnergyNeed).ToString();
		ShootButtonGreen.transform.FindChild ("Text").gameObject.GetComponent<Text>().text=(Scores.Green/EnergyNeed).ToString();

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
		CanvasOne.gameObject.SetActive (true);
		NextCanvas.gameObject.SetActive (false);
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MainGame> ().GamesBegin();
	}
	public void	Restart()
	{
		Map.MyInstance.LoadNewMap (TheLevel);
		CanvasOne.gameObject.SetActive (true);
		GameOverCanvas.gameObject.SetActive (false);
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MainGame> ().GamesBegin();

	}
	public void GameOver()
	{
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MainGame> ().GamesStop();
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
