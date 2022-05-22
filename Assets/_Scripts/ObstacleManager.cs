using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject crack;
   
    private void OnTriggerEnter(Collider other)
    {
        GameObject player = GameObject.Find("Player");
        GameObject ss = player.transform.GetChild(player.transform.childCount - 1).gameObject;
        ss.tag = "last";
        if (other.gameObject.tag == "last")
        {
         

            if (player.transform.childCount > 1)
            {
                Instantiate(crack, transform.position, transform.rotation);
                Destroy(other.gameObject);
                NodeMovement.instance.cargo.Remove(other.gameObject);
               


            }
            else
            {

                UiController.instance.OpenLosePanel();
            }
        }
    }
}
