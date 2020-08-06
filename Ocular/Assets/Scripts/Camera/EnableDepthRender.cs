using UnityEngine;
using System.Collections;

//Τρωει, μονο στα μενου

[RequireComponent(typeof(Camera))]

public class EnableDepthRender : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Camera> ().depthTextureMode = DepthTextureMode.DepthNormals;
	}

}
