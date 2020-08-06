using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screenshot : MonoBehaviour {

	int i = 0;

	public void lol(){
		GetComponent<Image> ().color = new Color (1, 1, 1, 0);
		ScreenCapture.CaptureScreenshot ("shot" + i.ToString() + ".png");
		i++;
		Invoke ("la", 1);
	}

	void la(){
		GetComponent<Image> ().color = new Color (1, 1, 1, .5f);
	}

}
