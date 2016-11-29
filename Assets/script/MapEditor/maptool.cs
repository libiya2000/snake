using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;
public class maptool : MonoBehaviour {
	
	private static string mFolderName;  
	private static string mFileName;  
	private  Dictionary<string, string> Dic_Value = new Dictionary<string, string>();  
	// Use this for initialization
	void Start () {
		CreateMap ();
	}

	private  string FileName {  
		get {  
			return Path.Combine(FolderName, mFileName);  
		}  
	}  

	private  string FolderName {  
		get {  
			return Path.Combine(Application.dataPath, mFolderName);  
		}  
	}  

	void  CreateMap()
	{
		for (int i = 1; i < 10; i++) {
			MapInit ("Map", "Map_00" + i+".json");
			Dic_Value.Add ("background", "background00" + i);
			Dic_Value.Add ("timelimit", "30" + i);
			Dic_Value.Add ("bossname", "Enermy00" + i);
			Dic_Value.Add ("bossSpownPoint", "200,300");

			Save (Dic_Value);
		}
	}
	//初始化方法 如有需要，可重载初始化方法  
	  void MapInit(string pFolderName, string pFileName) {  
		
		mFolderName = pFolderName;  
		mFileName = pFileName;  
		Dic_Value.Clear();  
		//Read();  
	}  
	// Update is called once per frame
	void Update () {
	
	}
	/*
	private  void Read() {  
		if(!Directory.Exists(FolderName)) {  
			Directory.CreateDirectory(FolderName);  
		}  
		if(File.Exists(FileName)) {  
			FileStream fs = new FileStream(FileName, FileMode.Open);  
			StreamReader sr = new StreamReader(fs);  
			JsonData values = JsonMapper.ToObject(sr.ReadToEnd());  
			foreach(var key in values.Keys) {  
				Dic_Value.Add(key, values[key].ToString());  
			}  
			if(fs != null) {  
				fs.Close();  
			}  
			if(sr != null) {  
				sr.Close();  
			}  
		}  
	} */
	//将Dictionary数据转成json保存到本地文件  
	private  void Save(Dictionary<string, string> Dic) {  
		string values = JsonMapper.ToJson(Dic);  

		if(!Directory.Exists(FolderName)) {  
			Directory.CreateDirectory(FolderName);  
		}  
		FileStream file = new FileStream(FileName, FileMode.Create);  
		byte[] bts = System.Text.Encoding.UTF8.GetBytes(values);  
		file.Write(bts,0,bts.Length);  
		Debug.Log(file.Name);  
		if(file != null) {  
			file.Close();  
		}  

	}  
}
