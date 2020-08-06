using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class UIDirection : MonoBehaviour {

	Vector3 dir;
	public float speed;


	// Use this for initialization
	void Start () {
		Timing.RunCoroutine (_setDir (), "setDir");
		Timing.RunCoroutine (_update (), "update");
	}

	// Update is called once per frame
	IEnumerator<float> _setDir () {
		
		while (gameObject != null) {

			dir = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), 0);
			yield return Timing.WaitForSeconds (5);

		}
			
	}

	IEnumerator<float> _update(){
		
		while (gameObject != null) {
			GetComponent<RectTransform> ().Translate (dir * Timing.DeltaTime * speed);
			yield return 0f;
		}
			
	}

}
