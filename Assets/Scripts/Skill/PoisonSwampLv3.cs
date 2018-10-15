using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Common;
using Assets.Scripts.Units;

namespace Assets.Scripts.Skill
{
	public class PoisonSwampLv3 : MonoBehaviour
	{

		public float damageThreshold = 1.0f;
		public float damageTime;

		int damage = 3;

		public int durationTime = 20;

		public List<Enemy> stayEnemiesList = new List<Enemy> ();
		// Use this for initialization

		void Awake ()
		{
			this.gameObject.transform.parent.gameObject.AddComponent<TimerDestroy> ();
			gameObject.transform.parent.gameObject.GetComponent<TimerDestroy> ().time = durationTime;
		}

		void Start ()
		{
		
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (damageThreshold < damageTime) {
				damageTime = 0;
				foreach (var enemy in stayEnemiesList.ToArray()) {
					enemy.TakeDamage (damage, this.gameObject);
					enemy.TakeSlow (1.0f);
					enemy.TakeDouble (1.0f);
				}
			} else {
				damageTime += Time.deltaTime;
			}
		}

		void OnTriggerStay (Collider col)
		{
			if (col.tag == "Enemy") {
				if (stayEnemiesList.Contains (col.gameObject.GetComponent<Enemy> ()) == false && col.gameObject.GetComponent<Enemy> ().healthPoint >= 1) {
					stayEnemiesList.Add (col.gameObject.GetComponent<Enemy> ());
				}
			}
		}

		void OnTriggerExit (Collider col)
		{
			if (col.tag == "Enemy") {
				stayEnemiesList.Remove (col.gameObject.GetComponent<Enemy> ());
			}
		}
	}

}