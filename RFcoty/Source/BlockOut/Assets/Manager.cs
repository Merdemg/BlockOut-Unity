using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	public string[, ,] gameBoard = new string[4, 3, 11];
	public bool[] fullLine = new bool[11];
	private GameObject[] gameObjects;
	public bool spawnTime = false;
	public bool freezeTime = false;
	[SerializeField] public GameObject ghostPrefab = null;
	public float moveRate = 0.5f;
	public int fullLineNo = 0;
	public bool weHaveFullLine = false;
	public int shapeNo = 0;
	[SerializeField] public GameObject myCenter = null;
	public bool rotatable = true;
	Vector3 myVec3 = Vector3.zero;
	public int level = 1;
	public int points = 0;
	public bool firstTourofFullLine = true;
	[SerializeField] public GUIText LevelNo;
	[SerializeField] public GUIText PointsNo;
	[SerializeField] public GUIText planes;
	[SerializeField] public GUIText cubesPlacedNo;
	[SerializeField] public GUIText highscoreNo;

	public int faceClearPoints = 0;
	public bool pitClear = true;
	public int clearedPlanes = 0;
	public int requiredPlanes = 3;
	public int cubesPlaced = 0;
	public bool FlatBlocks = true;

	void Awake() {
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				for (int k = 0; k < 11; k++) {
					gameBoard[i, j , k] = "Empty";
								}
			}
				}
		if (PlayerPrefs.GetInt ("FlatBlocks") == 1) {
						FlatBlocks = true;
				} else {
			FlatBlocks = false;
				}


		}
	
	string DetectKeyboardInput() {
				if (Input.GetKeyDown (KeyCode.UpArrow)) {
						Debug.Log ("up");
						return "Up";
				} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
						return "Down";
				} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
						return "Right";
				} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
						return "Left";
				} else if (Input.GetKeyDown (KeyCode.A)) {
						return "A";
				} else if (Input.GetKeyDown (KeyCode.D)) {
						return "D";
				} else if (Input.GetKeyDown (KeyCode.Q)) {
						return "Q";
				} else if (Input.GetKeyDown (KeyCode.E)) {
						return "E";
				} else if (Input.GetKeyDown (KeyCode.W)) {
						return "W";
				} else if (Input.GetKeyDown (KeyCode.S)) {
						return "S";
				}
		return null;
		}

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("RFScore5")) {
			highscoreNo.text = PlayerPrefs.GetInt ("RFScore5").ToString();
				} else {
			highscoreNo.text = "0";
				}

		level = PlayerPrefs.GetInt ("Level");
		LevelNo.text = level.ToString();
		Spawn ();
		StartCoroutine(MoveCoroutine(this.moveRate));
	}

	void NewLevel() {
		cubesPlaced = 0;
		cubesPlacedNo.text = cubesPlaced.ToString ();
		level++;
		LevelNo.text = level.ToString();
		clearedPlanes = 0;
		requiredPlanes++;
		StopCoroutine ("MoveCoroutine");
		moveRate = (75 * moveRate) / 100;

		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				for (int k = 0; k < 11; k++) {
					gameBoard[i, j , k] = "Empty";
				}
			}
		}

		Spawn ();
		StartCoroutine(MoveCoroutine(this.moveRate));

		}

	void CheckFullLine() {
		for (int i = 0; i<11; i++) {
			fullLine[i] = true;
				}


		for (int i = 0; i < 4; i++) {
						for (int j = 0; j < 3; j++) {
								for (int k = 0; k < 11; k++) {
										if (gameBoard [i, j, k] == "Empty")
												fullLine [k] = false;
								}
						}
				}
		for (int i = 10; i >= 0; i--) {
			if (fullLine[i] == true){
				weHaveFullLine = true;
				fullLineNo = i;
				if (firstTourofFullLine){
				points += 50 * (11-i);
					faceClearPoints += 50 * (11-i);
					clearedPlanes++;
				}
			}
		}
		if (weHaveFullLine) {
			if (firstTourofFullLine) {
				GoodSound.instance.Play();
			}
			firstTourofFullLine = false;
			DestroyFullLine();
				}
		CheckIfPitEmpty ();
		faceClearPoints = 0;
		}


	void CheckIfPitEmpty() {
		pitClear = true;
		for (int i = 0; i < 4; i++) {
						for (int j = 0; j < 3; j++) {
								for (int k = 0; k < 11; k++) {
										if (gameBoard [i, j, k] != "Empty")
												pitClear = false;
								}
						}
				}
		if (pitClear)
						points += faceClearPoints * 9;
	}

	void DestroyFullLine() {
		for (int i = 0; i < 4; i++) {
						for (int j = 0; j < 3; j++) {
								for (int k = fullLineNo; k >= 0; k--) {
										if (k != 0)
												gameBoard [i, j, k] = gameBoard [i, j, k - 1];
										else
												gameBoard [i, j, k] = "Empty";
								}
						}
				}
		weHaveFullLine = false;
		CheckFullLine ();

		}

	bool CheckIfLegitMove(string myDirection) {
			for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
			for (int k = 0; k < 11; k++) {
				if (gameBoard[i,j,k] == "Ghost") {
						if (myDirection == "Up" &&  j == 2)
							return false;
						if (myDirection == "Down" && j == 0)
							return false;
						if (myDirection == "Right" && i == 3)
							return false;
						if (myDirection == "Left" && i == 0)
							return false;
						if (myDirection == "Up" &&  gameBoard[i,j+1,k] == "Block")
							return false;
						if (myDirection == "Down" && gameBoard[i,j-1,k] == "Block")
							return false;
						if (myDirection == "Right" && gameBoard[i+1,j,k] == "Block")
							return false;
						if (myDirection == "Left" && gameBoard[i-1,j,k] == "Block")
							return false;
					}}}}
		return true;
		}
	
	// Update is called once per frame
	void Update () {
		cubesPlacedNo.text = cubesPlaced.ToString ();
		planes.text = clearedPlanes.ToString () + "/" + requiredPlanes.ToString ();
		firstTourofFullLine = true;
		PointsNo.text = points.ToString ();

		EraseBoard ();
		string input = DetectKeyboardInput ();
		if (input == "Up" || input == "Down" || input == "Right" || input == "Left") {
						bool legitMove = CheckIfLegitMove (input);
						if (legitMove) {
								MakePlayerMove (input);
						} else {
				BadSound.instance.Play();
					}
				} else if (input != null) {
						PlayerRotate (input);
				} 
			
				

		DrawBoard();

		if (clearedPlanes >= requiredPlanes) {
			points += 1000 * level;
			NewLevel();
				}
	}

	void PlayerRotate(string button) {
		gameObjects = GameObject.FindGameObjectsWithTag ("Ghost");
		var originalPos = myCenter.transform.position;
		var originalVec3 = myVec3;
		
		for(int i = 0 ; i < gameObjects.Length ;i++)
		{
			gameObjects[i].transform.parent = myCenter.transform;
		}
		if (button == "A") {
			myVec3.y -= 90;
			myCenter.transform.eulerAngles = myVec3;
		} if (button == "D") {
			myVec3.y += 90;
			myCenter.transform.eulerAngles = myVec3;
		}	if (button == "Q") {
			myVec3.z += 90;
			myCenter.transform.eulerAngles = myVec3;
		}	if (button == "E") {
			myVec3.z -= 90;
			myCenter.transform.eulerAngles = myVec3;
		}	if (button == "W") {
			myVec3.x -= 90;
			myCenter.transform.eulerAngles = myVec3;
		}	if (button == "S") {
			myVec3.x += 90;
			myCenter.transform.eulerAngles = myVec3;
		}

		rotatable = true;
		for(var i = 0 ; i < gameObjects.Length ; i ++)
		{
			//int a = (int)(gameObjects[i].transform.position.x);
			int tempX = (int)Mathf.RoundToInt(gameObjects[i].transform.position.x);
			int tempY = (int)Mathf.RoundToInt(gameObjects[i].transform.position.y);
			int tempZ = (int)Mathf.RoundToInt(gameObjects[i].transform.position.z);
			if(tempX < 0 || tempX > 3|| tempY < 0 || tempY > 2 || tempZ < 0 || tempZ > 10) {
				rotatable = false;
			}
			if (rotatable) {
				if (gameBoard[tempX, tempY, tempZ] == "Block") {
					rotatable = false;
				}
			}
		}

		if (rotatable) {
			for (int i = 3; i >= 0; i--) {
				for (int j = 2; j >= 0; j--) {
					for (int k = 0; k < 11; k++) {
						if (gameBoard [i, j, k] == "Ghost") {
							gameBoard[i,j,k] = "Empty";
						}
					}
				}
			}

			for(var i = 0 ; i < gameObjects.Length ; i ++)
			{
				var tempX = (int)Mathf.RoundToInt(gameObjects[i].transform.position.x);
				var tempY = (int)Mathf.RoundToInt(gameObjects[i].transform.position.y);
				var tempZ = (int)Mathf.RoundToInt(gameObjects[i].transform.position.z);
				gameBoard[tempX, tempY, tempZ] = "Ghost";
			}
		} else {
			BadSound.instance.Play();
			myCenter.transform.position = originalPos;
			myVec3 = originalVec3;
			myCenter.transform.eulerAngles = myVec3;
		}
	}

	void MakePlayerMove(string myDirection) {
		if (myDirection == "Left" || myDirection == "Down") {
						for (int i = 0; i < 4; i++) {
								for (int j = 0; j < 3; j++) {
										for (int k = 0; k < 11; k++) {
												if (gameBoard [i, j, k] == "Ghost") {
														gameBoard [i, j, k] = "Empty";
														if (myDirection == "Left")
																gameBoard [i - 1, j, k] = "Ghost";
														if (myDirection == "Down")
																gameBoard [i, j - 1, k] = "Ghost";
												}
										}
								}
						}
				}
		if (myDirection == "Right" || myDirection == "Up") {
			for (int i = 3; i >= 0; i--) {
				for (int j = 2; j >= 0; j--) {
					for (int k = 0; k < 11; k++) {
						if (gameBoard [i, j, k] == "Ghost") {
							gameBoard [i, j, k] = "Empty";
							if (myDirection == "Right")
								gameBoard [i + 1, j, k] = "Ghost";
							if (myDirection == "Up")
								gameBoard [i, j + 1, k] = "Ghost";
						}
					}
				}
			}
		}				
		var tempPos = myCenter.transform.position;
		if (myDirection == "Right")
			tempPos.x += 1;
		if (myDirection == "Left")
			tempPos.x -= 1;
		if (myDirection == "Up")
			tempPos.y += 1;
		if (myDirection == "Down")
			tempPos.y -= 1;

		myCenter.transform.position = tempPos;
	}
	void Spawn() {
		if (FlatBlocks) {
						SpawnFlat ();
				} else {
			SpawnBasic();
				}

		}
	void SpawnBasic() {
		shapeNo = Random.Range (0, 7);

		if (shapeNo == 0) {
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [2, 1, 0] = "Ghost";
			gameBoard [1, 2, 0] = "Ghost";
		}
		if (shapeNo == 1) {
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [2, 1, 0] = "Ghost";
			gameBoard [1, 2, 0] = "Ghost";
			gameBoard [1, 1, 1] = "Ghost";
		}
		if (shapeNo == 2) {
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [2, 1, 1] = "Ghost";
			gameBoard [1, 2, 0] = "Ghost";
			gameBoard [1, 1, 1] = "Ghost";
		}
		if (shapeNo == 3) {
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [0, 1, 1] = "Ghost";
			gameBoard [1, 2, 0] = "Ghost";
			gameBoard [1, 1, 1] = "Ghost";
		}
		if (shapeNo == 4) {
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [1, 2, 0] = "Ghost";
			gameBoard [1, 0, 0] = "Ghost";
			gameBoard [2, 0, 0] = "Ghost";
		}
		if (shapeNo == 5) {
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [2, 1, 0] = "Ghost";
			gameBoard [1, 0, 0] = "Ghost";
			gameBoard [2, 2, 0] = "Ghost";
		}
		if (shapeNo == 6) {
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [1, 2, 0] = "Ghost";
			gameBoard [1, 0, 0] = "Ghost";
			gameBoard [2, 1, 0] = "Ghost";
		}
		myCenter.transform.position = new Vector3(1,1,0);
		myVec3 = Vector3.zero;
		spawnTime = false;
		
		EraseBoard ();
		DrawBoard ();
	}

	void SpawnFlat() {
		shapeNo = Random.Range (0, 8);

		if (shapeNo == 0) {
						gameBoard [1, 1, 0] = "Ghost";
			myCenter.transform.position = new Vector3(1,1,0);
				}
		if (shapeNo == 1) {
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [1, 0, 0] = "Ghost";
			gameBoard [1, 2, 0] = "Ghost";
			gameBoard [2, 2, 0] = "Ghost";
			myCenter.transform.position = new Vector3(1,1,0);
				}
		if (shapeNo == 2) {
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [1, 0, 0] = "Ghost";
			gameBoard [2, 1, 0] = "Ghost";
			gameBoard [2, 2, 0] = "Ghost";
			myCenter.transform.position = new Vector3(1,1,0);
				}
		if (shapeNo == 3) {
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [1, 2, 0] = "Ghost";
			gameBoard [1, 0, 0] = "Ghost";
			gameBoard [2, 1, 0] = "Ghost";
			myCenter.transform.position = new Vector3(1,1,0);
				}
		if (shapeNo == 4) {
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [2, 1, 0] = "Ghost";
			gameBoard [1, 2, 0] = "Ghost";
			gameBoard [2, 2, 0] = "Ghost";
			myCenter.transform.position = new Vector3(1,1,0);
				}
		if (shapeNo == 5) {
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [2, 1, 0] = "Ghost";
			gameBoard [1, 2, 0] = "Ghost";
			myCenter.transform.position = new Vector3(1,1,0);
				}
		if (shapeNo == 6) {
			gameBoard [1, 0, 0] = "Ghost";
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [1, 2, 0] = "Ghost";
			myCenter.transform.position = new Vector3(1,1,0);

				}
		if (shapeNo == 7) {
			gameBoard [1, 1, 0] = "Ghost";
			gameBoard [2, 1, 0] = "Ghost";
			myCenter.transform.position = new Vector3(1,1,0);
				}
		myVec3 = Vector3.zero;
		spawnTime = false;

		EraseBoard ();
		DrawBoard ();
	}

	void DrawBoard() {
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				for (int k = 0; k < 11; k++) {
					if(gameBoard[i, j, k] == "Ghost") {
						GameObject myGhost = Instantiate(this.ghostPrefab) as GameObject;
						//this.transform.position = new Vector3(0, 0,0);
						myGhost.transform.position = new Vector3(i, j, k);
					}
					if(gameBoard[i,j,k] == "Block" && k != 0) {
						GameObject myBlock = Instantiate(Resources.Load("Blocks/Block"+k)) as GameObject;
						myBlock.transform.position = new Vector3(i, j, k);

					}
				}
			}
		}

	}

	void EraseBoard() {
		gameObjects = GameObject.FindGameObjectsWithTag ("Ghost");
		
		for(var i = 0 ; i < gameObjects.Length ; i ++)
		{
			Destroy(gameObjects[i]);
		}

		gameObjects = GameObject.FindGameObjectsWithTag ("Block");
		
		for(var i = 0 ; i < gameObjects.Length ; i ++)
		{
			Destroy(gameObjects[i]);
		}
	}

	private IEnumerator MoveCoroutine (float delay)
	{
		while (true)
		{
			// Wait for the specified delay in seconds.
			yield return new WaitForSeconds(delay);

			CheckBoardStatus();
			if (spawnTime) {
				Spawn();
			}

			if(freezeTime) {
				FreezeGhosts();
				freezeTime = false;
				spawnTime = true;

			}	else {
				MoveStuff();
			}
		}
	}

	void MoveStuff() {
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				for (int k = 9; k >= 0; k--) {
					if(gameBoard[i, j, k] == "Ghost") {
						gameBoard[i, j, k] = "Empty";
						gameBoard[i, j, k+1] = "Ghost";
					}
				}
			}
		}
		var tempPos = myCenter.transform.position;
		tempPos.z += 1;
		myCenter.transform.position = tempPos;
		}

	void CheckBoardStatus() {
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				if(gameBoard[i, j, 10] == "Ghost") {
					freezeTime = true;
				}
			}
		}

		for (int i = 0; i < 4; i++) {
						for (int j = 0; j < 3; j++) {
								for (int k = 9; k >= 0; k--) {
										if (gameBoard [i, j, k] == "Ghost") {
												if (gameBoard [i, j, k + 1] == "Block") {
														freezeTime = true;
												}
										}
								}
						}
				}

		}

	void FreezeGhosts() {
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				for (int k = 10; k >= 0; k--) {
					if(gameBoard[i, j, k] == "Ghost") {
						points += 5 * (11-k);
						cubesPlaced++;
						gameBoard[i, j, k] = "Block";
					}
				}
			}
		}
		CheckFullLine ();
		CheckGameOver ();
	}
	void CheckGameOver() {
		for (int i = 0; i < 4; i++) {
						for (int j=0; j < 3; j++) {
								if (gameBoard [i, j, 1] == "Block") {
										GameOver ();
								}
						}
				}
		}

	void GameOver() {
		PlayerPrefs.SetInt ("PlayerScore", points);
		Application.LoadLevel (2);
	}
	
}
