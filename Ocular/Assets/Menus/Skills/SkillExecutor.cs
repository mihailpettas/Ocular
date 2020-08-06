using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.SceneManagement;

public class SkillExecutor : MonoBehaviour {

	public bool canMove = true;
	[HideInInspector]
	public Transform activeSkills, activeSummons;

	[HideInInspector]
	public Animator anim;

	Transform skillsIntro, wands;
	[HideInInspector]
	public Transform source;
	GameObject noManaArrow;
	GameObject lockedSkill;

	[HideInInspector]
	public Camera cam;

	RaycastHit hit;
	Ray ray;

	[HideInInspector]
	public Vector3 target;

	[HideInInspector]
	public Gradient g = new Gradient();
	public float trailFadeSpeed;

	[HideInInspector]
	public GradientColorKey[] gck = new GradientColorKey[2];
	[HideInInspector]
	public GradientAlphaKey[] gak = new GradientAlphaKey[2];

	[HideInInspector]
	public Transform trail;
	[HideInInspector]
	public TrailRenderer trailRenderer;


	[HideInInspector]
	public SkillInfo[] skills = new SkillInfo[11];
	[HideInInspector]
	public int skillRunning, skillRunningLevel;


	public float pointsCollected;
	public int combo = 1;

	GameObject skillSpawnVar;



	void Start () {

		for (int i = 0; i < skills.Length; i++) {
			skills [i] = new SkillInfo ();
		}

		skills [0].skillName = "Axcendia";
		skills [0].skillType = SkillInfo.SkillType.Once;
		skills [0].prefabIndex = new int[] {2, 3};
		skills [0].energyNeeded = new int[] {5, 10};
		//skills [0].skillPoints = new int[] {5, 10};
		skills [0].damage = new float[] {5, 10};
		skills [0].trailColor = new Color[] {Color.cyan, Color.green};
		skills [0].gestureID = new string[] {"Axcendia1", "Axcendia2"};

		skills [1].skillName = "Disarma";
		skills [1].skillType = SkillInfo.SkillType.Once;
		skills [1].prefabIndex = new int[] {0};
		skills [1].energyNeeded = new int[] {0};
		//skills [1].skillPoints = new int[] {0};
		skills [1].damage = new float[] {0};
		skills [1].trailColor = new Color[] {Color.red + Color.yellow};
		skills [1].gestureID = new string[] {"Disarma"};

		skills [2].skillName = "Confusa";
		skills [2].skillType = SkillInfo.SkillType.Once;
		skills [2].prefabIndex = new int[] {0};
		skills [2].energyNeeded = new int[] {0};
		//skills [2].skillPoints = new int[] {0};
		skills [2].damage = new float[] {0};
		skills [2].trailColor = new Color[] {Color.black};
		skills [2].gestureID = new string[] {"Confusa"};

		skills [3].skillName = "FlammaBallo";
		skills [3].skillType = SkillInfo.SkillType.Once;
		skills [3].prefabIndex = new int[] {4};
		skills [3].energyNeeded = new int[] {10, 25, 70};
		//skills [3].skillPoints = new int[] {10, 25, 70};
		skills [3].damage = new float[] {10, 25, 70};
		skills [3].trailColor = new Color[] {Color.yellow};
		skills [3].gestureID = new string[] {"FlammaBallo1"};

		skills [4].skillName = "Fragor";
		skills [4].skillType = SkillInfo.SkillType.Once;
		skills [4].prefabIndex = new int[] {5, 6, 7};
		skills [4].energyNeeded = new int[] {15, 30, 50};
		//skills [4].skillPoints = new int[] {15, 30, 50};
		skills [4].damage = new float[] {15, 30, 50};
		skills [4].trailColor = new Color[] {Color.yellow, Color.yellow+Color.red, Color.red};
		skills [4].gestureID = new string[] {"Fragor1", "Fragor2", "Fragor3"};

		skills [5].skillName = "HaltJinx";
		skills [5].skillType = SkillInfo.SkillType.Once;
		skills [5].prefabIndex = new int[] {0};
		skills [5].energyNeeded = new int[] {0};
		//skills [5].skillPoints = new int[] {0};
		skills [5].damage = new float[] {0};
		skills [5].trailColor = new Color[] {Color.black};
		skills [5].gestureID = new string[] {"HaltJinx"};

		skills [6].skillName = "Nebula";
		skills [6].skillType = SkillInfo.SkillType.Continuous;
		skills [6].prefabIndex = new int[] {0};
		skills [6].energyNeeded = new int[] {0};
		//skills [6].skillPoints = new int[] {0};
		skills [6].damage = new float[] {0};
		skills [6].trailColor = new Color[] {Color.black};
		skills [6].gestureID = new string[] {"Nebula"};

		skills [7].skillName = "Prostatum";
		skills [7].skillType = SkillInfo.SkillType.Once;
		skills [7].prefabIndex = new int[] {8, 9};
		skills [7].energyNeeded = new int[] {5, 15};
		//skills [7].skillPoints = new int[] {5, 15};
		skills [7].damage = new float[] {0};
		skills [7].trailColor = new Color[] {Color.cyan+Color.green, Color.cyan+Color.green};
		skills [7].gestureID = new string[] {"Prostatum1", "Prostatum2"};

		skills [8].skillName = "Refrigero";
		skills [8].skillType = SkillInfo.SkillType.Continuous;
		skills [8].prefabIndex = new int[] {0};
		skills [8].energyNeeded = new int[] {0};
		//skills [8].skillPoints = new int[] {0};
		skills [8].damage = new float[] {0};
		skills [8].trailColor = new Color[] {Color.black};
		skills [8].gestureID = new string[] {"Refrigero"};

		skills [9].skillName = "ResistoMomentum";
		skills [9].skillType = SkillInfo.SkillType.Once;
		skills [9].prefabIndex = new int[] {0};
		skills [9].energyNeeded = new int[] {0};
		//skills [9].skillPoints = new int[] {0};
		skills [9].damage = new float[] {0};
		skills [9].trailColor = new Color[] {Color.black};
		skills [9].gestureID = new string[] {"ResistoMomentum"};

		skills [10].skillName = "Resurgo";
		skills [10].skillType = SkillInfo.SkillType.Once;
		skills [10].prefabIndex = new int[] {10};
		skills [10].energyNeeded = new int[] {20};
		//skills [10].skillPoints = new int[] {0};
		skills [10].damage = new float[] {0};
		skills [10].trailColor = new Color[] {Color.magenta};
		skills [10].gestureID = new string[] {"Resurgo"};


		skillsIntro = GameObject.Find ("Skills").transform.Find ("Intro");
		wands = GameObject.Find ("WandsList").transform;
		noManaArrow = GameObject.Find("BattleGlobes").transform.GetChild(0).gameObject;
		lockedSkill = GameObject.Find ("DuringBattle").transform.Find("SkillLocked").gameObject;

		if (SceneManager.GetActiveScene ().name == "AR") {
			
			trail = GameObject.Find ("GestureFXCamera").transform.Find ("Trail");
			trailRenderer = GameObject.Find ("Trail").GetComponent<TrailRenderer> ();		

			gak [1].alpha = 0.0F;
			gak [1].time = 1.0F;

		}

	}


