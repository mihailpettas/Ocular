using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ROT : EditorWindow{
	
	float input1, output1, input2, output2;

	// Add menu named "My Window" to the Window menu
	[MenuItem("Window/Rule Of Three")]

	static void Init(){
		// Get existing open window or if none, make a new one:
		ROT window = (ROT)EditorWindow.GetWindow(typeof(ROT));
		window.Show();
	}

	void OnGUI(){
		
		GUILayout.Label("Rule Of Three", EditorStyles.boldLabel);

		GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});

		try{
			input1 = float.Parse(EditorGUILayout.TextField("Input 1:", input1.ToString()));
		}catch{
			Debug.Log ("Only Numbers Allowed");
		}

		try{
			output1 = float.Parse(EditorGUILayout.TextField("Output 1:", output1.ToString()));
		}catch{
			Debug.Log ("Only Numbers Allowed");
		}

		try{
			input2 = float.Parse(EditorGUILayout.TextField("Input 2:", input2.ToString()));
		}catch{
			Debug.Log ("Only Numbers Allowed");
		}

		GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});

		if (GUILayout.Button ("Calculate")) {
			output2 = (input2 * output1) / input1;
		}

		EditorGUILayout.LabelField ("Result: " + output2.ToString ());

	}

}
