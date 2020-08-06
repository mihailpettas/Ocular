using UnityEngine;
using System.Collections.Generic;
using MEC;

public class Rotate : MonoBehaviour {

	[Header("Speed")]

	public float speed;

	[Header("Settings")]

	public bool randomizeDirection;
	public bool randomAxis;
	public bool randomizeOriginalRotation;
	public bool randomizeSpeed;

	[Header("Axes")]

	public bool x;
	public bool y;
	public bool z;



	void OnDestroy(){
		Timing.KillCoroutines ("rotation");
	}

	void Start(){

		if (randomizeSpeed) {
			speed = Random.Range (speed - speed / 2, speed + speed / 2);
		}

		if (randomizeOriginalRotation) {
			transform.localRotation = Quaternion.Euler (Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360));
		}

		if (randomizeDirection) {
			
			if (Random.Range (0.0f, 1.0f) > .5f) {
				speed = Mathf.Abs (speed);
			} else {
				speed = -Mathf.Abs (speed);
			}

		}

		if (randomAxis) {

			if (Random.Range (0.0f, 1.0f) > .5f) {
				x = true;
			} else {

				if (Random.Range (0.0f, 1.0f) > .5f) {
					y = true;
				} else {
					
					if (Random.Range (0.0f, 1.0f) > .5f) {
						z = true;
					} else {

						float rnd = Random.Range (0.0f, 1.0f);

						if (rnd <= .3f) {
							x = true;
						} else if (rnd > .3f && rnd <= .6f) {
							y = true;
						} else if (rnd > .6f) {
							z = true;
						}

					}

				}

			}

		}

		Timing.RunCoroutine (_rotate (), "rotation");

	}


	IEnumerator<float> _rotate () {
	
		while (gameObject != null) {

			if (x) {
				transform.Rotate (speed * Timing.DeltaTime, 0, 0);
			}
			if (y) {
				transform.Rotate (0, speed * Timing.DeltaTime, 0);
			}
			if (z) {
				transform.Rotate (0, 0, speed * Timing.DeltaTime);
			}

			yield return 0f;

		}
			
	}

}
