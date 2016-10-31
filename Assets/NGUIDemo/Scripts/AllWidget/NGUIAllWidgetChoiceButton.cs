using UnityEngine;
using System.Collections;

public class NGUIAllWidgetChoiceButton : MonoBehaviour {

	public WidgetType type;

	public void OnChoiceButtonClick(){
		NGUIAllWidgetManager.Instance.ShowWorkScerrn(this.type);
	}
}
