using UnityEngine;
using System.Collections;
using System; 
using System.Collections.Generic;
using System.IO;
using LitJson;

public class Map : MonoBehaviour {
	//单例模式
	public static Map MyInstance;
	// Use this for initialization
	public  List<GameObject> SpriteList=new List<GameObject>();
	public int MaxSpawnFood=200;
	public Vector2 BossSpownpoint;
	public int TimeLimit=0;
	private  string EnermyBoss;
	private string MapType;
	private List<KeyValuePair<string,Vector2>> TheMap=new List<KeyValuePair<string,Vector2>>();
	private GameObject MainMap;
	private  Dictionary<string, string> Dic_Map = new Dictionary<string, string>();  
	//private SortedList<string,Vector2>  TheMap=new SortedList<string, Vector2>();//degine a dictionary一个字典
	static Map()
	{
	//	Debug.Log ("333333333333333");
		UnityEngine.GameObject go = new UnityEngine.GameObject("Map");
		DontDestroyOnLoad(go);
		MyInstance = go.AddComponent<Map>();
	}
	//active Sington  
	public void init()
	{
		
	}
	void Start () {
	//	Debug.Log ("444444444444444");
		SpriteList.Clear ();
		TheMap.Clear ();
		LoadNewMap (GlobalData.MyGlobalData.SelectLevel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	public bool InitMap()
	{


		TheMap.Add (new KeyValuePair<string, Vector2>("Food_red_001", new Vector2 (100, 200)));
		TheMap.Add (new KeyValuePair<string, Vector2>("Food_red_001", new Vector2 (300, 100)));
		TheMap.Add (new KeyValuePair<string, Vector2>("Food_green_001", new Vector2 (250, 250)));
		TheMap.Add (new KeyValuePair<string, Vector2>("Food_green_001", new Vector2 (200, 200)));
		TheMap.Add (new KeyValuePair<string, Vector2>("Food_blue_001", new Vector2 (200, 300)));
	


		//TheMap.Add("trap001",(100,200));
	
		InvokeRepeating ("TimeCountDown", 1, 1);
		LoadFixMap (TheMap, MapType);
		SpawnRandomFood (MaxSpawnFood);
		return true;
	}

	public bool LoadFixMap(List<KeyValuePair<string,Vector2>> The_Map,string MapTypes)
	{

		GameObject Things;
		SpriteList.Add ((GameObject)Instantiate (Resources.Load (MapTypes), new Vector2 (0, 0), Quaternion.identity));
		if (EnermyBoss == "Enermy002") {
			SpriteList.Add ((GameObject)Instantiate(Resources.Load (EnermyBoss),new Vector2(300,-300),Quaternion.identity));
		}
		SpriteList.Add ((GameObject)Instantiate(Resources.Load (EnermyBoss),BossSpownpoint,Quaternion.identity));
	
		foreach(KeyValuePair<string,Vector2> KV in The_Map)
		{
			/// TTT = (Texture2D)Resources.Load (KV.Key);
			//Sprite Things= Sprite.Create(TTT,new Rect(0, 0, TTT.width, TTT.height),KV.Value);
			//Debug.Log ("xxxxx");
			 Things=(GameObject)Instantiate(Resources.Load (KV.Key),KV.Value,Quaternion.identity);
			SpriteList.Add (Things);
		}

		return true;
	}
	void SpawnRandomFood(int number) {
		// x position between left & right border
		//Sprite backgrond=MainMap.GetComponent<SpriteRenderer>().sprite;
		string	Resname;
		int x, y, type;
		int i = number;
		while(i>0)
		{
			i--;
			x= (int)UnityEngine.Random.Range(-512f,512f);
			y= (int)UnityEngine.Random.Range(-512f,512f);
			//x= UnityEngine.Random.Range((int)backgrond.rect.xMin,backgrond.rect.xMax);
		//	Debug.Log ("55555555555555555555555555"+backgrond.rect.xMin +backgrond.rect.xMax);
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

			GameObject foods=(GameObject)	Instantiate(Resources.Load (Resname),new Vector2(x,y),Quaternion.identity);
			SpriteList.Add (foods);
	//	Instantiate(food,			new Vector2(x, y),			Quaternion.identity); // default rotation
		}
	}

	void RemoveFood(GameObject TheFood)
	{
		SpriteList.Remove (TheFood);
		DestroyObject (TheFood);
	
	}

	public void  TimeCountDown()
	{
		TimeLimit--;
		UI_Data.UI_DataInstance.SetTimeLimit (TimeLimit.ToString ());
		if (TimeLimit == 0) {
			CancelInvoke ();

			UI_Data.UI_DataInstance.GameOver ();
		}
		if(SpriteList.Count<MaxSpawnFood)
			SpawnRandomFood ((int)10);
	}

	public  void LoadNewMap(int Level)
	{
		
		CancelInvoke ();	
		CloseOldMap ();
		Dic_Map.Clear ();

		string FolderName = Path.Combine(Application.dataPath, "Map");  // "Map_00" + Level + ".json";
		string 	FileName=Path.Combine(FolderName,  "Map_00" + Level + ".json"); 
			if(!Directory.Exists(FolderName)) {  
			return;  
			}  
		if (File.Exists (FileName)) {  
			FileStream fs = new FileStream (FileName, FileMode.Open);  
			StreamReader sr = new StreamReader (fs);  
			JsonData values = JsonMapper.ToObject (sr.ReadToEnd ());

			foreach (var key in values.Keys) {  
				Dic_Map.Add (key, values [key].ToString ());  
				Debug.Log (key+"  "+values [key].ToString());
			}  
			if (fs != null) {  
				fs.Close ();  
			}  
			if (sr != null) {  
				sr.Close ();  
			}  
		} else
			return;
		SpriteList.Clear ();
		TheMap.Clear ();
		MapType=Dic_Map ["background"];
		EnermyBoss = Dic_Map ["bossname"];

		string[] p=new string [2];
		p = Dic_Map ["bossSpownPoint"].Split(',');
		BossSpownpoint.x = int.Parse (p [0]);
		BossSpownpoint.y = int.Parse (p [1]);

		//Dic_Map.TryGetValue( "background",out MapType);

		//string xxx;
		//Dic_Map.TryGetValue ("timelimit", out xxx);
		TimeLimit = int.Parse(Dic_Map ["timelimit"]);
		InitMap();
		InvokeRepeating ("TimeCountDown", 1, 1);
	}

	void CloseOldMap()
	{
		int NUM = SpriteList.Count;
		GameObject G;
		for (int i=NUM-1;i>0;i--)
		{
			G = SpriteList [i];
			SpriteList.RemoveAt(i);
			Destroy (G);

		}
			
	}
}
