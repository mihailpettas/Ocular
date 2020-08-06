using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	public GameObject bombaPref;
	GameObject bomba;

	public float force, speed, turnspeed;
	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Space)) {
			bomba = Instantiate (bombaPref, transform.localPosition, bombaPref.transform.localRotation) as GameObject;
			bomba.GetComponent<Rigidbody> ().AddForce (Vector3.up * force);
		}

		if (bomba && bomba.transform.localPosition.y > target.localPosition.y) {
			Vector3 lookPos = target.position - bomba.transform.localPosition;
			bomba.transform.localRotation = Quaternion.LookRotation (lookPos * Time.deltaTime * 10000);

			bomba.GetComponent<Rigidbody>().MovePosition (bomba.transform.localPosition + bomba.transform.forward * speed * Time.deltaTime);
		}

	}

}
