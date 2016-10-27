using UnityEngine;
using System.Collections;

public class NGUIDemoFunctionChoiceButton : MonoBehaviour {

	public NGUIDemoType type;

	public void OnShowButtonClick(){
		NGUIDemoUIManager.Instance.ShowFunction(this.type);
	}
}
