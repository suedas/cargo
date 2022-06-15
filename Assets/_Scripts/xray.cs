using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class xray : MonoBehaviour
{
    public Material xrayTexture;
    float speed = 2f;
    public GameObject target;
	void FixedUpdate()
    {
       // xrayTexture.mainTextureOffset = new Vector2(xrayTexture.mainTextureOffset.x -speed * Time.deltaTime, 0);
        xrayTexture.mainTextureOffset = new Vector2(speed * Time.time, 0);
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("last"))
		{
			Debug.Log("giriþ yapýldý");
			PlayerMovement.instance.speed = 5f;
			if (PlayerController.instance.transform.childCount > 1)
			{
				other.transform.parent = null;
				other.transform.DOKill();
				NodeMovement.instance.count--;
				GameObject ss = PlayerController.instance.transform.GetChild(PlayerController.instance.transform.childCount - 1).gameObject;
				if (ss.tag!="Player")
				{
				ss.tag = "last";
				}
				NodeMovement.instance.cargo.Remove(other.gameObject);
				other.gameObject.transform.DOMove(target.transform.position, .6f);
				other.transform.parent = target.transform;
			
			}

		}
		else if (other.CompareTag("Player"))
		{
			PlayerController.instance.WinEvent();
		}
	}	
}
