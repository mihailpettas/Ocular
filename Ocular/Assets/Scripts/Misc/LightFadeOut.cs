using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

[RequireComponent(typeof(Light))]

public class LightFadeOut : MonoBehaviour {

	Light light;
	public float rate;
	public bool isExplosion;


	void Awake (){

		//Paw apo plagia gia na mi xrisimopoihsw gameobject.find
		if (QualitySettings.GetQualityLevel() == 1) {
			gameObject.SetActive (false);
		}

		light = GetComponent<Light> ();

		if (isExplosion) {
			Timing.RunCoroutine (_fadeOut ().CancelWith(gameObject), "light");
		}

	}

	public void runCoroutine (){
		Timing.RunCoroutine (_fadeOut ().CancelWith(gameObject), "light");
	}

	IEnumerator<float> _fadeOut (){

		while (gameObject != null) {

			light.intensity -= rate*Timing.DeltaTime;
			yield return 0f;

		}

	}

}
