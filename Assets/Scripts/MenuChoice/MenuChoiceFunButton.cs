using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuChoiceFunButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public MenuButtonType type;

	public void OnPointerEnter(PointerEventData data){
		Debug.Log ("MenuChoiceFunButton : " + data);
		if (null != MenuChoice.Instance) {
			MenuChoice.Instance.bIsRotate = false;
			MenuChoice.Instance.OnChildFunctionButtonShow(type);
			MenuChoice.Instance.bIsShowChildFunctionButton = false;
		}
	}

	public void OnPointerExit(PointerEventData data){
		Debug.Log ("MenuChoiceFunButton : " + data);
		if (null != MenuChoice.Instance && !MenuChoice.Instance.bIsShowChildFunctionButton) {
			MenuChoice.Instance.startRotate ();
			MenuChoice.Instance.OnChildFunctionButtonShow(MenuButtonType.none);
		}
	}

	public void OnMenuButtonClick(){
		if (null != MenuChoice.Instance) {
			MenuChoice.Instance.OnMenuButtonClick(type);
		}
	}
}
