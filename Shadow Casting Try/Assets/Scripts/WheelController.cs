using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour {

	private Transform rightWheel;
	private Transform leftWheel;

	public GameObject equipmentSlotPrefab;

	// Use this for initialization
	void Start () {

		this.rightWheel = this.transform.FindChild("Right").FindChild ("RightWheel");
		this.leftWheel = this.transform.FindChild ("Left").FindChild ("LeftWheel");

		Debug.Log (this.rightWheel.name + " " + this.leftWheel.name);

		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			Debug.Log ("Left shift");
		}


		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Space");
		}
	}
}
