using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlXS : MonoBehaviour
{
    public Material oldMaterial, newMaterial;


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("para"))
		{
			GetComponent<Renderer>().sharedMaterial = newMaterial;
		}
	}
}
