using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Vuforia;
using MEC;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;


public class Info : MonoBehaviour {

	static public Transform scaler;

	static public string scene;

	static public GameObject player, opponent;
	static public Character myChar;
	static public Controller myCtrl;

	static public PhotonManager pm;
	static public PhotonView pmv;

	static public bool UION;

	//-------------------------------------

	static public string playerUsername;
	static public string playerPassword;
	static public string playerEmail;
	static public int playerCoins = 0;
	static public int playerPoints = 100;
	static public int tutorial;
	static public List<int> playerBadges = new List<int> ();

	static public int lifeVial = 50;
	static public int energyVial = 50;
	static public int sLifeVial, sEnergyVial;

	static public List<int> unlockedWands = new List<int> ();
	static public string equippedWand;
	static public float wandDamage;

	static public List<int> unlockedSkills = new List<int> ();

	//-------------------------------------

	static public bool error;
	static public float opponentLife;

	//-------------------------------------

	[HideInInspector]
	static public SkillExecutor se;

	public CanvasGroup main, vials, wands, skills, settings;

	static public bool offline;

	/*int percentage = 25;
	float startV;

	void Update(){

		if (Input.GetMouseButtonUp (0)) {

			startV = 100;
			File.WriteAllText ("C:/Users/mihal/Desktop/test.txt", "");

			for (int i = 1; i <= 19; i++) {	

				//Write some text to the test.txt file
				StreamWriter writer = new StreamWriter ("C:/Users/mihal/Desktop/test.txt", true);
				print (startV + rot(100, percentage, startV));
				writer.WriteLine ((startV + rot(100, percentage, startV)).ToString());
				startV = startV + rot(100, percentage, startV);
				writer.Close ();

			}

		}

	}*/


	void Start(){

		pm = GetComponent<PhotonManager> ();
		pmv = GetComponent<PhotonView> ();


		//Set original landscape
		if (SceneManager.GetActiveScene ().name == "AR") {
			Screen.orientation = ScreenOrientation.Portrait;
		} else {
			Screen.orientation = ScreenOrientation.LandscapeLeft;
		}


		//If scene was reloaded with error show message
		if (error) {
			GameObject.Find ("Message").GetComponent<Message> ().message ("Connection Error");
			error = false;
		}		


		//Set misc variables

		Application.targetFrameRate = 60;
		Application.runInBackground = true;
		VuforiaARController.Instance.RegisterVuforiaStartedCallback (setVuforia);
		se = GameObject.Find ("DuringBattle").transform.Find ("SkillsRecognizer").GetComponent<SkillExecutor> ();


		//Run coroutine
		Timing.RunCoroutine (_update ());
			
	}
		

	public void startOffline(){

		offline = true;

		if (SceneManager.GetActiveScene ().name == "AR") {
			GameObject.Find ("Generator").GetComponent<PanelOnOff> ().show ();
		} else {			
			player = Instantiate (Info.pm.prefabs [1], new Vector3 (0, 0, -9), Quaternion.Euler (0, 0, 0));
			Destroy (player.GetComponent<PhotonLerp> ());
			Destroy (player.GetComponent<PhotonView> ());
		}

	}


	public void reBattle(){	//After battle
		Info.pmv.RPC("reBattle", PhotonTargets.All);
	}


	public void startBattle(){	//First battle
			
		GameObject.Find ("Ready").GetComponent<PanelOnOff> ().hide ();

		if (opponentLife == 0) {
			pmv.RPC ("getLife", PhotonTargets.Others, myChar.sLife, false);
		} else {
			pmv.RPC ("getLife", PhotonTargets.All, myChar.sLife, true);
		}

	}


	static public void loadMenus(bool value){

		Timing.KillCoroutines ();

		PhotonNetwork.Disconnect ();

		Info.error = value;
		SceneManager.LoadScene(Info.scene);

	}
		

	public IEnumerator<float> _update(){

		while (gameObject != null) {
			
			if (Info.player) {

				if (main.alpha == 0 && vials.alpha == 0 && wands.alpha == 0 && skills.alpha == 0 && settings.alpha == 0) {	//Δεν ειναι networked δε χρειαζεται authority check
					UION = false;
				} else {
					UION = true;
				}

			}

			yield return 0f;

		}
			
	}


	void setVuforia(){

		CameraDevice.Instance.SetFocusMode (CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

		// Query Vuforia for a target frame rate and set it in Unity:
		// by default, we use Application.targetFrameRate to set the recommended frame rate.
		// Cardboard does not use vsync, and OVR explicitly disables it. If you use vSync in your quality settings, you should 
		// also set the QualitySettings.vSyncCount according to the value returned above.
		// e.g. if targetFPS > 50 --> vSyncCount = 1; else vSyncCount = 2;
		//Application.targetFrameRate = VuforiaRenderer.Instance.GetRecommendedFps(VuforiaRenderer.FpsHint.FAST);

	}


	static public float remap(float v, float inA, float inB, float outA, float outB) {
		return (((v - inA) * (outB - outA)) / (inB - inA)) + outA;
	}


	static public Texture2D CalculateTexture(int h, int w,float r,float cx,float cy,Texture2D sourceTex){

		Texture2D b=new Texture2D(h,w);

		for(int i = (int)(cx-r) ; i < cx + r ; i ++){

			for(int j = (int)(cy-r) ; j < cy+r ; j ++)	{

				float dx = i - cx;
				float dy = j - cy;
				float d = Mathf.Sqrt(dx*dx + dy*dy);

				if (d <= r) {
					b.SetPixel (i - (int)(cx - r), j - (int)(cy - r), sourceTex.GetPixel (i, j));
				} else {
					b.SetPixel (i - (int)(cx - r), j - (int)(cy - r), Color.clear);
				}

			}

		}

		b.Apply ();
		return b;

	}


	static public float rot(float input1, float output1, float input2){
		return (input2 * output1) / input1;
	}

}