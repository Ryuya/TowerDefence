using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.SANA.Scripts.Master
{
	/// <summary>
	/// ステージマスタデータ
	/// author:SANA
	/// date:2017/11/18
	/// </summary>
	public class StageMasterData : ScriptableObject
	{
		public int id;
		public List<GameObject> spawnPoints;
		public List<WaveData> waveList;
	}

	[System.Serializable]
	public class WaveData
	{
		public int offsetSec;
		public List<SpawnData> spawns;
		public float getCoolDown;
	}

	[System.Serializable]
	public class SpawnData
	{
		public int unitId;
		public int pointId;
	}


}