using UnityEngine;
using System.Collections;

public class MoveCubes : MonoBehaviour {
	[SerializeField] public static MoveCubes instance = null;
	public bool rVThereYet = false;

	public float MoveRate = 1.5f;
	// Use this for initialization
	void Start () {
		MoveCubes.instance = this;

		StartCoroutine(MoveCoroutine(this.MoveRate));
	}

	// Update is called once per frame
	void Update () {
		if (rVThereYet == true) {


				}
	}

	private IEnumerator MoveCoroutine (float delay)
	{
		while (true)
		{
			// Wait for the specified delay in seconds.
			yield return new WaitForSeconds(delay);
			
			// After we finish waiting, spawn the enemy.
			Move();
		}
	}

	public void Move()
	{
		if (rVThereYet == false) {
						Vector3 pos = this.transform.position;
						pos.z = pos.z + 1f;
						this.transform.position = pos;
				} else {

				}
	}
}
