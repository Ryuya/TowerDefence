using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Units;
using Assets.Scripts;

namespace Assets.Scripts
{

	public class Base : Unit
	{
		// Use this for initialization

		void Start ()
		{
			this.healthPoint = 20;
			this.healthPointBar = transform.GetChild (0).transform.Find ("Slider").GetComponent<Slider> ();
			this.healthPointBar.maxValue = this.healthPoint;
		}
	
		// Update is called once per frame
		void Update ()
		{
			this.healthPointBar.value = this.healthPoint;
			if (healthPoint <= 0) {
				GameManager.GetInstance ().isWin = false;
				GameManager.GetInstance ().isGameOver = true;
			}
		}

		public void TakeDamage (int damage)
		{
			this.healthPoint -= damage;
		}

	}
}