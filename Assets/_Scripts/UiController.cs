using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UiController : MonoBehaviour
{
	#region Singleton
	public static UiController instance;
	void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}
	#endregion

	public GameObject winPanel, gamePanel, losePanel,tapToStartPanel;
	public TextMeshProUGUI scoreText,levelText,moneyText;

	private void Start()
	{
		gamePanel.SetActive(true);
		tapToStartPanel.SetActive(true);
		winPanel.SetActive(false);
		losePanel.SetActive(false);
		scoreText.text = PlayerPrefs.GetInt("score").ToString();
		levelText.text = "LEVEL " + LevelController.instance.totalLevelNo.ToString();
		
	}

	public void NextLevelButtonClick()
	{
		winPanel.SetActive(false);
		tapToStartPanel.SetActive(true);
		PlayerController.instance.PreStartingEvents();
		LevelController.instance.NextLevelEvents();
		//GameManager.instance.isContinue = true;
		//SwerveMovement.instance.swerve = true;
		//PlayerMovement.instance.speed = 4f;

	}

	public void RestartButtonClick()
	{
		losePanel.SetActive(false);
		tapToStartPanel.SetActive(true);
		//GameManager.instance.isContinue = false;
		PlayerController.instance.PreStartingEvents();
		LevelController.instance.RestartLevelEvents();
		
		//SwerveMovement.instance.swerve = true;
		//PlayerMovement.instance.speed = 4f;

	}

	public void SetScoreText()
	{
		StartCoroutine(SetScoreTextAnim());
	}

	// bu fonksiyon sayesinde score textimiz birer birer artýyor veya azalýyor hýzlý þekilde..
	// artýþ azalýþ animasyonu diyebiliriz.
	// eger alinan scorelar buyukse ve birer birer artirmak sacma derece uzun suruyorsa fonksiyon icinden tempscore artis miktarini artirin.
	IEnumerator SetScoreTextAnim()
	{
		int tempScore = int.Parse(scoreText.text);
		if(tempScore < GameManager.instance.score)
		{
			while (tempScore < GameManager.instance.score)
			{
				tempScore++;
				scoreText.text =tempScore.ToString();
				yield return new WaitForSeconds(.05f);
			}
		}
		else if(tempScore > GameManager.instance.score)
		{
			while (tempScore > GameManager.instance.score)
			{
				tempScore--;
				scoreText.text = tempScore.ToString();
				yield return new WaitForSeconds(.05f);
			}
		}		
	}

	public void SetLevelText()
	{
		levelText.text = "LEVEL " + LevelController.instance.totalLevelNo.ToString();
	}

	public void OpenWinPanel()
	{
		winPanel.SetActive(true);
	
		int money=PlayerController.instance.duvarTarget.transform.childCount;
		int x = money /4;
		moneyText.text = (money*x).ToString();
		//int totalScore = Int32.TryParse(scoreText.text) + (moneyText.text);
		//scoreText.text = .ToString();
	}


	public void OpenLosePanel()
	{
		losePanel.SetActive(true);
		PlayerMovement.instance.speed = 0;
		GameManager.instance.isContinue = false;
		SwerveMovement.instance.swerve = false;
		
	}
}
