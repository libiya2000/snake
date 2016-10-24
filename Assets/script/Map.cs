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
	private GameObject MainMap;
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

		TheMap.Add (new KeyValuePair<string, Vector2>("Food_red_001", new Vector2 (100, 200)));
		TheMap.Add (new KeyValuePair<string, Vector2>("Food_red_001", new Vector2 (300, 100)));
		TheMap.Add (new KeyValuePair<string, Vector2>("Food_green_001", new Vector2 (250, 250)));
		TheMap.Add (new KeyValuePair<string, Vector2>("Food_green_001", new Vector2 (200, 200)));
		TheMap.Add (new KeyValuePair<string, Vector2>("Food_blue_001", new Vector2 (200, 300)));
		TheMap.Add (new KeyValuePair<string, Vector2>("Enermy001", new Vector2 (-200, -300)));

	
		//TheMap.Add("trap001",(100,200));
		InitMap();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitMap()
	{
		GerneralMap ();
	}
	public bool GerneralMap()
	{
		LoadFixMap (TheMap, MapType);
		SpawnRandomFood ((int)100);
		return true;
	}

		public bool LoadFixMap(List<KeyValuePair<string,Vector2>> The_Map,string MapTypes)
	{

		GameObject Things;
		MainMap=(GameObject)Instantiate(Resources.Load (MapTypes),new Vector2(0,0),Quaternion.identity);
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
	void SpawnRandomFood(int number) {
		// x position between left & right border
		Sprite backgrond=MainMap.GetComponent<SpriteRenderer>().sprite;
		string	Resname;
		int x, y, type;
		int i = number;
		while(i>0)
		{
			i--;
			x= (int)UnityEngine.Random.Range(-512f,512f);
			y= (int)UnityEngine.Random.Range(-512f,512f);
			//x= UnityEngine.Random.Range((int)backgrond.rect.xMin,backgrond.rect.xMax);
			Debug.Log ("55555555555555555555555555"+backgrond.rect.xMin +backgrond.rect.xMax);
		// y position between top & bottom border
			//y =(int) UnityEngine.Random.Range(backgrond.rect.yMin,backgrond.rect.yMax);

			type = UnityEngine.Random.Range((int)1,(int)4);
		// Instantiate the food at (x, y)

		if (type == 1)
				Resname = "Food_blue_001";
		else if (type == 2)
			Resname = "Food_green_001";
		else
			Resname = "Food_red_001";
			

					
				
		Instantiate(Resources.Load (Resname),new Vector2(x,y),Quaternion.identity);
	//	Instantiate(food,			new Vector2(x, y),			Quaternion.identity); // default rotation
		}
	}

	void RemoveFood(GameObject TheFood)
	{
		SpriteList.Remove (TheFood);
		DestroyObject (TheFood);
	
	}



}
