using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class MenuDrag : MonoBehaviour {

	float height;
	bool touched;
	Vector2 mousePos;

	// Use this for initialization
	void Start () {
		height = Mathf.Abs(GetComponent<RectTransform> ().anchoredPosition.y);
		Timing.RunCoroutine (_update());
	}
	
	IEnumerator<float> _update () {

		while (gameObject != null) {
	
			if (touched) {

				if (mousePos.y - Input.mousePosition.y != 0 && (transform.localPosition.x.IsBetweenInclusive (-height, height))) {
					transform.Translate (0, -(mousePos.y - Input.mousePosition.y), 0);
				}

				if (transform.localPosition.y < -height) {
					transform.localPosition = new Vector3 (transform.localPosition.x, -height, transform.localPosition.z);
				}
				if (transform.localPosition.y > height) {
					transform.localPosition = new Vector3 (transform.localPosition.x, height, transform.localPosition.z);			
				}

				mousePos = Input.mousePosition;

			}

			yield return 0f;

		}
			
	}

	public void touch(bool value){
		touched = value;
		mousePos = Input.mousePosition;
	}

}
