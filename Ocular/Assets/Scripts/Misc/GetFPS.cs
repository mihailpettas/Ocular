using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class GetFPS : MonoBehaviour {

	public bool show;

	[HideInInspector]
	public float fps, msec;

	float deltaTime = 0.0f;


	void Start(){
		Timing.RunCoroutine (_update ().CancelWith(gameObject));
	}


	public IEnumerator<float> _update(){
		while (gameObject != null) {
			deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
			msec = deltaTime * 1000.0f;
			fps = 1.0f / deltaTime;
			yield return 0f;
		}
	}


	void OnGUI(){

		if (show) {

			int w = Screen.width, h = Screen.height;

			GUIStyle style = new GUIStyle ();

			Rect rect = new Rect (0, 0, w, h * 2 / 100);
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = h * 2 / 100;
			style.normal.textColor = Color.white;

			string text = string.Format ("{0:0.0} ms ({1:0.} fps)", msec, fps);

			GUI.Label (rect, text, style);

		}

	}

}