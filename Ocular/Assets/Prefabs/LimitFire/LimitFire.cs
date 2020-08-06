using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.SceneManagement;

public class LimitFire : MonoBehaviour {

	public AudioClip alarm, music;

	[HideInInspector]
	public ParticleSystem.EmissionModule em1, em2, em3;

	[HideInInspector]
	public float startEmission;



	public void extinguish(){

		//Set scaler
		if (SceneManager.GetActiveScene ().name == "AR") {
			Info.scaler = GameObject.Find ("ImageTarget").transform.Find ("Scaler");
		}

	
		//Set settings button
		if (!Info.offline) {
			GameObject.Find ("OpenSettings").GetComponent<PanelOnOff> ().hide ();
		} else {
			GameObject.Find ("OpenSettings").GetComponent<PanelOnOff> ().show ();
		}


		//Limit Fire
		transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
		transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
		//transform.GetChild(2).GetComponent<ParticleSystem>().Stop();


		//My Char
		if (SceneManager.GetActiveScene ().name == "AR") {
			Info.myCtrl.speed = Info.rot (.05f, 4, Info.scaler.transform.localScale.x);
		}


		//Menus

		GameObject.Find("MainMenu").GetComponent<Inventory>().life = GameObject.Find ("BattleLifeGlobe").GetComponent<MeshRenderer> ().material;
		GameObject.Find("MainMenu").GetComponent<Inventory>().energy = GameObject.Find ("BattleEnergyGlobe").GetComponent<MeshRenderer> ().material;

		GameObject.Find("MainMenu").GetComponent<Inventory>().lifeVial = GameObject.Find ("InventoryLifeGlobe").GetComponent<MeshRenderer> ().material;
		GameObject.Find("MainMenu").GetComponent<Inventory>().energyVial = GameObject.Find ("InventoryEnergyGlobe").GetComponent<MeshRenderer> ().material;

		GameObject.Find("MainMenu").GetComponent<Inventory>().points = GameObject.Find ("CollectedPointsGlobe").GetComponent<MeshRenderer> ().material;

		GameObject.Find("MainMenu").GetComponent<Inventory>().wandLife = GameObject.Find ("BattleWandLife").transform;


		GameObject.Find ("BattleGlobesCamera").GetComponent<Camera> ().enabled = true;
		GameObject.Find ("BattleGlobesUI").GetComponent<PanelOnOff> ().show ();

		GameObject.Find ("PointsGlobeCamera").GetComponent<Camera> ().enabled = true;
		GameObject.Find ("PointsGlobeUI").GetComponent<PanelOnOff> ().show ();

		if (SceneManager.GetActiveScene ().name == "AR") {
			GameObject.Find ("GestureFXCamera").GetComponent<Camera> ().enabled = true;
			GameObject.Find ("DuringBattle").transform.Find ("GestureTrail").GetComponent<PanelOnOff> ().show ();
		}

		Info.se.GetComponent<PanelOnOff> ().show ();
		GameObject.Find ("Target").GetComponent<PanelOnOff> ().show ();

		if (SceneManager.GetActiveScene ().name == "Classic") {
			GameObject.Find ("CameraJoystick").GetComponent<VirtualJoystick> ().cam = Info.player.transform.Find("Camera").transform;
			GameObject.Find ("MovementJoystick").GetComponent<PanelOnOff> ().show ();
			GameObject.Find ("CameraJoystick").GetComponent<PanelOnOff> ().show ();
		}


		// Misc
		Info.se.activeSkills = GameObject.Find ("ActiveSkills").transform;
		Info.se.activeSummons = GameObject.Find ("ActiveSummons").transform;


		//Coroutines

		Timing.RunCoroutine (GameObject.Find("MainMenu").GetComponent<Inventory>()._update(), "globesUpdate");

		if (SceneManager.GetActiveScene ().name == "AR") {
			Timing.RunCoroutine (Info.myCtrl._raycasts (), "raycasts");
		}

		Timing.RunCoroutine (Info.myChar._update (), "charUpdate");
		Timing.RunCoroutine (GameObject.Find("WandsList").transform.Find (Info.equippedWand).GetComponent<WandStats> ()._update (), "wandUpdate");
		Timing.RunCoroutine (GameObject.Find("WandsList").transform.Find (Info.equippedWand).GetComponent<WandStats> ()._revive (), "wandRevive");

	
		// Audio

		GameObject.Find("SFXAUS").GetComponent<AudioSource> ().clip = alarm;
		GameObject.Find("SFXAUS").GetComponent<AudioSource> ().Play ();

		GameObject.Find ("MusicAUS").GetComponent<AudioSource> ().clip = music;
		GameObject.Find("MusicAUS").GetComponent<AudioSource> ().Play ();

	}

}
