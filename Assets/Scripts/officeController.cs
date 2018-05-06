using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using System;

public class officeController : MonoBehaviour {

	public Camera vuforiaCamera;
	//public GameObject office3DPrefab;
	public GameObject office3D;
	public Text trackerText;

	//private GameObject officeARClone;
	private bool loadOnceCamera = false;
	private List<TrackableBehaviour> currentTrackers;
	private GameObject[] allTargetsVuforia;

	// Use this for initialization
	void Start () {
		allTargetsVuforia = GameObject.FindGameObjectsWithTag ("Target");
		currentTrackers = new List<TrackableBehaviour> ();
		office3D.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("List of trackables currently active: "+TrackableList.countTrackers());
		if (TrackableList.countTrackers () == 0){
			trackerText.text = "Tracking: Not tracker found";
			restartExtTrack ();
		} else if (TrackableList.countTrackers () == 1) {
			if (loadOnceCamera == false) {
				loadOnceCamera = true;
				if (currentTrackers.Count > 0) {
					restartExtTrack ();
				}
				TrackerManager.Instance.GetTracker<ObjectTracker> ().Start();
				foreach (TrackableBehaviour tb in TrackableList.getActiveTrackables()) {
					trackerText.text = "Tracking: " + tb.TrackableName;
					Debug.Log ("Tracking: " + tb.TrackableName+" status: "+tb.CurrentStatus);
					if (startAugmentation (tb)) {
						office3D.transform.parent = tb.gameObject.transform;
						Debug.Log ("Cool1");
						tb.GetComponent<ObjectTargetBehaviour> ().ObjectTarget.StartExtendedTracking ();
						Debug.Log ("Cool2");
					}
				}
			}
		} else if (TrackableList.countTrackers () > 1) {
			restartExtTrack ();
		} 
	}

	private bool startAugmentation (TrackableBehaviour tb){
		currentTrackers.Add (tb);
		office3D.SetActive(true);
		//officeARClone = Instantiate (office3DPrefab);
		if (getTargetByName (tb.TrackableName) != null) {
			setCoord (tb, getTargetByName (tb.TrackableName).transform);
			return true;
		} else {
			Debug.Log ("Object tracker " + tb.TrackableName + " does not have a child");
			restartExtTrack ();
			return false;
		}
	}

	private void restartExtTrack(){
		//Debug.Log ("Current Trackers ("+currentTrackers.Count+") "+currentTrackers.ToArray().ToString());
		try{
			loadOnceCamera = false;
			try{
				office3D.SetActive(false);
				//Destroy(officeARClone);
			}catch{};

			bool resetTracking = false;
			foreach (TrackableBehaviour tb in currentTrackers) {
				currentTrackers.Remove(tb);
				//Debug.Log (tb.TrackableName+" is "+tb.GetComponent<ObjectTargetBehaviour> ().ObjectTarget.IsExtendedTrackingStarted());
				tb.GetComponent<ObjectTargetBehaviour> ().ObjectTarget.StopExtendedTracking ();
				resetTracking = true;
			}
			if(resetTracking){
				TrackerManager.Instance.GetTracker<ObjectTracker> ().Stop ();
				TrackerManager.Instance.GetTracker<ObjectTracker> ().ResetExtendedTracking();
			}
		}catch(Exception e){
			//Debug.Log ("Couln't reset extTrack: "+e.Message);
		}
	}

	private void setCoord(TrackableBehaviour tb, Transform objectRoom){
		tb.transform.position = objectRoom.position;
		tb.transform.rotation = objectRoom.rotation;
		//vuforiaCamera.transform.position = position;
		//vuforiaCamera.transform.rotation = Quaternion.Euler(rotation);
	}

	private void restartCoordCam(){
		//vuforiaCamera.transform.position = new Vector3(0f,0f,0f);
		//vuforiaCamera.transform.rotation = Quaternion.Euler (new Vector3(0f,0f,0f));
		//vuforiaCamera.transform.localScale = new Vector3 (1f,1f,1f);
	}

	private GameObject getTargetByName(string targetName){
		foreach(GameObject target in allTargetsVuforia){
			if (target.name == targetName)
				return target;
		}
		return null;
	}
}
