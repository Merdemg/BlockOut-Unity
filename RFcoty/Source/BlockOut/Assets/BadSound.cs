using UnityEngine;
using System.Collections;

public class BadSound : MonoBehaviour {
	public static BadSound instance = null;
	// Use this for initialization
	void Start () {
		BadSound.instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play() {
		audio.Play ();
	}
}
