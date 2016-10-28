using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {

	// Use this for initialization
	private GameObject MainPlayer;
	private Vector2 SpownPoint = new Vector2 (0, 0);

	void Start () {
		//Texture2D TTT;
		//TTT= (Texture2D)Resources.Load ("background001");
		//Sprite MainMap= Sprite.Create(TTT,new Rect(0, 0, TTT.width, TTT.height),new Vector2(0,0));
		Instantiate(Resources.Load ("background000"),SpownPoint,Quaternion.identity);
		Debug.Log ("11111111111111111");
		Map.MyInstance.init();
		UI_Data.UI_DataInstance.init ();
		MainPlayer=(GameObject)Instantiate(Resources.Load ("head001"),SpownPoint,Quaternion.identity);

		Debug.Log ("666666666666");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GamePass(int level)
	{
		Debug.Log ("Pass level " + level);
		MainPlayer.GetComponent<Snakes> ().GameStop = true;
		UI_Data.UI_DataInstance.ShowChooseNextLevel (level);

	}
	public void GamesStop()
	{
		MainPlayer.GetComponent<Snakes> ().GameStop = true;
	}

	public void GamesBegin()
	{
		MainPlayer.GetComponent<Snakes> ().GameStop = false ;
		MainPlayer.GetComponent<Snakes> ().ReLive ();
	}
}
