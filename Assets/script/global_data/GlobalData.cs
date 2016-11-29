using UnityEngine;
using System.Collections;

public class GlobalData : MonoBehaviour {

 
	public  static GlobalData MyGlobalData = null;  
	public int SelectLevel = 1;


	static GlobalData()
	{
		if (MyGlobalData == null) {
			UnityEngine.GameObject go = new UnityEngine.GameObject ("GlobalData");
			DontDestroyOnLoad (go);
			MyGlobalData = go.AddComponent<GlobalData> ();
		}

	}

	public void init()   
	{  
		CryptoPrefs.SetInt ("PassLevel", 1);

		CryptoPrefs.Save ();
	}  
	void Awake()
	{

	}
	void Start (){
		MyGlobalData.init ();
	}
	public int GetCurrentLevel()
	{
		return CryptoPrefs.GetInt ("PassLevel");
	}
	public  void SetMaxPassLevel( int  Value)
	{
		int level = Mathf.Max (Value, this.GetCurrentLevel ());
		CryptoPrefs.SetInt ("PassLevel",level);
		return;
	}
	public bool IsCurrectLevelChoose(int Choosed)
	{
		if (Choosed > (this.GetCurrentLevel ()))
			return false;
		else
			return true;
			
	}
}
