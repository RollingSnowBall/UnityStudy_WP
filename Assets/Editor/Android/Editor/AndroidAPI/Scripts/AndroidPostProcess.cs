using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AndroidPostProcess {

	public static void ExportAndroidProject ()
	{
		string project_path = AndroidConfig.PROJECT_ANDROID + "/" + PlayerSettings.productName;
		BuildPipeline.BuildPlayer (EditorUtils.GetBuildScenes ().ToArray (), project_path, BuildTarget.Android, EditorUtils.GetBuildOptions(DevicePlatform.Android));

		BuildStudioStructure (project_path);
		ModifyJavaFile (project_path);
		ModifyGradleFile(project_path);
	}
		
	private static void BuildStudioStructure (string projectPath) {
		string fromFolder = projectPath + "/" + PlayerSettings.productName;
		string toFolder = projectPath + "/app";
		if (Directory.Exists (toFolder)) {
			EditorFileUtils.copyFolder (fromFolder, toFolder);
			EditorFileUtils.DeleteDir (fromFolder);
		} else {
			Directory.Move(fromFolder, toFolder); 
		}

		EditorFileUtils.copyFile (AndroidConfig.GRADLE_CONFIG + "/settings.gradle", projectPath + "/settings.gradle");
		EditorFileUtils.copyFile (AndroidConfig.GRADLE_CONFIG + "/build.gradle", projectPath + "/build.gradle");
		EditorFileUtils.copyFile (AndroidConfig.GRADLE_CONFIG + "/gradle.properties", projectPath + "/gradle.properties");
		EditorFileUtils.copyFile (AndroidConfig.GRADLE_CONFIG + "/wp.jks", projectPath + "/wp.jks");
		EditorFileUtils.copyFile (AndroidConfig.GRADLE_CONFIG + "/app/build.gradle", projectPath + "/app/build.gradle");


		/*将eclipse工程结构转成studio工程结构------start*/
		string mainFolder = projectPath + "/app/src/main";
		if (!Directory.Exists (mainFolder)) {
			Directory.CreateDirectory (mainFolder);
		}
		EditorFileUtils.moveFile(projectPath + "/app/AndroidManifest.xml",projectPath + "/app/src/main/AndroidManifest.xml");
		EditorFileUtils.moveFolder(projectPath + "/app/assets",projectPath + "/app/src/main/assets");
		EditorFileUtils.moveFolder(projectPath + "/app/res",projectPath + "/app/src/main/res");
		string javaFolder = projectPath + "/app/src/main/java";
		if (!Directory.Exists (javaFolder)) {
			Directory.CreateDirectory (javaFolder);
		}
		EditorFileUtils.moveFolder(projectPath + "/app/src/com",projectPath + "/app/src/main/java/com");
		/*将eclipse工程结构转成studio工程结构------end*/
	}

	private static void ModifyGradleFile (string projectPath) {
		GradleUtils.ModifySettinsGradleFile(projectPath);
		GradleUtils.AddDependencies(projectPath);
		GradleUtils.SetApplicationId(projectPath);
		GradleUtils.SetVersionName(projectPath);
	}

	private static void MatchGamesCommonConfig() {
		string file = Application.dataPath + "/Yodo1SDK/Editor/AndroidAPI/Build/yodo1config/c_common/yodo1_games_common_config.properties";
		EditorFileUtils.SetValueForKey(file,"thisProjectPackageName",PlayerSettings.bundleIdentifier);
		EditorFileUtils.SetValueForKey(file,"mainClassName",PlayerSettings.bundleIdentifier + ".UnityPlayerActivity");
	}

	private static void UpdateAppName(string projectPath) {
		string file = Application.dataPath + "/Yodo1SDK/Editor/AndroidAPI/Build/yodo1config/c_common/yodo1_games_common_config.properties";
		string appName = EditorFileUtils.GetValueForKey(file, "game_config_name");
		if (!string.IsNullOrEmpty(appName)) {
			string file2 = projectPath+"/app/src/main/res/values/strings.xml";
			string oldStr = string.Format("<string name=\"app_name\">{0}</string>", PlayerSettings.productName);
			string newStr = string.Format("<string name=\"app_name\">{0}</string>", appName);
			EditorFileUtils.Replace(file2, oldStr, newStr);
		}
	}

#region Editor Java File 
	private static void ModifyJavaFile (string projectPath)
	{
		string javaFilePath = projectPath + "/app/src/main/java/" + PlayerSettings.bundleIdentifier.Replace (".", "/") + "/UnityPlayerActivity.java";

		JavaClass jc = new JavaClass (javaFilePath);
		WriteImport (jc);
		WriteOnCreate (jc);
		WriteOnStart (jc);
		WriteOnStop (jc);
		WriteOnDestroy (jc);
		WriteOnPause (jc);
		WriteOnResume (jc);
		WriteOnActivityResult (jc);
		WriteOnRequestPermissionsResult(jc);
		WriteOnNewIntent(jc);
//		WriteOnBackPressed (jc);
	}

	// insert Import Source
	private static void WriteImport (JavaClass jc)
	{
		string insertCode = "\nimport android.content.Intent;\n";

		//在指定代码后面增加一行代码
		jc.WriteBelow ("import android.view.WindowManager;", insertCode);
	}

	// insert onCreate Sourece
	private static void WriteOnCreate (JavaClass jc)
	{
	}

	// WriteOnStart
	private static void WriteOnStart (JavaClass jc)
	{
		string insertSource = 
			JavaClass.gSpaceStrX1 + "@Override\n" + 
			JavaClass.gSpaceStrX1 + "protected void onStart() {\n" + 
			JavaClass.gSpaceStrX2 + "super.onStart();\n" + 
			JavaClass.gSpaceStrX1 + "}";

		jc.WriteFront ("	// This ensures the layout will be correct.", insertSource);
	}

	// onStop
	private static void WriteOnStop (JavaClass jc)
	{
		string insertSource = 
			JavaClass.gSpaceStrX1 + "@Override\n" + 
			JavaClass.gSpaceStrX1 + "protected void onStop() {\n" + 
			JavaClass.gSpaceStrX2 + "super.onStop();\n" + 
			JavaClass.gSpaceStrX1 + "}";
		jc.WriteFront ("	// This ensures the layout will be correct.", insertSource);
	}

	// onDestroy
	private static void WriteOnDestroy (JavaClass jc)
	{
		jc.WriteBelow ("super.onDestroy();", JavaClass.gSpaceStrX2 + "//Text End");
	}

	// onPause
	private static void WriteOnPause (JavaClass jc)
	{
		jc.WriteBelow ("super.onPause();", JavaClass.gSpaceStrX2 + "//Text End");
	}

	// onResume
	private static void WriteOnResume (JavaClass jc)
	{
		jc.WriteBelow ("super.onResume();", JavaClass.gSpaceStrX2 + "//Text End");
	}

	// onActivityResult
	private static void WriteOnActivityResult (JavaClass jc)
	{
		string replaceStr = 
			JavaClass.gSpaceStrX1 + "@Override\n" +
			JavaClass.gSpaceStrX1 + "protected void onActivityResult(int requestCode, int resultCode, Intent data) { \n" +
			JavaClass.gSpaceStrX2 + "super.onActivityResult(requestCode, resultCode, data);\n" +
			JavaClass.gSpaceStrX1 + "}";

		string objectStr = 
			JavaClass.gSpaceStrX1 + "@Override\n" +
			JavaClass.gSpaceStrX1 + "protected void onActivityResult(int requestCode, int resultCode, Intent data) { \n" +
			JavaClass.gSpaceStrX2 + "super.onActivityResult(requestCode, resultCode, data);\n" +
			JavaClass.gSpaceStrX1 + "}";
		if (!jc.Replace (objectStr, replaceStr)) {
			jc.WriteFront ("	// This ensures the layout will be correct.", replaceStr);
		}
	}

	private static void WriteOnBackPressed (JavaClass jc)
	{
		string replaceStr = 
			JavaClass.gSpaceStrX1 + "@Override\n" +
			JavaClass.gSpaceStrX1 + "public void onBackPressed() { \n" +
			JavaClass.gSpaceStrX3 + "return;\n" +
			JavaClass.gSpaceStrX2 + "super.onBackPressed();\n" +
			JavaClass.gSpaceStrX1 + "}";

		string objectStr = 
			JavaClass.gSpaceStrX1 + "@Override\n" +
			JavaClass.gSpaceStrX1 + "public void onBackPressed() { \n" +
			JavaClass.gSpaceStrX2 + "uper.onBackPressed();\n" +
			JavaClass.gSpaceStrX1 + "}";
		if (!jc.Replace (objectStr, replaceStr)) {
			jc.WriteFront ("	// This ensures the layout will be correct.", replaceStr);
		}
	}

	private static void WriteOnRequestPermissionsResult(JavaClass jc){
		string replaceStr = 
			JavaClass.gSpaceStrX1 + "//for android 6.0\n" +
			JavaClass.gSpaceStrX1 + "@Override\n" +
			JavaClass.gSpaceStrX1 + "public void onRequestPermissionsResult(int requestCode, String permissions[], int[] grantResults) { \n" +
			JavaClass.gSpaceStrX2 + "super.onRequestPermissionsResult(requestCode, permissions, grantResults); \n" + 
			JavaClass.gSpaceStrX1 + "}";

		string objectStr = 
			JavaClass.gSpaceStrX1 + "@Override\n" +
			JavaClass.gSpaceStrX1 + "public void onBackPressed() { \n" +
			JavaClass.gSpaceStrX2 + "uper.onBackPressed();\n" +
			JavaClass.gSpaceStrX1 + "}";
		if (!jc.Replace (objectStr, replaceStr)) {
			jc.WriteFront ("	// This ensures the layout will be correct.", replaceStr);
		}
		/*//for android 6.0
		public void onRequestPermissionsResult(int requestCode, String permissions[], int[] grantResults) {
			UnityYodo1SDK.onRequestPermissionsResult(requestCode, permissions, grantResults);
		}*/
	}

	private static void WriteOnNewIntent(JavaClass jc) {
		string replaceStr = 
			JavaClass.gSpaceStrX1 + "@Override\n" +
			JavaClass.gSpaceStrX1 + "protected void onNewIntent(Intent intent) { \n" +
			JavaClass.gSpaceStrX2 + "super.onNewIntent(intent); \n" +
			JavaClass.gSpaceStrX1 + "}";

		string objectStr = 
			JavaClass.gSpaceStrX1 + "@Override\n" +
			JavaClass.gSpaceStrX1 + "public void onBackPressed() { \n" +
			JavaClass.gSpaceStrX2 + "uper.onBackPressed();\n" +
			JavaClass.gSpaceStrX1 + "}";
		if (!jc.Replace (objectStr, replaceStr)) {
			jc.WriteFront ("	// This ensures the layout will be correct.", replaceStr);
		}
	}
#endregion

}
