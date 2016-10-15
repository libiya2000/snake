using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Texture2D TTT;
		//TTT= (Texture2D)Resources.Load ("background001");
		//Sprite MainMap= Sprite.Create(TTT,new Rect(0, 0, TTT.width, TTT.height),new Vector2(0,0));

		Debug.Log ("11111111111111111");
		Map.MayInstance.init ();

		Debug.Log ("666666666666");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
