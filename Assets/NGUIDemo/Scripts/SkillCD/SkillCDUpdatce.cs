using UnityEngine;
using System.Collections;

public class SkillCDUpdatce : SkillCDShowIconBase {

	private bool bIsInCD;

	public float fCD;
	private float fTimeCount;

	public UISprite _Sprite;

	void Update(){
		if(bIsInCD && null != _Sprite){
			fTimeCount += Time.deltaTime;
			if(fTimeCount > fCD){
				fTimeCount = 0;
				bIsInCD = false;
				_Sprite.gameObject.SetActive(false);
			}else{
				_Sprite.fillAmount = 1 - fTimeCount / fCD;
			}
		}
	}

	public override void StartCD(){
		if(!bIsInCD && null != _Sprite){//只有技能在可以使用时才会进入CD
			this.bIsInCD = true;
			_Sprite.gameObject.SetActive(true);
		}
	}
}
