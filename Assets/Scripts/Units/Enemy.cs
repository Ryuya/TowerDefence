using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Assets.Scripts.Units;

namespace Assets.Scripts.Units
{
	public class Enemy : Unit
	{
		private Vector3 prev;
		public NavMeshAgent agent;
		public bool isStop;
		public int baseDamage = 1;
		public bool knockBack;
		public Vector3 direction;


		// Use this for initialization
		protected override void Start ()
		{
			base.Start ();
			Init ();
			GameManager.GetInstance ().AddEnemy (this);
		}
	
		// Update is called once per frame
		public override void FixedUpdate ()
		{
			base.FixedUpdate ();
			this.aliveTime += Time.deltaTime;
			var diff = transform.position - prev;
			if (diff.magnitude > 0.01) {
				transform.rotation = Quaternion.LookRotation (diff);
			}
			if (knockBack) {           
				navMeshMove.agent.velocity = direction * 8;//Knocks the enemy back when appropriate
			}
			if (slowTime > 0) {
				slowFlag = true;
				slowTime -= Time.deltaTime;
			} else {
				slowFlag = false;
			}

			if (stunTime > 0) {
				stunFlag = true;
				stunTime -= Time.deltaTime;
			} else {
				stunFlag = false;
			}


			if (slowFlag) {

				navMeshMove.agent.speed = speed * 0.5f;
			}
			if (!slowFlag && !stunFlag) {
				navMeshMove.agent.speed = speed;
			}
			if (stunFlag) {
				navMeshMove.agent.speed = speed * 0;
			} 
			if (!stunFlag && !slowFlag) {
				navMeshMove.agent.speed = speed;
			}

			if (doubleTime > 0) {
				doubleFlag = true;
				doubleTime -= Time.deltaTime;
			} else {
				doubleFlag = false;
			}

//			agent.isStopped = isStop;
		

			this.healthPointBar.value = this.healthPoint;
			prev = transform.position;
		}

		void Init ()
		{
			prev = transform.position;
			agent = GetComponent<NavMeshAgent> ();
			this.supplyMoney = 10;
			GameManager.GetInstance ().AddEnemy (this);
			this.healthPointBar.maxValue = this.healthPoint;
		}

		void OnTriggerEnter (Collider col)
		{
			direction = col.transform.forward; //Always knocks ememy in the direction the main character is facing
			if (col.gameObject.tag == "PlayerBase") {
				col.gameObject.GetComponent<Base> ().TakeDamage (baseDamage);
				this.GetComponent<Unit> ().Die ();
			}
		}

		public void TakeDamage (int damage, GameObject lastAttackedDamageObj)
		{
			this.lastAttackedDamageObj = lastAttackedDamageObj;
			healthPoint -= damage;

			if (doubleFlag) {
				healthPoint -= damage;
			}

		}

		public void KnockBack ()
		{
			StartCoroutine (_KnockBack ());
		}

		public IEnumerator _KnockBack ()
		{      
			knockBack = true;
			navMeshMove.agent.speed = 10;
			navMeshMove.agent.angularSpeed = 0;//Keeps the enemy facing forwad rther than spinning
			navMeshMove.agent.acceleration = 20;                   

			yield return new WaitForSeconds (1.2f); //Only knock the enemy back for a short time    

			//Reset to default values
			knockBack = false;
			navMeshMove.agent.speed = 4;
			navMeshMove.agent.angularSpeed = 180;
			navMeshMove.agent.acceleration = 10;
		}
	}
}