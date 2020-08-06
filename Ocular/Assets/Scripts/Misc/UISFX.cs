using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFX : MonoBehaviour {

	AudioSource aus;
	public AudioClip clip;

	// Use this for initialization
	void Start () {
		aus = GetComponent<AudioSource> ();
	}
	
	public void uiSound(){
		aus.clip = clip;
		aus.Play ();
	}

}
