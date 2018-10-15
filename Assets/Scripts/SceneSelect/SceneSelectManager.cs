using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelectManager : MonoBehaviour
{
	public GameObject stage1, stage2, stage3, stage4, stage5, stage6, stage7, stage8, stage9, stage10;
	public Color pickupColor, droppedColor;
	List<GameObject> list = new List<GameObject> ();

	public List<Button> stages = new List<Button> ();
	public Button endlessButton;

	//	public int star;
	public Text starText;
	public GameObject NotUsedStars;
	// Use this for initialization
	void Start ()
	{
		//SaveDataReset ();
		endlessButton.interactable = false;
		HoshiWoKubaruyatu ();
		ButtonIntaractivate ();
		if ((SaveData.Instance.star_s1 + SaveData.Instance.star_s2 +
		    SaveData.Instance.star_s3 + SaveData.Instance.star_s4 +
		    SaveData.Instance.star_s5 + SaveData.Instance.star_s6 +
		    SaveData.Instance.star_s7 + SaveData.Instance.star_s8 +
		    SaveData.Instance.star_s9 + SaveData.Instance.star_s10 - SaveData.Instance.used_star) > 0) {
			NotUsedStars.SetActive (true);
			starText.text = "x" + (SaveData.Instance.star_s1 + SaveData.Instance.star_s2 +
			SaveData.Instance.star_s3 + SaveData.Instance.star_s4 +
			SaveData.Instance.star_s5 + SaveData.Instance.star_s6 +
			SaveData.Instance.star_s7 + SaveData.Instance.star_s8 +
			SaveData.Instance.star_s9 + SaveData.Instance.star_s10 - SaveData.Instance.used_star).ToString ();
		} else {
			NotUsedStars.SetActive (false);

		}
	}

	/// <summary>
	/// クリアしてないステージはボタンをオフにする。
	/// </summary>
	void ButtonIntaractivate ()
	{
		int maxClearStage = 0;

		if (SaveData.Instance.isClear_s1) {
			maxClearStage = 1;
		}
		if (SaveData.Instance.isClear_s2) {
			maxClearStage = 2;
		}
		if (SaveData.Instance.isClear_s3) {
			maxClearStage = 3;
		}
		if (SaveData.Instance.isClear_s4) {
			maxClearStage = 4;
		}
		if (SaveData.Instance.isClear_s5) {
			maxClearStage = 5;
		}
		if (SaveData.Instance.isClear_s6) {
			maxClearStage = 6;
		}
		if (SaveData.Instance.isClear_s7) {
			maxClearStage = 7;
		}
		if (SaveData.Instance.isClear_s8) {
			maxClearStage = 8;
		}
		if (SaveData.Instance.isClear_s9) {
			maxClearStage = 9;
		}

		for (int i = 9; i > maxClearStage; i--) {
			stages [i - 1].interactable = false;
			if (i - 1 == maxClearStage) {
				stages [i - 1].interactable = true;
			}
		}

		if (maxClearStage == 9) {
			endlessButton.interactable = true;
		}

	}

	/// <summary>
	/// 各ステージが星何個までクリアしているか表示する関数
	/// </summary>
	void HoshiWoKubaruyatu ()
	{
		list = new List<GameObject> ();

		//stage1のデータ
		foreach (var obj in stage1.GetComponentsInChildren<Transform>()) {
			list.Add (obj.gameObject);
		}
		list.RemoveRange (0, 1);

		foreach (var obj in list) {
			switch (SaveData.Instance.star_s1) {
			case 0:
				if (obj.GetComponent<Image> ()) {
					obj.GetComponent<Image> ().color = droppedColor;
				}
				break;
			case 1:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = droppedColor;
				list [2].GetComponent<Image> ().color = droppedColor;
				break;
			case 2:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = pickupColor;
				list [2].GetComponent<Image> ().color = droppedColor;
				break;
			case 3:
				if (obj.GetComponent<Image> ()) {
					obj.GetComponent<Image> ().color = pickupColor;
				}
				break;
			}
		}
		list = new List<GameObject> ();
		//stage2のデータ
		foreach (var obj2 in stage2.GetComponentsInChildren<Transform>()) {
			list.Add (obj2.gameObject);
		}
		list.RemoveRange (0, 1);

		foreach (var obj2 in list) {

			switch (SaveData.Instance.star_s2) {
			case 0:
				if (obj2.GetComponent<Image> ()) {
					obj2.GetComponent<Image> ().color = droppedColor;
				}
				break;
			case 1:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = droppedColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 2:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = pickupColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 3:
				if (obj2.GetComponent<Image> ()) {
					obj2.GetComponent<Image> ().color = pickupColor;
				}
				break;
			}
		}
		list = new List<GameObject> ();

		//stage3のデータ
		foreach (var obj3 in stage3.GetComponentsInChildren<Transform>()) {
			list.Add (obj3.gameObject);
		}
		list.RemoveRange (0, 1);

		foreach (var obj3 in list) {

			switch (SaveData.Instance.star_s3) {
			case 0:
				if (obj3.GetComponent<Image> ()) {
					obj3.GetComponent<Image> ().color = droppedColor;
				}
				break;
			case 1:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = droppedColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 2:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = pickupColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 3:
				if (obj3.GetComponent<Image> ()) {
					obj3.GetComponent<Image> ().color = pickupColor;
				}
				break;
			}
		}
		list = new List<GameObject> ();

		//stage4のデータ
		foreach (var obj4 in stage4.GetComponentsInChildren<Transform>()) {
			list.Add (obj4.gameObject);
		}
		list.RemoveRange (0, 1);

		foreach (var obj4 in list) {

			switch (SaveData.Instance.star_s4) {
			case 0:
				if (obj4.GetComponent<Image> ()) {
					obj4.GetComponent<Image> ().color = droppedColor;
				}
				break;
			case 1:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = droppedColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 2:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = pickupColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 3:
				if (obj4.GetComponent<Image> ()) {
					obj4.GetComponent<Image> ().color = pickupColor;
				}
				break;
			}
		}
		list = new List<GameObject> ();


		//stage1のデータ
		foreach (var obj5 in stage5.GetComponentsInChildren<Transform>()) {
			list.Add (obj5.gameObject);
		}
		list.RemoveRange (0, 1);

		foreach (var obj5 in list) {

			switch (SaveData.Instance.star_s5) {
			case 0:
				if (obj5.GetComponent<Image> ()) {
					obj5.GetComponent<Image> ().color = droppedColor;
				}
				break;
			case 1:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = droppedColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 2:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = pickupColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 3:
				if (obj5.GetComponent<Image> ()) {
					obj5.GetComponent<Image> ().color = pickupColor;
				}
				break;
			}
		}
		list = new List<GameObject> ();

		//stage6のデータ
		foreach (var obj6 in stage6.GetComponentsInChildren<Transform>()) {
			list.Add (obj6.gameObject);
		}
		list.RemoveRange (0, 1);

		foreach (var obj6 in list) {

			switch (SaveData.Instance.star_s6) {
			case 0:
				if (obj6.GetComponent<Image> ()) {
					obj6.GetComponent<Image> ().color = droppedColor;
				}
				break;
			case 1:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = droppedColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 2:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = pickupColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 3:
				if (obj6.GetComponent<Image> ()) {
					obj6.GetComponent<Image> ().color = pickupColor;
				}
				break;
			}
		}
		list = new List<GameObject> ();

		//stage7のデータ
		foreach (var obj7 in stage7.GetComponentsInChildren<Transform>()) {
			list.Add (obj7.gameObject);
		}
		list.RemoveRange (0, 1);

		foreach (var obj7 in list) {

			switch (SaveData.Instance.star_s7) {
			case 0:
				if (obj7.GetComponent<Image> ()) {
					obj7.GetComponent<Image> ().color = droppedColor;
				}
				break;
			case 1:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = droppedColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 2:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = pickupColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 3:
				if (obj7.GetComponent<Image> ()) {
					obj7.GetComponent<Image> ().color = pickupColor;
				}
				break;
			}
		}
		list = new List<GameObject> ();


		//stage8のデータ
		foreach (var obj8 in stage8.GetComponentsInChildren<Transform>()) {
			list.Add (obj8.gameObject);
		}
		list.RemoveRange (0, 1);

		foreach (var obj8 in list) {

			switch (SaveData.Instance.star_s8) {
			case 0:
				if (obj8.GetComponent<Image> ()) {
					obj8.GetComponent<Image> ().color = droppedColor;
				}
				break;
			case 1:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = droppedColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 2:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = pickupColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 3:
				if (obj8.GetComponent<Image> ()) {
					obj8.GetComponent<Image> ().color = pickupColor;
				}
				break;
			}
		}
		list = new List<GameObject> ();

		//stage9のデータ
		foreach (var obj9 in stage9.GetComponentsInChildren<Transform>()) {
			list.Add (obj9.gameObject);
		}
		list.RemoveRange (0, 1);

		foreach (var obj9 in list) {

			switch (SaveData.Instance.star_s9) {
			case 0:
				if (obj9.GetComponent<Image> ()) {
					obj9.GetComponent<Image> ().color = droppedColor;
				}
				break;
			case 1:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = droppedColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 2:
				list [0].GetComponent<Image> ().color = pickupColor;
				list [1].GetComponent<Image> ().color = pickupColor;
				list [2].GetComponent<Image> ().color = droppedColor;

				break;
			case 3:
				if (obj9.GetComponent<Image> ()) {
					obj9.GetComponent<Image> ().color = pickupColor;
				}
				break;
			}
		}
		list = new List<GameObject> ();
	}

	public void  SaveDataReset ()
	{
		SaveData.Instance.isPlayableEndlessGame = false;
		SaveData.Instance.endlessScore = 0;
		SaveData.Instance.star = 0;

		SaveData.Instance.isClear_s1 = false;
		SaveData.Instance.star_s1 = 0;
		SaveData.Instance.isClear_s2 = false;
		SaveData.Instance.star_s2 = 0;
		SaveData.Instance.isClear_s2 = false;
		SaveData.Instance.star_s2 = 0;
		SaveData.Instance.isClear_s3 = false;
		SaveData.Instance.star_s3 = 0;
		SaveData.Instance.isClear_s4 = false;
		SaveData.Instance.star_s4 = 0;
		SaveData.Instance.isClear_s5 = false;
		SaveData.Instance.star_s5 = 0;
		SaveData.Instance.isClear_s6 = false;
		SaveData.Instance.star_s6 = 0;
		SaveData.Instance.isClear_s7 = false;
		SaveData.Instance.star_s7 = 0;
		SaveData.Instance.isClear_s8 = false;
		SaveData.Instance.star_s8 = 0;
		SaveData.Instance.isClear_s9 = false;
		SaveData.Instance.star_s9 = 0;
		SaveData.Instance.isClear_s10 = false;
		SaveData.Instance.star_s10 = 0;

		SaveData.Instance.endless_score = 0;

		SaveData.Instance.meteoLv1 = false;
		SaveData.Instance.meteoLv2 = false;
		SaveData.Instance.meteoLv3 = false;

		SaveData.Instance.poisonLv1 = false;
		SaveData.Instance.poisonLv2 = false;
		SaveData.Instance.poisonLv3 = false;

		SaveData.Instance.thunderLv1 = false;
		SaveData.Instance.thunderLv2 = false;
		SaveData.Instance.thunderLv3 = false;

		SaveData.Instance.gatlingLv1 = false;
		SaveData.Instance.gatlingLv2 = false;
		SaveData.Instance.gatlingLv3 = false;
		SaveData.Instance.gatlingLv4 = false;

		SaveData.Instance.missileLv1 = false;
		SaveData.Instance.missileLv2 = false;
		SaveData.Instance.missileLv3 = false;
		SaveData.Instance.missileLv4 = false;

		SaveData.Instance.cannonLv1 = false;
		SaveData.Instance.cannonLv2 = false;
		SaveData.Instance.cannonLv3 = false;
		SaveData.Instance.cannonLv4 = false;

		SaveData.Instance.jammerLv1 = false;
		SaveData.Instance.jammerLv2 = false;
		SaveData.Instance.jammerLv3 = false;
		SaveData.Instance.jammerLv4 = false;

		SaveData.Instance.Save ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnClick (string scene)
	{
		SceneManager.LoadScene (scene);
	}
}
