using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationTextureManager : MonoBehaviour {

	public Slider sliderSpeed;

	public Material materialTexture;

	public void SpeedChanged(){
		if(null != sliderSpeed){
			materialTexture.SetFloat("_Speed", sliderSpeed.value);
		}
	}

	public void JumpGit(){
		Application.OpenURL("https://github.com/RollingSnowBall/UnityStudy_WP/tree/Shader/develop/Assets/Shader/Animation");
	}

	public void GoToMainMenu(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("ChoiceFunctionMenu");
	}
}
