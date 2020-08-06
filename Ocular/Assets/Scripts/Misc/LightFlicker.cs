using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	public float offSpeed = 0.05f;
	public float waitSpeedMin = .1f;
	public float waitSpeedMax = .75f;

	Light lightToFlick;
	LensFlare flare;
	Vector3 pos;

	float intensityOriginal;


	void Start(){

		lightToFlick = GetComponent<Light> ();
		intensityOriginal = lightToFlick.intensity;

		if (GetComponent<LensFlare> ()) {
			flare = GetComponent<LensFlare> ();
		}

		gameObject.SendMessage("flick");

	}

	IEnumerator flick (){

		lightToFlick.intensity = 0;
		flare.enabled = false;

		yield return new WaitForSeconds (offSpeed);

		lightToFlick.intensity = intensityOriginal;
		flare.enabled = true;

		yield return new WaitForSeconds (Random.Range(waitSpeedMin, waitSpeedMax));

		gameObject.SendMessage("flick");

	}

}
