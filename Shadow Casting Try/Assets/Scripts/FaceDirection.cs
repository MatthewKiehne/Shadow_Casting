using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirection : MonoBehaviour {

	public Vector3 EulerAngle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		this.transform.eulerAngles = this.EulerAngle;
		
	}
}