	public SkillInfo getSkill(string skillName){

		for (int i = 0; i < skills.Length; i++) {

			if (skills [i].skillName == skillName) {
				return skills [i];
			}

		}

		return null;

	}


	public void addPoints(int points){
		pointsCollected += points * combo;
	}


	public void recognize(GestureRecognizer.RecognitionResult result){

		if (result != GestureRecognizer.RecognitionResult.Empty && !anim.GetCurrentAnimatorStateInfo (0).IsName ("GetHitFront")) {
			
			for (int i = 0; i < skills.Length; i++) {

				for (int x = 0; x < skills [i].gestureID.Length; x++) {

					if (result.gesture.id == skills [i].gestureID [x]) {

						if (anim.speed == 0) {
							print ("end continuous");
							returnPos ();
							return;
						}

						if (skills [i].locked) {	//Check Locked
							print("Locked");
							lockedSkill.GetComponent<CanvasGroup> ().alpha = .5f;
							lockedSkill.GetComponent<PanelOnOff> ().hide ();
							canMove = true;
							return;
						}

						if (Info.myChar.energy < skills [i].energyNeeded [x]) {	//Check Mana
							print("No Mana");
							Timing.KillCoroutines ("noMana");
							Timing.RunCoroutine (_noMana (), "noMana");
							canMove = true;
							return;
						}

						if (!Info.offline) {
							Info.pmv.RPC ("setAnimation", PhotonTargets.All, "Attack", PhotonNetwork.player);
						} else {
							anim.Play ("Attack");
						}

						skillRunning = i;
						skillRunningLevel = x;

						Timing.RunCoroutine (_execute (), "execute");

						if (SceneManager.GetActiveScene ().name == "AR") {
							
							gck [0].color = skills [i].trailColor [x];
							gck [0].time = 0.0F;

							gck [1].color = skills [i].trailColor [x];
							gck [1].time = 1.0F;

						}

						return;

					}

				}

			}

		} else {	//Not Recognized

			if (SceneManager.GetActiveScene ().name == "AR") {	//AR Gesture Trail
				
				gck [0].color = Color.white;
				gck [0].time = 0.0F;

				gck [1].color = Color.white;
				gck [1].time = 1.0F;

			}

			canMove = true;
			print ("Not Recognized");

		}

		if (SceneManager.GetActiveScene ().name == "AR") {	//AR Gesture Trail		
			g.SetKeys (gck, gak);
			trailRenderer.colorGradient = g;
		}

	}


