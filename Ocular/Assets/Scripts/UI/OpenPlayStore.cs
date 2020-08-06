using UnityEngine;
using System.Collections;

public class OpenPlayStore : MonoBehaviour {

	public void openStore(){
		Application.OpenURL ("playstoreurl");
		Application.Quit ();
	}

}
