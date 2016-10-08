using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

	private static T instance;

	public static T Instance{
		get{
			if(null == instance){
				instance = (T)FindObjectOfType<T>();
				if(null == instance){
					Debug.LogError("There is not " + typeof(T) + "in scene");
				}
			}
			return instance;
		}
	}
}
