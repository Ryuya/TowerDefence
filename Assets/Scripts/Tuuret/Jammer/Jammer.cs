using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Units;
using Assets.Scripts.Common;
using Assets.Scripts.Tuuret;

namespace Assets.Scripts.Tuuret.Jammer
{
	public class Jammer : Turret
	{

		public GameObject Effect;
		public GameObject EffectsPosition;


		public int damage;
		public float stunTime;

		//public List<GameObject> stayEnemiesList;
		// Use this for initialization
		void Start ()
		{
			sphereCollider = GetComponent<SphereCollider> ();	
			radius = sphereCollider.radius;
		}
	
		// Update is called once per frame
		void Update ()
		{
			foreach (var enemy in stayEnemiesList.ToArray()) {
				if (enemy == null || enemy.GetComponent<Enemy> ().healthPoint <= 0) {
					stayEnemiesList.Remove (enemy);
					continue;
				}
			}
			if (reloadTime >= attackThreshold && stayEnemiesList.Count > 0) {
				Attack ();
			} else {
				reloadTime += Time.deltaTime;
			}
		}

		void OnTriggerStay (Collider col)
		{
			if (col.tag == "Enemy") {
				if (stayEnemiesList.Contains (col.gameObject) == false && col.gameObject.GetComponent<Enemy> ().healthPoint >= 1) {
					stayEnemiesList.Add (col.gameObject);
				}
			}
		}

		void OnTriggerExit (Collider col)
		{
			if (col.tag == "Enemy") {
				stayEnemiesList.Remove (col.gameObject);
			}
		}

		void Attack ()
		{
			reloadTime = 0f;
			var obj = Instantiate (Effect, this.transform.position, Quaternion.identity);
			obj.transform.SetParent (EffectsPosition.transform);
			obj.AddComponent<TimerDestroy> ();
			obj.GetComponent<TimerDestroy> ().time = 2.0f;
			var hitColliders = Physics.OverlapSphere (this.transform.position, radius * 3);
			foreach (var col2 in hitColliders) {
				if (col2.gameObject.CompareTag ("Enemy")) {
					col2.GetComponent<Enemy> ().TakeDamage (damage, this.gameObject);
					col2.GetComponent<Enemy> ().TakeStun (stunTime);
					var effect = Instantiate (GameManager.GetInstance ().teslaEffect, col2.transform.position, Quaternion.identity);
					effect.AddComponent<TimerDestroy> ();
					effect.GetComponent<TimerDestroy> ().time = stunTime;
					effect.transform.SetParent (col2.transform);
				}
			}

		}

		private void OnDrawGizmosSelected ()
		{
			Gizmos.color = Color.red;
			//Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
			Gizmos.DrawWireSphere (transform.position, (radius) * 3);
		}
	}
}