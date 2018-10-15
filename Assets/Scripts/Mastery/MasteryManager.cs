using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Mastery
{
	/// <summary>
	/// マスタリーが取得済みかの読み込み、保存を行う
	/// かなりハードコードしないとかけないです・・
	/// </summary>
	public class MasteryManager : MonoBehaviour
	{
		public MasteryNode meteo, meteoLv1, meteoLv2, meteoLv3;
		public MasteryNode poison, poisonLv1, poisonLv2, poisonLv3;
		public MasteryNode thunder, thunderLv1, thunderLv2, thunderLv3;

		public MasteryNode garlingLv1, garlingLv2, garlingLv3, garlingLv4;
		public MasteryNode cannonLv1, cannonLv2, cannonLv3, cannonLv4;
		public MasteryNode missileLv1, missileLv2, missileLv3, missileLv4;
		public MasteryNode jammerLv1, jammerLv2, jammerLv3, jammerLv4;

		public static Sprite clickableImage;
		public Text starText1, starText2;
		public Image onDescriptionImage1, onDescriptionImage2;
		public Text description1, description2;

		public GameObject lastFocusButton;
		[SerializeField]
		public Color defaultColor, upgradeColor, acquiredColor;
		public int star = 30;

		public bool showSkillCanvas = true;
		public GameObject skillCanvas, turretCanvas;

		public MasteryNode lastTouchedMasteryNode;

		public Button upgradeButton1, upgradeButton2;

		public List<GameObject> masteries = new List<GameObject> ();

		// Use this for initialization
		void Start ()
		{
			Load ();
			upgradeButton1.onClick.AddListener (() => {
				if (star >= lastTouchedMasteryNode.necessaryStar && !lastTouchedMasteryNode.isAcquired && lastTouchedMasteryNode.child.isAcquired) {
					star -= lastTouchedMasteryNode.necessaryStar;
					SaveData.Instance.used_star += lastTouchedMasteryNode.necessaryStar;
					lastTouchedMasteryNode.isAcquired = true;
					Save ();
				}
			});
			upgradeButton2.onClick.AddListener (() => {
				if (star >= lastTouchedMasteryNode.necessaryStar && !lastTouchedMasteryNode.isAcquired) {
					star -= lastTouchedMasteryNode.necessaryStar;
					SaveData.Instance.used_star += lastTouchedMasteryNode.necessaryStar;
					lastTouchedMasteryNode.isAcquired = true;
					Save ();
				}
			});

		}
	
		// Update is called once per frame
		void Update ()
		{
			if (EventSystem.current.currentSelectedGameObject != null) {
				if (EventSystem.current.currentSelectedGameObject.GetComponent<MasteryNode> () != null) {
					lastFocusButton = (EventSystem.current.currentSelectedGameObject);
					lastTouchedMasteryNode = EventSystem.current.currentSelectedGameObject.GetComponent<MasteryNode> ();
					onDescriptionImage1.sprite = lastFocusButton.gameObject.GetComponent<Image> ().sprite;
					onDescriptionImage2.sprite = lastFocusButton.gameObject.GetComponent<Image> ().sprite;
				}
			}
			if (lastFocusButton != null) {
				onDescriptionImage1.color = new Color (1, 1, 1, 1);
				onDescriptionImage2.color = new Color (1, 1, 1, 1);
			} else {
				onDescriptionImage1.color = new Color (1, 1, 1, 0);
				onDescriptionImage2.color = new Color (1, 1, 1, 0);
			}
				
			UpdateUI ();

		}

		public void UpdateUI ()
		{
			starText1.text = "x" + star.ToString ();
			starText2.text = "x" + star.ToString ();
			if (EventSystem.current.currentSelectedGameObject != null) {
				if (EventSystem.current.currentSelectedGameObject.GetComponent<MasteryNode> () != null) {
					description1.text = lastTouchedMasteryNode.Description;
					description2.text = lastTouchedMasteryNode.Description;
				}
			}

			if (showSkillCanvas) {
				skillCanvas.SetActive (true);
				turretCanvas.SetActive (false);
			} else {
				skillCanvas.SetActive (false);
				turretCanvas.SetActive (true);
			}
		}

		public void PageToggle ()
		{
			showSkillCanvas = !showSkillCanvas;
		}

		public void Load ()
		{
			star = SaveData.Instance.star_s1 + SaveData.Instance.star_s2 +
			SaveData.Instance.star_s3 + SaveData.Instance.star_s4 +
			SaveData.Instance.star_s5 + SaveData.Instance.star_s6 +
			SaveData.Instance.star_s7 + SaveData.Instance.star_s8 +
			SaveData.Instance.star_s9 + SaveData.Instance.star_s10 - SaveData.Instance.used_star;
			meteo.isAcquired = true;//UnityのPlayerPrefsから読み込みたいテスト
			poison.isAcquired = true;//UnityのPlayerPrefsから読み込みたいテスト
			thunder.isAcquired = true;//UnityのPlayerPrefsから読み込みたいテスト
			poisonLv1.isAcquired = SaveData.Instance.poisonLv1;
			poisonLv2.isAcquired = SaveData.Instance.poisonLv2;
			poisonLv3.isAcquired = SaveData.Instance.poisonLv3;
			meteoLv1.isAcquired = SaveData.Instance.meteoLv1;
			meteoLv2.isAcquired = SaveData.Instance.meteoLv2;
			meteoLv3.isAcquired = SaveData.Instance.meteoLv3;
			thunderLv1.isAcquired = SaveData.Instance.thunderLv1;
			thunderLv2.isAcquired = SaveData.Instance.thunderLv2;
			thunderLv3.isAcquired = SaveData.Instance.thunderLv3;
			garlingLv1.isAcquired = SaveData.Instance.gatlingLv1;
			garlingLv2.isAcquired = SaveData.Instance.gatlingLv2;
			garlingLv3.isAcquired = SaveData.Instance.gatlingLv3;
			garlingLv4.isAcquired = SaveData.Instance.gatlingLv4;
			cannonLv1.isAcquired = SaveData.Instance.cannonLv1;
			cannonLv2.isAcquired = SaveData.Instance.cannonLv2;
			cannonLv3.isAcquired = SaveData.Instance.cannonLv3;
			cannonLv4.isAcquired = SaveData.Instance.cannonLv4;
			missileLv1.isAcquired = SaveData.Instance.missileLv1;
			missileLv2.isAcquired = SaveData.Instance.missileLv2;
			missileLv3.isAcquired = SaveData.Instance.missileLv3;
			missileLv4.isAcquired = SaveData.Instance.missileLv4;
			jammerLv1.isAcquired = SaveData.Instance.jammerLv1;
			jammerLv2.isAcquired = SaveData.Instance.jammerLv2;
			jammerLv3.isAcquired = SaveData.Instance.jammerLv3;
			jammerLv4.isAcquired = SaveData.Instance.jammerLv4;



		}

		public void Save ()
		{
			SaveData.Instance.meteoLv1 = masteries.Find (x => x.name == "MeteoLv1").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.meteoLv2 = masteries.Find (x => x.name == "MeteoLv2").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.meteoLv3 = masteries.Find (x => x.name == "MeteoLv3").GetComponent<MasteryNode> ().isAcquired;

			SaveData.Instance.poisonLv1 = masteries.Find (x => x.name == "PoisonLv1").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.poisonLv2 = masteries.Find (x => x.name == "PoisonLv2").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.poisonLv3 = masteries.Find (x => x.name == "PoisonLv3").GetComponent<MasteryNode> ().isAcquired;

			SaveData.Instance.thunderLv1 = masteries.Find (x => x.name == "ThunderLv1").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.thunderLv2 = masteries.Find (x => x.name == "ThunderLv2").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.thunderLv3 = masteries.Find (x => x.name == "ThunderLv3").GetComponent<MasteryNode> ().isAcquired;

			SaveData.Instance.gatlingLv1 = masteries.Find (x => x.name == "GatlingLv1").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.gatlingLv2 = masteries.Find (x => x.name == "GatlingLv2").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.gatlingLv3 = masteries.Find (x => x.name == "GatlingLv3").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.gatlingLv4 = masteries.Find (x => x.name == "GatlingLv4").GetComponent<MasteryNode> ().isAcquired;


			SaveData.Instance.cannonLv1 = masteries.Find (x => x.name == "CannonLv1").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.cannonLv2 = masteries.Find (x => x.name == "CannonLv2").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.cannonLv3 = masteries.Find (x => x.name == "CannonLv3").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.cannonLv4 = masteries.Find (x => x.name == "CannonLv4").GetComponent<MasteryNode> ().isAcquired;


			SaveData.Instance.missileLv1 = masteries.Find (x => x.name == "MissileLv1").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.missileLv2 = masteries.Find (x => x.name == "MissileLv2").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.missileLv3 = masteries.Find (x => x.name == "MissileLv3").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.missileLv4 = masteries.Find (x => x.name == "MissileLv4").GetComponent<MasteryNode> ().isAcquired;


			SaveData.Instance.jammerLv1 = masteries.Find (x => x.name == "JammerLv1").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.jammerLv2 = masteries.Find (x => x.name == "JammerLv2").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.jammerLv3 = masteries.Find (x => x.name == "JammerLv3").GetComponent<MasteryNode> ().isAcquired;
			SaveData.Instance.jammerLv4 = masteries.Find (x => x.name == "JammerLv4").GetComponent<MasteryNode> ().isAcquired;

			SaveData.Instance.star = star;
			SaveData.Instance.Save ();
		}

		public void Reset ()
		{
			int necessaryStars = 0;
			foreach (var mastery in masteries) {
				var masterynode = mastery.GetComponent<MasteryNode> ();
				if (masterynode.isAcquired) {
					masterynode.isAcquired = false;
					necessaryStars += masterynode.necessaryStar;
				}
			}
			SaveData.Instance.star += SaveData.Instance.used_star;
			SaveData.Instance.used_star = 0;
			star += necessaryStars;
			Save ();
		}

		public void BackButton (string str)
		{
			SceneManager.LoadScene (str);
		}
	}
}