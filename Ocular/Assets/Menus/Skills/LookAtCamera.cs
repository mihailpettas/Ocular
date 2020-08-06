using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.SceneManagement;

public class LookAtCamera : MonoBehaviour {

	Transform cam;


	// Use this for initialization
	void Start () {

		if (SceneManager.GetActiveScene ().name == "AR") {
			cam = GameObject.Find ("ARCamera").transform;
		} else {
			cam = GameObject.Find ("Me").transform.Find ("Camera");
		}

		Timing.RunCoroutine (_update ().CancelWith(gameObject), "lookAtCamera");

	}


	IEnumerator<float> _update(){
		
		while (gameObject != null) {
			transform.LookAt (cam);
			yield return 0f;
		}

	}
		
}
