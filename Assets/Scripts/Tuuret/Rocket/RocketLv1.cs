using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Tuuret;
using Assets.Scripts.Tuuret.Rocket;
using Assets.Scripts.Units;

public class RocketLv1 : Turret
{
	public GameObject Muzzle;
	public GameObject RocketBullet;
	// Use this for initialization
	void Start ()
	{
		base.Start ();
		upgradeCost = 150;
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
			InstatiateRocketBullet ();
			reloadTime = 0f;
			isAttacking = true;
		} else {
			isAttacking = false;
		}
	}

	public void InstatiateRocketBullet ()
	{
		var obj = Instantiate (RocketBullet, Muzzle.transform.position, Quaternion.identity);
		obj.GetComponent<RocketBullet> ().target = target;
		obj.GetComponent<RocketBullet> ().damage = attackPoint;
	}
}
