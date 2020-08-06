using UnityEngine;
using System.Collections.Generic;
using MEC;

public class UpDown : MonoBehaviour {

	public float amplitude;	//Set in Inspector 
	public float speed;	 //Set in Inspector 

	float startY;
	float tempY;

	void Start () {
		startY = transform.localPosition.y;
		Timing.RunCoroutine (_update ());
	}

	IEnumerator<float> _update (){   

		while (gameObject != null) {
			tempY = startY + amplitude * Mathf.Sin (speed * Time.time);
			transform.localPosition = new Vector3(transform.localPosition.x, tempY, transform.localPosition.z);
			yield return 0f;
		}
			
	}

}
