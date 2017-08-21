using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Vector3 velocity;
	private Rigidbody2D body;
	private float speed = 6f;

	// Use this for initialization
	void Start () {

		this.body = this.GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {


		this.velocity = Vector3.zero;

		if (Input.anyKey) {
			if (Input.GetKeyDown (KeyCode.W)) {
				this.velocity += new Vector3 (0, 1, 0);
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				this.velocity += new Vector3 (0, -1, 0);
			}
			if (Input.GetKeyDown (KeyCode.D)) {
				this.velocity += new Vector3 (1, 0, 0);
			}
			if (Input.GetKeyDown (KeyCode.A)) {
				this.velocity += new Vector3 (-1, 0, 0);
			}



			if (Input.GetKey (KeyCode.W)) {
				this.velocity += new Vector3 (0, 1, 0);
			}
			if (Input.GetKey (KeyCode.S)) {
				this.velocity += new Vector3 (0, -1, 0);
			}
			if (Input.GetKey (KeyCode.D)) {
				this.velocity += new Vector3 (1, 0, 0);
			}
			if (Input.GetKey (KeyCode.A)) {
				this.velocity += new Vector3 (-1, 0, 0);
			}
		}

		this.body.MovePosition (this.transform.position + (this.velocity.normalized * Time.deltaTime * this.speed));
	}

	void FixedUpdate(){
		
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
