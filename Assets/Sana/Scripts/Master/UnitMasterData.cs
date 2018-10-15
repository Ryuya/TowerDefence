using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.SANA.Scripts.Master
{
	/// <summary>
	/// ユニットマスタデータ
	/// author:SANA
	/// date:2017/11/18
	/// </summary>
	public class UnitMasterData : ScriptableObject
	{
		public int id;
		/*
	public int healthPoint;
	public int supplyMoney;
	public float deathAnimationTime;
	public int baseDamage;
	public float speed;
	*/
		public GameObject prefab;
	}
}