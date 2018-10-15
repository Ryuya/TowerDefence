using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Tuuret;

namespace Assets.Scripts.Tuuret.Cannon
{


	public class CannonTurretLv3 : Turret
	{
		public GameObject Burrel;
		public GameObject plazmaBullet;
		// Use this for initialization
		void Start ()
		{
			base.Start ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (Burrel.transform.childCount == 0) {
				var obj = Instantiate (plazmaBullet);
				obj.transform.SetParent (Burrel.transform);
				obj.transform.position = Burrel.transform.position;
			}
			base.Update ();
		}
	}

}