using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Common
{
	public class TimerDestroy : MonoBehaviour
	{
		[SerializeField]
		public float time = 1.0f;
		// Use this for initialization
		void Awake ()
		{
			time = 1.0f;

		}

		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update ()
		{
			time -= Time.deltaTime;
			if (time < 0) {

				Destroy (gameObject);
			}
		}
	}
}