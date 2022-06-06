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
            //PlayerMovement.instance.speed = 2f;
            PlayerController.instance.Shake();
            if (player.transform.childCount > 1)
            {
                box = Instantiate(crack, transform.position, transform.rotation);
                NodeMovement.instance.count--;
                NodeMovement.instance.cargo.Remove(other.gameObject);       
                StartCoroutine(destroyCrack(box));
                Destroy(other.gameObject);
            }
            else
            {
                UiController.instance.OpenLosePanel();
            }
        }
        else if (other.gameObject.tag=="stack")
        {
            StartCoroutine(stackDestroy(other.gameObject));
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    PlayerMovement.instance.speed = 6f;
    //}


    IEnumerator destroyCrack(GameObject obj)
    {
        yield return new WaitForSeconds(.2f);
       
        Destroy(obj);
      
       
    }
    IEnumerator stackDestroy(GameObject other)
    {
        yield return new WaitForSeconds(.01f);
        int num = other.gameObject.transform.GetSiblingIndex();
        int cargosCount = NodeMovement.instance.cargo.Count;
        for (int i = num; i < cargosCount; i++)
        { //dokilll
            //box = Instantiate(crack, transform.position, transform.rotation);
            Destroy(NodeMovement.instance.cargo[i]);
            // NodeMovement.instance.count--;
            NodeMovement.instance.cargo.Remove(NodeMovement.instance.cargo[i]);
           
            //StartCoroutine(destroyCrack(box));
        }
    }
}

