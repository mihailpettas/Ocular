using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Vuforia;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

	PanelOnOff pof;

	Light lighting;

	AudioSource maus, sfxaus;
	Camera cam;

	bool flashStatus;
	UnityEngine.UI.Image flashToogle;

	Text orientationText;

	Text recAmount;
	GestureRecognizer.DrawDetector detector;

	public Mesh highMesh, lowMesh;

	public Transform[] draggers;

	bool touched;
	Vector2 mousePos;

	int dragger;

	public Vector2[] portraitPositions, landscapePositions;



	public void setSettings(){

		pof = GetComponent<PanelOnOff> ();
		lighting = GameObject.Find("Arena").transform.Find("DirectionalLight").GetComponent<Light>();
		maus = GameObject.Find ("MusicAUS").GetComponent<AudioSource> ();
		sfxaus = GameObject.Find ("SFXAUS").GetComponent<AudioSource> ();
		flashToogle = transform.Find ("Flash").Find ("FlashToggle").Find("Image").GetComponent<UnityEngine.UI.Image> ();

		if (SceneManager.GetActiveScene ().name == "AR") {
			orientationText = transform.Find ("Orientation").Find ("OrientationText").GetComponent<Text> ();
		}

		recAmount = transform.Find ("Recognition").Find ("Amount").GetComponent<Text> ();
		detector = GameObject.Find ("SkillsRecognizer").GetComponent<GestureRecognizer.DrawDetector> ();

		if (SceneManager.GetActiveScene ().name == "AR") {
			cam = GameObject.Find ("ARCamera").GetComponent<Camera> ();
		} else {
			cam = Info.player.transform.Find ("Camera").GetComponent<Camera> ();
		}


		//Get & Set Saved Settings

		PlayerPrefs.DeleteAll ();

		if (PlayerPrefs.HasKey ("ArenaSize")) {

			if (SceneManager.GetActiveScene ().name == "AR") {
				Info.scaler.transform.localScale = Vector3.one * PlayerPrefs.GetFloat ("ArenaSize");
				Info.scaler.transform.localPosition = new Vector3 (0, PlayerPrefs.GetFloat ("ArenaHeight"), 0);
			}

			lighting.intensity = PlayerPrefs.GetFloat ("Lights");
			maus.volume = PlayerPrefs.GetFloat ("Music");
			sfxaus.volume = PlayerPrefs.GetFloat ("SFX");
			detector.scoreToAccept = PlayerPrefs.GetFloat ("Recognition");

		}

		setQuality ();


		//Set Draggers Positions

		if (SceneManager.GetActiveScene ().name == "AR") {

			draggers [0].localPosition = new Vector3 (Info.remap (Info.scaler.transform.localScale.x, .05f, .1f, -980, 980), 0, 0);
			draggers [1].localPosition = new Vector3 (Info.remap (Info.scaler.transform.localPosition.y, -.1f, .1f, -980, 980), 0, 0);
			draggers [2].localPosition = new Vector3 (Info.remap (lighting.intensity, 0, 2, -980, 980), 0, 0);
			draggers [3].localPosition = new Vector3 (Info.remap (maus.volume, 0, 1, -980, 980), 0, 0);
			draggers [4].localPosition = new Vector3 (Info.remap (sfxaus.volume, 0, 1, -980, 980), 0, 0);

		} else {

			draggers [0].localPosition = new Vector3 (Info.remap (lighting.intensity, 0, 2, -980, 980), 0, 0);
			draggers [1].localPosition = new Vector3 (Info.remap (maus.volume, 0, 1, -980, 980), 0, 0);
			draggers [2].localPosition = new Vector3 (Info.remap (sfxaus.volume, 0, 1, -980, 980), 0, 0);

		}

		recAmount.text = (detector.scoreToAccept * 100).ToString ();


		//Run update
		Timing.RunCoroutine (_update());

	}


	IEnumerator<float> _update () {

		while (gameObject != null) {

			if (pof.showUI) {

				//Drag dragger

				if (touched) {

					if (mousePos.x - Input.mousePosition.x != 0 && (draggers[dragger].localPosition.x.IsBetweenInclusive (-980, 980))) {
						draggers[dragger].Translate (-(mousePos.x - Input.mousePosition.x), 0, 0);
					}

					if (draggers[dragger].localPosition.x < -980) {
						draggers[dragger].localPosition = new Vector3 (-980, 0, 0);
					}
					if (draggers[dragger].localPosition.x > 980) {
						draggers[dragger].localPosition = new Vector3 (980, 0, 0);			
					}

					mousePos = Input.mousePosition;

				}


				//Set setting by dragger position

				if (SceneManager.GetActiveScene ().name == "AR") {					

					Info.scaler.transform.localScale = Vector3.one * Info.remap (draggers [0].localPosition.x, -980, 980, .05f, .1f);

					if (Info.player) {
						Info.myCtrl.speed = Info.rot (.05f, 4, Info.scaler.transform.localScale.x);
					}

					Info.scaler.transform.localPosition = new Vector3 (0, Info.remap (draggers [1].localPosition.x, -980, 980, -.1f, .1f), 0);

					lighting.intensity = Info.remap (draggers[2].localPosition.x, -980, 980, 0, 2);

					maus.volume = Info.remap (draggers[3].localPosition.x, -980, 980, 0, 1);

					sfxaus.volume = Info.remap (draggers[4].localPosition.x, -980, 980, 0, 1);

				} else {

					lighting.intensity = Info.remap (draggers[0].localPosition.x, -980, 980, 0, 2);

					maus.volume = Info.remap (draggers[1].localPosition.x, -980, 980, 0, 1);

					sfxaus.volume = Info.remap (draggers[2].localPosition.x, -980, 980, 0, 1);

				}

			}

			yield return 0f;

		}

	}


	public void touch(int draggerIndex){
		touched = true;
		mousePos = Input.mousePosition;
		dragger = draggerIndex;
	}


	public void untouch(){
		touched = false;
	}


	public void flash(){	//Den apothikevetai

		flashStatus = !flashStatus;
		flashToogle.enabled = flashStatus;

		CameraDevice.Instance.SetFlashTorchMode (flashStatus);

	}


	public void orientation(){	//Den apothikevetai

		if(orientationText.text == "Vertical"){

			orientationText.text = "Horizontal";
			Screen.orientation = ScreenOrientation.LandscapeLeft;

			GameObject.Find ("Target").GetComponent<RectTransform> ().anchoredPosition = landscapePositions [0];
			GameObject.Find ("SkillLocked").GetComponent<RectTransform> ().anchoredPosition = landscapePositions [1];
			GameObject.Find ("BattleGlobesUI").GetComponent<RectTransform> ().anchoredPosition = landscapePositions [2];
			GameObject.Find ("PointsGlobeUI").GetComponent<RectTransform> ().anchoredPosition = landscapePositions [3];

			GameObject.Find ("SkillsRecognizer").GetComponent<RectTransform> ().anchoredPosition = landscapePositions [4];
			GameObject.Find ("SkillsRecognizer").GetComponent<RectTransform> ().sizeDelta = new Vector2 (1440, 1440);

		} else {

			orientationText.text = "Vertical";
			Screen.orientation = ScreenOrientation.Portrait;

			GameObject.Find ("Target").GetComponent<RectTransform> ().anchoredPosition = portraitPositions [0];
			GameObject.Find ("SkillLocked").GetComponent<RectTransform> ().anchoredPosition = portraitPositions [1];
			GameObject.Find ("BattleGlobesUI").GetComponent<RectTransform> ().anchoredPosition = portraitPositions [2];
			GameObject.Find ("PointsGlobeUI").GetComponent<RectTransform> ().anchoredPosition = portraitPositions [3];

			GameObject.Find ("SkillsRecognizer").GetComponent<RectTransform> ().anchoredPosition = portraitPositions [4];
			GameObject.Find ("SkillsRecognizer").GetComponent<RectTransform> ().sizeDelta = new Vector2 (2560, 2560);

		}

	}


	public void recognition(bool increase){

		if(increase && detector.scoreToAccept < 1){
			detector.scoreToAccept += .5f;
		}else if(!increase && detector.scoreToAccept > 5){
			detector.scoreToAccept += .5f;
		}

		recAmount.text = (detector.scoreToAccept * 100).ToString ();

	}


	public void setQuality(){

		if (QualitySettings.GetQualityLevel() == 1) {	//Low

			if (SceneManager.GetActiveScene ().name == "AR") {
				VuforiaConfiguration.Instance.Vuforia.CameraDeviceMode = CameraDevice.CameraDeviceMode.MODE_OPTIMIZE_SPEED;
			}

			cam.GetComponent<FxPro> ().enabled = false;
			GameObject.Find ("Ground").GetComponent<MeshFilter> ().mesh = lowMesh;
			GameObject.Find ("VialsBGCamera").transform.GetChild(0).gameObject.SetActive (false);
			GameObject.Find ("NexusBGCamera").transform.GetChild(0).gameObject.SetActive (false);
			GameObject.Find ("Intro").transform.Find ("BGModels").gameObject.SetActive (false);
			GameObject.Find ("Wands").transform.Find ("BG").Find ("BGModels").gameObject.SetActive (false);
			GameObject.Find ("Vials").transform.Find ("BG").Find ("BGModels").gameObject.SetActive (false);

		} else {	//High

			if (SceneManager.GetActiveScene ().name == "AR") {
				VuforiaConfiguration.Instance.Vuforia.CameraDeviceMode = CameraDevice.CameraDeviceMode.MODE_OPTIMIZE_QUALITY;
			}

			cam.GetComponent<FxPro> ().enabled = false;
			GameObject.Find ("Ground").GetComponent<MeshFilter> ().mesh = highMesh;
			GameObject.Find ("VialsBGCamera").transform.GetChild(0).gameObject.SetActive (true);
			GameObject.Find ("NexusBGCamera").transform.GetChild(0).gameObject.SetActive (true);
			GameObject.Find ("Intro").transform.Find ("BGModels").gameObject.SetActive (true);
			GameObject.Find ("Wands").transform.Find ("BG").Find ("BGModels").gameObject.SetActive (true);
			GameObject.Find ("Vials").transform.Find ("BG").Find ("BGModels").gameObject.SetActive (true);

		}

	}


	public void setQualityScreen(){

		Timing.KillCoroutines ();

		PhotonNetwork.Disconnect ();

		Info.error = false;
		SceneManager.LoadScene("Intro");

	}


	public void saveSettings(){

		PlayerPrefs.SetFloat ("ArenaSize", Info.scaler.transform.localScale.x);
		PlayerPrefs.SetFloat ("ArenaHeight", Info.scaler.transform.localPosition.y);
		PlayerPrefs.SetFloat ("Lights", lighting.intensity);
		PlayerPrefs.SetFloat ("Music", maus.volume);
		PlayerPrefs.SetFloat ("SFX", sfxaus.volume);
		PlayerPrefs.SetFloat ("Recognition", detector.scoreToAccept);

	}


	public void exit(){
		Info.loadMenus (false);
	}


	public void quit(){
		Application.Quit ();
	}

}
