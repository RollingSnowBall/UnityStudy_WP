using UnityEngine;
using System.Collections;

public enum WidgetType{
	none,
	InputFiled,
	ScrollBar,
	ScrollView
}

public class NGUIAllWidgetManager : Singleton<NGUIAllWidgetManager> {

	public Transform transformWorkScreenRoot;
	public GameObject goMainMenu;

	public void ShowWorkScerrn(WidgetType type){
		int count = transformWorkScreenRoot.childCount;
		for(int i = 0; i < count; i++){
			Transform tfChild = transformWorkScreenRoot.GetChild(i);
			if(null != tfChild){
				NGUIWidgetShowScreen screen = tfChild.gameObject.GetComponent<NGUIWidgetShowScreen>();
				if(null != screen){
					if(screen.type.Equals(type)){
						screen.gameObject.SetActive(true);
						if(null != goMainMenu){
							goMainMenu.SetActive(false);
						}
					}else{
						screen.gameObject.SetActive(false);
					}
				}
			}
		}
	}

	public void ShowMainMenu(){
		if(null != goMainMenu){
			goMainMenu.SetActive(true);
		}
		if(null != transformWorkScreenRoot){
			int count = transformWorkScreenRoot.childCount;
			for(int i = 0; i < count; i++){
				Transform tfChild = transformWorkScreenRoot.GetChild(i);
				if(null != tfChild){
					tfChild.gameObject.SetActive(false);
				}
			}
		}
	}
}
