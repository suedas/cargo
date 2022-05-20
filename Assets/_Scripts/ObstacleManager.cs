using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject crack;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "last")
        {
            GameObject player = GameObject.Find("Player");

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
