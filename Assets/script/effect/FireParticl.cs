using UnityEngine;
using System.Collections;

public class FireParticl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject other) {

		Debug.Log ("pppppppppppppppppppppppp");
		if ((other.name.StartsWith("body"))|| 
			(other.name.StartsWith("head"))){

			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MainGame> ().GamesBegin();	GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MainGame> ().GamesBegin();
		}
	}
}
