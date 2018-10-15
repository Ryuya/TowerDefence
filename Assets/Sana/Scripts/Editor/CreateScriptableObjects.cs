using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.SANA.Scripts.Master;

/// <summary>
/// ScriptableObject生成（エディタ）
/// author:SANA
/// date:2017/11/18
/// </summary>
public class CreateScriptableObjects : ScriptableObject
{
	[MenuItem ("Assets/Create/Data/UnitMasterData")]
	static void CreateItemMasterData ()
	{
		CreateObject<UnitMasterData> ();
	}

	[MenuItem ("Assets/Create/Data/StageMasterData")]
	static void CreateStageMasterData ()
	{
		CreateObject<StageMasterData> ();
	}

	public static void CreateObject<T> ()
	{
		ScriptableObject newObj = CreateInstance (typeof(T).Name);
		string dirPath = "Assets/Resources/ScriptableObjects/" + typeof(T).Name;
		CreateDirectorys (dirPath);
		AssetDatabase.CreateAsset (newObj, dirPath + "/newObj.asset");
		Debug.Log (dirPath + "/newObj.assetを生成");
		AssetDatabase.Refresh ();
	}

	public static void CreateDirectorys (string path, char delim = '/')
	{
		string[] pathArr = path.Split (delim);
		if (pathArr [0] != "Assets") {
			Debug.LogError ("不適切なパス:" + path);
			return;
		}
		StringBuilder sb = new StringBuilder ("Assets");
		for (int i = 1; i < pathArr.Length; i++) {
			sb.Append (delim);
			sb.Append (pathArr [i]);
			if (!Directory.Exists (sb.ToString ())) {
				Directory.CreateDirectory (sb.ToString ());
				Debug.Log (sb.ToString () + "を生成");
			}
		}
	}
}
