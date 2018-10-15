using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

namespace Assets.Scripts
{

	public class TurretBase : MonoBehaviour
	{
		public bool isHaveTurret;
		public GameObject TuuretPosition;
		public GameObject TurretCanvas;
		public bool isShowPuitTurretCanvas;
		// Use this for initialization
		void Start ()
		{
			GameManager.GetInstance ().turretbases.Add (this.gameObject);
			//TurretCanvas.SetActive(false);
		}
		// Update is called once per frame
		void Update ()
		{
			/*
		if(TuuretPosition.transform.childCount >= 1){
			isHaveTurret = true;
		} else {
			isHaveTurret = false;
		}

		if(TurretCanvas.activeSelf){
			isShowPuitTurretCanvas = true;
		} else {
			isShowPuitTurretCanvas = false;
		} 
		*/
		}
	}

}