using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour {
	
	public void left(){

		for (int i = 2; i < transform.parent.childCount; i++) {

			if (transform.parent.GetChild (i).GetComponent<PanelOnOff> ().showUI && i > 2) {
				transform.parent.GetChild (i).GetComponent<PanelOnOff> ().hide();
				transform.parent.GetChild (i-1).GetComponent<PanelOnOff> ().show ();
				return;
			}

		}

	}

	public void right(){

		for (int i = 2; i < transform.parent.childCount; i++) {

			if (transform.parent.GetChild (i).GetComponent<PanelOnOff> ().showUI && i < transform.parent.childCount-1) {
				transform.parent.GetChild (i).GetComponent<PanelOnOff> ().hide();
				transform.parent.GetChild (i+1).GetComponent<PanelOnOff> ().show ();
				return;
			}

		}

	}

}
