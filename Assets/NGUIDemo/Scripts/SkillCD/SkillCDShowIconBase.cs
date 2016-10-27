using UnityEngine;
using System.Collections;

public abstract class SkillCDShowIconBase : MonoBehaviour {

	public void Show(){
		this.StartCD();	
	}

	public abstract void StartCD();
}
