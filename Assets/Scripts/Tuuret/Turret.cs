using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.Units;
using Assets.Scripts;

namespace Assets.Scripts.Tuuret
{
	public class Turret : MonoBehaviour
	{
		public bool dynamite;

		Collider inSight;

		[SerializeField]
		GameObject Neck;

		public int attackPoint = 2;

		public Transform target;

		[SerializeField]
		public float reloadTime;
		public float attackThreshold = 1.5f;

		public Canvas turretUpgradeCanvas;


		public bool isShowUpgradeCanvas;

		public Button upgradeButton, removeButton;

		public int upgradeCost = 15;
		public int removeCost = 10;

		public List<GameObject> stayEnemiesList;

		public int level = 0;
		public bool isAttacking;
		public float radius;
		public SphereCollider sphereCollider;


		// Use this for initialization
		public void Start ()
		{
			sphereCollider = GetComponent<SphereCollider> ();
			GameManager.GetInstance ().turrets.Add (this.gameObject);
			radius = sphereCollider.radius * 3;
		}
	
		// Update is called once per frame
		public void Update ()
		{
			SetTatget ();
			if (target != null) {
				Neck.transform.LookAt (target);
			}
			if (target != null && gameObject.CompareTag ("Missile")) {
				Neck.transform.LookAt (target.transform);
				Vector3 rot = Neck.transform.eulerAngles;
				rot.x = -45f;
				Neck.transform.eulerAngles = rot;
			}
            stayEnemiesList.Clear();

		}

		/// <summary>
		/// タワーディフェンスは基本的にゴールに近い敵を優先して攻撃しますが、現在のコードはゲームに存在している時間が長い敵を優先して攻撃している。
		///こちらのリファレンスに書いてある関数を実行しても読み取り不能でInfinityが返ってきてしまう問題が解決できればこちらを使用したい。
		///（ゴールにかなり近くと計算結果の距離がきちんと返ってきている模様）
		/// </summary>
		public void SetTatget ()
		{
			int count = 0;
			int	index = 0;
			float max = 0f;
			foreach (var enemy in stayEnemiesList.ToArray<GameObject>()) {
				if (enemy == null || enemy.GetComponent<Enemy> ().healthPoint <= 0) {
					stayEnemiesList.Remove (enemy);

					continue;
				} else {
					if (Vector3.Distance (enemy.transform.position, this.transform.position) > sphereCollider.radius * 3) {
						stayEnemiesList.Remove (enemy);
					}
				}
				count++;
				if (max < enemy.GetComponent<Unit> ().aliveTime) {
					max = enemy.GetComponent<Unit> ().aliveTime;
				}
			}
			foreach (var enemy in stayEnemiesList.ToArray<GameObject>()) {
                //
				if (max - enemy.GetComponent<Unit> ().aliveTime < 0.1f) {
					target = enemy.transform;
				}
			}
		}

		public void ChangeShowCanvas ()
		{
			isShowUpgradeCanvas = !isShowUpgradeCanvas;
		}

		void OnTriggerStay (Collider col)
		{
			if (col.tag == "Enemy") {
				if (stayEnemiesList.Contains (col.gameObject) == false && col.gameObject.GetComponent<Enemy> ().healthPoint >= 1) {
					stayEnemiesList.Add (col.gameObject);
				}
			}
		}

		void OnTriggerExit (Collider col)
		{
			if (col.tag == "Enemy") {
                stayEnemiesList.Remove(col.gameObject);
			}
		}

		protected void Attack ()
		{
			reloadTime = 0f;
			if (target != null) {
				if (target.GetComponent<Unit> ().healthPoint <= 0) {
					target = null;
					isAttacking = false;
					return;
				}
				target.GetComponent<Enemy> ().TakeDamage (attackPoint, this.gameObject);
				isAttacking = true;
			} else {
				isAttacking = false;
			}
		}
	}
}