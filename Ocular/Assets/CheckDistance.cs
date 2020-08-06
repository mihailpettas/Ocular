using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CheckDistance : MonoBehaviour {

	public Transform o1, o2;

	
	// Update is called once per frame
	void Update () {
		
		if (o1 && o2) {
			print (Vector3.Distance (o1.position, o2.position));
		}

	}

}
