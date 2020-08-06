using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo : MonoBehaviour {

	public string skillName;

	public enum SkillType {Once, Continuous};
	public SkillType skillType;

	public int[] prefabIndex;
	public string[] gestureID;
	public int[] energyNeeded;
	public float[] damage;
	public Color[] trailColor;
	public int[] skillPoints;

	public bool locked = true;

}
