using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SetPicture : MonoBehaviour {

	public bool isEdit;

	//ImagePickerPlugin imagePickerPlugin;
	string imagePath=""; //Όταν βαλω το Image Picker θα ανοιξω το κατω και θα φυγει το warning
	Texture2D image;


	/*// Use this for initialization
	void Start () {
		imagePickerPlugin = ImagePickerPlugin.GetInstance();
		imagePickerPlugin.SetDebug(0);
		imagePickerPlugin.Init();
		imagePickerPlugin.SetImagePickerCallbackListener(onGetImageComplete, onGetImageCancel, onGetImageFail);
	}

	public void GetImage(){
		imagePickerPlugin.GetImage();
	}    */

	void onGetImageComplete(string imagePath){		
		this.imagePath = imagePath;
		Invoke("DelayLoadImage",0.5f);
	}
		
	void DelayLoadImage(){
		
		//image = AUP.Utils.LoadTexture(imagePath);
		Texture2D texture = Info.CalculateTexture (image.width, image.width, image.width/2, image.width/2, image.height/2, image);
		GetComponent<Image>().sprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (texture.width/2, texture.height/2));

		if (isEdit) {
			// Update DB (Insert happens on register button)
		}

		if (transform.childCount > 0) {
			Destroy (transform.GetChild(0));
		}
			
	}

	void onGetImageCancel(){
		
	}

	void onGetImageFail(){
		
	}

}