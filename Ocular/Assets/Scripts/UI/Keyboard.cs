using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour {

	Text input;

	Transform capsBtn;
	PanelOnOff me, upper, lower;


	// Use this for initialization
	void Start () {
		capsBtn = transform.Find ("Caps").transform;
		me = GetComponent<PanelOnOff> ();
		upper = transform.Find ("Upper").GetComponent<PanelOnOff> ();
		lower = transform.Find ("Lower").GetComponent<PanelOnOff> ();
	}

	public void caps(){

		if (capsBtn.GetComponent<Outline> ().enabled) {

			capsBtn.GetComponent<Outline> ().enabled = false;
			upper.show ();
			lower.hide ();

		} else {

			capsBtn.GetComponent<Outline> ().enabled = true;
			upper.hide ();
			lower.show ();

		}

	}

	public void backspace (){
		if (input.text.Length > 0) {
			input.text = input.text.Remove (input.text.Length - 1);
		}
	}

	public void addChar(string c){
		input.text += c;
	}

	public void touchInput (Text inputX){
		input = inputX;
		me.show ();
	}

}
