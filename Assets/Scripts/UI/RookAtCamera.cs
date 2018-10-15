using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
	public class RookAtCamera : MonoBehaviour
	{

		// Use this for initialization
		void Start ()
		{
		
		}
	
		// Update is called once per frame
		void Update ()
		{
			transform.rotation = Camera.main.transform.rotation;
		}
	}
}