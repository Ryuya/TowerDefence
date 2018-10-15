using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

namespace Assets.Scripts.Common
{
	public class SetTurretUI : MonoBehaviour
	{
		RectTransform rect;
		public Transform targetTrs;
		public GameObject turretBase;
		public bool isSelected;
		public Button setCannonButton, setGatlingButton, setMissileButton, setJammerButton;
		// Use this for initialization
		void Start ()
		{
			
			rect = GetComponent<RectTransform> ();
			setCannonButton = transform.Find ("CannonTurretButton").GetComponent<Button> ();
			setGatlingButton = transform.Find ("GatlingTurretButton").GetComponent<Button> ();
			setMissileButton = transform.Find ("MissileTurretButton").GetComponent<Button> ();
			setJammerButton = transform.Find ("JammerTurretButton").GetComponent<Button> ();

		}
	
		// Update is called once per frame
		void Update ()
		{
			if (targetTrs != null) {
				Camera cam = Camera.main;
				//Vector3 viewport = cam.WorldToScreenPoint (targetTrs.position);
				//this.transform.position = targetTrs.position;//rect.SetAnchoredPosition (new Vector3 (viewport.x, viewport.y, viewport.z));
				Vector3 viewport = cam.WorldToScreenPoint (targetTrs.position);
				this.rect.SetAnchoredPosition (new Vector2 (viewport.x, viewport.y));

				if (GameManager.GetInstance ().gatlingMoney > GameManager.GetInstance ().money) {
					setGatlingButton.interactable = false;

				} else {
					setGatlingButton.interactable = true;
				}
				if (GameManager.GetInstance ().cannonMoney > GameManager.GetInstance ().money) {
					setCannonButton.interactable = false;
				} else {
					setCannonButton.interactable = true;
				}
				if (GameManager.GetInstance ().missileMoney > GameManager.GetInstance ().money) {
					setMissileButton.interactable = false;
				} else {	
					setMissileButton.interactable = true;
				}
				if (GameManager.GetInstance ().jammerMoney > GameManager.GetInstance ().money) {
					setJammerButton.interactable = false;
				} else {
					setJammerButton.interactable = true;
				}
			}

		}

		public void SetPosition (GameObject turretbase)
		{
			turretBase = turretbase;
			targetTrs = turretbase.transform;
			//追加されていってしまうのでリセットする
			RemoveListener ();

			setCannonButton.onClick.AddListener (() => {
				GameManager.GetInstance ().PutCannonTurretLv1 (targetTrs);
			});

			setGatlingButton.onClick.AddListener (() => {
				GameManager.GetInstance ().PutGatlingTurretLv1 (targetTrs);
			});

			setMissileButton.onClick.AddListener (() => {
				GameManager.GetInstance ().PutMissileTurretLv1 (targetTrs);
			});

			setJammerButton.onClick.AddListener (() => {
				GameManager.GetInstance ().PutJammerTurretLv1 (targetTrs);
			});
		}

		public void RemoveListener ()
		{
			setCannonButton.onClick.RemoveAllListeners ();
			setGatlingButton.onClick.RemoveAllListeners ();
			setMissileButton.onClick.RemoveAllListeners ();
			setJammerButton.onClick.RemoveAllListeners ();

		}
	}

}