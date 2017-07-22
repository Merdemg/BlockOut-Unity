using UnityEngine;
using System.Collections;

public class GoodSound : MonoBehaviour {
	public static GoodSound instance = null;

	// Use this for initialization
	void Start () {
		GoodSound.instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play() {

		audio.Play ();
	}
}
