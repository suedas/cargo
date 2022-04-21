using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	#region Singleton
	public static LevelController instance;
	void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}
	#endregion

	public List<GameObject> LevelPrefabs = new();

	public int currentLevelNo, totalLevelNo;
	// ui kýsmýna totallevelno yazdýrýlýyor.. currentlevelno sadece level objelerinin instantiate edilmesini kontrol ediyor..

	public GameObject currentLevelObj;

	private void Start()
	{
		//PlayerPrefs.DeleteAll();
		totalLevelNo = PlayerPrefs.GetInt("totallevelno");
		if (totalLevelNo == 0)
		{
			totalLevelNo = 1;
			PlayerPrefs.SetInt("totallevelno", totalLevelNo);
		}
		CreateLevel();

	}

	public void CreateLevel()
	{
		if (totalLevelNo > LevelPrefabs.Count)
		{
			currentLevelNo = Random.Range(1, LevelPrefabs.Count + 1);
		}
		else
		{
			currentLevelNo = totalLevelNo;
		}
		if (currentLevelObj == null)
		{
			currentLevelObj = Instantiate(LevelPrefabs[currentLevelNo - 1], Vector3.zero, Quaternion.identity);
		}
		else
		{
			Destroy(currentLevelObj);
			currentLevelObj = Instantiate(LevelPrefabs[currentLevelNo - 1], Vector3.zero, Quaternion.identity);
		}
		UiController.instance.SetLevelText();
	}

	public void NextLevelEvents()
	{

		totalLevelNo++;
		PlayerPrefs.SetInt("totallevelno", totalLevelNo);
		CreateLevel();
		Debug.Log(totalLevelNo);
	}

	public void RestartLevelEvents()
	{
		Destroy(currentLevelObj);
		currentLevelObj = Instantiate(LevelPrefabs[currentLevelNo - 1], Vector3.zero, Quaternion.identity);
	}
}
