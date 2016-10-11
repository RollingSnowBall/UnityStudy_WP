using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Xml;

public class ManifestUtils : Editor {

	private static string TAG_MANIFEST = "manifest";
	private static string TAG_APPLICATION = "application";
	private static string TAG_SUPPORTS_SCREENS = "supports-screens";
	private static string TAG_USERS_SDK  = "uses-sdk";

	private static string TAG_USERS_PERMISSION = "uses-permission";
	private static string TAG_USERS_FEATURE = "uses-feature";

	public static void SetManifestAttribute(string filePath, string attributeName, string value){
		SetChildNode(filePath, TAG_MANIFEST, attributeName, value, null);
	}

	public static void SetApplicationAttribute(string filePath, string attributeName, string value) {
		SetChildNode(filePath, TAG_APPLICATION, attributeName, value, null);
	}

	public static void SetSupportsScreensAttribute(string filePath, string attributeName, string value) {
		SetChildNode(filePath, TAG_SUPPORTS_SCREENS, attributeName, value, null);
	}

	public static void SetUsersSdkAttribute(string filePath, string attributeName, string value) {
		SetChildNode(filePath, TAG_USERS_SDK, attributeName, value, null);
	}

	public static void SetUsersPermissionAttribute(string filePath, string attributeName, string newValue, string oldValue) {
		SetChildNode(filePath, TAG_USERS_PERMISSION, attributeName, newValue, oldValue);
	}

	public static void SetUsersFeatureAttribute(string filePath, string attributeName, string newValue, string oldValue) {
		SetChildNode(filePath, TAG_USERS_FEATURE, attributeName, newValue, oldValue);
	}

	private static void SetChildNode(string filePath, string tagName, string attributeName, string newValue, string oldValue) {
		XmlDocument doc = new XmlDocument();
		doc.Load(filePath);

		if (doc == null)
		{
			Debug.LogError("Couldn't load " + filePath);
			return;
		}

		XmlNode manNode = FindChildNode(doc, "manifest");
		string ns = manNode.GetNamespaceOfPrefix("android");

		if (manNode == null)
		{
			Debug.LogError("Error parsing " + filePath + ",tag for manifest not found.");
			return;
		}

		XmlNode node = null;

		if (TAG_APPLICATION.Equals(tagName) || 
			TAG_SUPPORTS_SCREENS.Equals(tagName) || 
			TAG_USERS_SDK.Equals(tagName))
		{
			node = FindChildNode(manNode, tagName);
		} 
		else if (TAG_MANIFEST.Equals(tagName)) 
		{
			node = manNode;
		} 
		else if (TAG_USERS_PERMISSION.Equals(tagName) || TAG_USERS_FEATURE.Equals(tagName)) 
		{
			node = FindChildNodeWithAttribute(manNode, tagName, attributeName, oldValue);
		}

		if (node == null)
		{
			Debug.LogError("Error parsing " + filePath + ",tag for " + tagName + " not found.");
			return;
		}

		XmlElement elem = (XmlElement)node;
		elem.SetAttribute (attributeName, ns, newValue);
		doc.Save (filePath);
	}



	private static XmlNode FindChildNode(XmlNode parent, string tagName)
	{
		XmlNode curr = parent.FirstChild;
		while (curr != null)
		{
			if (curr.Name.Equals(tagName))
			{
				return curr;
			}
			curr = curr.NextSibling;
		}
		return null;
	}

	private static XmlNode FindChildNodeWithAttribute(XmlNode parent, string tagName, string attribute, string value)
	{
		XmlNode curr = parent.FirstChild;
		while (curr != null)
		{
			if (curr.Name.Equals(tagName) && curr.Attributes[attribute].Value.Equals(value))
			{
				return curr;
			}
			curr = curr.NextSibling;
		}
		return null;
	}

	private static void SetChildNodeWithAttribute(string filePath, string tagName, string attributeName, string value) {
		XmlDocument doc = new XmlDocument();
		doc.Load(filePath);

		if (doc == null)
		{
			Debug.LogError("Couldn't load " + filePath);
			return;
		}

		XmlNode manNode = FindChildNode(doc, "manifest");
		string ns = manNode.GetNamespaceOfPrefix("android");

		if (manNode == null)
		{
			Debug.LogError("Error parsing " + filePath + ",tag for manifest not found.");
			return;
		}

		XmlNode node = FindChildNodeWithAttribute(manNode, tagName, attributeName, value);

		if (node == null)
		{
			Debug.LogError("Error parsing " + filePath + ",tag for " + tagName + " not found.");
			return;
		}

		XmlElement elem = (XmlElement)node;
		elem.SetAttribute (attributeName, ns, value);
		doc.Save (filePath);
	}
}
