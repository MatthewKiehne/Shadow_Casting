using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMouse : MonoBehaviour {

	private float speed = 2f;

	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		this.body = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetMouseButton (0)) {

			Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			screenPos.z = 0f;
			Vector3 dir = Vector3.Normalize(screenPos - this.transform.position);
			body.MovePosition(this.transform.position + dir * Time.deltaTime * this.Speed);
		}
	}

	public float Speed {
		get {
			return speed;
		}
		set {
			speed = value;
		}
	}
}
