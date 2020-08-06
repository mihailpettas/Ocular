using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.UI;

public class WandStats : MonoBehaviour {

	public int wandEnabled;

	public float damage;
	public float strength;
	float rate = 1;	//%

	public string description;

	[HideInInspector]
	public float sStrength;

	Inventory inv;


	void Start(){		
		sStrength = strength;
		inv = GameObject.Find ("Main").GetComponent<Inventory> ();	
	}


	public IEnumerator<float> _update(){

		while (gameObject != null) {		
			
			if (strength <= 0) {
				strength = 0;
				inv.disableWand (gameObject.name);
				yield break;
			}

			yield return 0f;

		}

	}


	public IEnumerator<float> _revive(){

		while (gameObject != null) {	

			if (strength < sStrength) {

				strength += Info.rot(100, rate, sStrength);

				if (strength > sStrength) {
					strength = sStrength;
				}

			}

			yield return Timing.WaitForSeconds (1);

		}
						
	}


}
