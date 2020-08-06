using UnityEngine;
using System.Collections;

public class RandomizeGlobe : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		GetComponent<MeshRenderer> ().material.SetFloat ("_LAYER_1_SCROLL_Y", Random.Range(-.1f, .1f));
		GetComponent<MeshRenderer> ().material.SetFloat ("_LAYER_1_SCROLL_X", Random.Range(-.1f, .1f));
		GetComponent<MeshRenderer> ().material.SetFloat ("_LAYER_2_SCROLL_Y", Random.Range(-.1f, .1f));
		GetComponent<MeshRenderer> ().material.SetFloat ("_LAYER_2_SCROLL_X", Random.Range(-.1f, .1f));
		GetComponent<MeshRenderer> ().material.SetFloat ("_LAYER_3_SCROLL_Y", Random.Range(-.1f, .1f));
		GetComponent<MeshRenderer> ().material.SetFloat ("_LAYER_3_SCROLL_X", Random.Range(-.1f, .1f));

	}

}
