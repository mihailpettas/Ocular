using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {


	void Start() {
		SceneManager.sceneLoaded += OnSceneLoaded;
		SceneManager.LoadSceneAsync ("PvP", LoadSceneMode.Additive);
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

		SceneManager.sceneLoaded -= OnSceneLoaded;
		SceneManager.UnloadSceneAsync ("Loading");

	}

}
