using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MEC;

public class Controller : MonoBehaviour {

	public float speed = 4;

	[HideInInspector]
	public Animator animator;
	[HideInInspector]
	public Camera cam;

	[HideInInspector]
	public Vector3 target;

	[HideInInspector]
	public PanelOnOff targetDot;

	Vector3 lookAt;
	Vector3 goPos;

	RaycastHit hit;
	Ray moveRay, lookAtRay;

	public bool moving;



	public IEnumerator<float> _raycasts(){

		while (gameObject != null) {

			if ((!Info.offline && GetComponent<PhotonView>().isMine) || Info.offline) {

				if ((goPos - transform.position).normalized != Vector3.zero && Vector3.Distance(goPos, transform.position) >= .5f) {
					transform.rotation = Quaternion.LookRotation ((goPos - transform.position).normalized);
				}


				if (Input.GetMouseButtonUp (0) && Info.se.canMove /*&& !Info.se.ribbonRuns */&& !Info.UION && !animator.GetCurrentAnimatorStateInfo (0).IsName ("GetHitFront")) {

					moveRay = cam.ScreenPointToRay (Input.mousePosition);

					if (Physics.Raycast (moveRay, out hit) && Vector3.Distance(hit.point, Vector3.zero) <= 13.5f && hit.collider.gameObject.name == "Ground") {

						goPos = hit.point;

						if (!moving) {

							if (!Info.offline) {

								if (Info.myChar.life > Info.rot (100, 10, Info.myChar.life)) {
									Info.pmv.RPC ("setAnimation", PhotonTargets.All, "Run", PhotonNetwork.player);
								} else {
									Info.pmv.RPC ("setAnimation", PhotonTargets.All, "RunInjured", PhotonNetwork.player);
								}

							} else {
								
								if (Info.myChar.life > Info.rot (100, 10, Info.myChar.life)) {
									animator.Play ("Run");
								} else {
									animator.Play ("RunInjured");
								}

							}
								
							moving = true;
							Timing.KillCoroutines ("move");
							Timing.RunCoroutine (_movement (), "move");

						}

					}

				}


				if (Screen.orientation == ScreenOrientation.Portrait) {
					lookAtRay = cam.ScreenPointToRay (new Vector3 (Screen.width / 2, (Screen.height / 2) + Info.rot (2560, 200, Screen.height), 0));
				} else {
					lookAtRay = cam.ScreenPointToRay (new Vector3 (Screen.width / 2, (Screen.height / 2), 0));
				}

				if (Physics.Raycast (lookAtRay, out hit) && hit.collider.gameObject.name == "Ground") {

					if (!moving) {
						
						target = new Vector3 (hit.point.x, 1.5f, hit.point.z);
						goPos = hit.point;	//Looking

						if (!Info.offline) {
							
							if (Vector3.Distance (hit.point, Info.opponent.transform.position) <= .5f) {
								targetDot.show ();
							} else {
								targetDot.hide ();
							}

						}

					} else {
						targetDot.hide ();
					}

				}
					
			}

			yield return 0f;

		}
			
	}


	IEnumerator<float> _movement(){

		while (gameObject != null) {

			if (Vector3.Distance (transform.position, goPos) >= .1f) {				
				transform.position += transform.forward * Timing.DeltaTime * speed;	//Vector3.MoveTowards(transform.position, goPos, Timing.DeltaTime * speed);
			} else {

				moving = false;

				//if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {

				if (!Info.offline) {
					Info.pmv.RPC ("setAnimation", PhotonTargets.All, "Idle", PhotonNetwork.player);
				} else {
					animator.Play ("Idle");
				}

				//}

				yield break;

			}

			yield return 0f;

		}
			
	}


	public void stopMovement(){

		moving = false;
		Timing.KillCoroutines ("move");

		if (!Info.offline) {
			Info.pmv.RPC ("setAnimation", PhotonTargets.All, "Idle", PhotonNetwork.player);
		} else {
			animator.Play ("Idle");
		}

	}

}
