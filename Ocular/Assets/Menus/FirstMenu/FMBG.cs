using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class FMBG : MonoBehaviour {

	PanelOnOff pof;
	RectTransform rect;
	public float speed = 15;
	public bool right;


	// Use this for initialization
	void Start () {
		pof = transform.parent.GetComponent<PanelOnOff> ();
		rect = GetComponent<RectTransform> ();
	}

		
	IEnumerator<float> _update(){

		while (pof.showUI) {

			if (right) {
				
				rect.anchoredPosition3D = new Vector3 (Mathf.MoveTowards (rect.anchoredPosition3D.x, 1400, Time.deltaTime*speed), 0, 0);

				if (rect.anchoredPosition3D.x == 1400) {
					right = false;
				}

			} else {

				rect.anchoredPosition3D = new Vector3 (Mathf.MoveTowards (rect.anchoredPosition3D.x, -1400, Time.deltaTime*speed), 0, 0);

				if (rect.anchoredPosition3D.x == -1400) {
					right = true;
				}

			}

			yield return 0f;

		}
			
	}

}
