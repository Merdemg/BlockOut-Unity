using UnityEngine;
using System.Collections;

public class CubeFeeler : MonoBehaviour {
	public bool touching = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (touching == true) {
						MoveCubes.instance.rVThereYet = true;
				} else {
			MoveCubes.instance.rVThereYet = false;
				}
	}
	void OnCollisionEnter()	 {
		touching = true;

		}

	void OnTriggerEnter(Collider other) {
		//if (other.tag == "Bottom") {
						touching = true;
			//	}
	}
	void OnTriggerExit(Collider other) {
					touching = false;
	}
}
