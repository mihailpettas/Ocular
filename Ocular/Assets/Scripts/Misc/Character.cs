using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using MEC;

public class Character : MonoBehaviour {

	public enum CharacterClass {Wizard};
	public CharacterClass charClass;

	public Material blue, yellow;

	Animator anim;

	[Header("Stats")]

	public float life = 100;
	[HideInInspector]
	public float sLife;

	public float energy = 100;
	[HideInInspector]
	public float sEnergy;
	public float rate = 4;	//%

	public float armor = 5;	//%
	[HideInInspector]	
	public float sArmor;

	public float damage = .5f;	//%
	[HideInInspector]
	public float sDamage;


	public void Start (){

		//Parentize or kill listener (AR // Classic)

		if (SceneManager.GetActiveScene ().name == "AR") {
			transform.parent = GameObject.Find ("ImageTarget").transform.Find ("Scaler");
		} else {
			Destroy(GameObject.Find("Menus").GetComponent<AudioListener>());
		}


		if ((!Info.offline && GetComponent<PhotonView>().isMine) || Info.offline) {	//My

			Destroy(GameObject.Find("Menus").GetComponent<AudioListener>());

			//Basics

			gameObject.layer = 8;
			gameObject.name = "Me";

			Info.player = gameObject;
			Info.myChar = this;

			if (PhotonNetwork.isMasterClient) {
				transform.position = new Vector3 (0, 0, -9);
				transform.eulerAngles = new Vector3 (0, 0, 0);
			} else {
				transform.position = new Vector3 (0, 0, 9);
				transform.eulerAngles = new Vector3 (0, 180, 0);
			}

			anim = GetComponent<Animator> ();
			Info.pm.myAnim = GetComponent<Animator> ();


			//Stats

			life += Mathf.Abs(Info.rot (100, 10, Info.playerPoints));
			energy += Mathf.Abs(Info.rot (100, 10, Info.playerPoints));
			armor += Mathf.Abs(Info.rot(100, Info.rot (100, 1, Info.playerPoints), armor));
			damage += Mathf.Abs(Info.rot (100, .01f, Info.playerPoints));
			rate += Mathf.Abs(Info.rot(100, Info.rot (100, 1, Info.playerPoints), rate));

			sLife = life;
			sEnergy = energy;
			sArmor = armor;
			sDamage = damage;


			//Menus

			GameObject.Find ("SkillsRecognizer").GetComponent<SkillExecutor> ().anim = GetComponent<Animator> ();
			GameObject.Find ("SkillsRecognizer").GetComponent<GestureRecognizer.DrawDetector> ().anim = GetComponent<Animator> ();

			if (SceneManager.GetActiveScene ().name == "Classic") {
				GameObject.Find ("SkillsRecognizer").GetComponent<SkillExecutor> ().cam = transform.Find ("Camera").GetComponent<Camera> ();
			} else {
				Info.player.GetComponent<Controller> ().cam = GameObject.Find ("ARCamera").GetComponent<Camera> ();
			}

			if (SceneManager.GetActiveScene ().name == "AR") {				
				Info.myCtrl = GetComponent<Controller> ();
				GetComponent<Controller> ().animator = GetComponent<Animator> ();
				GetComponent<Controller> ().targetDot = GameObject.Find ("TargetDot").GetComponent<PanelOnOff> ();
			} else {				
				GameObject.Find ("MovementJoystick").GetComponent<VirtualJoystick> ().animator = GetComponent<Animator> ();
			}


			//Skills Source

			if (SceneManager.GetActiveScene ().name == "AR") {
				GameObject.Find ("SkillsRecognizer").GetComponent<SkillExecutor> ().source = transform.Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("mixamorig:RightShoulder").Find ("mixamorig:RightArm").Find ("mixamorig:RightForeArm").Find ("mixamorig:RightHand").Find ("wand05_blue").GetChild (0);
			} else {
				GameObject.Find ("SkillsRecognizer").GetComponent<SkillExecutor> ().source = transform.Find ("Model").Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("mixamorig:RightShoulder").Find ("mixamorig:RightArm").Find ("mixamorig:RightForeArm").Find ("mixamorig:RightHand").Find ("wand05_blue").GetChild (0);
			}


			//Cape colors

			if (!Info.offline) {

				if(SceneManager.GetActiveScene ().name == "AR") {

					if (PhotonNetwork.isMasterClient) {
						transform.Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("Robes").Find ("skeleton_robe").GetComponent<SkinnedMeshRenderer> ().material = yellow;
					} else {
						transform.Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("Robes").Find ("skeleton_robe").GetComponent<SkinnedMeshRenderer> ().material = blue;
					}

				}else{

					if (PhotonNetwork.isMasterClient) {
						transform.Find ("Model").Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("Robes").Find ("skeleton_robe").GetComponent<SkinnedMeshRenderer> ().material = yellow;
					} else {
						transform.Find ("Model").Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("Robes").Find ("skeleton_robe").GetComponent<SkinnedMeshRenderer> ().material = blue;
					}

				}

			}


			//Set settings
			GameObject.Find ("Settings").GetComponent<Settings> ().setSettings ();


			//If offline go

			if (Info.offline) {
				GameObject.Find ("LimitFire").GetComponent<LimitFire> ().extinguish ();
			}

		} else if (!Info.offline && !GetComponent<PhotonView>().isMine){	//Opponent

			gameObject.layer = 9;
			gameObject.name = "Opponent";
			Info.opponent = gameObject;

			Info.pm.opAnim = GetComponent<Animator> ();


			if(SceneManager.GetActiveScene ().name == "AR") {

				if (PhotonNetwork.isMasterClient) {
					transform.Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("Robes").Find ("skeleton_robe").GetComponent<SkinnedMeshRenderer> ().material = blue;
				} else {
					transform.Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("Robes").Find ("skeleton_robe").GetComponent<SkinnedMeshRenderer> ().material = yellow;
				}

			}else{

				if (PhotonNetwork.isMasterClient) {
					transform.Find ("Model").Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("Robes").Find ("skeleton_robe").GetComponent<SkinnedMeshRenderer> ().material = blue;
				} else {
					transform.Find ("Model").Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("Robes").Find ("skeleton_robe").GetComponent<SkinnedMeshRenderer> ().material = yellow;
				}

			}

			if (SceneManager.GetActiveScene ().name == "Classic") {
				Destroy (transform.Find ("Camera").gameObject);
			}

		}

	}
		

