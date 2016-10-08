using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuChildFunctionButton : MonoBehaviour {

	public MenuButtonType type;

	public string szSceneName;

	public Text textFunctionName;

	public void OnButtonClick(){
		string szName = null == textFunctionName ? null : textFunctionName.text;
		MenuChoice.Instance.OnChildFunctionButtonClick (type, szSceneName, szName);
	}
}
