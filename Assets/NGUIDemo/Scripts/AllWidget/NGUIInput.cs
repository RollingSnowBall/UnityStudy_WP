using UnityEngine;
using System.Collections;

public class NGUIInput : NGUIWidgetShowScreen {

	public UIInput[] vectorInput;
	public UILabel[] vectorLabel;

	public void OnClickToSubmit(){
		for(int i = 0; i < vectorInput.Length; i++){
			if(vectorLabel.Length > i){
				vectorLabel[i].text = vectorInput[i].value;
			}
		}
	}
}
