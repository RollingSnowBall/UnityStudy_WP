using UnityEngine;
using System.Collections;

public class SkillCDWhile : SkillCDShowIconBase {

	private bool bIsInCD;

	public float fCD;
	private float fTimeCount;

	public UISprite _Sprite;

	public override void StartCD(){
		if(!bIsInCD && null != _Sprite){
			bIsInCD = true;
			StartCoroutine(Work());
			_Sprite.gameObject.SetActive(true);
		}
	}

	private IEnumerator Work(){
		while(bIsInCD && null != _Sprite){
			fTimeCount += Time.deltaTime;
			if(fTimeCount > fCD){
				fTimeCount = 0;
				bIsInCD = false;
				_Sprite.gameObject.SetActive(false);
			}else{
				_Sprite.fillAmount = 1 - fTimeCount / fCD; //倒计时UI更新
				yield return 0;
			}
		}
	}
}
