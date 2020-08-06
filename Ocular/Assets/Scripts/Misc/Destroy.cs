using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.Networking;

public class Destroy : MonoBehaviour {

	public ParticleSystem[] ps = new ParticleSystem[1];
	bool[] psDones;

	public bool failed;
	bool finale;


	// Use this for initialization
	void Start () {

		if (ps.Length > 0) {
			psDones = new bool[ps.Length];
			Timing.RunCoroutine (_update ().CancelWith (gameObject));
		}

	}


	IEnumerator<float> _update(){

		while (gameObject != null) {

			if(ps[0].time > 0){

				for (int i = 0; i < ps.Length; i++) {

					if (ps [i].particleCount == 0) {
						psDones [i] = true;
					}

				}

				finale = true;

				for (int i = 0; i < ps.Length; i++) {

					if (!psDones[i]) {
						finale = false;
					}

				}

				if(finale){
					destroy();
					yield break;
				}

			}

			yield return 0;

		}

	}


	//Kalountai apo to main script opote panta Authority = true
	public void destroy(float secs = 0){

		if (secs != 0) {
			Timing.RunCoroutine (_destroy (secs));
		} else {

			if (!Info.offline) {

				if (GetComponent<PhotonView> ()) {	// ???
					Info.pmv.RPC ("destroyOnNetwork", PhotonTargets.All, GetComponent<PhotonView> ().viewID);
				}

				if (failed) {
					Info.se.combo = 1;
				}

			} else {
				Destroy (gameObject);
			}

		}

	}


	public IEnumerator<float> _destroy(float secs){
		yield return Timing.WaitForSeconds (secs);
		destroy ();
	}


	public void stopPS(){

		for (int i = 0; i < ps.Length; i++) {
			ps [i].Stop ();
		}

	}

}
