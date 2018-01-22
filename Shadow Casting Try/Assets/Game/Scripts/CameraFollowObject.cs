using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour {

	public Transform target;
	private Vector3 cameraOffset = new Vector3(0,0, -10);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.position = target.position + this.cameraOffset;
	}
}
