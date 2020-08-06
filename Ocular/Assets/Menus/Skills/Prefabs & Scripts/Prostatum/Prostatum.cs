using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MEC;
using UnityEngine.SceneManagement;

public class Prostatum : MonoBehaviour {

	public int skillLevel;
	public float hitDistance = 1.5f;


	void Start (){

		if ((!Info.offline && GetComponent<PhotonView> ().isMine) || Info.offline) {
			name = "MyProstatum" + skillLevel.ToString() + Random.Range(1, 100).ToString();
		} else{
			name = "NotMyProstatum" + skillLevel.ToString () + Random.Range (1, 100).ToString ();
		}


		transform.SetParent (Info.se.activeSkills);

		for (int i = 0; i < Info.se.activeSkills.childCount; i++) {

			if (Vector3.Distance (transform.position, Info.se.activeSkills.GetChild (i).transform.position) < hitDistance && Info.se.activeSkills.GetChild (i).name.Contains("NotMyAxcendia") || Info.se.activeSkills.GetChild (i).name.Contains("NotMyFlammaBallo")) {
				collision (2, Info.se.activeSkills.GetChild(i).name);
			}

		}

		/*for (int i = 0; i < Info.se.activeSummons.childCount; i++) {

			if (Vector3.Distance (transform.position, Info.se.activeSummons.GetChild (i).transform.position) < hitDistance && transform != Info.se.activeSkills.GetChild (i).transform) {
				collision (3, Info.se.activeSummons.GetChild(i).name);
			}

		}*/

	}


	void collision(int type, string skillName = "", string summonName = ""){

		if (type == 1) {

			if (skillLevel == 1) {

				transform.Find ("Sparks").GetComponent<ParticleSystem> ().Play ();

				if (SceneManager.GetActiveScene ().name == "AR") {	

					if (GetComponent<PhotonView> ().isMine) {
						Info.se.activeSkills.Find (skillName).rotation = Quaternion.LookRotation (Info.opponent.transform.forward);
					} else {
						Info.se.activeSkills.Find (skillName).rotation = Quaternion.LookRotation (Info.player.transform.forward);
					}

				} else {

					if (GetComponent<PhotonView> ().isMine) {
						Info.se.activeSkills.Find (skillName).rotation = Quaternion.LookRotation (Info.opponent.transform.Find ("Camera").forward);
					} else {
						Info.se.activeSkills.Find (skillName).rotation = Quaternion.LookRotation (Info.player.transform.Find ("Camera").forward);
					}

				}

			}

		} else if (type == 2) {



		}

	}

}