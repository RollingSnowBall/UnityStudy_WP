using UnityEngine;
using System.Collections;

public class SkillCDManager : MonoBehaviour {

	public Transform transformSkillButtonRoot;

	public void PushSkillsInCD(){
		if(null != transformSkillButtonRoot){
			for(int i = 0; i < transformSkillButtonRoot.childCount; i++){
				Transform tf= transformSkillButtonRoot.GetChild(i);
				if(null != tf){
					SkillCDShowIconBase skill = tf.gameObject.GetComponent<SkillCDShowIconBase>();
					if(null != skill){
						skill.StartCD();
					}
				}
			}
		}
	}

	public void ShowMainMenu(){
		NGUIDemoUIManager.Instance.ShowMainMenu(true);
	}
}
