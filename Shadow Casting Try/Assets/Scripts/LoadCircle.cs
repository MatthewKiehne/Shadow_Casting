using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadCircle : MonoBehaviour {

	private Image loadCircle;
	private float fillTime = 3f;
	private float timeSinceStart = 0f;

	// Use this for initialization
	void Start () {

		this.loadCircle = this.transform.GetComponent<Image> ();
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void updateCircle(float time){
		this.timeSinceStart += time;

		if (timeSinceStart > fillTime) {
			this.loadCircle.fillAmount = 1f;
		} else {
			this.loadCircle.fillAmount = this.timeSinceStart / fillTime;
		}
	}

	public void clearTime(){
		this.timeSinceStart = 0f;
	}
}
