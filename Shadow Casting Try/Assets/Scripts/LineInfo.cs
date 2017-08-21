using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineInfo {

	public float Angle;
	public List<Vector3> Points;

	public LineInfo(float _angle){
		this.Angle = _angle;
		this.Points = new List<Vector3> ();
	}
}
