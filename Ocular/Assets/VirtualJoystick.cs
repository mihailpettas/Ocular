using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using MEC;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	public float speed;
	public Vector3 InputDirection;

	Image jsContainer;
	Image joystick;

	[HideInInspector]
	public Transform cam;
	PointerEventData pedo;
	bool movementGo, cameraGo;

	[HideInInspector]
	public Animator animator;

	int dir;


	void Start(){

		if (name == "CameraJoystick") {			
			jsContainer = GetComponent<Image> ();
			joystick = transform.GetChild (0).GetComponent<Image> (); //this command is used because there is only one child in hierarchy
			InputDirection = Vector3.zero;
		}

		Timing.RunCoroutine (_update ());

	}


	public void OnPointerDown(PointerEventData ped){
		
		if (name == "CameraJoystick") {
			pedo = ped;
			cameraGo = true;
		}

	}
		

	IEnumerator<float> _update(){

		while (gameObject != null) {

			if (cameraGo && Info.se.canMove) {

				Vector2 position = Vector2.zero;

				//To get InputDirection
				RectTransformUtility.ScreenPointToLocalPointInRectangle (jsContainer.rectTransform, pedo.position, pedo.pressEventCamera, out position);

				position.x = (position.x / jsContainer.rectTransform.sizeDelta.x);
				position.y = (position.y / jsContainer.rectTransform.sizeDelta.y);

				float x = (jsContainer.rectTransform.pivot.x == 1f) ? position.x*2 + 1 : position.x*2 - 1;
				float y = (jsContainer.rectTransform.pivot.y == 1f) ? position.y*2 + 1 : position.y*2 - 1;

				InputDirection = new Vector3 (x, y, 0);
				InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

				//to define the area in which joystick can move around
				joystick.rectTransform.anchoredPosition = new Vector3 (InputDirection.x * (jsContainer.rectTransform.sizeDelta.x/3), InputDirection.y * (jsContainer.rectTransform.sizeDelta.y)/3);

				Info.player.transform.Rotate (new Vector3 (0, InputDirection.x, 0) * Timing.DeltaTime * speed);
				cam.Rotate (new Vector3 (-InputDirection.y, 0, 0) * Timing.DeltaTime * speed);

				if (cam.rotation.x > .15f) {
					cam.rotation = new Quaternion (.15f, cam.rotation.y, cam.rotation.z, cam.rotation.w);
				} else if (cam.rotation.x < -.15f) {
					cam.rotation = new Quaternion (-.15f, cam.rotation.y, cam.rotation.z, cam.rotation.w);
				}
					
			}

			if(movementGo && Info.se.canMove){

				if (dir == 0) {
					Info.player.transform.position += Info.player.transform.forward * Timing.DeltaTime * speed;
				} else if (dir == 1) {
					Info.player.transform.position += Info.player.transform.forward*-1 * Timing.DeltaTime * speed;
				} else if (dir == 2) {
					Info.player.transform.position += Info.player.transform.right * Timing.DeltaTime * speed;
				} else if (dir == 3) {
					Info.player.transform.position += Info.player.transform.right*-1 * Timing.DeltaTime * speed;
				}

			}

			yield return 0f;

		}

	}


	public void OnDrag(PointerEventData ped){
		pedo = ped;
	}


	public void OnPointerUp(PointerEventData ped){

		InputDirection = Vector3.zero;
		joystick.rectTransform.anchoredPosition = Vector3.zero;
		cameraGo = false;

	}


	public void up(){

		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Run")) {

			if (!Info.offline) {
				Info.pmv.RPC ("setAnimation", PhotonTargets.All, "Run", PhotonNetwork.player);
			} else {
				animator.Play ("Run");
			}

		}

		dir = 0;
		movementGo = true;

	}


	public void down(){

		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("RunBack")) {

			if (!Info.offline) {
				Info.pmv.RPC ("setAnimation", PhotonTargets.All, "RunBack", PhotonNetwork.player);
			} else {
				animator.Play ("RunBack");
			}

		}

		dir = 1;
		movementGo = true;

	}


	public void right(){

		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("RunRight")) {

			if (!Info.offline) {
				Info.pmv.RPC ("setAnimation", PhotonTargets.All, "RunRight", PhotonNetwork.player);
			} else {
				animator.Play ("RunRight");
			}

		}

		dir = 2;
		movementGo = true;

	}


	public void left(){

		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("RunLeft")) {

			if (!Info.offline) {
				Info.pmv.RPC ("setAnimation", PhotonTargets.All, "RunLeft", PhotonNetwork.player);
			} else {
				animator.Play ("RunLeft");
			}

		}

		dir = 3;
		movementGo = true;

	}


	public void pointerUp(){

		movementGo = false;

		if (!Info.offline) {
			Info.pmv.RPC ("setAnimation", PhotonTargets.All, "Idle", PhotonNetwork.player);
		} else {
			animator.Play ("Idle");
		}

	}

}