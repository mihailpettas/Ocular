using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using MEC;

[RequireComponent(typeof(PhotonView))]
public class PhotonLerp : Photon.MonoBehaviour {

	//public int sendRate;

	public float speed = 10;
	public float turnSpeed = 10;

	float x;
	float z;
	float rotY;

	Vector3 velocity1;
	float velocity2;

	PhotonView pv;


	void Start (){

		pv = GetComponent<PhotonView> ();

		if (!pv.isMine) {
			Timing.RunCoroutine (_update ());
		}

		//PhotonNetwork.sendRateOnSerialize = sendRate;

	}


	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

		if (stream.isWriting) {

			if (pv && pv.isMine) {
				stream.SendNext (transform.position.x);
				stream.SendNext (transform.position.z);
				stream.SendNext (transform.rotation.y);
			}

		}else{
			
			if (pv && !pv.isMine) {				
				x = (float)stream.ReceiveNext ();
				z = (float)stream.ReceiveNext ();
				rotY = (float)stream.ReceiveNext ();
			}

		}

	}


	public IEnumerator<float> _update(){

		while (gameObject != null) {

			transform.position = Vector3.SmoothDamp (transform.position, new Vector3 (x, transform.position.y, z), ref velocity1, speed * Timing.DeltaTime); //Vector3.LerpUnclamped (transform.position, new Vector3 (x, transform.position.y, z), speed * Timing.DeltaTime); 
			transform.rotation = new Quaternion (transform.rotation.x, Mathf.SmoothDamp (transform.rotation.y, rotY, ref velocity2, turnSpeed * Timing.DeltaTime), transform.rotation.z, transform.rotation.w);

			yield return 0f;

		}

	}


}
