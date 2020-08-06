using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MEC;

public class Fragor : MonoBehaviour {

	public int skillLevel;
	public float hitDistance = 1.5f;

	public GameObject[] explosions;
	ParticleSystem.Burst burst;


	void Start (){

		if ((!Info.offline && GetComponent<PhotonView> ().isMine) || Info.offline) {
			name = "MyFragor" + skillLevel.ToString() + Random.Range(1, 100).ToString();
		} else{
			name = "NotMyFragor" + skillLevel.ToString () + Random.Range (1, 100).ToString ();
		}


		transform.SetParent (Info.se.activeSkills);

		if (gameObject.name.Contains("Fragor0")) {

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(5, 10));
			explosions [0].transform.Find ("Distortion").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(5, 20));
			explosions [0].transform.Find ("Particles1").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(5, 50));
			explosions [0].transform.Find ("Particles1").GetComponent<ParticleSystem> ().emission.SetBurst (1, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(10, 100));
			explosions [0].transform.Find ("Particles1").GetComponent<ParticleSystem> ().emission.SetBurst (2, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(5, 20));
			explosions [0].transform.Find ("Particles1").GetComponent<ParticleSystem> ().emission.SetBurst (3, burst);

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(5, 10), Random.Range(10, 20));
			explosions [0].transform.Find ("Particles2").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(100, 2000));
			explosions [0].transform.Find ("Particles2").GetComponent<ParticleSystem> ().emission.SetBurst (1, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(100, 2000));
			explosions [0].transform.Find ("Particles2").GetComponent<ParticleSystem> ().emission.SetBurst (2, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(100, 2000));
			explosions [0].transform.Find ("Particles2").GetComponent<ParticleSystem> ().emission.SetBurst (3, burst);

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(5, 10));
			explosions [0].transform.Find ("Flash").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);

			Instantiate (explosions [0], transform.position, Quaternion.identity, transform);

		} else if (gameObject.name.Contains("Fragor1")) {
			
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 3));
			explosions [0].transform.Find ("DustAngle10").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 2));
			explosions [0].transform.Find ("DustAngle10").GetComponent<ParticleSystem> ().emission.SetBurst (1, burst);

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 4));
			explosions [0].transform.Find ("DustAngle45").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 4));
			explosions [0].transform.Find ("DustAngle45").GetComponent<ParticleSystem> ().emission.SetBurst (1, burst);

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 7));
			explosions [0].transform.Find ("BlackParticlesAngle50").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 7));
			explosions [0].transform.Find ("BlackParticlesAngle50").GetComponent<ParticleSystem> ().emission.SetBurst (1, burst);

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 3));
			explosions [0].transform.Find ("BlackParticlesAngle15").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 3));
			explosions [0].transform.Find ("BlackParticlesAngle15").GetComponent<ParticleSystem> ().emission.SetBurst (1, burst);

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 10));
			explosions [0].transform.Find ("Sparks").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 20));
			explosions [0].transform.Find ("Sparks").GetComponent<ParticleSystem> ().emission.SetBurst (1, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 30));
			explosions [0].transform.Find ("Sparks").GetComponent<ParticleSystem> ().emission.SetBurst (2, burst);

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 30));
			explosions [0].transform.Find ("Stones1Angle3").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 5));
			explosions [0].transform.Find ("Stones1Angle10").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 10));
			explosions [0].transform.Find ("Stones1Angle10").GetComponent<ParticleSystem> ().emission.SetBurst (1, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 15));
			explosions [0].transform.Find ("Stones1Angle10").GetComponent<ParticleSystem> ().emission.SetBurst (2, burst);

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 10));
			explosions [0].transform.Find ("Stones1Angle40").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 10));
			explosions [0].transform.Find ("Stones1Angle40").GetComponent<ParticleSystem> ().emission.SetBurst (1, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 10));
			explosions [0].transform.Find ("Stones1Angle40").GetComponent<ParticleSystem> ().emission.SetBurst (2, burst);

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 5));
			explosions [0].transform.Find ("Stones1Big").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 5));
			explosions [0].transform.Find ("Stones1Big").GetComponent<ParticleSystem> ().emission.SetBurst (1, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 5));
			explosions [0].transform.Find ("Stones1Big").GetComponent<ParticleSystem> ().emission.SetBurst (2, burst);

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 5));
			explosions [0].transform.Find ("Stones2Big").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 5));
			explosions [0].transform.Find ("Stones2Big").GetComponent<ParticleSystem> ().emission.SetBurst (1, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 5));
			explosions [0].transform.Find ("Stones2Big").GetComponent<ParticleSystem> ().emission.SetBurst (2, burst);

			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 5));
			explosions [0].transform.Find ("Stones2Angle15").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 10));
			explosions [0].transform.Find ("Stones2Angle15").GetComponent<ParticleSystem> ().emission.SetBurst (1, burst);
			burst.count = new ParticleSystem.MinMaxCurve(Random.Range(1, 15));
			explosions [0].transform.Find ("Stones2Angle15").GetComponent<ParticleSystem> ().emission.SetBurst (2, burst);

			Instantiate (explosions [0], new Vector3(transform.position.x, 5, transform.position.z), Quaternion.identity, transform);

		} else if (gameObject.name.Contains("Fragor2")) {			
			Instantiate (explosions [Random.Range (0, explosions.Length - 1)], transform.position, Quaternion.identity, transform);
			Timing.RunCoroutine (_update ().CancelWith (gameObject));
		}

	
		if (!Info.offline && GetComponent<PhotonView>().isMine) {
			
			if (!Info.offline && GetComponent<PhotonView> ().isMine) {

				if (Vector3.Distance (transform.position, Info.opponent.transform.position) < hitDistance) {
					collision (1);
				}

			} else if (!Info.offline && !GetComponent<PhotonView> ().isMine) {

				if (Vector3.Distance (transform.position, Info.player.transform.position) < hitDistance) {
					collision (1);
				}

			}


			for (int i = 0; i < Info.se.activeSkills.childCount; i++) {

				if (!Info.offline && GetComponent<PhotonView> ().isMine) {					

					if (Vector3.Distance (transform.position, Info.se.activeSkills.GetChild (i).transform.position) < hitDistance && Info.se.activeSkills.GetChild (i).name.Contains ("NotMyProstatum")) {
						collision (2, Info.se.activeSkills.GetChild (i).name);
					}

				} else if (!Info.offline && !GetComponent<PhotonView> ().isMine) {

					if (Vector3.Distance (transform.position, Info.se.activeSkills.GetChild (i).transform.position) < hitDistance && Info.se.activeSkills.GetChild (i).name.Contains ("MyProstatum")) {
						collision (2, Info.se.activeSkills.GetChild (i).name);
					}

				}

			}


			//for (int i = 0; i < Info.se.activeSummons.childCount; i++) {

				//if (Vector3.Distance (transform.position, Info.se.activeSummons.GetChild (i).transform.position) < hitDistance && transform) {
				//	collision (3, Info.se.activeSummons.GetChild(i).name);
				//}

			//}
	
		}

	}


	IEnumerator<float> _update(){

		yield return Timing.WaitForSeconds (1);

		while (gameObject != null) {

			if (transform.childCount == 0) {
				GetComponent<Destroy> ().destroy (0);
			}

			yield return 0f;

		}

	}
		

	void collision(int type, string skillName = "", string summonName = ""){

		if (type == 1) {
			
			Info.pmv.RPC("getDamage", PhotonTargets.Others, Info.se.skills [Info.se.skillRunning].damage [skillLevel] * Info.wandDamage * Info.myChar.damage);
			Info.pmv.RPC ("resetCombo", PhotonTargets.Others);

			Info.se.pointsCollected += Info.se.skills [Info.se.skillRunning].damage [skillLevel] * Info.wandDamage * Info.myChar.damage;
			Info.se.combo++;
			GetComponent<Destroy> ().failed = false;

		} else if (type == 2) {

			if (skillName == "NotMyProstatum") {
				GetComponent<Destroy> ().failed = true;
			}

		} else if (type == 3) {



		}

	}
		
}
