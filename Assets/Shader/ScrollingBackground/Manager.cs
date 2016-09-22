using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public Material mMaterial;
	public float fSpeedIncrement;//速度增量

	public Text textSpeedIncrement;

	void Awake(){
		if(null != textSpeedIncrement){
			textSpeedIncrement.text = fSpeedIncrement.ToString();
		}	
	}

	public void setSpeedIncrement(float fCount){
		fSpeedIncrement += fCount;
		textSpeedIncrement.text = fSpeedIncrement.ToString();
	}

	public void setBackgroundTexSpeed(float fSpeed){
		setMaterialFloat("_BackgroundTexSpeed", fSpeed);
	}
	public float getBackgroundTexSpeed(){
		return getMaterialFloat("_BackgroundTexSpeed");
	}

	public void setFrontTexSpeed(float fSpeed){
		setMaterialFloat("_NearTexSpeed", fSpeed);
	}
	public float getFrontTexSpeed(){
		return getMaterialFloat("_NearTexSpeed");
	}

	public void setBrightness(float fValue){
		setMaterialFloat("_Multiplier", fValue);
	}

	void setMaterialFloat(string key, float fValue){
		if(null != mMaterial){
			mMaterial.SetFloat(key, fValue);
		}else{
			Debug.Log("Manager : the mMaterial is null, get by : " + key);
		}
	}

	float getMaterialFloat(string key){
		if(null != mMaterial){
			return mMaterial.GetFloat(key);
		}else{
			Debug.Log("Manager : the mMaterial is null, set by : " + key);
		}
		return -1000;
	}

	public void BackgroundSpeedUp(){
		setBackgroundTexSpeed(getBackgroundTexSpeed() + fSpeedIncrement);
	}
	public void FrontSpeedUp(){
		setFrontTexSpeed(getFrontTexSpeed() + fSpeedIncrement);
	}

	public void SpeedUp(){
		BackgroundSpeedUp();
		FrontSpeedUp();
	}

	public void openCodeURL(){
		Application.OpenURL(Config.szCodeURL);
	}
}
