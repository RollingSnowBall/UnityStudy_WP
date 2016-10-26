using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Utils {

	public static void openMainMenue(){
		SceneManager.LoadScene(Config.szMainSceneName);
	}
}
