using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject crack;
    GameObject box;
   
    private void OnTriggerEnter(Collider other)
    {
        GameObject player = GameObject.Find("Player");
        GameObject ss = player.transform.GetChild(player.transform.childCount - 1).gameObject;
        ss.tag = "last";
        if (other.gameObject.tag == "last")
        {
            PlayerMovement.instance.speed = 2f;
            PlayerController.instance.Shake();
            if (player.transform.childCount > 1)
            {
                box= Instantiate(crack, transform.position, transform.rotation);
                NodeMovement.instance.count--;
                NodeMovement.instance.cargo.Remove(other.gameObject);
                Destroy(other.gameObject);
             
                StartCoroutine(destroyCrack());
              
            }
            else
            {
                UiController.instance.OpenLosePanel();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerMovement.instance.speed = 6f;
    }


    IEnumerator destroyCrack()
    {
        yield return new WaitForSeconds(.2f);
       
        Destroy(box);
      
       
    }
}

