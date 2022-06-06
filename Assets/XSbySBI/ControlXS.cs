using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlXS : MonoBehaviour
{
    public Material oldMaterial, newMaterial;
	public GameObject confeti;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("para"))
		{
			GetComponent<Renderer>().sharedMaterial = newMaterial;
			confeti.SetActive(true);
		}
	}
}
