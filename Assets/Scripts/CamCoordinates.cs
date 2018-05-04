using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CamCoordinates : MonoBehaviour {

	public Text coordText;
	public Camera camVuforia;

	// Use this for initialization
	void Start () {
		//camVuforia = GetComponent<Camera> ();
		coordText.text = camVuforia.transform.position.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		coordText.text = camVuforia.transform.position.ToString();
	}


	public void startingCoord(){
		
		camVuforia.transform.Translate (0,0,0);
	}
}
