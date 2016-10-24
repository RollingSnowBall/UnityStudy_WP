using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using I2.Loc;
using UnityEngine.SceneManagement;

public class ClothManager : Singleton<ClothManager> {

	public Cloth cloth;
	public Text textStart;

	public Slider sliderDamping;

	private bool bIsStart;//开始或者重新开始

	public void start(){
		if(!bIsStart){
			if(null != cloth){
				cloth.useGravity = true;
			}
			if(null != textStart){
				textStart.text = ScriptLocalization.Get("startRestart");
			}
		}else{
			SceneManager.LoadScene("FunctionTest_Cloth_Scene");
		}
		bIsStart = !bIsStart;
	}

	public void changeDamping(){
		if(null != sliderDamping && null != cloth){
			cloth.damping = sliderDamping.value;
		}
	}
}
