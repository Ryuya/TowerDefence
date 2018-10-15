using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Tuuret.Rocket;

public class RocketDestroy : MonoBehaviour
{
	public RocketBullet rocketBullet;
	public float time = 5.0f;
	// Use this for initialization
	void Start ()
	{
		
		rocketBullet = GetComponent<RocketBullet> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		time -= Time.deltaTime;
		if (time < 0) {
			rocketBullet.RocketDestroy ();
		}
	}
}
