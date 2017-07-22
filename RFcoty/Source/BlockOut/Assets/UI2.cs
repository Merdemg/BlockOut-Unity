using UnityEngine;
using System.Collections;

public class UI2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width/2+50,Screen.height/2,120,30),"Flat Blocks")){
			PlayerPrefs.SetInt("FlatBlocks", 1);
			Application.LoadLevel(1);
		}

		if (GUI.Button(new Rect(Screen.width/2-170,Screen.height/2,120,30),"Basic Blocks"))
		{
			PlayerPrefs.SetInt("FlatBlocks", 0);
			Application.LoadLevel(1);
			
		}
		if (GUI.Button(new Rect(Screen.width/2-60,Screen.height/2+140,120,30),"Back")) {
			Application.LoadLevel(0);
			
		}

	}
}
