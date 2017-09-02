using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOFFire : MonoBehaviour {

	private int numRays = 4;

	private float coneDistance = 10f;

	[Range (0, 180)]
	private float coneAngle = 50f;

	private float[,] sinCos;

	public LayerMask collisionLayer;

	public MeshFilter viewMeshFilter;
	Mesh viewMesh;

	private List<Vector3> endPoints = new List<Vector3> ();
	// Use this for initialization

	// Use this for initialization
	void Start () {

		viewMesh = new Mesh ();
		viewMesh.name = "Cone Mesh";
		viewMeshFilter.mesh = viewMesh;
		this.updateSinCos (this.ConeAngle, this.NumRays);
	}
	
	// Update is called once per frame
	void LateUpdate () {

//		float width = Mathf.Tan (this.coneAngle * Mathf.Deg2Rad) * this.ConeDistance;
//		float height = this.coneDistance;
//
//		Debug.Log ("width:" + width + " height:" + height);
//
//		bool widthIsSmaller = true;
//		float boxSize = width;
//
//		float boxes = height / width;
//
//		if (height < width) {
//			boxSize = height;
//			boxes = width / height;
//			Debug.Log ("height is smaller: " + boxes);
//		} else {
//			Debug.Log ("width is smaller: " + boxes);
//		}
//
//
//
//
//		this.castRays ();
	}

	private void castRays () {

		float angleSlice = this.ConeAngle / this.NumRays;

		for (int i = 0; i < sinCos.GetLength (0); i++) {

			Vector3 sinCosPoint = this.transform.TransformPoint (new Vector3 (sinCos [i, 0], sinCos [i, 1], 0));

			Vector3 rayDirection = (sinCosPoint - this.transform.position).normalized;

			Vector3 rayPoint = this.transform.position + (rayDirection * this.ConeDistance);

			float nextAngle = ((angleSlice * (i + 1)) - (this.ConeAngle / 2));

			RaycastHit2D[] rayHits = Physics2D.RaycastAll (this.transform.position, rayDirection, this.ConeAngle, this.collisionLayer);
			Debug.DrawLine (this.transform.position, rayPoint, Color.cyan);
		}
	}

	private void updateSinCos (float viewAngle, int numRays) {


		this.sinCos = new float[(numRays + 1), 2];
		float angleSlice = this.ConeAngle / this.NumRays;

		for (int i = 0; i < numRays + 1; i++) {

			float angle = ((angleSlice * i) - (this.ConeAngle / 2));
			this.sinCos [i, 0] = Mathf.Sin (angle * Mathf.Deg2Rad);
			this.sinCos [i, 1] = Mathf.Cos (angle * Mathf.Deg2Rad);
		}
	}

	public float ConeAngle {
		get {
			return coneAngle;
		}
		set {
			coneAngle = value;
			updateSinCos (this.coneAngle, this.NumRays);
		}
	}

	public int NumRays {
		get {
			return numRays;
		}
		set {
			numRays = value;
			updateSinCos (this.coneAngle, this.NumRays);
		}
	}

	public float ConeDistance {
		get {
			return coneDistance;
		}
		set {
			coneDistance = value;
		}
	}
}
