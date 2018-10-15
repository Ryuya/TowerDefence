using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : SavableSingleton<SaveData>
{
	/*
	 *  そのステージをクリアしたか
	 *	ステージごとの星の数(クリア時のHPの状態により変化)
	 *	ステージで取得した星の総数
	 *	マスタリーのセット状態
	 *	エンドレスゲームの最高スコア
	 * */
	//GameData
	public bool isPlayableEndlessGame;
	public int endlessScore;
	public int star;
	public int used_star;
	//Stage1
	public bool isClear_s1;
	public int star_s1;
	//Stage2
	public bool isClear_s2;
	public int star_s2;
	//Stage3
	public bool isClear_s3;
	public int star_s3;
	//Stage4
	public bool isClear_s4;
	public int star_s4;
	//Stage5
	public bool isClear_s5;
	public int star_s5;
	//Stage6
	public bool isClear_s6;
	public int star_s6;
	//Stage7
	public bool isClear_s7;
	public int star_s7;
	//Stage8
	public bool isClear_s8;
	public int star_s8;
	//Stage9
	public bool isClear_s9;
	public int star_s9;
	//Stage10
	public bool isClear_s10;
	public int star_s10;
	//StageEndless
	public int endless_score;

	//Mastery
	public bool meteoLv1, meteoLv2, meteoLv3;
	public bool poisonLv1, poisonLv2, poisonLv3;
	public bool thunderLv1, thunderLv2, thunderLv3;
	public bool gatlingLv1, gatlingLv2, gatlingLv3, gatlingLv4;
	public bool missileLv1, missileLv2, missileLv3, missileLv4;
	public bool cannonLv1, cannonLv2, cannonLv3, cannonLv4;
	public bool jammerLv1, jammerLv2, jammerLv3, jammerLv4;
}
