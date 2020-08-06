using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class Intro : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Screen.orientation = ScreenOrientation.Portrait;

		PlayerPrefs.DeleteAll();

		if (PlayerPrefs.HasKey ("Quality")) {
			setQualityX(PlayerPrefs.GetInt("Quality"));
		}

	}


	public void setQualityX(int q){
		QualitySettings.SetQualityLevel(q);
		GameObject.Find ("QualityMenu").GetComponent<PanelOnOff> ().hide ();
		GameObject.Find ("TechnologyMenu").GetComponent<PanelOnOff> ().show ();
	}


	public void setTechnology(string ar){
		Info.scene = ar;
		SceneManager.LoadScene (Info.scene);
	}

}
