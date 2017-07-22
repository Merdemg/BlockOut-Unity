using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour {
	public GUIText p1, p2, p3, p4, p5, s1, s2, s3, s4, s5, yourScore;
	bool buttonPressed = false;
	bool ShowInputBox = false;
	bool newHighScore = false;
	int playerScore = 10, Score1, Score2, Score3, Score4, Score5;
	string Player1, Player2, Player3, Player4, Player5, PlayerName = "";
	int tempInt = 0;
	string tempName = "";
	// Use this for initialization
	void Start () {
		playerScore = PlayerPrefs.GetInt ("PlayerScore");
		yourScore.text = playerScore.ToString ();
		if (!PlayerPrefs.HasKey ("RFScore1")) {
		//create default high scores
			PlayerPrefs.SetInt ("RFScore1", 1000000);
			PlayerPrefs.SetInt ("RFScore2", 5000);
			PlayerPrefs.SetInt ("RFScore3", 3000);
			PlayerPrefs.SetInt ("RFScore4", 666);
			PlayerPrefs.SetInt ("RFScore5", 0);
			PlayerPrefs.SetString ("RFPlayer1", "Chuck Norris");
			PlayerPrefs.SetString ("RFPlayer2", "CrazyBoy99");
			PlayerPrefs.SetString ("RFPlayer3", "NeckBeard");
			PlayerPrefs.SetString ("RFPlayer4", "GothChick");
			PlayerPrefs.SetString ("RFPlayer5", "HowCanUCThis?");
		}
		GetScores();
		if (playerScore > Score5) {
						newHighScore = true;
						ShowInputBox = true;
				}

		ShowScores();
				
	}

	void GetScores() {
		Score1 = PlayerPrefs.GetInt ("RFScore1");
		Score2 = PlayerPrefs.GetInt ("RFScore2");
		Score3 = PlayerPrefs.GetInt ("RFScore3");
		Score4 = PlayerPrefs.GetInt ("RFScore4");
		Score5 = PlayerPrefs.GetInt ("RFScore5");
		Player1 = PlayerPrefs.GetString ("RFPlayer1");
		Player2 = PlayerPrefs.GetString ("RFPlayer2");
		Player3 = PlayerPrefs.GetString ("RFPlayer3");
		Player4 = PlayerPrefs.GetString ("RFPlayer4");
		Player5 = PlayerPrefs.GetString ("RFPlayer5");
	}
	void ArrangeScores() {
		if (playerScore > Score5) {
			Score5 = playerScore;
			Player5 = PlayerName;
		}
		if (Score5 > Score4) {
			tempInt = Score4;
			Score4 = Score5;
			Score5 = tempInt;
			tempName = Player4;
			Player4 = Player5;
			Player5 = tempName;
				}
		if (Score4 > Score3) {
			tempInt = Score3;
			Score3 = Score4;
			Score4 = tempInt;
			tempName = Player3;
			Player3 = Player4;
			Player4 = tempName;
		}if (Score3 > Score2) {
			tempInt = Score2;
			Score2 = Score3;
			Score3 = tempInt;
			tempName = Player2;
			Player2 = Player3;
			Player3 = tempName;
		}if (Score2 > Score1) {
			tempInt = Score1;
			Score1 = Score2;
			Score2 = tempInt;
			tempName = Player1;
			Player1 = Player2;
			Player2 = tempName;
		}
		ShowScores();
		SaveScores ();
	}
	void ShowScores() {
		p1.text = Player1;
		p2.text = Player2;
		p3.text = Player3;
		p4.text = Player4;
		p5.text = Player5;
		s1.text = Score1.ToString ();
		s2.text = Score2.ToString ();
		s3.text = Score3.ToString ();
		s4.text = Score4.ToString ();
		s5.text = Score5.ToString ();
	}
	void SaveScores() {
		PlayerPrefs.SetInt ("RFScore1", Score1);
		PlayerPrefs.SetInt ("RFScore2", Score2);
		PlayerPrefs.SetInt ("RFScore3", Score3);
		PlayerPrefs.SetInt ("RFScore4", Score4);
		PlayerPrefs.SetInt ("RFScore5", Score5);
		PlayerPrefs.SetString ("RFPlayer1", Player1);
		PlayerPrefs.SetString ("RFPlayer2", Player2);
		PlayerPrefs.SetString ("RFPlayer3", Player3);
		PlayerPrefs.SetString ("RFPlayer4", Player4);
		PlayerPrefs.SetString ("RFPlayer5", Player5);
	}

	void OnGUI() {
		if (ShowInputBox) {
						//GUI.Window(0, new Rect((Screen.width/2)-150, (Screen.height/2)-75, 300, 250), 
						//          GUILayout.TextField(Player5, 12) ,"Enter your name");
						GUI.Window (0, new Rect ((Screen.width / 2) - 150, (Screen.height / 2) - 75
			                       
			                       , 300, 250), EnterName, "Enter your name");

						/*
			if (buttonPressed == false) {
			GUI.Window(0, new Rect((Screen.width/2)-150, (Screen.height/2)-75
			                       
			                       , 300, 250), ShowPopUp, "Enter your name");
			}
			//NewHighScore = false;


*/
				} else {
			if (GUI.Button(new Rect(Screen.width-140,40,120,30),"Main Menu"))
			{
				Application.LoadLevel(0);			
			}
				}
	}

	void EnterName(int windowID) {
		PlayerName = GUI.TextField(new Rect(75, 100, 150, 30), PlayerName, 12);
		//Rect labelRect = GUILayoutUtility.GetRect(new GUIContent("AAAAAAAAAAAA"), "label");
		//GUI.TextField (labelRect, PlayerName, 12); 
		if (GUI.Button(new Rect(115, 170, 75, 30), "OK")) {			
			buttonPressed = true;
			ShowInputBox = false;
			ArrangeScores();
			}

	}

}
