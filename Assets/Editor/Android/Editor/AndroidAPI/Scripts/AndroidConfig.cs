using UnityEngine;
using System.Collections;
using UnityEditor;

public class AndroidConfig : Editor
{
	public static string PROJECT_ANDROID   	= Application.dataPath + "/../" + "Project/Android";
	public static string GRADLE_CONFIG 		= Application.dataPath + "/Editor/Android/GradleTemplate";
}
