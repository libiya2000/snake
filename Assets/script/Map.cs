using UnityEngine;
using System.Collections;
using System; 
using System.Collections.Generic;

public class Map : MonoBehaviour {
	//单例模式
	public static Map MayInstance;
	// Use this for initialization
	public  List<GameObject> SpriteList=new List<GameObject>();
	private string MapType;
	private List<KeyValuePair<string,Vector2>> TheMap=new List<KeyValuePair<string,Vector2>>();
	//private SortedList<string,Vector2>  TheMap=new SortedList<string, Vector2>();//degine a dictionary一个字典
	static Map()
	{
	//	Debug.Log ("333333333333333");
		UnityEngine.GameObject go = new UnityEngine.GameObject("Map");
		DontDestroyOnLoad(go);
		MayInstance = go.AddComponent<Map>();
	}
	//active Sington  
	public void init()
	{
	}
	void Start () {
	//	Debug.Log ("444444444444444");
		SpriteList.Clear ();
		TheMap.Clear ();
		MapType = "background001";

		TheMap.Add (new KeyValuePair<string, Vector2>("trap002", new Vector2 (100, 200)));
		TheMap.Add (new KeyValuePair<string, Vector2>("trap001", new Vector2 (300, 100)));
		TheMap.Add (new KeyValuePair<string, Vector2>("trap002", new Vector2 (250, 250)));
		TheMap.Add (new KeyValuePair<string, Vector2>("trap002", new Vector2 (200, 200)));
		TheMap.Add (new KeyValuePair<string, Vector2>("trap002", new Vector2 (200, 300)));

		LoadFixMap (TheMap, MapType);
		//TheMap.Add("trap001",(100,200));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitMap()
	{
		
	}
	public bool GerneralMap()
	{
		
		return true;
	}

	public bool LoadFixMap(List<KeyValuePair<string,Vector2>> The_Map,string MapTypes)
	{

		GameObject Things;
		GameObject MainMap=(GameObject)Instantiate(Resources.Load (MapTypes),new Vector2(0,0),Quaternion.identity);
	//	Debug.Log ("55555555555555555555555555");
		SpriteList.Add (MainMap);
	//	Debug.Log ("rrrrrr");
		//Sprite.Instantiate ((UnityEngine.GameObject)Resources.Load (MapTypes), Vector3.zero,Quaternion.identity); //backgroad
		foreach(KeyValuePair<string,Vector2> KV in The_Map)
		{
			/// TTT = (Texture2D)Resources.Load (KV.Key);
			//Sprite Things= Sprite.Create(TTT,new Rect(0, 0, TTT.width, TTT.height),KV.Value);
			//Debug.Log ("xxxxx");
			 Things=(GameObject)Instantiate(Resources.Load (KV.Key),KV.Value,Quaternion.identity);

			//Sprite Things = Sprite.Instantiate ((Sprite)Resources.Load (KV.Key), new Vector3 (KV.Value.x, KV.Value.y, 0),
			//	Quaternion.identity);
			SpriteList.Add (Things);
		}

	//	Debug.Log ("eeeeee");
		return true;
	}
	void SpawnRandomFood(Sprite backgrond,Sprite food) {
		// x position between left & right border
		int x = (int) UnityEngine.Random.Range(backgrond.rect.xMin,backgrond.rect.xMax);
			
		// y position between top & bottom border
		int y = (int)UnityEngine.Random.Range(backgrond.rect.yMin,backgrond.rect.yMax);
			
		// Instantiate the food at (x, y)

	//	Instantiate(food,			new Vector2(x, y),			Quaternion.identity); // default rotation
	}

	void RemoveFood(GameObject TheFood)
	{
		SpriteList.Remove (TheFood);
		DestroyObject (TheFood);
	
	}



}
