using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class GradleUtils : Editor {
	private static List<string> _pluginsList = new List<string> ();

	//修改gradle中applicationId
	public static void SetApplicationId (string projectPath) {
		string gradlePath = projectPath + "/app/build.gradle";
		string value =  "\'" + PlayerSettings.bundleIdentifier + "\'";
		UpdateAppGradle (gradlePath, "applicationId", value);
	}

	//修改gradle中versionName
	public static void SetVersionName (string projectPath) {
		string gradlePath = projectPath + "/app/build.gradle";
		string value =  "\"" + PlayerSettings.bundleVersion + "\"";
		UpdateAppGradle (gradlePath, "versionName", value);
	}

	public static void SetVersionCode () {

	}

	private static void UpdateAppGradle (string gradlePath, string key, string value) {
		StreamReader streamReader = new StreamReader(gradlePath);
		string text_all = streamReader.ReadToEnd();
		streamReader.Close();
		//text_all = UpdateCode (text_all, "versionName", "\"" + PlayerSettings.bundleVersion + "\"");

		text_all = UpdateCode (text_all, key, value);

		if(text_all != null){
			StreamWriter streamWriter = new StreamWriter(gradlePath);
			streamWriter.Write(text_all);
			streamWriter.Close();
		}
	}

	private static string UpdateCode(string buildText, string key, string value){
		//		string szVersionCodeFlag = "versionCode ";

		if(buildText != null && buildText.Contains(key))
		{
			string newString = key + " " + value;

			//计算开始位置
			int StartCount = buildText.IndexOf(key);
			//计算结束位置
			int EndCount = buildText.Substring(StartCount).IndexOf('\n');
			//检出 VersionCode 字符串 例如：“versionCode 1”
			string oldString = buildText.Substring(StartCount, EndCount);
			return buildText.Replace (oldString, newString);

		}
		return null;
	}

	private static void SetPluginList(string projectPath) {
		if (_pluginsList != null) {
			_pluginsList.Clear();
		}
		string projectPropertiesFile = projectPath + "/app/project.properties";
		if (System.IO.File.Exists (projectPropertiesFile)) {
			StreamReader streamReader = new StreamReader (projectPropertiesFile);
			string text_all = streamReader.ReadToEnd ();
			streamReader.Close ();

			MatchCollection match = Regex.Matches (text_all, "../(\\S*)");  

			foreach (Match m in match)
			{
				_pluginsList.Add (m.Groups[1].Value);
			}
		} else {
			Debug.LogError (string.Format("文件不存在 ：{0}", projectPropertiesFile));
		}
	}

	public static void AddDependencies(string projectPath){
		/* /app/build.gradle
		* 
		* dependencies {
			*     compile fileTree(dir: 'libs', include: '*.jar')
			* } 
		* 
		*/
		SetPluginList(projectPath);

		string fileName = projectPath + "/app/build.gradle";
		if (System.IO.File.Exists (fileName)) {
			StreamReader streamReader = new StreamReader (fileName);
			string text_all = streamReader.ReadToEnd ();
			streamReader.Close ();
			//[\w\W]*?
			Match mstr = Regex.Match(text_all, @"dependencies {([\s\S]*?)}");  
			string objectStr = mstr.Groups[1].Value.ToString(); 
			if (!objectStr.Equals ("")) {
				string newStr = objectStr;
				bool needReplace = false;
				for (int i = 0; i < _pluginsList.Count; ++i)
				{
					string item = _pluginsList [i];
					if (objectStr.IndexOf(item) == -1){
						if (!needReplace) {
							needReplace = true;
						}
						newStr += string.Format ("    compile project(':{0}')\n", item);	
					}
				}

				if (needReplace){
					text_all = text_all.Replace (objectStr, newStr);
					StreamWriter streamWriter = new StreamWriter (fileName);
					streamWriter.Write (text_all);
					streamWriter.Close ();
					Debug.Log (string.Format("{0} modify successed !", fileName));
				}else {
					Debug.Log (string.Format("{0} isn't need modify", fileName));
				}
			} else {
				Debug.LogError (string.Format("ModifyAppBuildGradleFile Match Error 请检查{0}文件dependencies配置", fileName));
			}

			Debug.Log (string.Format("ModifyAppBuildGradleFile match : ", objectStr));

		} else {
			Debug.LogError (string.Format("ModifyAppBuildGradleFile 文件不存在 ：{0}", fileName));
		}
	}

	public static void ModifySettinsGradleFile(string projectPath)
	{
		/**
		 * 
		 * /settings.gradle
		* 
		* include ':app'
		* include ':Extra'
			* */

		SetPluginList(projectPath);
		
		string fileName = projectPath + "/settings.gradle";
		if (System.IO.File.Exists (fileName)) {
			StreamReader streamReader = new StreamReader (fileName);
			string text_all = streamReader.ReadToEnd ();
			streamReader.Close ();

			string newStr = text_all;
			bool needReplace = false;
			for (int i = 0; i < _pluginsList.Count; ++i)
			{
				string item = _pluginsList [i];
				if (text_all.IndexOf(item) == -1){
					if (!needReplace) {
						needReplace = true;
					}
					text_all += string.Format ("include ':{0}'\n", item);	
				}
			}
			if (needReplace) {
				StreamWriter streamWriter = new StreamWriter (fileName);
				streamWriter.Write (text_all);
				streamWriter.Close ();
				Debug.Log (string.Format("{0} modify successed !", fileName));
			} else {
				Debug.Log (string.Format("{0} isn't need modify", fileName));
			}
		} else {
			Debug.LogError (string.Format("ModifySettinsGradleFile 文件不存在 ：{0}", fileName));
		}
	}
}
