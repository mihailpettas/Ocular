using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRenderDepthTexture : MonoBehaviour {

	public void setCameraMode(DepthTextureMode mode){
		GetComponent<Camera> ().depthTextureMode = mode;
	}

}
