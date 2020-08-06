using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policies : MonoBehaviour {

	public void openPolicies(){
		Application.OpenURL ("http://ocular.000webhostapp.com/TOS.html");
		Application.OpenURL ("http://ocular.000webhostapp.com/PP.html");
	}

}
