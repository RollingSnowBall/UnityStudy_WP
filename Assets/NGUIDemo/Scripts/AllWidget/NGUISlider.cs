using UnityEngine;
using System.Collections;

public class NGUISlider : NGUIWidgetShowScreen {

	public UISlider sliderAudio;

	public UIPlaySound sound;

	public void setVolume(){
		if(null != sliderAudio &&null != sound){
			sound.volume = sliderAudio.value;
		}
	}
}
