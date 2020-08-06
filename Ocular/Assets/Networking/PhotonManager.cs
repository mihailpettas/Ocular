using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;
using MEC;
using UnityEngine.SceneManagement;

public class PhotonManager : PunBehaviour {

	public List<GameObject> prefabs;
	GameObject prefabVar;

	Text roomName;
	Message msg;

	[HideInInspector]
	public Animator myAnim, opAnim;

	PhotonView pv;


	void Start(){
		msg = GameObject.Find ("Message").GetComponent<Message> ();
		roomName = GameObject.Find ("RoomName").transform.GetChild (0).GetComponent<Text> ();
		pv = GetComponent<PhotonView> ();
	}


	public void connectToPhoton(){
		PhotonNetwork.ConnectUsingSettings("v0.3");
	}


	public override void OnConnectedToMaster (){
		PhotonNetwork.JoinLobby ();
	}


	public override void OnJoinedLobby (){
		GameObject.Find ("CreateBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;
		GameObject.Find ("RandomBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;
		GameObject.Find ("JoinByNameBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;	
		GameObject.Find ("RoomName").transform.GetChild(0).GetComponent<Button> ().interactable = true;	
	}


	public void create(){

		GameObject.Find ("CreateBlack").transform.GetChild(0).GetComponent<Button> ().interactable = false;
		GameObject.Find ("RandomBlack").transform.GetChild(0).GetComponent<Button> ().interactable = false;
		GameObject.Find ("JoinByNameBlack").transform.GetChild(0).GetComponent<Button> ().interactable = false;
		GameObject.Find ("RoomName").transform.GetChild(0).GetComponent<Button> ().interactable = false;	

		RoomOptions options = new RoomOptions ();
		options.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable ();
		options.CustomRoomProperties.Add ("points", Info.playerPoints);
		options.MaxPlayers = 2;

		PhotonNetwork.CreateRoom (Info.playerUsername, options, PhotonNetwork.lobby);

	}


	public void join(){

		GameObject.Find ("CreateBlack").transform.GetChild(0).GetComponent<Button> ().interactable = false;
		GameObject.Find ("RandomBlack").transform.GetChild(0).GetComponent<Button> ().interactable = false;
		GameObject.Find ("JoinByNameBlack").transform.GetChild(0).GetComponent<Button> ().interactable = false;		
		GameObject.Find ("RoomName").transform.GetChild(0).GetComponent<Button> ().interactable = false;	

		RoomOptions options = new RoomOptions ();
		options.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable ();
		options.CustomRoomProperties.Add ("points", Info.playerPoints);
		options.MaxPlayers = 2;

		PhotonNetwork.JoinRoom (roomName.text);

	}


	public void random(){

		GameObject.Find ("CreateBlack").transform.GetChild(0).GetComponent<Button> ().interactable = false;
		GameObject.Find ("RandomBlack").transform.GetChild(0).GetComponent<Button> ().interactable = false;
		GameObject.Find ("JoinByNameBlack").transform.GetChild(0).GetComponent<Button> ().interactable = false;
		GameObject.Find ("RoomName").transform.GetChild(0).GetComponent<Button> ().interactable = false;	

		ExitGames.Client.Photon.Hashtable roomProperties = new ExitGames.Client.Photon.Hashtable();
		roomProperties.Add ("points", 1);

		PhotonNetwork.JoinRandomRoom(roomProperties, 2);

	}


	public override void OnJoinedRoom(){

		print ("Room Joined");

		if (PhotonNetwork.playerList.Length == 2) {

			GameObject.Find ("OnlineMenu").GetComponent<PanelOnOff> ().hide ();

			if (SceneManager.GetActiveScene ().name == "AR") {
				GameObject.Find ("Generator").GetComponent<PanelOnOff> ().show ();
			}

		} else {
			msg.message ("Waiting for someone to join", 400, false, Message.ButtonType.CancelRoom);
			/*GameObject.Find ("CreateBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;
			GameObject.Find ("JoinBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;
			GameObject.Find ("JoinByNameBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;*/
		}

	}


	public override void OnPhotonPlayerConnected(PhotonPlayer player){		

		print ("Player Connected");

		msg.hide ();
		GameObject.Find ("OnlineMenu").GetComponent<PanelOnOff> ().hide ();

		if (SceneManager.GetActiveScene ().name == "AR") {
			GameObject.Find ("Generator").GetComponent<PanelOnOff> ().show ();
		} else {
			Info.pmv.RPC ("getInfo", PhotonTargets.Others, Info.playerUsername, Info.playerPoints);
		}

	}


	//-------------------------------------- Check if both snapped ----------------------------------------


	[PunRPC]
	public void checkArena(){

		if (GameObject.Find ("ImageTarget")) {
			Info.pmv.RPC ("getInfo", PhotonTargets.Others, Info.playerUsername, Info.playerPoints);
		} 

	}


	//------------------------------------- Exchange Infos -----------------------------------------


	[PunRPC]
	public void getInfo(string pName, int pPoints){

		if (!GameObject.Find ("OpponentInfo").GetComponent<PanelOnOff> ().showUI) {

			GameObject.Find ("OpponentName").GetComponent<Text> ().text = pName;
			GameObject.Find ("OpponentPoints").GetComponent<Text> ().text = "Opponent points: " + pPoints.ToString ();
			GameObject.Find ("OpponentInfo").GetComponent<PanelOnOff> ().show ();
			GameObject.Find ("OpenSettings").GetComponent<PanelOnOff> ().show ();

			if (SceneManager.GetActiveScene ().name == "AR") {
				pv.RPC("spawnOnNetwork", PhotonTargets.All, 0, Vector3.zero, Quaternion.identity, PhotonNetwork.AllocateViewID());
			} else {
				pv.RPC("spawnOnNetwork", PhotonTargets.All, 1, Vector3.zero, Quaternion.identity, PhotonNetwork.AllocateViewID());
			}

			Info.pmv.RPC ("getInfo", PhotonTargets.Others, Info.playerUsername, Info.playerPoints);

		} else if (!GameObject.Find ("Ready").GetComponent<PanelOnOff> ().showUI) {

			GameObject.Find ("Ready").GetComponent<PanelOnOff> ().show ();
			Info.pmv.RPC ("getInfo", PhotonTargets.Others, Info.playerUsername, Info.playerPoints);

		}

	}


	//------------------------------------- When both press 'Ready' -----------------------------------------


	[PunRPC]
	public void getLife(float pLife, bool ready){

		if (Info.opponentLife == 0) {
			Info.opponentLife = pLife;
		}

		if (ready) {
			GameObject.Find ("OpponentInfo").GetComponent<PanelOnOff> ().hide ();
			GameObject.Find ("LimitFire").GetComponent<LimitFire> ().extinguish();
		}

	}


	//-------------------------------------- When battle ends----------------------------------------


	[PunRPC]
	public void victory(){
		Timing.RunCoroutine(Info.myChar._addPoints (Info.se.pointsCollected));
	}


	[PunRPC]
	public void getWinnerInfo(string winnerName, float winnerPoints, int[] winnerBadges){

		//Midenize points
		Info.se.pointsCollected = 0;


		//Reset Menus

		GameObject.Find ("BattleGlobesCamera").GetComponent<Camera> ().enabled = false;
		GameObject.Find ("DuringBattle").transform.Find ("BattleGlobesUI").GetComponent<PanelOnOff> ().hide ();

		GameObject.Find ("PointsGlobeCamera").GetComponent<Camera> ().enabled = false;
		GameObject.Find ("DuringBattle").transform.Find ("PointsGlobeUI").GetComponent<PanelOnOff> ().hide ();

		if (SceneManager.GetActiveScene ().name == "AR") {
			GameObject.Find ("GestureFXCamera").GetComponent<Camera> ().enabled = false;
			GameObject.Find ("DuringBattle").transform.Find ("GestureTrail").GetComponent<PanelOnOff> ().hide ();
		}


		//Reset stats

		Info.player.GetComponent<Character> ().life = Info.player.GetComponent<Character> ().sLife;		//Afta bainoun prin kleisw ta coroutines gia na epanerthoun ta globes 
		Info.player.GetComponent<Character> ().energy = Info.player.GetComponent<Character> ().sEnergy;

		Info.lifeVial = Info.sLifeVial;
		Info.energyVial = Info.sEnergyVial;


		//Stop coroutines
		Timing.KillCoroutines ("globesUpdate");
		Timing.KillCoroutines ("raycasts");
		Timing.KillCoroutines ("charUpdate");


		//Reset position

		if (PhotonNetwork.player.IsMasterClient) {
			Info.player.transform.position = new Vector3 (0, 0, -9);
			Info.player.transform.rotation = Quaternion.Euler (0, 0, 0);
		} else {
			Info.player.transform.position = new Vector3 (0, 0, 9);
			Info.player.transform.rotation = Quaternion.Euler (0, 180, 0);
		}


		//Ξεκιναω το countdown για το φραγμα
		GameObject.Find ("LimitFire").transform.GetChild(0).GetComponent<ParticleSystem>().Play();
		GameObject.Find ("LimitFire").transform.GetChild(1).GetComponent<ParticleSystem>().Play();
		GameObject.Find ("LimitFire").transform.GetChild(2).GetComponent<ParticleSystem>().Play();


		//Set winner info

		Info.se.GetComponent<PanelOnOff> ().hide ();
		GameObject.Find ("DuringBattle").transform.Find ("Target").GetComponent<PanelOnOff> ().hide ();
		GameObject.Find ("OpenSettings").GetComponent<PanelOnOff> ().hide ();

		GameObject.Find ("WinnerName").GetComponent<Text> ().text = winnerName;
		GameObject.Find ("WinnerPoints").GetComponent<Text> ().text = "Points: " + winnerPoints.ToString ();

		for (int i = 0; i < winnerBadges.Length; i++) {
			GameObject.Find ("WinnerBadges").transform.Find ("Badges").GetChild (i).GetComponent<Image> ().sprite = Resources.Load ("Badges/" + i) as Sprite;
			GameObject.Find ("WinnerBadges").transform.Find ("Badges").GetChild (i).GetComponent<Image> ().color = Color.white;
		}

		if (SceneManager.GetActiveScene ().name == "AR") {
			GameObject.Find ("WinnerInfo").transform.Find("Black").GetComponent<PanelOnOff> ().show ();
		} else {
			GameObject.Find ("WinnerInfo").transform.Find ("Re-Battle").GetComponent<PanelOnOff> ().show ();
			GameObject.Find ("WinnerInfo").transform.Find ("Quit").GetComponent<PanelOnOff> ().show ();
		}

		GameObject.Find ("WinnerInfo").GetComponent<PanelOnOff> ().show ();

	}


	[PunRPC]
	public void reBattle(){
		Info.myChar.Start ();	//Vazei ta kainourgia stats meta ton proigoumeno agona
		GameObject.Find ("WinnerInfo").GetComponent<PanelOnOff> ().hide ();
		GameObject.Find ("LimitFire").GetComponent<LimitFire> ().extinguish ();
	}


	//-------------------------------------- Misc Tools ----------------------------------------


	/*[PunRPC]
	public void spawnObject(string oName, Vector3 pos, Quaternion rot){
		PhotonNetwork.Instantiate (oName, pos, rot, 0);
	}


	[PunRPC]
	public void destroyObject(int viewID){
		PhotonNetwork.Destroy (PhotonView.Find (viewID));
	}*/


	[PunRPC]
	void spawnOnNetwork(int prefabIndex, Vector3 pos, Quaternion rot, int id1) {			
		prefabs [prefabIndex].GetComponent<PhotonView> ().viewID = id1;
		prefabVar = Instantiate(prefabs[prefabIndex], pos, rot);
	}


	[PunRPC]
	void destroyOnNetwork(int viewID) {	

		if (PhotonView.Find (viewID)) {	// ???
			Destroy (PhotonView.Find (viewID).gameObject);
		}

	}


	[PunRPC]
	public void setAnimation(string animation, PhotonPlayer pPlayer){		

		if (pPlayer == PhotonNetwork.player) {
			myAnim.Play (animation);
		} else {
			opAnim.Play (animation);
		}

	}


	[PunRPC]
	public void getDamage(float damage){
		Info.myChar.getDamage (damage);
	}


	[PunRPC]
	public void resetCombo(){
		Info.se.combo = 1;
	}


	public override void OnPhotonCreateRoomFailed(object[] codeAndMsg){
		print ("Room Creation Failed");
		msg.message ("Room Creation Failed");
		GameObject.Find ("CreateBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;
		GameObject.Find ("RandomBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;
		GameObject.Find ("JoinByNameBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;
		GameObject.Find ("RoomName").transform.GetChild(0).GetComponent<Button> ().interactable = true;	
	}


	public override void OnCreatedRoom(){
		print ("Room Created");
	}


	public override void OnPhotonJoinRoomFailed(object[] codeAndMsg){
		print ("join room failed");
		msg.message ("Wrong Room Name");
		GameObject.Find ("CreateBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;
		GameObject.Find ("RandomBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;
		GameObject.Find ("JoinByNameBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;
		GameObject.Find ("RoomName").transform.GetChild(0).GetComponent<Button> ().interactable = true;	
	}


	public override void OnPhotonPlayerDisconnected(PhotonPlayer player){
		print ("Player Disconnected");
		Info.loadMenus (true);
	}


	public override void OnFailedToConnectToPhoton(DisconnectCause cause){
		print ("Connection to Photon Failed");
	}


	public override void OnConnectionFail(DisconnectCause cause){
		print ("Connection Failed");
	}


	public override void OnPhotonMaxCccuReached(){
		print ("Max CCU Reached");
		msg.message ("Server Full");
	}


	public override void OnPhotonRandomJoinFailed(object[] codeAndMsg){
		print ("Random Room Join Failed");
		msg.message ("No Rooms Available");
		GameObject.Find ("CreateBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;
		GameObject.Find ("RandomBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;
		GameObject.Find ("JoinByNameBlack").transform.GetChild(0).GetComponent<Button> ().interactable = true;
		GameObject.Find ("RoomName").transform.GetChild(0).GetComponent<Button> ().interactable = true;	
	}

}
