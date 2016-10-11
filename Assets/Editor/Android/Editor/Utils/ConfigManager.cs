using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;

public class ConfigManager 
{
	public const string wechatAppKey 		= "WeChatAppKey";
	public const string qqAppKey 			= "QQAppKey";
	public const string sinaWeiboAppKey		= "SinaWeiboAppKey";
	public const string sinaWeiboSecrit		= "SinaWeiboSecrit";
	public const string sinaCallbackUrl 	= "SinaCallbackUrl";
	private static Dictionary<string, string> configDic;

	public static string getYodo1ConfigValueWithKey(string key)
	{
		if(configDic != null && configDic.ContainsKey(key))
		{
			return configDic [key].ToString ();
		}
		CsvStreamReader  csv = new CsvStreamReader(Application.dataPath + "/Yodo1SDK/Editor/Config/Yodo1Config.csv");
		configDic = new Dictionary<string, string> ();
		for (int i = 1; i < csv.RowCount + 1; i++) {
			configDic.Add (csv[i, 1], csv[i, 2]);
		}
		return configDic [key].ToString();
	}
}
