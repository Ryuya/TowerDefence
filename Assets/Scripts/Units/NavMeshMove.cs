using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units
{
	public class NavMeshMove : MonoBehaviour
	{

		public NavMeshAgent agent;
		public GameObject target;
		public bool stopped;
		public Vector3 reccentPos;

		void Awake ()
		{
			agent = GetComponent<NavMeshAgent> ();
			target = GameObject.Find ("PlayerBase").gameObject;
			stopped = false;
			if (agent.pathStatus != NavMeshPathStatus.PathInvalid) {
				agent.SetDestination (target.transform.position);
			}
		}

		// Use this for initialization
		void Start ()
		{
		

		}

		// Update is called once per frame
		void Update ()
		{
		}
		//Enemy.csに移動
		/*
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "PlayerBase"){
			col.gameObject.GetComponent<Base>().TakeDamage(this.baseDamage);
			this.GetComponent<Unit>().Die();
		}
	}
	*/
	}
}