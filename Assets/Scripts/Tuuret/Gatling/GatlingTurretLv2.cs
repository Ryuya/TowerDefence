using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Tuuret;

namespace Assets.Scripts.Tuuret.Gatling
{
	public class GatlingTurretLv2 : Turret
	{
		public GameObject particles;
		public GameObject parent;
		MasteryLevel masteryLevel;

		public Material defaultMaterial;
		public Material redMaterial;

		public float masterAttackThreshold;
		public float minusAttackThushold;

		public int masterAttackPoint;

		//mastery1
		public List<GameObject> attackedEnemyies = new List<GameObject> ();
		public int doubleStack;
		//mastery2

		//mastery3

		// 赤 -> 緑 -> 青 -> の順に変化させる
		IEnumerator ChangeCameraColor ()
		{
			while (true) {
				yield return StartCoroutine (ChangeColor (Color.red, 3.5f));

			}
			yield break;
		}

		IEnumerator ChangeColor (Color toColor, float duration)
		{
			Color fromColor = transform.GetChild (0).GetChild (0).GetComponent<Renderer> ().material.color;
			float startTime = Time.time;
			float endTime = Time.time + duration;
			float marginR = toColor.r - fromColor.r;
			float marginG = toColor.g - fromColor.g;
			float marginB = toColor.b - fromColor.b;

			while (Time.time < endTime) {
				fromColor.r = fromColor.r + (Time.deltaTime / duration) * marginR;
				fromColor.g = fromColor.g + (Time.deltaTime / duration) * marginG;
				fromColor.b = fromColor.b + (Time.deltaTime / duration) * marginB;
				transform.GetChild (0).GetChild (0).GetComponent<Renderer> ().material.color = fromColor;
				yield return 0;
			}

			transform.GetChild (0).GetChild (0).GetComponent<Renderer> ().material.color = toColor;
			yield break;
		}

		public void ReplaceMaterials ()
		{
			StartCoroutine (ChangeCameraColor ());
		}
		// Use this for initialization
		void Start ()
		{
			base.Start ();
			defaultMaterial = transform.GetChild (0).GetComponent<Renderer> ().material;

			upgradeCost = 100;

			masterAttackThreshold = attackThreshold;
			masterAttackPoint = attackPoint;
			if (SaveData.Instance.gatlingLv3) {
				masteryLevel = MasteryLevel.Lv3;
				this.gameObject.tag = "GatlingLv3";

			} else if (SaveData.Instance.gatlingLv2) {
				masteryLevel = MasteryLevel.Lv2;
			} else if (SaveData.Instance.gatlingLv1) {
				masteryLevel = MasteryLevel.Lv1;
			} else {
				masteryLevel = MasteryLevel.Lv0;
			}
		}
	
		// Update is called once per frame
		void Update ()
		{
			base.Update ();
			attackPoint = masterAttackPoint;
			attackThreshold = masterAttackThreshold + minusAttackThushold;
			if (!dynamite) {
				attackPoint = masterAttackPoint;
				attackThreshold = masterAttackThreshold + minusAttackThushold;
			} else {
				minusAttackThushold = 0;
				attackPoint = masterAttackPoint * 2;
				attackThreshold = 0.2f;
			}
			switch (masteryLevel) {
			case MasteryLevel.Lv0:
				if (reloadTime >= attackThreshold && target != null) {
					Attack ();
				} else {
					reloadTime += Time.deltaTime;
				}

				if (isAttacking) {
					particles.SetActive (true);
				} else {
					particles.SetActive (false);
				}
				break;
			case MasteryLevel.Lv1:
				if (reloadTime >= attackThreshold && target != null) {
					Attack ();
					if (attackedEnemyies.Contains (target.gameObject) == false) {
						attackThreshold = masterAttackThreshold;
						minusAttackThushold = 0;
						attackedEnemyies.Add (target.gameObject);
					} else if (attackedEnemyies.Contains (target.gameObject) == true) {
						doubleStack++;
						if (attackThreshold + minusAttackThushold >= 0.2f) {
							minusAttackThushold -= 0.2f;
						} 
					}
				} else {
					reloadTime += Time.deltaTime;
				}

				if (isAttacking) {
					particles.SetActive (true);
				} else {
					particles.SetActive (false);
				}
				break;
			case MasteryLevel.Lv2:
				if (reloadTime >= attackThreshold && target != null) {

					if (doubleStack > 0) {
						doubleStack--;
						attackPoint = attackPoint * 2;
					}
					Attack ();

					if (attackedEnemyies.Contains (target.gameObject) == false) {
						attackThreshold = masterAttackThreshold;
						minusAttackThushold = 0;
						attackedEnemyies.Add (target.gameObject);
					} else if (attackedEnemyies.Contains (target.gameObject) == true) {
						doubleStack++;
						if (attackThreshold + minusAttackThushold >= 0.2f) {
							minusAttackThushold -= 0.2f;
						} 
					}
				} else {
					reloadTime += Time.deltaTime;
				}

				if (isAttacking) {
					particles.SetActive (true);
				} else {
					particles.SetActive (false);
				}
				break;

			case MasteryLevel.Lv3:
				if (reloadTime >= attackThreshold && target != null) {

					if (doubleStack > 0) {
						doubleStack--;
						attackPoint = attackPoint * 2;
					}
					Attack ();

					if (attackedEnemyies.Contains (target.gameObject) == false) {
						attackThreshold = masterAttackThreshold;
						minusAttackThushold = 0;
						attackedEnemyies.Add (target.gameObject);
					} else if (attackedEnemyies.Contains (target.gameObject) == true) {
						doubleStack++;
						if (attackThreshold + minusAttackThushold >= 0.2f) {
							minusAttackThushold -= 0.2f;
						} 
					}
				} else {
					reloadTime += Time.deltaTime;
				}

				if (isAttacking) {
					particles.SetActive (true);
				} else {
					particles.SetActive (false);
				}
				break;
			}
			if (target == null)
				isAttacking = false;
		}

	}

}