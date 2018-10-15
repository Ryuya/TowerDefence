using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Units;
using Assets.SANA.Scripts.Master;
using UnityEngine.SceneManagement;

/// <summary>
/// ウェーブ総括
/// author:SANA
/// date:2017/11/18
/// </summary>
namespace Assets.SANA.Scripts.Behaviour
{
	public class WaveManager : MonoBehaviour
	{
		public int idx;
		public StageMasterData master;
		public Dictionary<int, UnitMasterData> unitDic;
		private float wait;
		public bool isEndWave;
		// Use this for initialization
		void Start ()
		{
			idx = 0;
			unitDic = new Dictionary<int, UnitMasterData> ();
			wait = 1f;
		}

		void Update ()
		{
			if (wait > 0) {
				wait -= Time.deltaTime;
				if (wait <= 0) {
					NextWave ();
				}
			}

			if (master.waveList.Count == idx) {
				isEndWave = true;
			}
		}

		public void NextWave ()
		{
			if (master.waveList.Count <= idx)
				return;
			wait = 0;
			StartCoroutine (Spawn (idx));
			idx++;
			if (master.waveList.Count > idx) {
				wait = master.waveList [idx].offsetSec + master.waveList [idx].spawns.Count;
			}
		}

		IEnumerator Spawn (int idx)
		{
			foreach (SpawnData spawn in master.waveList[idx].spawns) {
				if (!unitDic.ContainsKey (spawn.unitId)) {
					UnitMasterData master = Resources.Load<UnitMasterData> ("ScriptableObjects/UnitMasterData/" + spawn.unitId.ToString ());
					unitDic.Add (master.id, master);
				}
				//master.waveList[idx]
				GameObject obj = Instantiate (unitDic [spawn.unitId].prefab, master.spawnPoints [spawn.pointId].transform.position, Quaternion.identity);
				Enemy enemy = obj.GetComponent<Enemy> ();
				enemy.GetComponent<Enemy> ().healthPoint += idx;
				yield return new WaitForSeconds (1.0f);
			}
			idx++;
		}
		//
		//		IEnumerator RandomSpawn (int idx)
		//		{
		//			int random = Random.Range (1, 100);
		//			int num = 0;
		//			switch (random) {
		//			case random < 10:
		//				num = 1;
		//				break;
		//			case random < 20 && random >= 10:
		//				num = 2;
		//				break;
		//			default:
		//				num = 1;
		//				break;
		//			}
		//			UnitMasterData masterObj = Resources.Load<UnitMasterData> ("ScriptableObjects/UnitMasterData/" + num.ToString ());
		//			GameObject obj = Instantiate (masterObj.prefab, Resources.Load(""), Quaternion.identity);
		//
		//			yield return new WaitForSeconds (1.0f);
		//			idx++;
		//		}
	}
}