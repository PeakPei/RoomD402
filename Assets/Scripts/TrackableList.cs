using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackableList : MonoBehaviour {

	static IEnumerable<TrackableBehaviour> activeTrackables;
	StateManager sm; 		// Get the Vuforia StateManager

	// Use this for initialization
	void Start () {
		sm = TrackerManager.Instance.GetStateManager ();
	}
	
	// Update is called once per frame
	void Update () {
		// Query the StateManager to retrieve the list of
		// currently 'active' trackables 
		//(i.e. the ones currently being tracked by Vuforia)
		activeTrackables = sm.GetActiveTrackableBehaviours ();

	}

	public static int countTrackers (){
		int count = 0;
		// Iterate through the list of active trackables
		try{
			foreach (TrackableBehaviour tb in activeTrackables) {
				count++;
			}
			return count;
		}catch{
			return 0;
		}

	}

	public static IEnumerable<TrackableBehaviour> getActiveTrackables (){
		return activeTrackables;
	}
}
