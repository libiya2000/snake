using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI_Main : MonoBehaviour {

	// Use this for initialization

	public float Actionspeed=4;
	private GameObject Panels;
	private RectTransform MianRect;
	private Vector2 Tar =new Vector2( 0,0);
	void Start () {
		Panels = transform.FindChild ("MainWindow").gameObject;
		MianRect = Panels.GetComponent<RectTransform> ();
		//SceneManager.LoadScene ("PlayPlace");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (MianRect.anchoredPosition.y);
		if (MianRect.anchoredPosition.y != Tar.y) {

			MianRect.anchoredPosition=new Vector2(0,Mathf.Lerp(MianRect.anchoredPosition.y, Tar.y,Time.deltaTime*Actionspeed));
		}
	//		Debug.Log ("00000000000000000");
			//Panels.GetComponent<Rigidbody2D> ().gravityScale = 0;
	//		Panels.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
	//	}
			
	}

	public  void  StartGame()
	{
		PanelDown ();
	}
	public void  Help()
	{
		PanelUp ();
	}
	public  void Exit ()
	{
		Debug.Log ("quit");
		Application.Quit ();
	}
	public void  GameBacktoMain()
	{
		PanelZero ();
	}
	public void  HelpBacktoMain()
	{
		PanelZero ();
	}
	private void  PanelUp()
	{
		//Panels.transform.Translate (new Vector2 (0, 5000));
		Tar.y=2000f;
	//	StartCoroutine_Auto(Moveing());

	}
	private void  PanelDown ()
	{		
		//Panels.transform.Translate(new Vector2(0,-5000));
		Tar.y=-2000f;
		//StartCoroutine_Auto(Moveing());
	}
	private void  PanelZero ()
	{
		Tar.y=0f;		
	}
/*	private IEnumerator Moveing()
	{
		Panels.GetComponent<Rigidbody2D> ().velocity =Tar;
		yield return new WaitForSeconds(1f);
		Panels.GetComponent<Rigidbody2D> ().velocity = new Vector2(0,0);
	}
*/
	public void EnterGame(int level)
	{
		if(!permit(level))
			{
				Debug.Log("not allowed to use");
				return;
			}
			else
			{
			GlobalData.MyGlobalData.SelectLevel = level;
			SceneManager.LoadScene ("PlayPlace");
			}
	}
	private bool permit(int level)
	{
		return true;
	}
}
