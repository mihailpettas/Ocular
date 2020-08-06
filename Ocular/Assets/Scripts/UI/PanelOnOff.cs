using UnityEngine;
using System.Collections.Generic;
using MEC;

[RequireComponent(typeof(CanvasGroup))]

public class PanelOnOff : MonoBehaviour {

	public float speed = 3;
	public bool showUI = true;
	public bool isGhost;

	[HideInInspector]
	public CanvasGroup cg;
	bool destroyThis;

			
	void Start (){
		
		cg = GetComponent<CanvasGroup> ();

		if (showUI && cg.alpha < 1f) {
			Timing.RunCoroutine(_fadeIn());
		}else if(!showUI && cg.alpha > 0f){
			Timing.RunCoroutine(_fadeOut());
		}

	}


	IEnumerator<float> _fadeIn(){

		while(cg.alpha < 1f){
			
			cg.alpha = Mathf.MoveTowards(cg.alpha, 1f, Timing.DeltaTime * speed);

			if (cg.alpha > .5f && !isGhost) {
				cg.interactable = true;
				cg.blocksRaycasts = true;
			}

			yield return 0f;

		}
			
	}

	IEnumerator<float> _fadeOut(){

		while(cg.alpha > 0f){

			cg.alpha = Mathf.MoveTowards(cg.alpha, 0f, Timing.DeltaTime * speed);

			if (cg.alpha < .5f) {
				cg.interactable = false;
				cg.blocksRaycasts = false;
			}
			if (cg.alpha == 0f && destroyThis) {
				Destroy (gameObject);
			}

			yield return 0f;

		}
			
	}

	public void show(){

		Timing.RunCoroutine(_fadeIn());
		showUI = true;

		/*if (GetComponent<WindowAnimation> ()) {

			transform.Find ("Elements").GetComponent<CanvasGroup> ().alpha = 0;
			transform.Find ("Elements").GetComponent<CanvasGroup> ().interactable = false;
			transform.Find ("Elements").GetComponent<CanvasGroup> ().blocksRaycasts = false;

			GetComponent<WindowAnimation> ().show ();

			if (GetComponent<WindowsDecorations> ()) {
				GetComponent<WindowsDecorations> ().show ();
			}

		}*/

	}

	public void hide(bool destroy = false){
		Timing.RunCoroutine(_fadeOut());
		destroyThis = destroy;
		showUI = false;
	}

}
