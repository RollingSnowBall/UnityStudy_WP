using UnityEngine;
using System.Collections;

public enum NGUIDemoType{NONE, SKILLCD }

public class NGUIDemoUIManager : Singleton<NGUIDemoUIManager> {

	public Transform transformFunctionRoot;
	public GameObject goMainMenu;

	public void ShowFunction(NGUIDemoType type){
		int count = transformFunctionRoot.childCount;
		for(int i = 0; i < count; i++){
			Transform tfChild = transformFunctionRoot.GetChild(i);
			if(null != tfChild){
				NGUIDemoChild child = tfChild.gameObject.GetComponent<NGUIDemoChild>();
				if(null != child){
					if(child.type.Equals(type)){
						tfChild.gameObject.SetActive(true);//显示子功能页面
						if(null != goMainMenu){
							ShowMainMenu(false);
						}
					}else{
						tfChild.gameObject.SetActive(false);
					}
				}
			}
		}
	}

	public void ShowMainMenu(bool bIsShow){
		if(null != goMainMenu){
			TweenAlpha tween = goMainMenu.GetComponent<TweenAlpha>();
			if(null != tween){
				if(!bIsShow){
					tween.PlayReverse();
					StartCoroutine(NGUITils.HideUiWithAlphaTween(goMainMenu.gameObject));
				}else{
					goMainMenu.gameObject.SetActive(true);
					tween.Play();
					int count = transformFunctionRoot.childCount;
					for(int i = 0; i < count; i++){
						transformFunctionRoot.GetChild(i).gameObject.SetActive(false);
					}
				}
			}
		}
	}

	public void GoBackToMainMenu(){
		Utils.openMainMenue();
	}
}
