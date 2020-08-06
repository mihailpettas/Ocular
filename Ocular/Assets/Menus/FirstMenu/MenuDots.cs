using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuDots : MonoBehaviour {

	public float min, max;
	public int population;
	public Color color;

	public GameObject prefab;
	GameObject skata;


	// Use this for initialization
	void Start () {

		for (int i = 0; i < Random.Range (population / 2, population); i++) {
			
			skata = Instantiate (prefab, Vector3.zero, Quaternion.identity, transform.Find ("BG").transform) as GameObject;
			skata.GetComponent<RectTransform> ().sizeDelta = Vector2.one*Random.Range(min, max);
			skata.GetComponent<Image> ().color = color;

			if (SceneManager.GetActiveScene ().name == "AR" || SceneManager.GetActiveScene ().name == "Intro") {
				skata.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (Random.Range (-720, 720), Random.Range (-1280, 1280));
			} else {
				skata.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (Random.Range (-1280, 1280), Random.Range (0, 1440));
			}

		}

	}

}
