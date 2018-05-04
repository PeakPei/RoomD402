using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using System;

public class officeController : MonoBehaviour {

	public Camera vuforiaCamera;
	public GameObject officeAR1;
	public Text trackerText;
	private bool loadOnceCamera = false;

	// Use this for initialization
	void Start () {
		officeAR1.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		//TrackableBehaviour 
		Debug.Log ("List of trackables currently active: "+TrackableList.countTrackers());
		if (TrackableList.countTrackers () > 0 && TrackableList.countTrackers () <= 1) {
			if (loadOnceCamera == false) {
				foreach (TrackableBehaviour tb in TrackableList.getActiveTrackables()) {
					trackerText.text = "Tracking: " + tb.TrackableName;
					Debug.Log ("Tracking: " + tb.TrackableName);
					officeAR1.transform.parent = tb.gameObject.transform;
					if ("nudie_box".Equals (tb.TrackableName)) {
						setCoordCam(new Vector3 (-10.7f, -2.2f, -0.9f),new Vector3 (0f, 0f, 0f));
					} else if ("coffee".Equals (tb.TrackableName)) {
						setCoordCam(new Vector3 (-3.24f, -2.2f, -8.24f),new Vector3 (0f, 180f, 0f));
					} else if ("Room".Equals (tb.TrackableName)) {
						setCoordCam(new Vector3 (-6.61f, 2.15f, -20.48f),new Vector3 (-90f, 0f, 7.1f));
					} else {
						Debug.Log ("Object tracker " + tb.TrackableName + " does not have a child");
					}
					officeAR1.SetActive (true);
				}
				loadOnceCamera = true;
			}

		} else if (TrackableList.countTrackers () > 1) {
			restartExtTrack ();
			restartCoordCam ();
		} else {
			trackerText.text = "Tracking: Not tracker found";
			restartCoordCam ();
		}

	}

	private void setCoordCam(Vector3 position, Vector3 rotation){
		//vuforiaCamera.transform.localScale = new Vector3 (2.2f, 2.2f, 2.2f);
		vuforiaCamera.transform.position = position;
		vuforiaCamera.transform.rotation = Quaternion.Euler(rotation);
	}

	private void restartExtTrack(){
		TrackerManager.Instance.GetTracker<ObjectTracker> ().Stop ();
		TrackerManager.Instance.GetTracker<ObjectTracker> ().ResetExtendedTracking();
		TrackerManager.Instance.GetTracker<ObjectTracker> ().Start ();
	}
	private void restartCoordCam(){
		loadOnceCamera = false;
		officeAR1.transform.parent = null;
		//officeAR1.SetActive(false);
		vuforiaCamera.transform.position = new Vector3(0f,0f,0f);
		vuforiaCamera.transform.rotation = Quaternion.Euler (new Vector3(0f,0f,0f));
		vuforiaCamera.transform.localScale = new Vector3 (1f,1f,1f);
	}
}
