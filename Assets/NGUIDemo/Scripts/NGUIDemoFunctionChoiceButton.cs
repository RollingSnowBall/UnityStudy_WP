using UnityEngine;
using System.Collections;

public class NGUIDemoFunctionChoiceButton : MonoBehaviour {

	public NGUIDemoType type;

	public void OnClick(){
		NGUIDemoUIManager.Instance.ShowFunction(this.type);
	}
}
