using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using UnityEngine.SceneManagement;

public class MySQL : MonoBehaviour {

	Message message;

	[Header("Login")]

	public Text loginUsername;
	public Text loginPassword;

	public Button loginButton, loginBack;

	[Header("Register")]

	public Text registerUsername;
	public Text registerPassword;
	public Text registerEmail;

	public Button registerButton, registerBack;


	void Start(){		
		message = GameObject.Find ("Message").GetComponent<Message> ();
		checkConnTrigger ();
	}


	public void checkConnTrigger(){
		Timing.RunCoroutine (checkConn ());
	}


	IEnumerator<float> checkConn(){

		WWWForm checkForm = new WWWForm ();

		// Create a download object
		WWW check = new WWW ("http://ocular.000webhostapp.com/php/checkConn.php", checkForm);

		// Wait until the download is done
		yield return Timing.WaitUntilDone(check);

		if (check.text.Trim () != "Success") {
			message.message ("No Connection", 400, false, Message.ButtonType.Retry);
			GameObject.Find ("Message").transform.Find ("Button").Find ("Text").GetComponent<Button> ().interactable = true;
		} else {

			message.hide ();

			if (Info.playerUsername.IsNOTNullOrEmpty ()) {
				GameObject.Find ("FirstMenu").GetComponent<PanelOnOff> ().hide ();
				GameObject.Find ("OnlineMenu").GetComponent<PanelOnOff> ().show ();
				Info.pm.connectToPhoton ();
			}

		}

	}


	public void loginTrigger(bool offline){
		
		loginUsername.text = loginUsername.text.Trim ();
		loginPassword.text = loginPassword.text.Trim ();
		loginUsername.GetComponent<Button> ().interactable = false;
		loginPassword.GetComponent<Button> ().interactable = false;
		loginButton.interactable = false;
		loginBack.interactable = false;
		loginButton.GetComponent<Text> ().text = "Please wait...";
		Timing.RunCoroutine (login (offline));

	}


