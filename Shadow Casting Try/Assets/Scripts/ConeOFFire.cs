using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOFFire : MonoBehaviour {

	private int numRays = 4;

	private float coneDistance = 50f;

	[Range (0, 180)]
	private float coneAngle = 45f;

	private float[,] sinCos;

	public LayerMask collisionLayer;

	public MeshFilter viewMeshFilter;
	Mesh viewMesh;

	private Vector3 bottomLeftPoint;
	private Vector3 topRightPoint;

	private List<Vector3> endPoints = new List<Vector3> ();
	// Use this for initialization

	// Use this for initialization
	void Start () {

		viewMesh = new Mesh ();
		viewMesh.name = "Cone Mesh";
		viewMeshFilter.mesh = viewMesh;
		this.updateSinCos (this.ConeAngle, this.NumRays);
		this.updateBox ();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 worldBotLeft = this.transform.TransformPoint (this.bottomLeftPoint);
		Vector3 worldTopRight = this.transform.TransformPoint (this.topRightPoint);

		Debug.DrawLine (worldBotLeft, worldTopRight);


//		Vector3 worldBotRight = this.transform.TransformPoint (new Vector3 (this.topRightPoint.x, 0, 0));
//		Vector3 worldTopLeft = this.transform.TransformPoint (new Vector3 (-this.topRightPoint.x, this.ConeDistance, 0));
//		Debug.DrawLine (worldTopRight, worldTopLeft, Color.red);
//		Debug.DrawLine (worldTopLeft, worldBotLeft, Color.red);
//		Debug.DrawLine (worldBotLeft, worldBotRight, Color.red);
//		Debug.DrawLine (worldBotRight, worldTopRight, Color.red);

		this.castRays ();

	}

	private void castRays(){

		float angleSlice = this.ConeAngle / this.NumRays;

		for (int i = 0; i < sinCos.GetLength (0); i++) {

			Vector3 rayPoint = this.transform.TransformPoint (new Vector3 (sinCos [i, 0], sinCos [i, 1], 0));

			Vector3 rayDirection = (rayPoint - this.transform.position).normalized;

			float nextAngle = ((angleSlice * (i + 1)) - (this.ConeAngle / 2));

			RaycastHit2D[] rayHits = Physics2D.RaycastAll (this.transform.position, rayDirection, this.ConeAngle, this.collisionLayer);
			Debug.DrawLine (this.transform.position,this.transform.parent.TransformPoint( rayPoint * ConeDistance), Color.cyan);
		}
	}

	private void updateBox(){

		float right = Mathf.Tan ((this.ConeAngle / 2) * Mathf.Deg2Rad) * this.coneDistance;
		this.topRightPoint = new Vector3 (right, this.coneDistance, 0);
		this.bottomLeftPoint = new Vector3 (-right, 0, 0);
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
			updateBox ();
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
			updateBox ();
		}
	}
}
