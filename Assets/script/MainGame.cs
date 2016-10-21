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

		Debug.Log ("11111111111111111");
		Map.MayInstance.init ();
		UI_Data.UI_DataInstance.init ();
		MainPlayer=(GameObject)Instantiate(Resources.Load ("Snake001"),SpownPoint,Quaternion.identity);

		Debug.Log ("666666666666");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