	IEnumerator<float> login(bool offline) {

		if (offline) {

			Info.playerUsername = "lolitsa";
			Info.playerPassword = "123";
			Info.playerEmail = "me@mail.com";
			Info.playerCoins = 0;
			Info.playerPoints = 100;
			Info.tutorial = 1;

			GameObject.Find ("Vials").transform.Find ("CoinsWhite").Find ("Coins").Find ("Price").GetComponent<Text> ().text = "0";
			GameObject.Find ("Wands").transform.Find ("CoinsWhite").Find ("Coins").Find ("Price").GetComponent<Text> ().text = "0";

			GameObject.Find ("Skills").transform.Find ("Intro").Find ("CoinsWhite").Find ("Coins").Find ("Price").GetComponent<Text> ().text = "0";

			Info.lifeVial = 50;
			Info.energyVial = 50;
			Info.sLifeVial = 50;
			Info.sEnergyVial = 50;

			Info.equippedWand = "PuplwoodWand";
			Info.wandDamage = GameObject.Find ("WandsList").transform.GetChild (0).GetComponent<WandStats> ().damage;

			Destroy (GameObject.Find ("Skills").transform.Find ("Intro").Find ("Axcendia").Find ("Lock").gameObject);
			Info.se.getSkill ("Axcendia").locked = false;
			Destroy (GameObject.Find ("Skills").transform.Find ("Intro").Find ("FlammaBallo").Find ("Lock").gameObject);
			Info.se.getSkill ("FlammaBallo").locked = false;	
			Destroy (GameObject.Find ("Skills").transform.Find ("Intro").Find ("Prostatum").Find ("Lock").gameObject);
			Info.se.getSkill ("Prostatum").locked = false;	
			Destroy (GameObject.Find ("Skills").transform.Find ("Intro").Find ("Fragor").Find ("Lock").gameObject);
			Info.se.getSkill ("Fragor").locked = false;	
			Destroy (GameObject.Find ("Skills").transform.Find ("Intro").Find ("Refrigero").Find ("Lock").gameObject);
			Info.se.getSkill ("Refrigero").locked = false;	
			Destroy (GameObject.Find ("Skills").transform.Find ("Intro").Find ("Resurgo").Find ("Lock").gameObject);
			Info.se.getSkill ("Resurgo").locked = false;	
			Destroy (GameObject.Find ("Skills").transform.Find ("Intro").Find ("Nebula").Find ("Lock").gameObject);
			Info.se.getSkill ("Nebula").locked = false;	
			Destroy (GameObject.Find ("Skills").transform.Find ("Intro").Find ("Disarma").Find ("Lock").gameObject);
			Info.se.getSkill ("Disarma").locked = false;	
			Destroy (GameObject.Find ("Skills").transform.Find ("Intro").Find ("Confusa").Find ("Lock").gameObject);
			Info.se.getSkill ("Confusa").locked = false;	
			Destroy (GameObject.Find ("Skills").transform.Find ("Intro").Find ("ResistoMomentum").Find ("Lock").gameObject);
			Info.se.getSkill ("ResistoMomentum").locked = false;	
			Destroy (GameObject.Find ("Skills").transform.Find ("Intro").Find ("HaltJinx").Find ("Lock").gameObject);
			Info.se.getSkill ("HaltJinx").locked = false;	

			GameObject.Find ("LoginMenu").GetComponent<PanelOnOff> ().hide ();
			GameObject.Find ("FirstMenu").GetComponent<PanelOnOff> ().hide ();
			GameObject.Find ("OnlineMenu").GetComponent<PanelOnOff> ().show ();
			Info.pm.connectToPhoton ();

		} else {

			WWWForm loginForm = new WWWForm ();
			loginForm.AddField ("username", loginUsername.text);

			// Create a download object
			WWW check = new WWW ("http://ocular.000webhostapp.com/php/login.php", loginForm);

			// Wait until the download is done
			yield return Timing.WaitUntilDone (check);

			if (check.text.Trim () == "No Results") {
			
				message.message ("Wrong username");
				loginButton.GetComponent<Text> ().text = "Login";
				loginButton.interactable = true;
				loginUsername.GetComponent<Button> ().interactable = true;
				loginPassword.GetComponent<Button> ().interactable = true;

			} else {

				string[] playerInfo = check.text.Split (new char[] { '-' });

				foreach (string info in playerInfo) {
					info.Trim ();
				}

				if (loginPassword.text == playerInfo [0]) {

					//Get Player Info

					Info.playerUsername = loginUsername.text;
					Info.playerPassword = playerInfo [0];
					Info.playerEmail = playerInfo [1];
					Info.playerCoins = int.Parse (playerInfo [2]);
					Info.playerPoints = int.Parse (playerInfo [3]);
					Info.tutorial = int.Parse (playerInfo [4]);

					GameObject.Find ("Vials").transform.Find ("CoinsWhite").Find ("Coins").Find ("Price").GetComponent<Text> ().text = playerInfo [2];
					GameObject.Find ("Wands").transform.Find ("CoinsWhite").Find ("Coins").Find ("Price").GetComponent<Text> ().text = playerInfo [2];
					GameObject.Find ("Skills").transform.Find ("Intro").Find ("CoinsWhite").Find ("Coins").Find ("Price").GetComponent<Text> ().text = playerInfo [2];

					//Get Vials PANTA THA EXEI RESULTS GIATI TOU VAZEI ME TO REGISTER

					WWWForm getVials = new WWWForm ();
					getVials.AddField ("username", loginUsername.text);

					// Create a download object
					WWW vials = new WWW ("http://ocular.000webhostapp.com/php/getVials.php", getVials);

					// Wait until the download is done
					yield return Timing.WaitUntilDone (vials);

					string[] vialRows = vials.text.Split (new char[] { '_' });

					foreach (string vial in vialRows) {

						if (string.IsNullOrEmpty (vial)) {
							continue;
						}

						string[] columns = vial.Split (new char[] { '-' });

						foreach (string column in columns) {
							column.Trim ();
						}

						Info.lifeVial = int.Parse (columns [0]);
						Info.energyVial = int.Parse (columns [1]);
						Info.sLifeVial = int.Parse (columns [0]);
						Info.sEnergyVial = int.Parse (columns [1]);

					}

			
					//Get Wands

					WWWForm getWands = new WWWForm ();
					getWands.AddField ("username", loginUsername.text);

					// Create a download object
					WWW wands = new WWW ("http://ocular.000webhostapp.com/php/getWands.php", getWands);

					// Wait until the download is done
					yield return Timing.WaitUntilDone (wands);

					if (wands.text.Trim () == "No Results") {

						Info.equippedWand = "PuplwoodWand";
						Info.wandDamage = GameObject.Find ("WandsList").transform.GetChild (0).GetComponent<WandStats> ().damage;

					} else {

						string[] wandRows = wands.text.Split (new char[] { '_' });

						foreach (string wand in wandRows) {

							if (string.IsNullOrEmpty (wand)) {
								continue;
							}

							string[] columns = wand.Split (new char[] { '-' });

							foreach (string column in columns) {
								column.Trim ();
							}

							Destroy (GameObject.Find ("WandsList").transform.GetChild (int.Parse (columns [0])).Find ("Icon").Find ("Lock").gameObject);
							GameObject.Find ("WandsList").transform.GetChild (int.Parse (columns [0])).GetComponent<WandStats> ().wandEnabled = int.Parse (columns [1]);

							if (int.Parse (columns [2]) == 1) {
							
								Info.equippedWand = columns [0];
								Info.wandDamage = GameObject.Find ("WandsList").transform.GetChild (int.Parse (columns [0])).GetComponent<WandStats> ().damage;

							}

						}

					}


					//Get Skills

					WWWForm getSkillsForm = new WWWForm ();
					getSkillsForm.AddField ("username", loginUsername.text);

					// Create a download object
					WWW skills = new WWW ("http://ocular.000webhostapp.com/php/getSkills.php", getSkillsForm);

					// Wait until the download is done
					yield return Timing.WaitUntilDone (skills);

					if (skills.text.Trim () != "No Results") {
					
						string[] skillsRows = skills.text.Split (new char[] { '_' });

						foreach (string skill in skillsRows) {
						
							if (string.IsNullOrEmpty (skill)) {
								continue;
							}

							skill.Trim ();

							Destroy (GameObject.Find ("Skills").transform.Find ("Intro").Find (skill).Find ("Lock").gameObject);
							Info.se.getSkill (skill).locked = false;				

						}

					}


					//Get Badges

					WWWForm getBadgesForm = new WWWForm ();
					getBadgesForm.AddField ("username", loginUsername.text);

					// Create a download object
					WWW badges = new WWW ("http://ocular.000webhostapp.com/php/getBadges.php", getBadgesForm);

					// Wait until the download is done
					yield return Timing.WaitUntilDone (badges);

					if (badges.text.Trim () != "No Results") {
					
						string[] badgesRows = badges.text.Split (new char[] { '_' });

						foreach (string badge in badgesRows) {

							if (string.IsNullOrEmpty (badge)) {
								continue;
							}

							badge.Trim ();

							Info.playerBadges.Add (int.Parse (badge));

							GameObject.Find ("OpponentBadges").transform.Find ("Badges").GetChild (int.Parse (badge)).GetComponent<Image> ().sprite = Resources.Load ("Badges/" + badge) as Sprite;
							GameObject.Find ("OpponentBadges").transform.Find ("Badges").GetChild (int.Parse (badge)).GetComponent<Image> ().color = Color.white;

						}

					}

					GameObject.Find ("LoginMenu").GetComponent<PanelOnOff> ().hide ();
					GameObject.Find ("FirstMenu").GetComponent<PanelOnOff> ().hide ();
					GameObject.Find ("OnlineMenu").GetComponent<PanelOnOff> ().show ();
					Info.pm.connectToPhoton ();

				} else {
				
					message.message ("Wrong password or connection error");
					loginButton.GetComponent<Text> ().text = "Login";
					loginButton.interactable = true;
					loginBack.interactable = true;
					loginUsername.GetComponent<Button> ().interactable = true;
					loginPassword.GetComponent<Button> ().interactable = true;

				}

			} 
		
		}

	} 


