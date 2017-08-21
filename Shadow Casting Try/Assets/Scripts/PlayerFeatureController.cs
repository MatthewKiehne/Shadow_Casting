using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeatureController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			Debug.Log ("Change Equipment");
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Change Abilities");
		}
		
	}
}
