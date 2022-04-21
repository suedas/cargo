using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadCreator : MonoBehaviour
{
	public GameObject roadPrefab1, roadPrefab2,road;
	public InputField count;

	public void CreateRoad()
	{
		int adet = int.Parse(count.text);
		for (int i = 0; i < adet; i++)
		{

			if (i % 2 == 0) Instantiate(roadPrefab1, new Vector3(0, 0, i), Quaternion.identity, road.transform);
			else if (i % 2 == 1) Instantiate(roadPrefab2, new Vector3(0, 0, i), Quaternion.identity, road.transform);
		}
	}
}
