using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MEC;

public class Axcendia: MonoBehaviour {

	public int skillLevel;
	public float speed;
	public float hitDistance = 1.5f;

	Light light;


	void Start (){

		if ((!Info.offline && GetComponent<PhotonView> ().isMine) || Info.offline) {
			name = "MyAxcendia" + skillLevel.ToString() + Random.Range(1, 100).ToString();
		} else{
			name = "NotMyAxcendia" + skillLevel.ToString () + Random.Range (1, 100).ToString ();
		}

		transform.SetParent (Info.se.activeSkills);
		light = transform.Find ("Point light").GetComponent<Light> ();
		Timing.RunCoroutine (_update ().CancelWith(gameObject), "moveAxcendia");

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

					if (Vector3.Distance (transform.position, Info.se.activeSkills.GetChild (i).transform.position) < hitDistance && Info.se.activeSkills.GetChild (i).name.Contains ("NotMyProstatum")) {
						collision (2, Info.se.activeSkills.GetChild (i).name);
						yield break;
					}

				} else if (!Info.offline && !GetComponent<PhotonView> ().isMine) {

					if (Vector3.Distance (transform.position, Info.se.activeSkills.GetChild (i).transform.position) < hitDistance && Info.se.activeSkills.GetChild (i).name.Contains ("MyProstatum")) {
						collision (2, Info.se.activeSkills.GetChild (i).name);
						yield break;
					}

				}

			}

			/*for (int i = 0; i < Info.se.activeSummons.childCount; i++) {

				if (Vector3.Distance (transform.position, Info.se.activeSummons.GetChild (i).transform.position) < hitDistance) {
					collision (3, Info.se.activeSummons.GetChild (i).name);
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


		transform.Find("Ball").gameObject.SetActive(false);
		light.intensity = 0;	//pairnei ti thesi tou to light ap to collision

		if (skillLevel == 0) {

			transform.Find ("Flare").GetComponent<Flares> ().go ();

		} else if (skillLevel == 1) {

			if (Random.Range (0f, 1f) < .75f) {
				transform.Find ("Flare1").localPosition = Random.Range (-2, 2) * Vector3.one;
				transform.Find ("Flare1").GetComponent<Flares> ().go (Random.Range(.05f, .2f));
			}
			if (Random.Range (0f, 1f) < .75f) {
				transform.Find ("Flare2").localPosition = Random.Range (-2, 2) * Vector3.one;
				transform.Find ("Flare2").GetComponent<Flares> ().go (Random.Range(.05f, .2f));
			}
			if (Random.Range (0f, 1f) < .75f) {
				transform.Find ("Flare3").localPosition = Random.Range (-2, 2) * Vector3.one;
				transform.Find ("Flare3").GetComponent<Flares> ().go (Random.Range(.05f, .2f));
			}

		}

		GetComponent<Destroy> ().destroy (0);

	}

}
