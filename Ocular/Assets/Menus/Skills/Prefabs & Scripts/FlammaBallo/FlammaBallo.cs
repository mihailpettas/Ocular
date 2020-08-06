using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MEC;

public class FlammaBallo : MonoBehaviour {

	public GameObject collisionPref;
	public int skillLevel;
	public float speed;
	public float hitDistance = 1.5f;

	Light light;

	ParticleSystem.Burst burst;
	ParticleSystem.EmissionModule em;


	void Start (){

		if ((!Info.offline && GetComponent<PhotonView> ().isMine) || Info.offline) {
			name = "MyFlammaBallo" + skillLevel.ToString() + Random.Range(1, 100).ToString();
		} else{
			name = "NotMyFlammaBallo" + skillLevel.ToString () + Random.Range (1, 100).ToString ();
		}

		em = transform.Find ("Particles1").GetComponent<ParticleSystem>().emission;
		em.rateOverTime = new ParticleSystem.MinMaxCurve(Random.Range(100, 2000));

		em = transform.Find ("Particles2").GetComponent<ParticleSystem> ().emission;
		em.rateOverTime = new ParticleSystem.MinMaxCurve(Random.Range(100, 2000));

		em = transform.Find ("Drops").GetComponent<ParticleSystem> ().emission;
		em.rateOverTime = new ParticleSystem.MinMaxCurve(Random.Range(50, 400));

		em = transform.Find ("FireTrail").GetComponent<ParticleSystem> ().emission;
		em.rateOverTime = new ParticleSystem.MinMaxCurve(Random.Range(5, 15));

		em = transform.Find ("Distortion").GetComponent<ParticleSystem> ().emission;
		em.rateOverTime = new ParticleSystem.MinMaxCurve(Random.Range(50, 500));

		transform.SetParent (Info.se.activeSkills);
		light = transform.Find ("Point light").GetComponent<Light> ();
		Timing.RunCoroutine (_update ().CancelWith(gameObject), "moveFlammaBallo");

	}


	IEnumerator<float> _update(){

		while (gameObject != null) {

			transform.position += transform.forward * Timing.DeltaTime * speed;	//Koitaei panta brosta giati spawned me to rotation


			if (Vector3.Distance (transform.position, Vector3.zero) > 20 || transform.position.y < 0) {
				collision (0);
				yield break;
			}


			if (!Info.offline && GetComponent<PhotonView> ().isMine) {

				if (Vector3.Distance (transform.position, Info.opponent.transform.position) < hitDistance) {
					collision (1);
					yield break;
				}

			} else if (!Info.offline && !GetComponent<PhotonView> ().isMine) {

				if (Vector3.Distance (transform.position, Info.player.transform.position) < hitDistance) {
					collision (1);
					yield break;
				}

			}


			for (int i = 0; i < Info.se.activeSkills.childCount; i++) {

				if (!Info.offline && GetComponent<PhotonView> ().isMine) {					

					if (Vector3.Distance (transform.position, Info.se.activeSkills.GetChild (i).transform.position) < hitDistance && Info.se.activeSkills.GetChild (i).name.Contains ("NotMyProstatum0")) {
						collision (2, Info.se.activeSkills.GetChild (i).name);
						yield break;
					}
				
				} else if (!Info.offline && !GetComponent<PhotonView> ().isMine) {

					if (Vector3.Distance (transform.position, Info.se.activeSkills.GetChild (i).transform.position) < hitDistance && Info.se.activeSkills.GetChild (i).name.Contains ("MyProstatum0")) {
						collision (2, Info.se.activeSkills.GetChild (i).name);
						yield break;
					}

				}

			}

			/*for (int i = 0; i < Info.se.activeSummons.childCount; i++) {

				if (Vector3.Distance (transform.position, Info.se.activeSummons.GetChild (i).transform.position) < hitDistance) {
					collision (3, Info.se.activeSummons.GetChild(i).name);
					yield break;
				}

			}*/

			yield return 0f;

		}

	}


	void collision(int type, string skillName = "", string summonName = ""){

		print (type);

		if (!Info.offline && GetComponent<PhotonView> ().isMine) {

			if (type == 0) {

				if (!Info.offline) {
					GetComponent<Destroy> ().failed = true;
				}

			} else if (type == 1) {

				Info.pmv.RPC ("getDamage", PhotonTargets.Others, Info.se.skills [Info.se.skillRunning].damage [skillLevel] * Info.wandDamage * Info.myChar.damage);
				Info.pmv.RPC ("resetCombo", PhotonTargets.Others);

				Info.se.pointsCollected += Info.se.skills [Info.se.skillRunning].damage [skillLevel] * Info.wandDamage * Info.myChar.damage;
				Info.se.combo++;
				GetComponent<Destroy> ().failed = false;

			} else if (type == 2) {

				if (skillName.Contains ("NotMyProstatum1")) {
					GetComponent<Destroy> ().failed = true;
				} else if (skillName.Contains ("NotMyProstatum2")) {
					return;
				}

			} else if (type == 3) {



			}

		}


		GetComponent<Destroy> ().stopPS ();

		burst.count = new ParticleSystem.MinMaxCurve (Random.Range (100, 2000));
		collisionPref.transform.Find ("Drops").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);

		burst.count = new ParticleSystem.MinMaxCurve (Random.Range (100, 2000));
		collisionPref.transform.Find ("Sparks").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);

		burst.count = new ParticleSystem.MinMaxCurve (Random.Range (5, 20));
		collisionPref.transform.Find ("Fire").GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);

		Instantiate (collisionPref, transform.position, Quaternion.identity, Info.se.activeSkills);

	}


}
