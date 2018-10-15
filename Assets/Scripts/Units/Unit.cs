using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using Assets.Scripts.Tuuret.Gatling;

namespace Assets.Scripts.Units
{
	public class Unit : MonoBehaviour
	{

		public int healthPoint;
		public Slider healthPointBar;
		public float aliveTime;
		public int supplyMoney;
		public Animator anim;
		public NavMeshMove navMeshMove;
		public float deathAnimationTime;
		public float speed;
		public bool slowFlag = false;
		public float slowTime;
		public bool doubleFlag = false;
		public float doubleTime;
		public bool stunFlag = false;
		public float stunTime;

		public GameObject lastAttackedDamageObj;
		// Use this for initialization
		public void Awake ()
		{
			
		}

		protected virtual void Start ()
		{
			anim.SetBool ("Run", true);
			navMeshMove = GetComponent<NavMeshMove> ();
			navMeshMove.agent.speed = speed;
		}
	
		// Update is called once per frame
		public virtual void FixedUpdate ()
		{
			CheckAlive ();
		}



		public virtual void TakeSlow (float addSlowTime)
		{
			slowTime += addSlowTime;
		}

		public virtual void TakeStun (float addStunTime)
		{
			stunTime += addStunTime;
		}

		public virtual void TakeDouble (float addDoubleTime)
		{
			doubleTime += addDoubleTime;
		}



		public virtual void CheckAlive ()
		{
			if (healthPoint <= 0) {
				Die ();
				if (lastAttackedDamageObj != null)
				if (lastAttackedDamageObj.tag == "GatlingLv3") {
					GameManager.GetInstance ().SupllyMoney (this.supplyMoney);
					GameManager.GetInstance ().SupllyMoney (this.supplyMoney);
				}
			}
		}

		public virtual void Die ()
		{
			GameManager.GetInstance ().SupllyMoney (this.supplyMoney);
			GameManager.GetInstance ().enemiesList.Remove (this);
			StartCoroutine (WaitDestroy (this.gameObject));
			Debug.Log (this.supplyMoney);
			this.enabled = false;
			navMeshMove.agent.speed = 0f;
		}

		public IEnumerator  WaitDestroy (GameObject obj)
		{
			anim.SetTrigger ("Die");
			yield return new WaitForSeconds (deathAnimationTime);
			Destroy (obj);
		}

	}

}