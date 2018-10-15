using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Tuuret;

namespace Assets.Scripts.Tuuret.Cannon
{
	public class ParicleScaleUp : MonoBehaviour
	{
		public GameObject effect;

		public Transform target;
		// Use this for initialization
		float x, y, z;
		float scale;

		public bool shotReady;
		public bool isShooting;

		public bool One;

		void Start ()
		{
			One = true;
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (scale < 2f) {
				scale += Time.deltaTime;
				x = scale;
				y = scale;
				z = scale;
				effect.transform.localScale = new Vector3 (x, y, z);
				shotReady = false;
			} else {
				shotReady = true;
				//ScaleReset ();
			}

			if (One) {
				if (shotReady && this.gameObject.transform.parent.parent.parent.parent.parent.GetComponent<Turret> ().target != null) {
					this.gameObject.AddComponent<CannonBurret> ();
					this.gameObject.GetComponent<CannonBurret> ().target = this.gameObject.transform.parent.parent.parent.parent.parent.GetComponent<Turret> ().target;
					One = false;
				}
			}
		}

		public void ScaleReset ()
		{
			scale = 0f;
		}
	}
}