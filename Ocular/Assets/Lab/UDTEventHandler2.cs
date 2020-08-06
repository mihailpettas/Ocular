/*============================================================================== 
 Copyright (c) 2016 PTC Inc. All Rights Reserved.
 
 Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

public class UDTEventHandler2 : MonoBehaviour, IUserDefinedTargetEventHandler {


	/// Can be set in the Unity inspector to reference a ImageTargetBehaviour that is instanciated for augmentations of new user defined targets.
	public ImageTargetBehaviour ImageTargetTemplate;


	UserDefinedTargetBuildingBehaviour mTargetBuildingBehaviour;

	ObjectTracker mObjectTracker;

	// DataSet that newly defined targets are added to
	DataSet mBuiltDataSet;

	// Currently observed frame quality
	ImageTargetBuilder.FrameQuality mFrameQuality = ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE;


	public void Start() {

		mTargetBuildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour>();

		if (mTargetBuildingBehaviour) {
			mTargetBuildingBehaviour.RegisterEventHandler(this);
			Debug.Log("Registering User Defined Target event handler.");
		}

	}


	/// Called when UserDefinedTargetBuildingBehaviour has been initialized successfully
	public void OnInitialized() {

		mObjectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

		if (mObjectTracker != null) {
			// Create a new dataset
			mBuiltDataSet = mObjectTracker.CreateDataSet();
			mObjectTracker.ActivateDataSet(mBuiltDataSet);
		}

	}


	/// Updates the current frame quality
	public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality) {
		Debug.Log("Frame quality changed: " + frameQuality.ToString());
		mFrameQuality = frameQuality;
	}


	void Update(){

		if (Input.GetMouseButtonUp(0) && (((Input.acceleration.x > -.2f && Input.acceleration.x < .2f) && (Input.acceleration.y > -.2f && Input.acceleration.y < .2f)) || Application.isEditor) && (mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_MEDIUM ||mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH)) {
			BuildNewTarget ();
		}

	}


	/// Takes a new trackable source and adds it to the dataset
	/// This gets called automatically as soon as you 'BuildNewTarget with UserDefinedTargetBuildingBehaviour
	public void OnNewTrackableSource(TrackableSource trackableSource) {

		// Deactivates the dataset first
		mObjectTracker.DeactivateDataSet(mBuiltDataSet);

		// Get predefined trackable and instantiate it
		ImageTargetBehaviour imageTargetCopy = (ImageTargetBehaviour)Instantiate(ImageTargetTemplate);
		imageTargetCopy.gameObject.name = "ImageTarget";

		// Add the duplicated trackable to the data set and activate it
		mBuiltDataSet.CreateTrackable(trackableSource, imageTargetCopy.gameObject);

		// Activate the dataset again
		mObjectTracker.ActivateDataSet(mBuiltDataSet);

		// Extended Tracking with user defined targets only works with the most recently defined target.
		// If tracking is enabled on previous target, it will not work on newly defined target.
		// Don't need to call this if you don't care about extended tracking.
		//StopExtendedTracking();
		// mObjectTracker.Stop();
		// mObjectTracker.ResetExtendedTracking();
		// mObjectTracker.Start();

		// Make sure TargetBuildingBehaviour keeps scanning...
		//mTargetBuildingBehaviour.StartScanning();

	}



	/// Instantiates a new user-defined target and is also responsible for dispatching callback to 
	/// IUserDefinedTargetEventHandler::OnNewTrackableSource
	public void BuildNewTarget() {

		if (mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_MEDIUM || mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH) {

			// create the name of the next target.
			// the TrackableName of the original, linked ImageTargetBehaviour is extended with a continuous number to ensure unique names
			string targetName = ImageTargetTemplate.TrackableName;

			// generate a new target:
			mTargetBuildingBehaviour.BuildNewTarget(ImageTargetTemplate.TrackableName, ImageTargetTemplate.GetSize().y);

		} else {
			Debug.Log("Cannot build new target, due to poor camera image quality");
		}

	}


	/*  /// This method only demonstrates how to handle extended tracking feature when you have multiple targets in the scene
	/// So, this method could be removed otherwise
	private void StopExtendedTracking(){

		// If Extended Tracking is enabled, we first disable it for all the trackables
		// and then enable it only for the newly created target
		bool extTrackingEnabled = mTrackableSettings && mTrackableSettings.IsExtendedTrackingEnabled();

		if (extTrackingEnabled)
		{
			StateManager stateManager = TrackerManager.Instance.GetStateManager();

			// 1. Stop extended tracking on all the trackables
			foreach (var tb in stateManager.GetTrackableBehaviours()){

				var itb = tb as ImageTargetBehaviour;

				if (itb != null){
					itb.ImageTarget.StopExtendedTracking();
				}

			}

			// 2. Start Extended Tracking on the most recently added target
			List<TrackableBehaviour> trackableList = stateManager.GetTrackableBehaviours().ToList();
			ImageTargetBehaviour lastItb = trackableList[LastTargetIndex] as ImageTargetBehaviour;

			if (lastItb != null){
				if (lastItb.ImageTarget.StartExtendedTracking ()) {
					Debug.Log ("Extended Tracking successfully enabled for " + lastItb.name);
				}
			}

		}

	}*/

}