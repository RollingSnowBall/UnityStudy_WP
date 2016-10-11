using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class Yodo1Editor : Editor 
{
	[MenuItem("WpCoustomTool/Android/Export Project")]
	public static void ExportAndroidProject ()
	{
		AndroidPostProcess.ExportAndroidProject ();
	}

	[MenuItem("WpCoustomTool/Android/Sign Editor")]
	public static void SignEditor () 
	{
		string shell = Path.GetFullPath(Application.dataPath +"/Editor/Android/Editor/AndroidAPI/Build/print");
		EditorUtils.Command(shell);
	}
}
