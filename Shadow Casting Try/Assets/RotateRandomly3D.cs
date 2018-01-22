using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRandomly3D : MonoBehaviour {

	public Vector3 direction;

	// Use this for initialization
	void Start () {

		this.direction = new Vector3 (
			Random.Range (-5, 6),
			Random.Range (-5, 6),
			Random.Range (-5, 6));
		
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.Rotate (this.direction * Time.deltaTime * 5);
		
	}
}
