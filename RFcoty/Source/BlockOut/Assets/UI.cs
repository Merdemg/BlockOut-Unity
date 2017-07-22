using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {
	//public GameObject game;
	public bool buttonPressed = false;

	void OnGUI()
	{
		if (GUI.Button(new Rect(Screen.width/2-60,Screen.height/2+70,120,30),"How to Play")){
			//windowRect = GUI.Window(0, windowRect, DoMyWindow, "My Window");
			buttonPressed = !buttonPressed;
		}
		if(buttonPressed) {
			GUI.Window(0, new Rect((Screen.width/2)-150, (Screen.height/2)-75
			                       
			                       , 300, 250), ShowPopUp, "How to Play");

		}
		if (GUI.Button(new Rect(Screen.width/2-60,Screen.height/2,120,30),"New Game"))
		{
			//Destroy(gameObject);

			//GameObject g = Instantiate(game) as GameObject;
			//Instantiate(game);
			Application.LoadLevel(3);

		}
		if (GUI.Button(new Rect(Screen.width/2-60,Screen.height/2+140,120,30),"Quit Game")) {
			Application.Quit();

		}

	}
	void ShowPopUp(int windowID) {
		//GUI.Label(new Rect(65, 40, 200, 30), 
		//GUI.Label(new Rect(65, 40, 90000000, 2000000),
		Rect labelRect = GUILayoutUtility.GetRect(new GUIContent("Use arrow keys to move the blocks. " +
			"A and D keys rotate blocks in Y axes, " +
			"Q and E in Z axes and " +
			"W and S rotates in X axes. " +       
		    "(only if you have enough space to do so) " +
			"Complete a full line to destroy it. " +
		    "Score is calculated as described in the outline. " +
			"With each level, the game will be harder. You will have less time to manipulate the blocks and " +
			"number of planes required will increase. " +
		    "Good Luck!"), "label");

		GUI.Label(labelRect, "Use arrow keys to move the blocks. " +
		          "A and D keys rotate blocks in Y axes, " +
		          "Q and E in Z axes and " +
		          "W and S rotates in X axes. " +
		          "(only if you have enough space to do so) " +
		          "Complete a full line to destroy it. " +
		          "Score is calculated as described in the outline. " +
		          "With each level, the game will be harder. You will have less time to manipulate the blocks and " +
		          "number of planes required will increase. " +
		          "Good Luck!");	
			
		// You may put a button to close the pop up too
		if (GUI.Button(new Rect(115, 150, 75, 30), "OK"))		
		{			
			buttonPressed = false;			
		}
	}
}