	IEnumerator<float> _execute(){

		yield return Timing.WaitForSeconds(.33f);

		gameObject.SendMessage (skills [skillRunning].skillName);

		if (skills [skillRunning].skillType == SkillInfo.SkillType.Once) {

			Info.myChar.energy -= skills[skillRunning].energyNeeded[skillRunningLevel];

			if (Info.equippedWand != "PuplwoodWand") {
				wands.Find (Info.equippedWand).GetComponent<WandStats> ().strength -= skills [skillRunning].energyNeeded[skillRunningLevel];
			}

			yield return Timing.WaitForSeconds (.33f);
			canMove = true;
			print ("Executed");

		} else {
			anim.speed = 0;
			Timing.RunCoroutine (_continuous (), "continuous");
		}

	}


	public IEnumerator<float> _continuous(){

		while (gameObject != null) {	

			yield return Timing.WaitForSeconds (1);

			Info.myChar.energy -= skills [skillRunning].energyNeeded[skillRunningLevel];

			if (Info.equippedWand != "PuplwoodWand") {
				wands.Find (Info.equippedWand).GetComponent<WandStats> ().strength -= skills [skillRunning].energyNeeded[skillRunningLevel];
			}

			if(Info.myChar.energy < skills [skillRunning].energyNeeded[skillRunningLevel] || wands.Find (Info.equippedWand).GetComponent<WandStats> ().strength <  skills [skillRunning].energyNeeded[skillRunningLevel]){
				returnPos ();
			}

			yield return 0f;		

		}

	}


	public void returnPos(){
		anim.speed = 1;
		Timing.KillCoroutines ("continuous");
		canMove = true;
		print ("Continuous Stopped");
	}


	public IEnumerator<float> _gestureTrail(){

		while (gameObject != null) {	

			trail.localPosition = new Vector3 (Info.remap (Input.mousePosition.x, 0, Screen.width, -.56f, .56f), Info.remap (Input.mousePosition.y, 0, Screen.height, -1, 1), 1);
			yield return 0f;		

		}

	}


	public IEnumerator<float> _trailFadeOut(){

		while (gameObject != null) {

			gak [0].alpha = Mathf.MoveTowards (gak [0].alpha, 0.0f, Timing.DeltaTime * trailFadeSpeed);

			g.SetKeys(gck, gak);
			trailRenderer.colorGradient = g;

			yield return 0f;	

		}

	}


	public IEnumerator<float> _noMana(){

		for (int i = 0; i < 3; i++) {

			noManaArrow.SetActive (true);
			yield return Timing.WaitForSeconds (.2f);
			noManaArrow.SetActive (false);
			yield return Timing.WaitForSeconds (.2f);

		}

	}


	public void Axcendia(){

		if (SceneManager.GetActiveScene ().name == "AR") {

			if (!Info.offline) {
				Info.pmv.RPC("spawnOnNetwork", PhotonTargets.All, skills [0].prefabIndex [skillRunningLevel], source.position, Quaternion.LookRotation ((Info.myCtrl.target - source.position).normalized), PhotonNetwork.AllocateViewID());
			} else {
				Instantiate (Info.pm.prefabs[skills [0].prefabIndex [skillRunningLevel]], source.position, Quaternion.LookRotation ((Info.myCtrl.target - source.position).normalized));
			}

		} else {

			if (!Info.offline) {
				Info.pmv.RPC("spawnOnNetwork", PhotonTargets.All, skills [0].prefabIndex [skillRunningLevel], source.position, Quaternion.LookRotation (cam.transform.forward), PhotonNetwork.AllocateViewID());
			} else {
				Instantiate (Info.pm.prefabs [skills [0].prefabIndex [skillRunningLevel]], source.position, Quaternion.LookRotation (cam.transform.forward));
			}

		}

	}


