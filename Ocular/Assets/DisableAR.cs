using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class DisableAR : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Camera cam = GetComponent<Camera> ();

		if(cam.GetComponent<VuforiaBehaviour>() != null) {
			Destroy (cam.GetComponent<VuforiaBehaviour> ());
		}

		if(cam.GetComponent<VideoBackgroundBehaviour>() != null) {
			Destroy (cam.GetComponent<VideoBackgroundBehaviour> ());
		}

		if(cam.GetComponent<DefaultInitializationErrorHandler>() != null) {
			Destroy (cam.GetComponent<DefaultInitializationErrorHandler> ());
		}

		if (cam.transform.Find ("BackgroundPlane")) {
			Destroy (cam.transform.Find ("BackgroundPlane").gameObject);
		}

		cam.clearFlags = CameraClearFlags.Skybox;

	}

}