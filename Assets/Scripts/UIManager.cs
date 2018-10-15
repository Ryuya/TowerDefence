using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using UnityEngine.SceneManagement;


namespace Assets.Scripts
{
	public class UIManager : MonoBehaviour
	{
		public ClearDialogInit clearDialogInit;
		public DialogCall clearDialogCall;
		public DialogCall pauseDialogCall;
		[SerializeField]
		private Text currentWaveText;
		[SerializeField]
		private Text time;
		[SerializeField]
		private Text baseHealth;
		[SerializeField]
		private Text moneyText;
		//public Text currentWaveText;

		public GameManager gameManager;
		//UI関係
		public GameObject resultCanvas;
		public Image star1, star2, star3;
		public Color pickupColor;
		public Color droppedColor;
		public Image poisonImage;
		public Image meteoImage;
		public Button poisonButton;
		public Button meteoButton;


		//		void Awake ()
		//		{
		//			gameManager = GameManager.GetInstance ();
		//		}

		// Use this for initialization
		void Start ()
		{
			resultCanvas.SetActive (false);
			meteoButton.onClick.AddListener (() => {
				GameManager.GetInstance ().SetMeteo ();
			});
			poisonButton.onClick.AddListener (() => {
				GameManager.GetInstance ().SetPoison ();
			});
		}
	
		// Update is called once per frame
		void Update ()
		{
			gameManager = GameManager.GetInstance ();
			CheckSkillCoolDown ();
			UpdatePlayUI ();

			switch (gameManager.uiState) {

			}
			switch (gameManager.gameState) {
			case GameState.Pause:
				//ShowPause ();
				break;
			case GameState.Play:
				break;
			case GameState.Result:
				ShowResult ();
				break; 
			}
		}

		void UpdatePlayUI ()
		{
			if (currentWaveText != null)
				currentWaveText.text = gameManager.wave.idx.ToString () + "/" + gameManager.wave.master.waveList.Count;
			if (baseHealth != null)
				baseHealth.text = gameManager.playerBase.healthPoint.ToString ();
			if (moneyText != null)
				moneyText.text = gameManager.money.ToString ();
		}

		public void ShowResult ()
		{
			//resultCanvas.SetActive (true);
			clearDialogCall.Open ();
			if (resultCanvas != null) {
				clearDialogInit.stars = GameManager.GetInstance ().clearStar;
			}

		}

		public void showPause ()
		{
			StartCoroutine (_showPause ());
		}

		IEnumerator _showPause ()
		{
			pauseDialogCall.Open ();
			yield return new WaitForSeconds (1.0f);
			Time.timeScale = 0;
		}



		public void CheckSkillCoolDown ()
		{
			if (gameManager.poisonCoolDown < 25f) {
				gameManager.poisonCoolDown += Time.deltaTime;
				poisonImage.fillAmount = gameManager.poisonCoolDown / 25f;
				gameManager.isActivePoison = false;
				poisonButton.interactable = false;
			} else {
				if (poisonButton != null)
					poisonButton.interactable = true;
				gameManager.isActivePoison = true;
			}

			if (gameManager.meteoCoolDown < 40f) {
				gameManager.meteoCoolDown += Time.deltaTime;
				meteoImage.fillAmount = gameManager.meteoCoolDown / 40f;
				gameManager.isActiveMeteo = false;
				meteoButton.interactable = false;
			} else {
				if (meteoButton != null)
					meteoButton.interactable = true;
				gameManager.isActiveMeteo = true;
			}
		}

		public void GoStageSelect ()
		{
			SceneManager.LoadScene ("SceneSelect");
		}
	}




}