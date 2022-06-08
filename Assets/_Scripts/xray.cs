using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class xray : MonoBehaviour
{
    public Material xrayTexture;
    float speed = 5f;
    public GameObject target;
	

	

	void Update()
    {
        xrayTexture.mainTextureOffset = new Vector2(-speed * Time.deltaTime, 0);
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
			//int money = target.transform.childCount;

			PlayerController.instance.WinEvent();
		}
	}

	//private void OnTriggerEnter(Collider other)
	//   {
	//       if (other.CompareTag("last"))
	//       {
	//           PlayerMovement.instance.speed = 3f;
	//           if (transform.childCount > 1)
	//           {
	//               gameObject.transform.GetChild(gameObject.transform.childCount - 1).DOMove(target.transform.position, .2f).OnComplete(() => gameObject.transform.GetChild(gameObject.transform.childCount - 1).parent = target.transform);
	//               NodeMovement.instance.count--;
	//               NodeMovement.instance.cargo.Remove(gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject);
	//               yield return new WaitForSeconds(.2f);
	//               //gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.SetActive(false);
	//           }
	//           else
	//           {
	//               GameObject cameraTarget = GameObject.Find("cameraTarget");


	//               PlayerMovement.instance.speed = 0;
	//               PlayerController.instance.vcam.LookAt = cameraTarget.transform;
	//               PlayerController.instance.vcam.Follow = cameraTarget.transform;
	//               PlayerController.instance.vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 1, -9f);
	//               StartCoroutine(PlayerController.instance.instantiateMoney(target.transform.childCount * 2));
	//           }

	//       }
	//   } 

}
