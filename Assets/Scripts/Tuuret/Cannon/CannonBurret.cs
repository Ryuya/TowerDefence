using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Tuuret;
using Assets.Scripts.Units;

namespace Assets.Scripts.Tuuret.Cannon
{
	public class CannonBurret : MonoBehaviour
	{
		public Transform target;
		public float speed = 0.08f;
		// Use this for initialization
		void Start ()
		{
		
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (target != null) {
				this.transform.position = Vector3.Lerp (this.transform.position, target.transform.position, 0.2f);
			} else {
				Destroy (this.gameObject);
			}
		}

		public void OnTriggerEnter (Collider col)
		{
			if (col.tag == "Enemy") {
				if (target.gameObject == col.gameObject) {
					col.gameObject.GetComponent<Enemy> ().TakeDamage (this.gameObject.transform.parent.parent.parent.parent.parent.GetComponent<Turret> ().attackPoint, this.gameObject);
					col.gameObject.GetComponent<Enemy> ().KnockBack ();
					Destroy (gameObject);
				}
			}
		}
	}
}