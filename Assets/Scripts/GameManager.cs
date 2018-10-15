using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Assets.Scripts.Common;
using Assets.Scripts.Tuuret;
using Assets.Scripts.Units;
using Assets.SANA.Scripts.Behaviour;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public enum GameState:int
	{
		Pause,
		Play,
		Result,
	}

	public enum UIState:int
	{
		TurretSelect,
		BaseSelect,
		None,
	}

	public enum ActionState:int
	{
		MeteoLv0,
		MeteoLv1 = 1,
		MeteoLv2,
		MeteoLv3,
		PoisonLv0,
		PoisonLv1,
		PoisonLv2,
		PoisonLv3,
		ThunderLv0,
		ThunderLv1,
		ThunderLv2,
		ThunderLv3,
		None,
	}

	public class GameManager : MonoBehaviour
	{
		public int clearStar = 0;

		public static GameManager instance;

		UIManager uiManager;

		public GameObject goblinBlue;
		//public GameObject enemyPrefabB;
		public WaveManager wave;
		public Transform enemySpawnPoint;
		// ターレットのプレファブ
		public GameObject GatlingTurretLv1, GatlingTurretLv2, GatlingTurretLv3, CannonTurretLv1, CannonTurretLv2, CannonTurretLv3, MissileTurretLv1, MissileTurretLv2, MissileTurretLv3, JammerTurretLv1, JammerTurretLv2, JammerTurretLv3;
		public int gatlingMoney = 70, cannonMoney = 120, missileMoney = 100, jammerMoney = 200;
		public Base playerBase;
		public Transform basePosition;
		public Slider debugSlider;


		//なぜか置いとかないといけなくなったエフェクト
		public GameObject teslaEffect;

		//現在のウェーブ数
		int currentWave = 0;
		float elapsedWaveTime = 0;
		float nextWaveThreshold = 2f;

		public LayerMask mask;

		public Vector3 instatiatePosition;


		public List<Unit> enemiesList;

		public float notAliveEnemyTime;

		//ゲーム内時間
		public int supplyMoney = 1;
		public bool isWin;
		public bool isGameOver;

		public GameObject checkerPrefab;
		public NavMeshPath path = null;

		public ActionState actionState;
		public GameState gameState;
		public UIState uiState;

		public List<GameObject> turrets = new List<GameObject> ();
		public List<GameObject> turretbases = new List<GameObject> ();

		//プレイヤー側のステータス
		public int money;

		//スキルのprefab
		public GameObject meteoLv0, meteoLv1, meteoLv2, meteoLv3;
		public GameObject poisonLv0, poisonLv1, poisonLv2, poisonLv3;

		public GameObject lastTouchTurretPosition;
		public Transform lastTouchPosition;

		RaycastHit hit;

		//スキルクールダウン
		public float meteoCoolDown;
		public float poisonCoolDown;
		public bool isActivePoison;
		public bool isActiveMeteo;

		public GameObject Turret;

		//UI
		public Text UpgradeCostText;
		public GameObject SetTurretCanvas;
		public GameObject SetUpgradeCanvas;

		void Awake ()
		{
			// 加算シーンの読み込み
			SceneManager.LoadScene ("UI", LoadSceneMode.Additive);

		}

		// Use this for initialization
		void Start ()
		{
			SetTurretCanvas = GameObject.FindGameObjectWithTag ("SetTuuretCanvas");
			SetUpgradeCanvas = GameObject.FindGameObjectWithTag ("SetUpgradeCanvas");
			UpgradeCostText = GameObject.FindGameObjectWithTag ("UpgradeCostText").GetComponent<Text> ();

			gameState = GameState.Play;
			instance = this;
			enemiesList = new List<Unit> ();
			actionState = ActionState.None;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			poisonCoolDown = 25f;
			meteoCoolDown = 40f;
		}
	
		// Update is called once per frame
		void Update ()
		{
			

			elapsedWaveTime += Time.deltaTime;
			CheckWin ();

			if (elapsedWaveTime > nextWaveThreshold) {
				elapsedWaveTime = 0;
			}
			StartCoroutine (ClickAction ());
			if (lastTouchTurretPosition != null && SetUpgradeCanvas.GetComponent<SetUpgradeUI> ().isSelected) {
				Turret = lastTouchTurretPosition.transform.GetChild (0).GetChild (0).gameObject;
			} else {
                Turret = null;
			}
			if (Turret != null) {
				if (Turret.GetComponent<Turret> ().upgradeCost != 0) {
					UpgradeCostText.text = Turret.GetComponent<Turret> ().upgradeCost.ToString ();
				} else {
					UpgradeCostText.text = "-";
				}
			}

			if (playerBase.healthPoint <= 0) {
				gameState = GameState.Result;
			}
			if (wave.isEndWave && notAliveEnemyTime > 2.0f) {
				gameState = GameState.Result;
				Save ();
			}
		}




		public void CheckWin ()
		{
			foreach (var enemy in enemiesList.ToArray()) {
				if (enemy == null) {
					enemiesList.Remove (enemy);
				}
			}
			if (enemiesList.Count == 0) {
				notAliveEnemyTime += Time.deltaTime;
				if (notAliveEnemyTime > 2.0f && wave.isEndWave) {
					if (playerBase.healthPoint >= 1) {
						this.isWin = true;
					}
					if (playerBase.healthPoint >= 15) {
						clearStar = 3;
					} else if (playerBase.healthPoint >= 10 && playerBase.healthPoint < 15) {
						clearStar = 2;
					} else if (playerBase.healthPoint > 0 && playerBase.healthPoint < 10) {
						clearStar = 1;
					} else {
						this.isWin = false;
						clearStar = 0;
					}
				}
			} else {
				notAliveEnemyTime = 0;
				isWin = false;
			}
		}

		public static GameManager GetInstance ()
		{
			return instance;
		}

		IEnumerator ClickAction ()
		{
			if (Input.GetMouseButtonDown (0)) {
				//SetTurretCanvas.GetComponent<SetTurretUI> ().isSelected = false;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				hit = new RaycastHit ();
				//TODO 実機だと削除ボタンを押した時にもターレットが設置されてしまうらしい
				if (IsPointerOverUIObject ()) {
					
				} else {
					SetUpgradeCanvas.GetComponent<SetUpgradeUI> ().isSelected = false;

					foreach (var turretbase in turretbases) {
					}
					if (Physics.Raycast (ray, out hit, 300f)) {
						Debug.Log (hit.collider.name);
					}
					switch (actionState) {
					case ActionState.None:
						if (Physics.Raycast (ray, out hit, 300f, mask)) {
							if (hit.collider.gameObject.layer == LayerMask.NameToLayer ("Default")) {
								
								SetTurretCanvas.GetComponent<SetTurretUI> ().SetPosition (hit.collider.gameObject.transform.GetChild (0).GetChild (0).gameObject);
								SetTurretCanvas.SetActive (true);
							} else if (hit.collider.gameObject.layer == LayerMask.NameToLayer ("Turret")) {
								lastTouchTurretPosition = hit.collider.gameObject.transform.parent.parent.parent.gameObject;
								lastTouchPosition = lastTouchTurretPosition.transform;
								Debug.Log ("AAA");
								Turret = lastTouchTurretPosition.transform.GetChild (0).GetChild (0).gameObject;

								if (Turret != null) {
									if (!Turret.GetComponent<Turret> ().dynamite) {
										SetUpgradeCanvas.GetComponent<SetUpgradeUI> ().SetPosition (hit.collider.gameObject);
										SetUpgradeCanvas.GetComponent<SetUpgradeUI> ().isSelected = true;
										SetUpgradeCanvas.SetActive (true);
									}
								}
							} else {
								SetUpgradeCanvas.GetComponent<SetUpgradeUI> ().isSelected = false;
								SetTurretCanvas.SetActive (false);
								SetUpgradeCanvas.SetActive (false);
							}
						}
						break;
					case ActionState.MeteoLv0:
						if (Physics.Raycast (ray, out hit, 300f)) {
							yield return new WaitForSeconds (0.1f);

							Meteo (hit.point);
						}
						break;
					case ActionState.MeteoLv1:
						if (Physics.Raycast (ray, out hit, 300f)) {
							yield return new WaitForSeconds (0.1f);
							Meteo (hit.point);
						}
						break;
					case ActionState.MeteoLv2:
						if (Physics.Raycast (ray, out hit, 300f)) {
							yield return new WaitForSeconds (0.1f);
							Meteo (hit.point);
						}
						break;
					case ActionState.MeteoLv3:
						if (Physics.Raycast (ray, out hit, 300f)) {
							yield return new WaitForSeconds (0.1f);
							Meteo (hit.point);
						}
						break;
					case ActionState.PoisonLv0:
						if (Physics.Raycast (ray, out hit, 300f)) {
							yield return new WaitForSeconds (0.1f);
							Poison (hit.point);
						}
						break;
					case ActionState.PoisonLv1:
						if (Physics.Raycast (ray, out hit, 300f)) {
							yield return new WaitForSeconds (0.1f);
							Poison (hit.point);
						}
						break;
					case ActionState.PoisonLv2:
						if (Physics.Raycast (ray, out hit, 300f)) {
							yield return new WaitForSeconds (0.1f);
							Poison (hit.point);
						}
						break;
					case ActionState.PoisonLv3:
						if (Physics.Raycast (ray, out hit, 300f)) {
							yield return new WaitForSeconds (0.1f);
							Poison (hit.point);
						}
						break;
					case ActionState.ThunderLv1:
						break;
					case ActionState.ThunderLv2:
						break;
					case ActionState.ThunderLv3:
						break;
					}
				}
			}
		}

		public void AddEnemy (Unit unit)
		{
			enemiesList.Add (unit);
		}


		public void PutGatlingTurretLv1 (Transform trans)
		{
			if (money >= gatlingMoney) {
				var obj = GameObject.Instantiate (GatlingTurretLv1);
				obj.transform.SetParent (trans);
				obj.transform.localPosition = Vector3.zero;
				money -= gatlingMoney;
			}
			SetTurretCanvas.SetActive (false);
		}

		public void PutGatlingTurretLv2 (Transform trans)
		{
			if (money >= Turret.GetComponent<Turret> ().upgradeCost) {
				var obj = GameObject.Instantiate (GatlingTurretLv2);
				obj.transform.SetParent (trans);
				obj.transform.localPosition = Vector3.zero;
				money -= Turret.GetComponent<Turret> ().upgradeCost;
			}
			SetTurretCanvas.SetActive (false);
		}

		public void PutGatlingTurretLv3 (Transform trans)
		{
			if (money >= Turret.GetComponent<Turret> ().upgradeCost) {
				var obj = GameObject.Instantiate (GatlingTurretLv3);
				obj.transform.SetParent (trans);
				obj.transform.localPosition = Vector3.zero;
				money -= Turret.GetComponent<Turret> ().upgradeCost;
			}
			SetTurretCanvas.SetActive (false);
		}

		public void PutCannonTurretLv1 (Transform trans)
		{
			if (money >= cannonMoney) {
				var obj = GameObject.Instantiate (CannonTurretLv1);
				obj.transform.SetParent (trans);
				obj.transform.localPosition = Vector3.zero;
				money -= cannonMoney;
			}
			SetTurretCanvas.SetActive (false);
		}

		public void PutCannonTurretLv2 (Transform trans)
		{
			if (money >= Turret.GetComponent<Turret> ().upgradeCost) {
				var obj = GameObject.Instantiate (CannonTurretLv2);
				obj.transform.SetParent (trans);
				obj.transform.localPosition = Vector3.zero;
				money -= Turret.GetComponent<Turret> ().upgradeCost;
			}
			SetTurretCanvas.SetActive (false);
		}

		public void PutCannonTurretLv3 (Transform trans)
		{
			if (money >= Turret.GetComponent<Turret> ().upgradeCost) {
				var obj = GameObject.Instantiate (CannonTurretLv3);
				obj.transform.SetParent (trans);
				obj.transform.localPosition = Vector3.zero;
				money -= Turret.GetComponent<Turret> ().upgradeCost;
			}
			SetTurretCanvas.SetActive (false);
		}

		public void PutMissileTurretLv1 (Transform trans)
		{
			if (money >= missileMoney) {
				var obj = GameObject.Instantiate (MissileTurretLv1);
				obj.transform.SetParent (trans);
				obj.transform.localPosition = Vector3.zero;
				money -= missileMoney;
			}
			SetTurretCanvas.SetActive (false);
		}

		public void PutMissileTurretLv2 (Transform trans)
		{
			if (money >= Turret.GetComponent<Turret> ().upgradeCost) {
				var obj = GameObject.Instantiate (MissileTurretLv2);
				obj.transform.SetParent (trans);
				obj.transform.localPosition = Vector3.zero;
				money -= Turret.GetComponent<Turret> ().upgradeCost;
			}
			SetTurretCanvas.SetActive (false);
		}

		public void PutMissileTurretLv3 (Transform trans)
		{
			if (money >= Turret.GetComponent<Turret> ().upgradeCost) {
				var obj = GameObject.Instantiate (MissileTurretLv3);
				obj.transform.SetParent (trans);
				obj.transform.localPosition = Vector3.zero;
				money -= Turret.GetComponent<Turret> ().upgradeCost;
			}
			SetTurretCanvas.SetActive (false);
		}

		public void PutJammerTurretLv1 (Transform trans)
		{
			if (money >= jammerMoney) {
				var obj = GameObject.Instantiate (JammerTurretLv1);
				obj.transform.SetParent (trans);
				obj.transform.localPosition = Vector3.zero;
				money -= jammerMoney;
			}
			SetTurretCanvas.SetActive (false);
		}

		public void PutJammerTurretLv2 (Transform trans)
		{
			if (money >= Turret.GetComponent<Turret> ().upgradeCost) {
				var obj = GameObject.Instantiate (JammerTurretLv2);
				obj.transform.SetParent (trans);
				obj.transform.localPosition = Vector3.zero;
				money -= Turret.GetComponent<Turret> ().upgradeCost;
			}
			SetTurretCanvas.SetActive (false);
		}

		public void PutJammerTurretLv3 (Transform trans)
		{
			if (money >= Turret.GetComponent<Turret> ().upgradeCost) {
				var obj = GameObject.Instantiate (JammerTurretLv3);
				obj.transform.SetParent (trans);
				obj.transform.localPosition = Vector3.zero;
				money -= Turret.GetComponent<Turret> ().upgradeCost;
			}
			SetTurretCanvas.SetActive (false);
		}

		public void subtractionMoney (int cost)
		{
			money -= cost;
		}

		public void SupllyMoney (int cost)
		{
			money += cost;
		}

		public void Save ()
		{

			switch (SceneManager.GetActiveScene ().name) {
			case "Demo":
				if (!SaveData.Instance.isClear_s1)
					SaveData.Instance.isClear_s1 = true;
				if (SaveData.Instance.star_s1 <= clearStar)
					SaveData.Instance.star_s1 = clearStar;
				break;
			case "Stage1":
				if (!SaveData.Instance.isClear_s1)
					SaveData.Instance.isClear_s1 = true;
				if (SaveData.Instance.star_s1 <= clearStar)
					SaveData.Instance.star_s1 = clearStar;
				break;
			case "Stage2":
				if (!SaveData.Instance.isClear_s2)
					SaveData.Instance.isClear_s2 = true;
				if (SaveData.Instance.star_s2 <= clearStar)
					SaveData.Instance.star_s2 = clearStar;
				break;
			case "Stage3":
				if (!SaveData.Instance.isClear_s3)
					SaveData.Instance.isClear_s3 = true;
				if (SaveData.Instance.star_s3 <= clearStar)
					SaveData.Instance.star_s3 = clearStar;
				break;
			case "Stage4":
				if (!SaveData.Instance.isClear_s4)
					SaveData.Instance.isClear_s4 = true;
				if (SaveData.Instance.star_s4 <= clearStar)
					SaveData.Instance.star_s4 = clearStar;
				break;
			case "Stage5":
				if (!SaveData.Instance.isClear_s5)
					SaveData.Instance.isClear_s5 = true;
				if (SaveData.Instance.star_s5 <= clearStar)
					SaveData.Instance.star_s5 = clearStar;
				break;
			case "Stage6":
				if (!SaveData.Instance.isClear_s6)
					SaveData.Instance.isClear_s6 = true;
				if (SaveData.Instance.star_s6 <= clearStar)
					SaveData.Instance.star_s6 = clearStar;
				break;
			case "Stage7":
				if (!SaveData.Instance.isClear_s7)
					SaveData.Instance.isClear_s7 = true;
				if (SaveData.Instance.star_s7 <= clearStar)
					SaveData.Instance.star_s7 = clearStar;
				break;
			case "Stage8":
				if (!SaveData.Instance.isClear_s8)
					SaveData.Instance.isClear_s8 = true;
				if (SaveData.Instance.star_s8 <= clearStar)
					SaveData.Instance.star_s8 = clearStar;
				break;
			case "Stage9":
				if (!SaveData.Instance.isClear_s9)
					SaveData.Instance.isClear_s9 = true;
				if (SaveData.Instance.star_s9 <= clearStar)
					SaveData.Instance.star_s9 = clearStar;
				break;
			case "Stage10":
				if (!SaveData.Instance.isClear_s10)
					SaveData.Instance.isClear_s10 = true;
				if (SaveData.Instance.star_s10 <= clearStar)
					SaveData.Instance.star_s10 = clearStar;
				break;
			case "Endless":
				SaveData.Instance.endless_score = currentWave;
				break;
			}

			SaveData.Instance.Save ();
		}

		/// <summary>
		/// SceneManager.GetActiveScene().nameを引数に渡して確認
		/// </summary>
		/// <param name="name">Name.</param>
		public void SaveDebug (string name)
		{
			switch (name) {
			case "Demo":
				Debug.Log (SaveData.Instance.isClear_s1.ToString () + "\n" + SaveData.Instance.star_s1.ToString ());
				break;
			case "Stage1":
				Debug.Log (SaveData.Instance.isClear_s1.ToString () + "\n" + SaveData.Instance.star_s1.ToString ());
				break;
			case "Stage2":
				Debug.Log (SaveData.Instance.isClear_s2.ToString () + "\n" + SaveData.Instance.star_s2.ToString ());
				break;
			case "Stage3":
				Debug.Log (SaveData.Instance.isClear_s3.ToString () + "\n" + SaveData.Instance.star_s3.ToString ());
				break;
			case "Stage4":
				Debug.Log (SaveData.Instance.isClear_s4.ToString () + "\n" + SaveData.Instance.star_s4.ToString ());
				break;
			case "Stage5":
				Debug.Log (SaveData.Instance.isClear_s5.ToString () + "\n" + SaveData.Instance.star_s5.ToString ());
				break;
			case "Stage6":
				Debug.Log (SaveData.Instance.isClear_s6.ToString () + "\n" + SaveData.Instance.star_s6.ToString ());
				break;
			case "Stage7":
				Debug.Log (SaveData.Instance.isClear_s7.ToString () + "\n" + SaveData.Instance.star_s7.ToString ());
				break;
			case "Stage8":
				Debug.Log (SaveData.Instance.isClear_s8.ToString () + "\n" + SaveData.Instance.star_s8.ToString ());
				break;
			case "Stage9":
				Debug.Log (SaveData.Instance.isClear_s9.ToString () + "\n" + SaveData.Instance.star_s9.ToString ());
				break;
			case "Stage10":
				Debug.Log (SaveData.Instance.isClear_s10.ToString () + "\n" + SaveData.Instance.star_s10.ToString ());
				break;
			case "Endless":
				Debug.Log (SaveData.Instance.endless_score.ToString ());
				break;
				
			}
		}

		public void SetPoison ()
		{
			if (SaveData.Instance.poisonLv3) {
				actionState = ActionState.PoisonLv3;
			} else if (SaveData.Instance.poisonLv2) {
				actionState = ActionState.PoisonLv2;
			} else if (SaveData.Instance.poisonLv1) {
				actionState = ActionState.PoisonLv1;
			} else {
				actionState = ActionState.PoisonLv0;
			}
		}

		void Poison (Vector3 pos)
		{
			switch (actionState) {
			case ActionState.PoisonLv0:
				Instantiate (poisonLv0, pos, Quaternion.identity);
				break;
			case ActionState.PoisonLv1:
				Instantiate (poisonLv1, pos, Quaternion.identity);
				break;
			case ActionState.PoisonLv2:
				Instantiate (poisonLv2, pos, Quaternion.identity);
				break;
			case ActionState.PoisonLv3:
				Instantiate (poisonLv3, pos, Quaternion.identity);
				break;
			}
			poisonCoolDown = 0;
			actionState = ActionState.None;
		}

		public void SetMeteo ()
		{
			if (SaveData.Instance.meteoLv3) {
				actionState = ActionState.MeteoLv3;
			} else if (SaveData.Instance.meteoLv2) {
				actionState = ActionState.MeteoLv2;
			} else if (SaveData.Instance.meteoLv1) {
				actionState = ActionState.MeteoLv1;
			} else {
				actionState = ActionState.MeteoLv0;
			}
		}

		void Meteo (Vector3 pos)
		{
			switch (actionState) {
			case ActionState.MeteoLv3:
				Instantiate (meteoLv3, pos, Quaternion.identity);
				break;
			case ActionState.MeteoLv2:
				Instantiate (meteoLv2, pos, Quaternion.identity);
				break;
			case ActionState.MeteoLv1:
				Instantiate (meteoLv1, pos, Quaternion.identity);
				break;
			case ActionState.MeteoLv0:
				Instantiate (meteoLv0, pos, Quaternion.identity);
				break;
			}
			meteoCoolDown = 0;
			actionState = ActionState.None;
		}

		public void SetPoisonLv1State ()
		{
			actionState = ActionState.PoisonLv1;
		}

		public void SetPoisonLv2State ()
		{
			actionState = ActionState.PoisonLv2;
		}

		public void SetPoisonLv3State ()
		{
			actionState = ActionState.PoisonLv3;
		}



		public void Retry ()
		{
			string currentScene = SceneManager.GetActiveScene ().name;
			SceneManager.LoadScene (currentScene);
		}

		private bool IsPointerOverUIObject ()
		{
			PointerEventData eventDataCurrentPosition = new PointerEventData (EventSystem.current);
			eventDataCurrentPosition.position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			List<RaycastResult> results = new List<RaycastResult> ();
			EventSystem.current.RaycastAll (eventDataCurrentPosition, results);
			return results.Count > 0;
		}

		/// <summary>
		/// 次ウェーブの強制実行
		/// </summary>
		public void StartNextWave ()
		{
			money += 100;
			//次のウェーブを強制実行する処理
			wave.NextWave ();
			meteoCoolDown += wave.master.waveList [wave.idx].getCoolDown;
			poisonCoolDown += wave.master.waveList [wave.idx].getCoolDown;
		}
	}
}