	public void FlammaBallo(){

		if (SceneManager.GetActiveScene ().name == "AR") {

			if (!Info.offline) {
				Info.pmv.RPC("spawnOnNetwork", PhotonTargets.All, skills [3].prefabIndex [skillRunningLevel], source.position, Quaternion.LookRotation ((Info.myCtrl.target - source.position).normalized), PhotonNetwork.AllocateViewID());
			} else {
				Instantiate (Info.pm.prefabs [skills [3].prefabIndex [skillRunningLevel]], source.position, Quaternion.LookRotation ((Info.myCtrl.target - source.position).normalized));
			}

		} else {

			if (!Info.offline) {
				Info.pmv.RPC("spawnOnNetwork", PhotonTargets.All, skills [3].prefabIndex [skillRunningLevel], source.position, Quaternion.LookRotation (cam.transform.forward), PhotonNetwork.AllocateViewID());
			} else {
				Instantiate (Info.pm.prefabs [skills [3].prefabIndex [skillRunningLevel]], source.position, Quaternion.LookRotation (cam.transform.forward));
			}

		}

	}


	public void Confusa(){

	}


	public void Disarma(){

	}


	public void Fragor(){

		if (SceneManager.GetActiveScene ().name == "AR") {

			if (!Info.offline) {
				Info.pmv.RPC("spawnOnNetwork", PhotonTargets.All, skills [4].prefabIndex [skillRunningLevel], Info.myCtrl.target, Quaternion.identity, PhotonNetwork.AllocateViewID());
			} else {
				Instantiate (Info.pm.prefabs [skills [4].prefabIndex [skillRunningLevel]], Info.myCtrl.target, Quaternion.identity);
			}

		} else {

			ray = cam.ScreenPointToRay (new Vector2(Screen.width/2, (Screen.height/2)-80));

			if (Physics.Raycast (ray, out hit)) {

				target = hit.point;

				if (!Info.offline) {
					Info.pmv.RPC("spawnOnNetwork", PhotonTargets.All, skills [4].prefabIndex [skillRunningLevel], hit.point, Quaternion.identity, PhotonNetwork.AllocateViewID());
				} else {
					Instantiate (Info.pm.prefabs [skills [4].prefabIndex [skillRunningLevel]], hit.point, Quaternion.identity);
				}

			}

		}

	}


	public void HaltJinx(){

	}


	public void Nebula(){

	}


	public void Prostatum(){

		if (SceneManager.GetActiveScene ().name == "AR") {

			if (!Info.offline) {
				Info.pmv.RPC("spawnOnNetwork", PhotonTargets.All, skills [7].prefabIndex [skillRunningLevel], source.position, Quaternion.LookRotation ((Info.myCtrl.target - source.position).normalized), PhotonNetwork.AllocateViewID());
			} else {
				Instantiate (Info.pm.prefabs [skills [7].prefabIndex [skillRunningLevel]], source.position, Quaternion.LookRotation ((Info.myCtrl.target - source.position).normalized));
			}

		} else {

			if (!Info.offline) {
				Info.pmv.RPC("spawnOnNetwork", PhotonTargets.All, skills [7].prefabIndex [skillRunningLevel], source.position, Quaternion.LookRotation (cam.transform.forward), PhotonNetwork.AllocateViewID());
			} else {
				Instantiate (Info.pm.prefabs [skills [7].prefabIndex [skillRunningLevel]], source.position, Quaternion.LookRotation (cam.transform.forward));
			}

		}

	}


	public void ResistoMomentum(){

	}


	public void Resurgo(){

		if (SceneManager.GetActiveScene ().name == "AR") {

			if (!Info.offline) {
				Info.pmv.RPC("spawnOnNetwork", PhotonTargets.All, skills [10].prefabIndex [skillRunningLevel], Vector3.zero, Quaternion.identity, PhotonNetwork.AllocateViewID());
			} else {
				Instantiate (Info.pm.prefabs [skills [10].prefabIndex [skillRunningLevel]], Vector3.zero, Quaternion.identity);
			}

		} else {

			ray = cam.ScreenPointToRay (new Vector2(Screen.width/2, (Screen.height/2)-80));

			if (Physics.Raycast (ray, out hit)) {

				if (!Info.offline) {
					Info.pmv.RPC("spawnOnNetwork", PhotonTargets.All, skills [10].prefabIndex [skillRunningLevel], hit.point, Quaternion.identity, PhotonNetwork.AllocateViewID());
				} else {
					Instantiate (Info.pm.prefabs [skills [10].prefabIndex [skillRunningLevel]], hit.point, Quaternion.identity);
				}

			}

		}

	}

}