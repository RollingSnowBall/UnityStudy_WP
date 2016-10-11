using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class JavaClass : Editor
{
	public static string gSpaceStrX1 = "    ";
	public static string gSpaceStrX2 = "    " + gSpaceStrX1;
	public static string gSpaceStrX3 = "    " + gSpaceStrX2;
	public static string gSpaceStrX4 = "    " + gSpaceStrX3;
	//	public static string gSpaceStrX5 = "    " + gSpaceStrX4;
	//	public static string gSpaceStrX6 = "    " + gSpaceStrX5;

	private string filePath;
	private bool   mInitSuccess = false;
	
	public JavaClass (string fPath)
	{
		filePath = fPath;
		if (!System.IO.File.Exists (filePath)) {
			Debug.LogError (filePath + "路径下文件不存在");
			return;
		}
		mInitSuccess = true;
//		Debug.Log ("Init Successed !!!");
	}
	
	// WriteBelow
	public void WriteBelow (string below, string text)
	{
		StreamReader streamReader = new StreamReader (filePath);
		string text_all = streamReader.ReadToEnd ();
		streamReader.Close ();
		
		int beginIndex = text_all.LastIndexOf (below);
		if (beginIndex == -1) {
			Debug.LogError (filePath + "中没有找到标致" + below);
			return; 
		}

		if (text_all.IndexOf (text) == -1) {
			int endIndex = beginIndex + below.Length;
			
			text_all = text_all.Substring (0, endIndex) + "\n" + text + "\n" + text_all.Substring (endIndex);
			
			StreamWriter streamWriter = new StreamWriter (filePath);
			streamWriter.Write (text_all);
			streamWriter.Close ();
		}
	}

	// WriteFront
	public void WriteFront (string front, string text)
	{
		StreamReader streamReader = new StreamReader (filePath);
		string text_all = streamReader.ReadToEnd ();
		streamReader.Close ();
		
		int beginIndex = text_all.IndexOf (front);
		if (beginIndex == -1) {
			Debug.LogError (filePath + "中没有找到标致" + front);
			return; 
		}
		
		if (text_all.IndexOf (text) == -1) {
		
			text_all = text_all.Substring (0, beginIndex) + "\n" + text + "\n\n" + text_all.Substring (beginIndex);
			
			StreamWriter streamWriter = new StreamWriter (filePath);
			streamWriter.Write (text_all);
			streamWriter.Close ();
		}
	}

	// Replace
	public bool Replace (string below, string newText)
	{
		bool bRet = false;
		StreamReader streamReader = new StreamReader (filePath);
		string text_all = streamReader.ReadToEnd ();
		streamReader.Close ();
		
		int beginIndex = text_all.IndexOf (below);

		StreamWriter streamWriter = null;
		if (beginIndex != -1) {
			text_all = text_all.Replace (below, newText);
			streamWriter = new StreamWriter (filePath);
			streamWriter.Write (text_all);
			streamWriter.Close ();
			bRet = true;
		}
		return bRet;
	}
}
