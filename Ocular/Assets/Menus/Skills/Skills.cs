using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

public class Skills : MonoBehaviour {

	public Text playerCoins;

	PanelOnOff verification;
	Message message;
	string skillToUnlock;


	void Start(){
		message = GameObject.Find ("Message").GetComponent<Message>();
		verification = transform.Find ("Verification").GetComponent<PanelOnOff> ();
	}


	public void setCoinsBar(){
		playerCoins.text = Info.playerCoins.ToString();
	}


	public void openOrUnlockSkill(string skillName){	//Ta skills sti sira ston editor gia na anigei to sosto
		
		if (transform.Find(skillName).Find ("Lock")) {

			skillToUnlock = skillName;

			verification.transform.Find ("SkillName").GetComponent<Text> ().text = transform.Find(skillName).Find ("Name").GetComponent<Text> ().text;
			verification.transform.Find ("Price").Find ("Amount").GetComponent<Text> ().text = transform.Find(skillName).Find ("Price").GetChild (0).name;
			verification.transform.Find ("White").Find ("Icon").Find ("SkillIcon").GetComponent<Image> ().sprite = transform.Find(skillName).Find ("Icon").GetComponent<Image> ().sprite;
			verification.transform.Find ("White").Find ("Icon").Find ("SkillIcon").GetComponent<Image> ().color = transform.Find ("Intro").Find(skillName).Find ("Icon").GetComponent<Image> ().color;
			verification.transform.Find ("SkillDescription").GetComponent<Text> ().text = transform.Find(skillName).Find ("Description").GetComponent<Text> ().text;

			verification.show ();

		} else {
			transform.Find(skillName).GetComponent<PanelOnOff> ().show ();
			transform.Find ("Intro").GetComponent<PanelOnOff> ().hide ();
		}

	}


	public void unlockSkillVerify(){

		if (Info.playerCoins >= int.Parse (verification.transform.Find ("Price").Find("Amount").GetComponent<Text> ().text)) {
			Timing.RunCoroutine (unlockSkillTransaction (skillToUnlock, Info.playerCoins - int.Parse (verification.transform.Find ("Price").Find ("Amount").GetComponent<Text> ().text)));
		} else {
			message.message ("Not enough thoinks");
		}

	}


	IEnumerator<float> unlockSkillTransaction(string skillName, int coins){

		WWWForm unlockWand = new WWWForm ();

		unlockWand.AddField ("username", Info.playerUsername);
		unlockWand.AddField ("skillName", skillName);
		unlockWand.AddField ("coins", coins);

		// Create a download object
		WWW check = new WWW ("http://ocular.000webhostapp.com/php/unlockSkill.php", unlockWand);

		// Wait until the download is done
		yield return Timing.WaitUntilDone(check);

		if (check.text.Trim ().Contains("Success")) {

			Info.playerCoins -= int.Parse (verification.transform.Find ("Price").Find ("Amount").GetComponent<Text> ().text);
			Destroy (transform.Find("Intro").Find (skillName).Find ("Lock").gameObject);
			verification.hide ();
			playerCoins.text = Info.playerCoins.ToString ();

		} else {
			Info.loadMenus(true);
		}

	}

}
