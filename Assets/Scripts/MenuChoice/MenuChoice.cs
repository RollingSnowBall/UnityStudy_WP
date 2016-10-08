using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using I2.Loc;
using UnityEngine.SceneManagement;

public enum MenuButtonType{none, shader, game, other}

public class MenuChoice : Singleton<MenuChoice> { 

	public Transform[] listFunction;
	public MenuChildFunctionButton[] listChildFuctionButton;

	public Transform transformPivot;

	public float fRadius;

	public float fTime;

	public bool bIsRotate;

	public Animator animatorWarningWindow;
	public Text textWarningWindow;

	[HideInInspector]public float fPos;
	[HideInInspector]public bool bIsShowChildFunctionButton;

	public float fSpeed;

	void Update(){
		if (bIsRotate) {
			fTime += Time.deltaTime * fSpeed;
			for (int i = 0; i < listFunction.Length; i++) {
				fPos = Mathf.Deg2Rad * (360 / listFunction.Length * i + fTime);
				listFunction[i].localPosition = new Vector3(transformPivot.localPosition.x + Mathf.Cos(fPos) * fRadius,
				                                            transformPivot.localPosition.y + Mathf.Sin(fPos) * fRadius, 
				                                            0);
			}
		}
	}

	public void startRotate(){
		bIsRotate = true;
		showWarningWindow (false);
	}

	void showWarningWindow(bool bIsShow){
//		animatorWarningWindow.gameObject.SetActive (bIsShow);
		if (bIsShow) {
			animatorWarningWindow.SetTrigger ("goDown");
		} else {
			animatorWarningWindow.SetTrigger ("goUp");
		}
	}

	public void OnMenuButtonClick(MenuButtonType type){
		bIsShowChildFunctionButton = true;
	}

	public void OnChildFunctionButtonShow(MenuButtonType type){
		if (null != listChildFuctionButton) {
			foreach(var childBt in listChildFuctionButton){
				if(childBt.type.Equals(type)){
					childBt.gameObject.SetActive(true);
				}else{
					childBt.gameObject.SetActive(false);
				}
			}
		}
	}

	/// <summary>
	/// 子功能选择按钮点击事件
	/// </summary>
	/// <param name="type">备用-当前子功能按钮都为打开场景因此type无用.</param>
	/// <param name="szData">Size data.</param>
	public void OnChildFunctionButtonClick(MenuButtonType type, string szData, string szFunctionName = ""){
		if (!string.IsNullOrEmpty (szData)) {
			SceneManager.LoadScene (szData);
		} else {
			showWarningWindow (true);
			textWarningWindow.text = string.Format (ScriptLocalization.Get ("warningProgramNotFinish"), szFunctionName);
		}
	}
}
