using UnityEngine;
using System.Collections;

public class RightMainMenu : MonoBehaviour {

	public Transform transformFunctionButton;

	public Transform transformHead;

	private Vector3 vector3FunctionButtonHide;

	private bool bIsFunctionButtonShow;

	public void OnHeadButtonClick(){
		if (null != transformFunctionButton && null != transformHead && !bIsFunctionButtonShow) {
			bIsFunctionButtonShow = true;
			vector3FunctionButtonHide = transformFunctionButton.position;
			transformFunctionButton.position = new Vector3 (transformHead.position.x, transformFunctionButton.position.y, transformFunctionButton.position.z);
		} else if (null != transformFunctionButton && null != transformHead) {
			bIsFunctionButtonShow = false;
			transformFunctionButton.position = vector3FunctionButtonHide;
		}
	}
}
