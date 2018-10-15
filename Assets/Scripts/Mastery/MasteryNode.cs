using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.Mastery
{
	public class MasteryNode : MonoBehaviour
	{

		public MasteryNode child;
		public bool isAcquired;
		public Button button;

		//データベースに保存していることが保証されているか
		public bool isDatabase;
		//取得に必要なスター
		public int necessaryStar;

		public bool upgradeable;
		ColorBlock cb;
		MasteryManager masteryManager;

		public string Description;

		// Use this for initialization
		void Start ()
		{
			masteryManager = GameObject.Find ("/Scripts/").GetComponent<MasteryManager> ();
			button = GetComponent<Button> ();
			cb = button.colors;
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (this.gameObject.tag != "FirstSkill") {
				if (child.isAcquired) {
					upgradeable = true;
				} else {
					upgradeable = false;
				}
			}


			if (isAcquired) {
				cb = button.colors;
				cb.normalColor = masteryManager.acquiredColor;
				button.colors = cb;
			} else if (upgradeable) {
				cb = button.colors;
				cb.normalColor = masteryManager.defaultColor;
				;
				button.colors = cb;
			} else {
				cb = button.colors;
				cb.normalColor = masteryManager.defaultColor;

				button.colors = cb;
			}
		}
	}
}