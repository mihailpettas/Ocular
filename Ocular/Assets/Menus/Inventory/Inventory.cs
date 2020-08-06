using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour {

	Text wandsPlayerCoins, vialsPlayerCoins;
	PanelOnOff vialsUsage;
	Camera cam;

	Message message;
	Transform wands;

	PanelOnOff verification, repair, increase;
	string wandToUnlock, wandToRepair;

	int increasePrice, finalQuantity, type;

	[HideInInspector]
	public Material life, energy, lifeVial, energyVial, points;
	[HideInInspector]
	public Transform wandLife;


	void Start(){		
		wandsPlayerCoins = transform.Find("Wands").Find ("CoinsWhite").Find ("Coins").Find ("Price").GetComponent<Text> ();
		vialsPlayerCoins = transform.Find ("Vials").Find ("CoinsWhite").Find ("Coins").Find ("Price").GetComponent<Text> ();
		vialsUsage = GameObject.Find ("DuringBattle").transform.Find ("UsageVialsUI").GetComponent<PanelOnOff> ();
		cam = GameObject.Find ("UsageVialsCamera").GetComponent<Camera> ();
		message = GameObject.Find ("Message").GetComponent<Message> ();
		wands = GameObject.Find ("WandsList").transform;
		verification = transform.Find ("Wands").Find ("Verification").GetComponent<PanelOnOff> ();
		repair = transform.Find ("Wands").Find ("Repair").GetComponent<PanelOnOff> ();
		increase = transform.Find ("Vials").Find ("Increase").GetComponent<PanelOnOff> ();
	}


	public IEnumerator<float> _update(){

		while (gameObject != null) {

			life.SetFloat("_Progress", Info.remap(Info.myChar.life, 0, Info.myChar.sLife, .5f, 1));
			energy.SetFloat("_Progress", Info.remap(Info.myChar.energy, 0, Info.myChar.sEnergy, .5f, 1));

			lifeVial.SetFloat("_Progress", Info.remap(Info.lifeVial, 0, Info.sLifeVial, 0, 1));
			energyVial.SetFloat("_Progress", Info.remap(Info.energyVial, 0, Info.sEnergyVial, 0, 1));

			if (!Info.offline){
				points.SetFloat ("_Progress", Info.remap (Info.se.pointsCollected, 0, Info.opponentLife, 0, 1));
			}

			if (Info.equippedWand != "PuplwoodWand") {
				wandLife.rotation = Quaternion.Euler (0, 0, Info.remap (wands.Find (Info.equippedWand).GetComponent<WandStats> ().strength, 0, wands.Find (Info.equippedWand).GetComponent<WandStats> ().sStrength, 180, 0));
			}

			yield return 0f;

		}

	}


	public void setCoinsBar(){
		vialsPlayerCoins.text = Info.playerCoins.ToString();
		wandsPlayerCoins.text = Info.playerCoins.ToString();
	}


	//------------------------------------------------------------------------


	public void useVial(int type){

		//Ta 4 γκλομπς χρησιμοποιουν κοινα ματιριαλς

		if (type == 0 && Info.myChar.life < Info.myChar.sLife) {
			cam.enabled = true;
			vialsUsage.show ();
			Info.myChar.life++;
			Info.lifeVial--;
		} else if (type == 1 && Info.myChar.energy < Info.myChar.sEnergy) {
			cam.enabled = true;
			vialsUsage.show ();
			Info.myChar.energy++;
			Info.energyVial--;
		}

	}


	public void stopUsingVial(){
		cam.enabled = false;
		vialsUsage.hide ();
	}

	//------------------------------------ VIALS ------------------------------------


	public void increaseVial(string percent){

		type = (int)char.GetNumericValue(percent [0]);

		if (type == 0) {

			if (int.Parse (percent.Substring (1, percent.Length - 1)) == 30) {
				increasePrice = (Info.lifeVial + 30 + 20) * 4;
			} else if (int.Parse (percent.Substring (1, percent.Length - 1)) == 70) {
				increasePrice = (Info.lifeVial + 70 + 10) * 4;
			} else if (int.Parse (percent.Substring (1, percent.Length - 1)) == 100) {
				increasePrice = (Info.lifeVial + 100) * 4;
			}

		} else if (type == 0) {

			if (int.Parse (percent.Substring (1, percent.Length - 1)) == 30) {
				increasePrice = (Info.energyVial + 30 + 20) * 4;
			} else if (int.Parse (percent.Substring (1, percent.Length - 1)) == 70) {
				increasePrice = (Info.energyVial + 70 + 10) * 4;
			} else if (int.Parse (percent.Substring (1, percent.Length - 1)) == 100) {
				increasePrice = (Info.energyVial + 100) * 4;
			}

		}


		if (type == 0) {
			finalQuantity += (int)Info.rot (100, 100 + int.Parse (percent.Substring (1, percent.Length - 1)), Info.lifeVial);
			increase.transform.Find ("Title").GetComponent<Text> ().text = "Increase life vial quantity by: " + percent + "% for " + increasePrice + " thoinks?";
		} else if (type == 1) {
			finalQuantity += (int)Info.rot (100, 100 + int.Parse (percent.Substring (1, percent.Length - 1)), Info.energyVial);
			increase.transform.Find ("Title").GetComponent<Text> ().text = "Increase energy vial quantity by: " + percent + "% for " + increasePrice + " thoinks?";
		}

		increase.show ();

	}


	public void increaseVialVerify(){

		if (Info.playerCoins >= increasePrice) {
			Timing.RunCoroutine(increaseVial(type, finalQuantity, Info.playerCoins-increasePrice));
		} else {
			message.message ("Not enought thoinks");
		}

	}


	//------------------------------------------------------------------------


	public void disableWand(string wandName){
		Timing.RunCoroutine(enableDisableWand(wandName, 0, 0));
	}


	//------------------------------------------------------------------------


	public void equipRepairUnlockWand(string wandName){

		if (wands.Find (wandName).Find ("Lock")) {

			verification.transform.Find ("WandName").GetComponent<Text> ().text = "• " + wands.Find (wandName).Find ("Name").GetComponent<Text> ().text + " •";
			verification.transform.Find ("Price").Find ("Amount").GetComponent<Text> ().text = wands.Find (wandName).Find ("Price").Find ("Amount").GetComponent<Text> ().text;
			verification.transform.Find ("White").Find ("Icon").Find ("WandIcon").GetComponent<Image> ().sprite = wands.Find (wandName).Find ("Icon").GetComponent<Image> ().sprite;
			verification.transform.Find ("WandDescription").GetComponent<Text> ().text = wands.Find (wandName).GetComponent<WandStats> ().description;

			verification.show ();
			wandToUnlock = wandName;

		} else {

			if (wands.Find (wandName).Find ("Icon").GetComponent<Image> ().color.a == 1) {

				if (Info.equippedWand != wandName) {
					Timing.RunCoroutine (equipWandTransaction (wandName));
				} else {
					if (SceneManager.GetActiveScene ().name == "AR") {
						message.message (wands.Find (wandName).Find ("Name").GetComponent<Text> ().text + " Equipped" + "\n\n" + "Strength: " + wands.Find (wandName).GetComponent<WandStats> ().strength/* + "\n" + "Cooldown Rate: " + wands.Find (wandName).GetComponent<WandStats> ().rate*/ + "\n\n" + wands.Find (wandName).GetComponent<WandStats> ().description, 1200);
					} else {
						message.message (wands.Find (wandName).Find ("Name").GetComponent<Text> ().text + " Equipped" + "\n\n" + "Strength: " + wands.Find (wandName).GetComponent<WandStats> ().strength/* + "\n" + "Cooldown Rate: " + wands.Find (wandName).GetComponent<WandStats> ().rate*/ + "\n\n" + wands.Find (wandName).GetComponent<WandStats> ().description, 700);
					}
				}

			} else {

				repair.transform.Find ("WandName").GetComponent<Text> ().text = "Repair: " + wands.Find (wandName).Find ("Name").GetComponent<Text> ().text;
				repair.transform.Find ("Price").Find ("Amount").GetComponent<Text> ().text = (int.Parse (wands.Find (wandName).Find ("Price").Find ("Amount").GetComponent<Text> ().text) / 2).ToString ();
				repair.transform.Find ("White").Find ("Icon").Find ("WandIcon").GetComponent<Image> ().sprite = wands.Find (wandName).Find ("Icon").GetComponent<Image> ().sprite;
				repair.transform.Find ("WandDescription").GetComponent<Text> ().text = wands.Find (wandName).GetComponent<WandStats> ().description;

				repair.show ();
				wandToRepair = wandName;

			}

		}

	}


	public void repairWandVerify(){
		Timing.RunCoroutine(enableDisableWand(wandToRepair, int.Parse(wands.Find (wandToRepair).Find("Price").Find("Amount").GetComponent<Text>().text)/2, 1));
	}


	public void unlockWandVerify(){

		if (Info.playerCoins >= int.Parse (verification.transform.Find ("Price").Find("Amount").GetComponent<Text> ().text)) {
			Timing.RunCoroutine (unlockWandTransaction (wandToUnlock, Info.playerCoins - int.Parse (verification.transform.Find ("Price").Find ("Amount").GetComponent<Text> ().text)));
		} else {
			message.message ("Not enough thoinks");
		}

	}





	//------------------------------------ MYSQL ------------------------------------


	IEnumerator<float> increaseVial(int vialType, int finalQuantity, int coins){

		WWWForm increaseVial = new WWWForm ();

		increaseVial.AddField ("username", Info.playerUsername);
		increaseVial.AddField ("vialType", vialType);
		increaseVial.AddField ("coins", coins);

		// Create a download object
		WWW check = new WWW ("http://ocular.000webhostapp.com/php/increaseVial.php", increaseVial);

		// Wait until the download is done
		yield return Timing.WaitUntilDone(check);

		if (check.text.Trim ().Contains("Success")) {

			if (vialType == 0) {				
				Info.lifeVial += finalQuantity;
			} else if (vialType == 1) {
				Info.energyVial += finalQuantity;
			}

			Info.playerCoins -= coins;
			vialsPlayerCoins.text = Info.playerCoins.ToString ();

		} else {
			Info.loadMenus(true);
		}

	}


	IEnumerator<float> unlockWandTransaction(string wandName, int coins){

		WWWForm unlockWand = new WWWForm ();

		unlockWand.AddField ("username", Info.playerUsername);
		unlockWand.AddField ("wandName", wandName);
		unlockWand.AddField ("coins", coins);

		// Create a download object
		WWW check = new WWW ("http://ocular.000webhostapp.com/php/unlockWand.php", unlockWand);

		// Wait until the download is done
		yield return Timing.WaitUntilDone(check);

		if (check.text.Trim ().Contains("Success")) {

			Info.playerCoins -= int.Parse (verification.transform.Find ("Price").Find ("Amount").GetComponent<Text> ().text);
			Destroy (wands.Find (wandToUnlock).Find ("Icon").Find("Lock").gameObject);
			verification.hide ();
			equipRepairUnlockWand (wandToUnlock);
			wandsPlayerCoins.text = Info.playerCoins.ToString ();

		} else {
			Info.loadMenus(true);
		}

	}


	IEnumerator<float> equipWandTransaction(string wandName){

		WWWForm equipWandTransaction = new WWWForm ();

		equipWandTransaction.AddField ("username", Info.playerUsername);
		equipWandTransaction.AddField ("wandName", wandName);

		// Create a download object
		WWW check = new WWW ("http://ocular.000webhostapp.com/php/equipWand.php", equipWandTransaction);

		// Wait until the download is done
		yield return Timing.WaitUntilDone(check);

		if (check.text.Trim ().Contains("Success")) {

			Timing.KillCoroutines ("wandUpdate");
			Timing.KillCoroutines ("wandRevive");

			Info.equippedWand = wandName;
			Info.wandDamage = wands.Find (wandName).GetComponent<WandStats> ().damage;

			if (SceneManager.GetActiveScene ().name == "AR") {
				message.message (wands.Find (wandName).Find ("Name").GetComponent<Text> ().text + " Equipped" + "\n\n" + "Damage: " + wands.Find (wandName).GetComponent<WandStats> ().damage + "\n\n" + "Strength: " + wands.Find (wandName).GetComponent<WandStats> ().strength/* + "\n" + "Cooldown Rate: " + wands.Find (wandName).GetComponent<WandStats> ().rate */ + "\n\n" + wands.Find (wandName).GetComponent<WandStats> ().description, 700);
			} else {
				message.message (wands.Find (wandName).Find ("Name").GetComponent<Text> ().text + " Equipped" + "\n\n" + "Damage: " + wands.Find (wandName).GetComponent<WandStats> ().damage + "\n\n" + "Strength: " + wands.Find (wandName).GetComponent<WandStats> ().strength/* + "\n" + "Cooldown Rate: " + wands.Find (wandName).GetComponent<WandStats> ().rate */ + "\n\n" + wands.Find (wandName).GetComponent<WandStats> ().description, 1200);
			}
		
		} else {
			Info.loadMenus(true);
		}

	}


	IEnumerator<float> enableDisableWand(string wandName, int coins, int enable){

		WWWForm enableDisableWandTransaction = new WWWForm ();

		enableDisableWandTransaction.AddField ("username", Info.playerUsername);
		enableDisableWandTransaction.AddField ("wandName", wandName);
		enableDisableWandTransaction.AddField ("enable", enable);

		// Create a download object
		WWW check = new WWW ("http://ocular.000webhostapp.com/php/enableDisableWand.php", enableDisableWandTransaction);

		// Wait until the download is done
		yield return Timing.WaitUntilDone(check);

		if (check.text.Trim ().Contains("Success")) {

			if (enable == 0) {

				wands.Find (wandName).Find ("Icon").GetComponent<Image> ().color = new Color (wands.Find (wandName).Find ("Icon").GetComponent<Image> ().color.r, wands.Find (wandName).Find ("Icon").GetComponent<Image> ().color.g, wands.Find (wandName).Find ("Icon").GetComponent<Image> ().color.b, 128);
				wands.Find (wandName).Find ("Icon").GetComponent<Button> ().interactable = false;

				equipRepairUnlockWand ("PuplwoodWand");

			} else {

				wands.Find (wandName).Find ("Icon").GetComponent<Image> ().color = new Color (wands.Find (wandName).Find ("Icon").GetComponent<Image> ().color.r, wands.Find (wandName).Find ("Icon").GetComponent<Image> ().color.g, wands.Find (wandName).Find ("Icon").GetComponent<Image> ().color.b, 255);
				wands.Find (wandName).Find ("Icon").GetComponent<Button> ().interactable = true;
				wands.Find (wandName).GetComponent<WandStats> ().strength = wands.Find (wandName).GetComponent<WandStats> ().sStrength;

			}

		} else {
			Info.loadMenus(true);
		}

	}


}