	public void registerTrigger(){

		if (registerUsername.text.Length > 20 || registerPassword.text.Length > 20) {
			message.message ("Username & Password must have less than 20 characters");
		} else {

			registerUsername.text = registerUsername.text.Trim ();
			registerPassword.text = registerPassword.text.Trim ();
			registerUsername.GetComponent<Button> ().interactable = false;
			registerPassword.GetComponent<Button> ().interactable = false;
			registerButton.interactable = false;
			registerBack.interactable = false;
			registerButton.GetComponent<Text> ().text = "Please wait...";
			Timing.RunCoroutine (register ());

		}

	}


	IEnumerator<float> register(){

		WWWForm registerForm = new WWWForm ();

		registerForm.AddField ("username", registerUsername.text);
		registerForm.AddField ("password", registerPassword.text);
		registerForm.AddField ("email", registerEmail.text);

		// Create a download object
		WWW check = new WWW ("http://ocular.000webhostapp.com/php/register.php", registerForm);

		// Wait until the download is done
		yield return Timing.WaitUntilDone(check);

		if (check.text.Trim ().Contains("Success")) {

			Info.playerUsername = registerUsername.text;
			Info.playerPassword = registerPassword.text;
			Info.playerEmail = registerEmail.text;
			Info.playerCoins = 20;
			Info.playerPoints = 0;
			Info.tutorial = 0;

			GameObject.Find ("RegisterMenu").GetComponent<PanelOnOff> ().hide ();
			GameObject.Find ("FirstMenu").GetComponent<PanelOnOff> ().hide ();
			GameObject.Find ("OnlineMenu").GetComponent<PanelOnOff> ().show ();
			Info.pm.connectToPhoton ();

		} else {
			
			message.message ("Username or E-Mail already registered kid. (or connection error)");
			registerButton.GetComponent<Text> ().text = "Register";
			registerButton.interactable = true;
			registerBack.interactable = true;
			registerUsername.GetComponent<Button> ().interactable = true;
			registerPassword.GetComponent<Button> ().interactable = true;

		}

	}


}