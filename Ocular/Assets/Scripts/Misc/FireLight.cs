using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class FireLight : MonoBehaviour {

	public float maxDistance;
	public float speed = 1;
	public float distance;

	private Light _lightSource;


	public void Start() {
		_lightSource = GetComponent<Light>();
		Timing.RunCoroutine(_doFlicker());	
	}


	IEnumerator<float> _doFlicker() {

		while (gameObject != null) {

			distance = Random.Range (0, maxDistance);

			while (_lightSource.intensity != distance && _lightSource.intensity != 0) {
				_lightSource.intensity = Mathf.MoveTowards (_lightSource.intensity, distance, speed * Timing.DeltaTime);
				yield return 0f;
			}
				
			yield return 0f;

		}
			
	}

}
