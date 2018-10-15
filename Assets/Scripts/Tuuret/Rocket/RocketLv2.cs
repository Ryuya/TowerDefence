﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Tuuret;
using Assets.Scripts.Tuuret.Rocket;
using Assets.Scripts.Units;

public class RocketLv2 : Turret
{
	public GameObject Muzzle1;
	public GameObject Muzzle2;
	public GameObject RocketBullet;
	// Use this for initialization
	void Start ()
	{
		base.Start ();
		upgradeCost = 180;
	}
	
	// Update is called once per frame
	void Update ()
	{
		base.Update ();
		if (reloadTime >= attackThreshold) {
			Attack ();
		} else {
			reloadTime += Time.deltaTime;
		}

		if (isAttacking) {
		} else {
		}
	}

	public void Attack ()
	{
		if (target != null) {
			if (target.GetComponent<Unit> ().healthPoint <= 0) {
				target = null;
				isAttacking = false;
				return;
			}
			//Attack
			for (int i = 0; i < 2; i++) {
				StartCoroutine (InstatiateRocketBullet1 ());
			}
			reloadTime = 0f;
			isAttacking = true;
		} else {
			isAttacking = false;
		}
	}

	public IEnumerator InstatiateRocketBullet1 ()
	{
		float wait = Random.Range (0.5f, 1.0f);
		int rand = Mathf.CeilToInt (Random.Range (1, 3));
		yield return new WaitForSeconds (wait);

		switch (rand) {
		case 1:
			var obj = Instantiate (RocketBullet, Muzzle1.transform.position, Quaternion.identity);
			obj.GetComponent<RocketBullet> ().target = target;
			obj.GetComponent<RocketBullet> ().damage = attackPoint;
			break;
		case 2:
			var obj2 = Instantiate (RocketBullet, Muzzle2.transform.position, Quaternion.identity);
			obj2.GetComponent<RocketBullet> ().target = target;
			obj2.GetComponent<RocketBullet> ().damage = attackPoint;

			break;
		}
	}
}
