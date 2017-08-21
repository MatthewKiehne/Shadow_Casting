using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRandomSpeed : MonoBehaviour {

	private float speed = 1f;
	private int clockwise = 1;

	// Use this for initialization
	void Start () {

		this.speed = Random.Range (30f, 50f);

		int direction = Random.Range (0, 2);

		if (direction == 1) {
			this.clockwise = -this.clockwise;
		}
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.Rotate (new Vector3 (0, 0, Time.deltaTime * this.speed * this.clockwise));
	}
}
