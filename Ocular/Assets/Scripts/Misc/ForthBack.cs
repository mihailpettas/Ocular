using UnityEngine;
using System.Collections;

public class ForthBack : MonoBehaviour {

	public float distance;
	public float speed;

	public float start;
	public bool forth;


	void Start(){
		start = transform.localPosition.z;
		//InvokeRepeating ("change", 0, time);
	}

	void Update(){

		if (forth) {		

			transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, Mathf.MoveTowards(transform.localPosition.z, start+distance, Time.deltaTime*speed));

			if (transform.localPosition.z == start + distance) {
				forth = false;
			}

		} else {

			transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, Mathf.MoveTowards(transform.localPosition.z, start-distance, Time.deltaTime*speed));

			if (transform.localPosition.z == start - distance) {
				forth = true;
			}

		}

	}

	void change(){
		forth = !forth;
	}

}
