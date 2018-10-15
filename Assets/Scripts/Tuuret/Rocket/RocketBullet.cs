using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Units;

namespace Assets.Scripts.Tuuret.Rocket
{
	public class RocketBullet : MonoBehaviour
	{
		private Vector3 prev;
		public GameObject explosionParticle;
		public Transform target;
		public float speed = 0.16f;
		public bool isLaunch;

		float radius = 2.0f;
		public int damage = 5;


		public float launchTime = 0;
		public float launchThreshold = 5.0f;
		public Rigidbody rb;
		// Use this for initialization
		void Awake ()
		{
			
		}

		void Start ()
		{
			rb = GetComponent<Rigidbody> ();
			isLaunch = true;
		}
	
		// Update is called once per frame
		void Update ()
		{
			var diff = transform.position - prev;
			if (diff.magnitude > 0.01) {
				transform.rotation = Quaternion.LookRotation (diff);
			}
			prev = transform.position;
			if (target != null) {
				if (launchThreshold > launchTime) {
					launchTime += Time.deltaTime;
				} else {
					isLaunch = false;
				}

				if (target.GetComponent<Enemy> ().healthPoint <= 0) {
					var hitColliders = Physics.OverlapSphere (this.transform.position, radius * 3);
					foreach (var col in hitColliders) {
						if (col.gameObject.CompareTag ("Enemy")) {
							target = col.gameObject.transform;
						}
					}
				}

				if (isLaunch) {
					rb.isKinematic = false;
					rb.AddForce (Vector3.up * 80.0f);
				} else {
					rb.isKinematic = true;
					this.transform.position = Vector3.Lerp (this.transform.position, target.transform.position, speed);
				}
			} else {
				rb.isKinematic = false;
				rb.AddForce (this.transform.forward * 80.0f);
				var hitColliders = Physics.OverlapSphere (this.transform.position, radius);
				foreach (var col2 in hitColliders) {
					if (col2.gameObject.CompareTag ("Enemy")) {
						target = col2.gameObject.transform;
					}
				}
			}
		}

		void OnTriggerEnter (Collider col)
		{
			if (col.tag == "Enemy" || col.tag == "Enviromment") {
				RocketDestroy ();

			}
		}

		public void RocketDestroy ()
		{
			Instantiate (explosionParticle, this.transform.position, Quaternion.identity);
			var hitColliders = Physics.OverlapSphere (this.transform.position, radius * 5);
			foreach (var col2 in hitColliders) {
				if (col2.gameObject.CompareTag ("Enemy")) {
					col2.GetComponent<Enemy> ().TakeDamage (damage, this.gameObject);
				}
			}
			GameObject.Destroy (gameObject);
		}
	}
}