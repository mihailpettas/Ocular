using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class Flares : MonoBehaviour {

	public int min, max;
	bool turn;
	float rate;


	public void go(float delay = 0){
		rate = Info.rot (10, 100, max);
		Timing.RunCoroutine (_update (delay).CancelWith (gameObject));
	}

	// Update is called once per frame
	IEnumerator<float> _update (float delay) {

		yield return Timing.WaitForSeconds (delay);

		while (gameObject != null) {

			if (!turn) {

				transform.localScale += Vector3.one * rate * Time.deltaTime;

				if (transform.localScale.x > Random.Range(min, max)) {
					turn = true;
				}

			} else {

				transform.localScale -= Vector3.one * rate * Time.deltaTime;

				if (transform.localScale.x < 0 && turn) {
					Destroy (gameObject);
				}

			}

			yield return 0f;

		}

	}

}
