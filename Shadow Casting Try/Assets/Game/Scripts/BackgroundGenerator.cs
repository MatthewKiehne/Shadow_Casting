using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour {

	public List<Sprite> tileSprites;
	public Material mask;

	private int height = 20;
	private int width = 20;

	// Use this for initialization
	void Start () {

		Vector3 offset = new Vector3(this.width,this.height,0) * -2;

		for (int x = 0; x < this.width; x++) {
			for (int y = 0; y < this.height; y++) {
				GameObject go = new GameObject ();
				SpriteRenderer sr = go.AddComponent<SpriteRenderer> ();
				sr.sprite = this.tileSprites [Random.Range (0, this.tileSprites.Count)];
				go.transform.position = (new Vector3 (x, y, 0) * 4 ) + offset;
				go.transform.Rotate (0f, 0f, 90 * Random.Range (0, 4));
				go.transform.SetParent (this.transform);
				sr.material = mask;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
