using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class SkillsRibbon : MonoBehaviour {

	public int speed;
	int turnGoal;

	bool open;
	RectTransform rect;

	//SkillExecutor se;


	void Start(){
		rect = GetComponent<RectTransform> ();
		//se = GameObject.Find ("SkillsRecognizer").GetComponent<SkillExecutor> ();
	}


	public void openClose(){

		if (open) {
			Timing.KillCoroutines ("open");
			Timing.RunCoroutine (_close(), "close");
			open = false;
		} else {
			Timing.KillCoroutines ("close");
			Timing.RunCoroutine (_open(), "open");
			open = true;
		}

	}


	IEnumerator<float> _open(){

		while(rect.anchoredPosition.x != 200){
			//se.ribbonRuns = true;
			rect.anchoredPosition = new Vector2 (Mathf.MoveTowards(rect.anchoredPosition.x, 200, Timing.DeltaTime*speed), rect.anchoredPosition.y);
			yield return 0f;
		}

		//se.ribbonRuns = false;

	}


	IEnumerator<float> _close(){

		while(rect.anchoredPosition.x != 1440){
			//se.ribbonRuns = true;
			rect.anchoredPosition = new Vector2 (Mathf.MoveTowards(rect.anchoredPosition.x, 1440, Timing.DeltaTime*speed), rect.anchoredPosition.y);
			yield return 0f;
		}

		//se.ribbonRuns = false;

	}


	public void turn(int turnGoalX){

		turnGoal = turnGoalX;
		open = true;
		//se.ribbonRuns = true;
		Timing.RunCoroutine (_turn(), "turn");

	}


	IEnumerator<float> _turn(){

		while(rect.anchoredPosition.x != turnGoal){
			rect.anchoredPosition = new Vector2 (Mathf.MoveTowards(rect.anchoredPosition.x, turnGoal, Timing.DeltaTime*speed), rect.anchoredPosition.y);
			yield return 0f;
		}

		//se.ribbonRuns = false;

	}

}
