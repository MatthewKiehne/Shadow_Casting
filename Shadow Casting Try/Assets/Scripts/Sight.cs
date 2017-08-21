using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour {

	private int numRays = 100;

	private float viewDistance = 20f;

	[Range (0, 360)]
	private float viewAngle = 140f;

	private float ridgeDistance = .15f;

	private float[,] sinCos;

	public LayerMask collisionLayer;

	public MeshFilter viewMeshFilter;
	Mesh viewMesh;

	private List<Vector3> endPoints = new List<Vector3> ();
	// Use this for initialization

	void Start () {

		viewMesh = new Mesh ();
		viewMesh.name = "View Mesh";
		viewMeshFilter.mesh = viewMesh;
		this.updateSinCos (this.ViewAngle, this.NumRays);
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		this.endPoints.Clear ();
		List<LineInfo> unorderedList = this.findPolygonCorners ();
		Stack<LineInfo> lineStack = this.orderLines (unorderedList);
		this.castRays (lineStack);
		this.makeMesh ();
	}

	private List<LineInfo> findPolygonCorners () {
		//casts a ray to all the points in the polygon within range
		//it then finds if it ray contiues of the corner or collides with the polygon
		//after it adds them to a line and then adds the lines to a list

		Collider2D[] foundColliders = Physics2D.OverlapCircleAll (this.transform.position, this.viewDistance, this.collisionLayer);
		PolygonCollider2D[] polygonColliders = new PolygonCollider2D[foundColliders.Length];
		for (int i = 0; i < foundColliders.Length; i++) {
			polygonColliders [i] = (PolygonCollider2D)foundColliders [i];
		}

		List<LineInfo> unorderedLineInfo = new List<LineInfo> ();

		foreach (PolygonCollider2D poly in polygonColliders) {
			foreach (Vector2 polyPoint in poly.points) {
				
				Vector3 colPointWorld = poly.transform.TransformPoint (polyPoint);
				Vector3 colPointLocal = this.transform.InverseTransformPoint (colPointWorld);

				float colPointAngle = Vector3.Angle (colPointLocal, Vector2.up);

				if (colPointAngle < this.ViewAngle / 2) {

					//fixxes the angle bug
					if (colPointLocal.x < 0) {
						colPointAngle = -colPointAngle;
					} 
						
					Vector3 pointDir = (colPointWorld - this.transform.position).normalized;
					float pointDis = Vector3.Distance (this.transform.position, colPointWorld);

					RaycastHit2D[] hits = Physics2D.RaycastAll (this.transform.position, pointDir, this.viewDistance, this.collisionLayer);

					if (hits.Length != 0) {
						
						float firstHitDis = Vector3.Distance (this.transform.position, hits [0].point);
						float errorRange = .005f;

						LineInfo line = new LineInfo (colPointAngle);

						//checks to see if the ray passes the corner
						if (firstHitDis > pointDis + errorRange) {
							//Debug.DrawLine (this.transform.position, hits [0].point, Color.cyan);
							line.Points.Add (this.addRidgeToPoint (colPointWorld, pointDir));
							line.Points.Add (this.addRidgeToPoint (hits [0].point, pointDir));
							unorderedLineInfo.Add (line);

							//this is where the line hits the corner
						} else if (firstHitDis > pointDis - errorRange) {

							line.Points.Add (this.addRidgeToPoint (hits [0].point, pointDir));

							//test to see if the line continues from the point
							Collider2D lineContinuesCollider = Physics2D.OverlapPoint (colPointWorld + (errorRange * pointDir));

							if (lineContinuesCollider != null) {
								//Debug.DrawLine (this.transform.position, hits [0].point, Color.black);
							} else {
								//line.Points.Add (this.transform.position + (pointDir * viewDistance));

								if (hits.Length == 1) {
									//Debug.DrawLine (this.transform.position, this.transform.position + (pointDir * this.viewDistance), Color.blue);
									line.Points.Add (this.addRidgeToPoint (this.transform.position + (pointDir * this.viewDistance), pointDir));
								} else if (hits.Length > 1) {
									//Debug.DrawLine (this.transform.position, hits [1].point, Color.green);

									line.Points.Add (this.addRidgeToPoint(hits [1].point,pointDir));

								}
							}
								
							unorderedLineInfo.Add (line);
						}

					} else if (Vector2.Distance (this.transform.position, colPointWorld) < this.viewDistance) {
						
						LineInfo line = new LineInfo (colPointAngle);

						line.Points.Add (this.addRidgeToPoint(colPointWorld,pointDir));
						line.Points.Add (this.addRidgeToPoint(this.transform.position + (pointDir * viewDistance),pointDir));
						unorderedLineInfo.Add (line);

						//Debug.DrawLine (this.transform.position, colPointWorld, Color.red);
					}
				}
			}
		}
	
		return unorderedLineInfo;
	}

	public Stack<LineInfo> orderLines (List<LineInfo> unorderedLines) {
		//orders the lines by thier angle and puts them in a stack
		//the biggest value is on top and the smallest is on the bottom

		Stack<LineInfo> orderedLines = new Stack<LineInfo> ();

		while (unorderedLines.Count != 0) {

			int biggestIndex = 0;
			float biggestAngle = float.MinValue;

			for (int i = 0; i < unorderedLines.Count; i++) {
				if (unorderedLines [i].Angle > biggestAngle) {
					biggestIndex = i;
					biggestAngle = unorderedLines [i].Angle;
				}
			}

			orderedLines.Push (unorderedLines [biggestIndex]);
			unorderedLines.RemoveAt (biggestIndex);
		}
			
		return orderedLines;
	}

	private void castRays (Stack<LineInfo> orderedLines) {
		//casts rays in front of the player is a semi cirle equal to the player's viewAngle
		//places the ordered lines in the correct position between the ray casts

		LineInfo nextLine = null;
		if (orderedLines.Count != 0) {
			nextLine = orderedLines.Pop ();
		}
		float angleSlice = this.ViewAngle / this.NumRays;
		
		for (int i = 0; i < sinCos.GetLength (0); i++) {

			Vector3 rayPoint = this.transform.TransformPoint (new Vector3 (sinCos [i, 0], sinCos [i, 1], 0));

			Vector3 rayDirection = (rayPoint - this.transform.position).normalized;

//			float angle = ((angleSlice * i) - (this.ViewAngle / 2));
			float nextAngle = ((angleSlice * (i + 1)) - (this.ViewAngle / 2));

			RaycastHit2D[] rayHits = Physics2D.RaycastAll (this.transform.position, rayDirection, this.viewDistance, this.collisionLayer);

			if (rayHits.Length != 0) {
				this.endPoints.Add (this.addRidgeToPoint(rayHits [0].point,rayDirection));

			} else {
				this.endPoints.Add (this.addRidgeToPoint(this.transform.position + (rayDirection * this.viewDistance),rayDirection));
			}
			if (nextLine != null) {
				
				while (orderedLines.Count != 0 && nextLine.Angle < nextAngle) {

					this.addLine (nextLine);
					nextLine = orderedLines.Pop ();
				}

				if (nextLine.Angle < nextAngle) {

					this.addLine (nextLine);
					nextLine = null;
				}
			}
		}
	}

	private void addLine (LineInfo line) {
		//checks to see which one is closer and adds that one first
		//the farther points is then added after the losest one is added

		if (line.Points.Count != 1) {
			float distanceFirst = Vector3.Distance (this.endPoints [this.endPoints.Count - 1], line.Points [0]);
			float distanceSecond = Vector3.Distance (this.endPoints [this.endPoints.Count - 1], line.Points [1]);

			if (distanceFirst < distanceSecond) {
				this.endPoints.Add (line.Points [0]);
				this.endPoints.Add (line.Points [1]);
			} else {
				this.endPoints.Add (line.Points [1]);
				this.endPoints.Add (line.Points [0]);
			}
		} else {
			this.endPoints.Add (line.Points [0]);
		}
	}

	private void makeMesh () {
		//this makes the mesh of all the endpoints 
		
		int vertexCount = endPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];

		vertices [0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++) {
			vertices [i + 1] = transform.InverseTransformPoint (this.endPoints [i]);

			if (i < vertexCount - 2) {
				triangles [i * 3] = 0;
				triangles [i * 3 + 1] = i + 1;
				triangles [i * 3 + 2] = i + 2;
			}
		}

		viewMesh.Clear ();
		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals ();
	}

	private Vector3 addRidgeToPoint (Vector3 point, Vector3 direction) {
		//adds the ridge distance to each point
		//this allows the player to see just the side of the object

		return point + (direction * this.ridgeDistance);
	}

	private void updateSinCos (float viewAngle, int numRays) {


		this.sinCos = new float[(numRays + 1), 2];
		float angleSlice = this.ViewAngle / this.NumRays;

		for (int i = 0; i < numRays + 1; i++) {

			float angle = ((angleSlice * i) - (this.ViewAngle / 2));
			this.sinCos [i, 0] = Mathf.Sin (angle * Mathf.Deg2Rad);
			this.sinCos [i, 1] = Mathf.Cos (angle * Mathf.Deg2Rad);
		}
	}

	public int NumRays {
		get {
			return numRays;
		}
		set {
			numRays = value;
			updateSinCos (this.ViewAngle, this.NumRays);
		}
	}

	public float ViewAngle {
		get {
			return viewAngle;
		}
		set {
			viewAngle = value;
			updateSinCos (this.ViewAngle, this.NumRays);
		}
	}
}
