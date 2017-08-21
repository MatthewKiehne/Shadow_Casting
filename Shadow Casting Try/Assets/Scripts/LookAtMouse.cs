using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 mousePos = Input.mousePosition;
		Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);

		Vector3 vectorToTarget = screenPos - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90f;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = q;

		Debug.DrawLine (this.transform.position,this.transform.TransformPoint(this.transform.forward),Color.cyan);
	}
}
