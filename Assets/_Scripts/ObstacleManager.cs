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
            PlayerController.instance.Shake();
            if (player.transform.childCount > 1)
            {
                Instantiate(crack, transform.position, transform.rotation);
                NodeMovement.instance.count--;
                NodeMovement.instance.cargo.Remove(other.gameObject);
                Destroy(other.gameObject);
              
            }
            else
            {
                UiController.instance.OpenLosePanel();
            }
        }
    }
}
