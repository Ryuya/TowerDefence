using UnityEngine;
using System;
using System.Collections;
using System.IO;

abstract public class SavableSingleton<T> where T : SavableSingleton<T>, new()
{
	private static T instance;

	public static T Instance {
		get {
			if (null == instance) {
				LitJson.JsonMapper.RegisterExporter<float> ((obj, writer) => writer.Write (Convert.ToDouble (obj)));
				LitJson.JsonMapper.RegisterExporter<decimal> ((obj, writer) => writer.Write (Convert.ToString (obj)));
				LitJson.JsonMapper.RegisterImporter<double, float> (input => Convert.ToSingle (input));
				LitJson.JsonMapper.RegisterImporter<int, long> (input => Convert.ToInt64 (input));
				LitJson.JsonMapper.RegisterImporter<string, decimal> (input => Convert.ToDecimal (input));
				var json = PlayerPrefs.GetString (GetSaveKey ());
				if (json.Length > 0) {
					instance = LitJson.JsonMapper.ToObject<T> (json);
				} else {
					instance = new T ();
				}
			}
			return instance;
		}

	}

	public void Save ()
	{
		PlayerPrefs.SetString (GetSaveKey (), LitJson.JsonMapper.ToJson (this));
		PlayerPrefs.Save ();
	}

	public void Reset ()
	{
		PlayerPrefs.DeleteKey (GetSaveKey ());
		instance = null;
	}

	private static string GetSaveKey ()
	{
		return typeof(T).FullName;
	}
}