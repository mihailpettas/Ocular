using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]

public class FadeInOut : MonoBehaviour {

	public enum FadeType {Nothing, FadeIn, FadeOut};
	public FadeType fade = FadeType.Nothing;

	public bool isGhost;
	public bool loop;

	public float delay;
	public float duration;

	CanvasGroup cg;


	void Start(){
		Invoke ("delayL", delay);
		cg = GetComponent<CanvasGroup> ();
	}

	void delayL(){
		fade = FadeType.FadeIn;
	}

	// Update is called once per frame
	void Update () {

		if (fade == FadeType.FadeIn) {
			cg.alpha = Mathf.MoveTowards (cg.alpha, 1, Time.deltaTime / 2);
		} else if (fade == FadeType.FadeOut) {
			cg.alpha = Mathf.MoveTowards (cg.alpha, 0, Time.deltaTime / 2);
		}

		if (cg.alpha > .5f && !isGhost) {
			cg.interactable = true;
			cg.blocksRaycasts = true;
		}

		if (cg.alpha == 1) {
			fade = FadeType.Nothing;
			Invoke ("fadeOut", duration);
		}
			
		if (fade == FadeType.FadeOut) {
			if(cg.alpha == 0) {
				if (loop) {
					fade = FadeType.FadeIn;
				} else {
					Destroy (gameObject);
				}
			}
		}
			
	}

	void fadeOut(){
		if (duration != 0) {
			cg.interactable = false;
			cg.blocksRaycasts = false;
			fade = FadeType.FadeOut;
		}
	}

}
