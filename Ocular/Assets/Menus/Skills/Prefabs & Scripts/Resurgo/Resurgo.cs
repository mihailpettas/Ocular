using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Resurgo: MonoBehaviour {

	public GameObject effect;


	void Start (){

		if ((!Info.offline && GetComponent<PhotonView> ().isMine) || Info.offline) {
			name = "MyResurgo" + Random.Range(1, 100).ToString();
		} else{
			name = "NotMyResurgo" + Random.Range (1, 100).ToString ();
		}


		transform.SetParent (Info.se.activeSkills);


		if (GetComponent<PhotonView> ().isMine) {

			Instantiate (effect, new Vector3 (Info.player.transform.position.x, 1.5f, Info.player.transform.position.z), Quaternion.identity, transform);

			if (SceneManager.GetActiveScene ().name == "AR") {
				GetComponent<PhotonView> ().RPC ("setPositions", PhotonTargets.Others, Info.myCtrl.target);
				Info.player.transform.position = new Vector3(Info.myCtrl.target.x, 0, Info.myCtrl.target.z);
				Instantiate (effect, Info.myCtrl.target, Quaternion.identity, transform);
			} else {
				GetComponent<PhotonView> ().RPC ("setPositions", PhotonTargets.Others, Info.se.target);
				Info.player.transform.position = new Vector3(Info.se.target.x, 0, Info.myCtrl.target.z);
				Instantiate (effect, Info.se.target, Quaternion.identity, transform);
			}

		}

		Timing.RunCoroutine (_update ().CancelWith (gameObject));

	}


	IEnumerator<float> _update(){

		yield return Timing.WaitForSeconds (1);

		while (gameObject != null) {

			if (transform.childCount == 0) {

				if (!Info.offline && GetComponent<PhotonView>().isMine) {
					PhotonNetwork.Destroy (PhotonView.Find (GetComponent<PhotonView> ().viewID));
				} else {
					Destroy (gameObject);
				}

			}

			yield return 0f;

		}

	}


	[PunRPC]
	void setPositions(Vector3 pos){
		Instantiate (effect, new Vector3 (Info.opponent.transform.position.x, 1.5f, Info.opponent.transform.position.z), Quaternion.identity, transform);
		Instantiate (effect, pos, Quaternion.identity, transform);
	}

}
