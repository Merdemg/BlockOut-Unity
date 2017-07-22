using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	public GameObject myCube;
	public GameObject mySphere;
	float tempX;
	float tempY;
	float tempZ;
	Vector3 myVec3 = Vector3.zero;

	// Use this for initialization
	void Start () {
		mySphere.transform.parent = myCube.transform;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			RotateIt();
		}

	}

		void RotateIt() {

		//new var tempPos = myCube.transform.position;
		//var tempPos = myCube.transform.eulerAngles;
		//tempY = tempPos.y;
		//tempZ = tempPos.z;
		//tempPos.x = tempPos.x + 45;

		//tempPos.y = tempY;
		//tempPos.z = tempZ;

		//transform.eulerAngles = tempPos;

		//var tempAngles = myCube.transform.rotation;
		//tempAngles.x += 45;
		//myCube.transform.rotation = tempAngles;

		myVec3.x += 45;
		myCube.transform.eulerAngles = myVec3;



		Destroy (mySphere);
		}
}
