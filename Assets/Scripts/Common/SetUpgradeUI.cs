using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Tuuret;
using Assets.Scripts.Tuuret.Gatling;
using Assets.Scripts.Units;
using Assets.Scripts.Tuuret.Jammer;
using Assets.Scripts.Tuuret.Cannon;

namespace Assets.Scripts.Common
{
	public class SetUpgradeUI : MonoBehaviour
	{
		RectTransform rect;
		public Transform targetTrs;
		public GameObject turretBase;
		public Button upgradeButton, dynamiteButton;
		public bool isSelected;
		public GameObject Turret;

		public GameObject dynamiteParticle;
		// Use this for initialization
		void Start ()
		{
			rect = GetComponent<RectTransform> ();

		}

		// Update is called once per frame
		void Update ()
		{
			if (targetTrs != null && isSelected) {
				Camera cam = Camera.main;
				Vector3 viewport = cam.WorldToScreenPoint (targetTrs.position);
				this.rect.SetAnchoredPosition (new Vector2 (viewport.x, viewport.y));
			}
		}

		public void SetPosition (GameObject turretbase)
		{
			turretBase = turretbase;
			targetTrs = turretbase.transform;
			Turret = GameManager.GetInstance ().lastTouchTurretPosition.transform.GetChild (0).GetChild (0).gameObject;

			int removeCost = Turret.GetComponent<Turret> ().removeCost;
			int upgradeCost = Turret.GetComponent<Turret> ().upgradeCost;


			upgradeButton.onClick.RemoveAllListeners ();
			dynamiteButton.onClick.RemoveAllListeners ();

			//GatringLv1 のアップグレードUIの機能と表示
			if (Turret.name == "Tow_Gatling1") {
				//Upgrade
				upgradeButton.interactable = true;

				if (GameManager.GetInstance ().money < upgradeCost) {
					upgradeButton.interactable = false;

				}

				upgradeButton.onClick.AddListener (() => {
					if (upgradeCost <= GameManager.GetInstance ().money) {
						//GameManager.GetInstance().money -= upgradeCost;

						GameManager.GetInstance ().PutGatlingTurretLv2 (GameManager.GetInstance ().lastTouchPosition);
						StartCoroutine (UpgradeChangePrefab ());
					}
					isSelected = false;
					ToDefaultPosition ();
				});
				//ダイナマイトスキル
				dynamiteButton.onClick.AddListener (() => {
					GameManager.GetInstance ().money += removeCost;
					Turret.GetComponent<GatlingTurret> ().ReplaceMaterials ();
					Turret.GetComponent<GatlingTurret> ().dynamite = true;
					Turret.GetComponent<GatlingTurret> ().parent.gameObject.AddComponent<TimerDestroy> ();
					Turret.GetComponent<GatlingTurret> ().parent.gameObject.GetComponent<TimerDestroy> ().time = 3.5f;
					isSelected = false;
					ToDefaultPosition ();
				});
			} 
			if (Turret.name == "Tow_Gatling2") {
				//Upgrade
				upgradeButton.interactable = true;
				if (GameManager.GetInstance ().money < upgradeCost) {
					upgradeButton.interactable = false;

				}

				upgradeButton.onClick.AddListener (() => {
					if (upgradeCost <= GameManager.GetInstance ().money) {
						GameManager.GetInstance ().PutGatlingTurretLv3 (GameManager.GetInstance ().lastTouchPosition);
						StartCoroutine (UpgradeChangePrefab ());
					}
					isSelected = false;
					StartCoroutine (UpgradeChangePrefab ());
					ToDefaultPosition ();
				});
				//ダイナマイトスキル
				dynamiteButton.onClick.AddListener (() => {
					GameManager.GetInstance ().money += removeCost;
					Turret.GetComponent<GatlingTurretLv2> ().ReplaceMaterials ();
					Turret.GetComponent<GatlingTurretLv2> ().dynamite = true;
					Turret.GetComponent<GatlingTurretLv2> ().parent.gameObject.AddComponent<TimerDestroy> ();
					Turret.GetComponent<GatlingTurretLv2> ().parent.gameObject.GetComponent<TimerDestroy> ().time = 3.5f;
					isSelected = false;
					ToDefaultPosition ();
				});
			} 
			if (Turret.name == "Tow_Gatling3") {

				//Lv3なのでUpgradeはなしにしたい。
				//ダイナマイトスキル
				upgradeButton.interactable = false;

				dynamiteButton.onClick.AddListener (() => {
					GameManager.GetInstance ().money += removeCost;
					Turret.GetComponent<GatlingTurretLv3> ().ReplaceMaterials ();
					Turret.GetComponent<GatlingTurretLv3> ().dynamite = true;
					Turret.GetComponent<GatlingTurretLv3> ().parent.gameObject.AddComponent<TimerDestroy> ();
					Turret.GetComponent<GatlingTurretLv3> ().parent.gameObject.GetComponent<TimerDestroy> ().time = 3.5f;
					isSelected = false;
					ToDefaultPosition ();
				});
			} 
			if (Turret.name == "Tow_Missile1") {
				//Upgrade
				upgradeButton.interactable = true;
				if (GameManager.GetInstance ().money < upgradeCost) {
					upgradeButton.interactable = false;

				}


				upgradeButton.onClick.AddListener (() => {
					if (upgradeCost <= GameManager.GetInstance ().money) {
						//GameManager.GetInstance().money -= upgradeCost;

						GameManager.GetInstance ().PutMissileTurretLv2 (GameManager.GetInstance ().lastTouchPosition);
						StartCoroutine (UpgradeChangePrefab ());
					}
					isSelected = false;
					ToDefaultPosition ();
					StartCoroutine (UpgradeChangePrefab ());
				});
				//ダイナマイトスキル
				dynamiteButton.onClick.AddListener (() => {
					Vector3 pos = Turret.transform.position;
					GameObject.Destroy (Turret.transform.parent.gameObject);
					var hitColliders = Physics.OverlapSphere (pos, Turret.GetComponent<RocketLv1> ().radius);
					foreach (var col2 in hitColliders) {
						if (col2.gameObject.CompareTag ("Enemy")) {
							col2.GetComponent<Enemy> ().TakeDamage (30, this.gameObject);
						}
					}
					isSelected = false;
					Instantiate (dynamiteParticle, pos, Quaternion.identity);
					ToDefaultPosition ();
				});
			} 
			if (Turret.name == "Tow_Missile2") {
				//Upgrade
				upgradeButton.interactable = true;
				if (GameManager.GetInstance ().money < upgradeCost) {
					upgradeButton.interactable = false;

				}


				upgradeButton.onClick.AddListener (() => {
					if (upgradeCost <= GameManager.GetInstance ().money) {
						GameManager.GetInstance ().PutMissileTurretLv3 (GameManager.GetInstance ().lastTouchPosition);
						StartCoroutine (UpgradeChangePrefab ());
					}
					isSelected = false;
					ToDefaultPosition ();
					StartCoroutine (UpgradeChangePrefab ());
				});
				//ダイナマイトスキル
				dynamiteButton.onClick.AddListener (() => {
					Vector3 pos = Turret.transform.position;
					GameObject.Destroy (Turret.transform.parent.gameObject);
					var hitColliders = Physics.OverlapSphere (pos, Turret.GetComponent<RocketLv2> ().radius);
					foreach (var col2 in hitColliders) {
						if (col2.gameObject.CompareTag ("Enemy")) {
							col2.GetComponent<Enemy> ().TakeDamage (60, this.gameObject);
						}
					}
					isSelected = false;
					Instantiate (dynamiteParticle, pos, Quaternion.identity);
					ToDefaultPosition ();
				});
			} 
			if (Turret.name == "Tow_Missile3") {
				//Upgrade
				//ダイナマイトスキル
				upgradeButton.interactable = false;


				dynamiteButton.onClick.AddListener (() => {
					Vector3 pos = Turret.transform.position;
					GameObject.Destroy (Turret.transform.parent.gameObject);
					var hitColliders = Physics.OverlapSphere (pos, 8);
					foreach (var col2 in hitColliders) {
						if (col2.gameObject.CompareTag ("Enemy")) {
							col2.GetComponent<Enemy> ().TakeDamage (60, this.gameObject);
						}
					}
					isSelected = false;
					Instantiate (dynamiteParticle, pos, Quaternion.identity);
					ToDefaultPosition ();
				});
			} 
			if (Turret.name == "Tow_Cannon1") {
				//Upgrade
				upgradeButton.interactable = true;
				if (GameManager.GetInstance ().money < upgradeCost) {
					upgradeButton.interactable = false;

				}

				upgradeButton.onClick.AddListener (() => {
					if (upgradeCost <= GameManager.GetInstance ().money) {
						//GameManager.GetInstance().money -= upgradeCost;

						GameManager.GetInstance ().PutCannonTurretLv2 (GameManager.GetInstance ().lastTouchPosition);
						StartCoroutine (UpgradeChangePrefab ());
					}
					isSelected = false;
					ToDefaultPosition ();
				});
				//ダイナマイトスキル
				dynamiteButton.onClick.AddListener (() => {
					Vector3 pos = Turret.transform.position;
					GameObject.Destroy (Turret.transform.parent.gameObject);
					var hitColliders = Physics.OverlapSphere (pos, Turret.GetComponent<CannonTurretLv1> ().radius);
					foreach (var col2 in hitColliders) {
						if (col2.gameObject.CompareTag ("Enemy")) {
							col2.GetComponent<Enemy> ().TakeDamage (30, this.gameObject);
						}
					}
					isSelected = false;
					Instantiate (dynamiteParticle, pos, Quaternion.identity);
					ToDefaultPosition ();
				});
			} 
			if (Turret.name == "Tow_Cannon2") {
				upgradeButton.interactable = true;
				if (GameManager.GetInstance ().money < upgradeCost) {
					upgradeButton.interactable = false;

				}

				upgradeButton.onClick.AddListener (() => {
					if (upgradeCost <= GameManager.GetInstance ().money) {
						//GameManager.GetInstance().money -= upgradeCost;

						GameManager.GetInstance ().PutCannonTurretLv3 (GameManager.GetInstance ().lastTouchPosition);
						StartCoroutine (UpgradeChangePrefab ());
					}
					isSelected = false;
					ToDefaultPosition ();
				});
				//ダイナマイトスキル
				dynamiteButton.onClick.AddListener (() => {
					Vector3 pos = Turret.transform.position;
					GameObject.Destroy (Turret.transform.parent.gameObject);
					var hitColliders = Physics.OverlapSphere (pos, Turret.GetComponent<CannonTurretLv2> ().radius);
					foreach (var col2 in hitColliders) {
						if (col2.gameObject.CompareTag ("Enemy")) {
							col2.GetComponent<Enemy> ().TakeDamage (60, this.gameObject);
						}
					}
					isSelected = false;
					Instantiate (dynamiteParticle, pos, Quaternion.identity);
					ToDefaultPosition ();
				});
			} 
			if (Turret.name == "Tow_Cannon3") {
				upgradeButton.interactable = false;

//				upgradeButton.onClick.AddListener (() => {
//					if (upgradeCost <= GameManager.GetInstance ().money) {
//						//GameManager.GetInstance().money -= upgradeCost;
//
//						GameManager.GetInstance ().PutCannonTurretLv3 (GameManager.GetInstance ().lastTouchPosition);
//						StartCoroutine (UpgradeChangePrefab ());
//					}
//					isSelected = false;
//					ToDefaultPosition ();
//				});
				//ダイナマイトスキル
				dynamiteButton.onClick.AddListener (() => {
					Vector3 pos = Turret.transform.position;
					GameObject.Destroy (Turret.transform.parent.gameObject);
					var hitColliders = Physics.OverlapSphere (pos, Turret.GetComponent<CannonTurretLv3> ().radius);
					foreach (var col2 in hitColliders) {
						if (col2.gameObject.CompareTag ("Enemy")) {
							col2.GetComponent<Enemy> ().TakeDamage (120, this.gameObject);
						}
					}
					isSelected = false;
					Instantiate (dynamiteParticle, pos, Quaternion.identity);
					ToDefaultPosition ();
					ToDefaultPosition ();
				});
			} 
			if (Turret.name == "Tow_Jammer1") {
				upgradeButton.interactable = true;
				if (GameManager.GetInstance ().money < 100) {
					upgradeButton.interactable = false;

				}

				upgradeButton.onClick.AddListener (() => {
					if (upgradeCost <= GameManager.GetInstance ().money) {

						GameManager.GetInstance ().PutJammerTurretLv2 (GameManager.GetInstance ().lastTouchPosition);
						StartCoroutine (UpgradeChangePrefab ());
					}
					isSelected = false;
					ToDefaultPosition ();
				});
				//ダイナマイトスキル
				dynamiteButton.onClick.AddListener (() => {
					Vector3 pos = Turret.transform.position;
					GameObject.Destroy (Turret.transform.parent.gameObject);
					var hitColliders = Physics.OverlapSphere (pos, Turret.GetComponent<Jammer> ().radius * 3);
					foreach (var col2 in hitColliders) {
						if (col2.gameObject.CompareTag ("Enemy")) {
							col2.GetComponent<Enemy> ().TakeDamage (0, this.gameObject);
							col2.GetComponent<Enemy> ().TakeStun (5f);
							var effect = Instantiate (GameManager.GetInstance ().teslaEffect, col2.transform.position, Quaternion.identity);
							effect.AddComponent<TimerDestroy> ();
							effect.GetComponent<TimerDestroy> ().time = 5f;
							effect.transform.SetParent (col2.transform);
						}
					}
					isSelected = false;
					Instantiate (dynamiteParticle, pos, Quaternion.identity);
					ToDefaultPosition ();
				});
			} 
			if (Turret.name == "Tow_Jammer2") {
				upgradeButton.interactable = true;
				if (GameManager.GetInstance ().money < 150) {
					upgradeButton.interactable = false;

				}

				upgradeButton.onClick.AddListener (() => {
					if (upgradeCost <= GameManager.GetInstance ().money) {
						//GameManager.GetInstance().money -= upgradeCost;

						GameManager.GetInstance ().PutJammerTurretLv3 (GameManager.GetInstance ().lastTouchPosition);
						StartCoroutine (UpgradeChangePrefab ());
					}
					isSelected = false;
					ToDefaultPosition ();
				});
				//ダイナマイトスキル
				dynamiteButton.onClick.AddListener (() => {
					Vector3 pos = Turret.transform.position;
					GameObject.Destroy (Turret.transform.parent.gameObject);
					var hitColliders = Physics.OverlapSphere (pos, Turret.GetComponent<Jammer> ().radius * 3);
					foreach (var col2 in hitColliders) {
						if (col2.gameObject.CompareTag ("Enemy")) {
							col2.GetComponent<Enemy> ().TakeDamage (60, this.gameObject);
							col2.GetComponent<Enemy> ().TakeStun (8f);
							var effect = Instantiate (GameManager.GetInstance ().teslaEffect, col2.transform.position, Quaternion.identity);
							effect.AddComponent<TimerDestroy> ();
							effect.GetComponent<TimerDestroy> ().time = 8f;
							effect.transform.SetParent (col2.transform);
						}
					}
					isSelected = false;
					Instantiate (dynamiteParticle, pos, Quaternion.identity);
					ToDefaultPosition ();
				});
			} 
			if (Turret.name == "Tow_Jammer3") {
				upgradeButton.interactable = false;

//				upgradeButton.onClick.AddListener (() => {
//					if (upgradeCost <= GameManager.GetInstance ().money) {
//						//GameManager.GetInstance().money -= upgradeCost;
//
//						GameManager.GetInstance ().PutJammerTurretLv1 (GameManager.GetInstance ().lastTouchPosition);
//						StartCoroutine (UpgradeChangePrefab ());
//					}
//					isSelected = false;
//					ToDefaultPosition ();
//				});
				//ダイナマイトスキル
				dynamiteButton.onClick.AddListener (() => {
					Vector3 pos = Turret.transform.position;
					GameObject.Destroy (Turret.transform.parent.gameObject);
					var hitColliders = Physics.OverlapSphere (pos, Turret.GetComponent<Jammer> ().radius * 3);
					foreach (var col2 in hitColliders) {
						if (col2.gameObject.CompareTag ("Enemy")) {
							col2.GetComponent<Enemy> ().TakeDamage (120, this.gameObject);
							col2.GetComponent<Enemy> ().TakeStun (12f);
							var effect = Instantiate (GameManager.GetInstance ().teslaEffect, col2.transform.position, Quaternion.identity);
							effect.AddComponent<TimerDestroy> ();
							effect.GetComponent<TimerDestroy> ().time = 12f;
							effect.transform.SetParent (col2.transform);
						}
					}
					isSelected = false;
					Instantiate (dynamiteParticle, pos, Quaternion.identity);
					ToDefaultPosition ();
					ToDefaultPosition ();
				});
			}
		}

		IEnumerator UpgradeChangePrefab ()
		{
			yield return new WaitForSeconds (0.1f);
			Destroy (GameManager.GetInstance ().lastTouchTurretPosition.transform.GetChild (0).gameObject);
		}

		public void ToDefaultPosition ()
		{
			this.rect.SetAnchoredPosition (new Vector2 (0f, 0f));
		}
	}
}