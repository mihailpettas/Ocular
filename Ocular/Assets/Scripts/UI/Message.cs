using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour {

	Text text;
	Button closeMessageButton;
	Transform button;
	PanelOnOff pof;
	RectTransform rect, textRect;
	UISFX aus;

	public enum ButtonType{ None, Retry, CancelHost, DisconnectFromDevice, CancelRoom };
	public ButtonType btnTypeFunc;


	void Start (){
		pof = GetComponent<PanelOnOff> ();
		text = transform.Find("Text").GetComponent<Text> ();
		closeMessageButton = text.GetComponent<Button> ();
		button = transform.Find ("Button");
		rect = transform.Find("Image").GetComponent<RectTransform> ();
		textRect = transform.Find ("Text").GetComponent<RectTransform> ();
		aus = GameObject.Find ("SFXAUS").GetComponent<UISFX> ();
	}


	public void message(string txt, int height = 400, bool closable = true, ButtonType btnType = ButtonType.None){

		if (!GetComponent<PanelOnOff> ().showUI) {

			btnTypeFunc = btnType;

			if (btnType == ButtonType.Retry) {
				button.GetComponent<PanelOnOff> ().show ();
				button.Find ("Text").GetComponent<Text> ().text = "Retry";
			} else if (btnType == ButtonType.CancelHost) {
				button.GetComponent<PanelOnOff> ().show ();
				button.Find ("Text").GetComponent<Text> ().text = "Cancel";
			} else if (btnType == ButtonType.DisconnectFromDevice) {
				button.GetComponent<PanelOnOff> ().show ();
				button.Find ("Text").GetComponent<Text> ().text = "Cancel";
			} else if (btnType == ButtonType.CancelRoom) {
				button.GetComponent<PanelOnOff> ().show ();
				button.Find ("Text").GetComponent<Text> ().text = "Cancel";
			} else if (btnType == ButtonType.None) {
				button.GetComponent<PanelOnOff> ().hide ();
				button.Find ("Text").GetComponent<Text> ().text = string.Empty;
			}

			closeMessageButton.interactable = closable;
			text.text = txt;
			rect.sizeDelta = new Vector2 (rect.sizeDelta.x, height);
			textRect.sizeDelta = new Vector2 (rect.sizeDelta.x, height);
			pof.show ();

		}

	}


	public void buttonFunc(){

		button.Find ("Text").GetComponent<Button> ().interactable = false;

		if (btnTypeFunc == ButtonType.Retry) {
			GameObject.Find ("NetworkManager").GetComponent<MySQL> ().checkConnTrigger ();
		} else if (btnTypeFunc == ButtonType.CancelHost || btnTypeFunc == ButtonType.DisconnectFromDevice || btnTypeFunc == ButtonType.CancelRoom) {
			Info.loadMenus (false);
		}

	}


	public void hide(){
		pof.hide ();
		aus.uiSound ();
	}

}