	public IEnumerator<float> _update() {

		while (gameObject != null) {

			if (energy < sEnergy) {

				energy += Info.rot(100, rate, sEnergy);

				if (energy > sEnergy) {
					energy = sEnergy;
				}

			}

			yield return Timing.WaitForSeconds (1);

		}

	}


	//--------------------------------------------------------------------------------------------------------------------------------


	public void getDamage(float dmg){	//Το authority ελεγχεται απο εκει που καλειται η συναρτηση οποτε εδω παντα AUTHORITY == TRUE

		if (life > 0) {

			life = life - (dmg - Info.rot(100, armor, dmg));

			if (life <= 0) {

				if (!Info.offline) {
					Info.pmv.RPC ("setAnimation", PhotonTargets.All, "DeathFront", PhotonNetwork.player);
					Info.pmv.RPC ("victory", PhotonTargets.Others);
				} else {
					anim.Play ("DeathFront");
				}

			} else {

				if(!anim.GetCurrentAnimatorStateInfo (0).IsName ("GetHitFront")){
					
					if (!Info.offline) {
						Info.pmv.RPC ("setAnimation", PhotonTargets.All, "GetHitFront", PhotonNetwork.player);
					} else {
						anim.Play ("GetHitFront");
					}

				}

				armor = Info.remap (life, 0, sLife, armor, sArmor);
				damage = Info.remap (life, 0, sLife, damage, sDamage);

			}

		}

	}


	public IEnumerator<float> _addPoints(float points){

		WWWForm addPoints = new WWWForm ();

		addPoints.AddField ("username", Info.playerUsername);
		addPoints.AddField ("points", /*(int)points*/0);

		// Create a download object
		WWW check = new WWW ("http://ocular.000webhostapp.com/php/addPoints.php", addPoints);

		// Wait until the download is done
		yield return Timing.WaitUntilDone(check);

		if (check.text.Trim ().Contains ("Success")) {	
			Info.playerPoints += (int)points;
			Info.pmv.RPC ("getWinnerInfo", PhotonTargets.All, Info.playerUsername, Info.playerPoints + Info.se.pointsCollected, Info.playerBadges.ToArray ());
		}else{
			Info.loadMenus(true);
		}

	}

}
