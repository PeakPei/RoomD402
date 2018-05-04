using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCController : MonoBehaviour {
	public float translationCam = 1f;
	public float straffeCam = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float translation = Input.GetAxis ("Vertical") * translationCam;
		float straffe = Input.GetAxis ("Horizontal") * straffeCam;
		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;

		transform.Translate (straffe, 0 , translation);

	}
}
