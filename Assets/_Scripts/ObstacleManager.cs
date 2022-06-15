using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleManager : MonoBehaviour
{
    public GameObject crack;
    GameObject box;
    public int cargosCount;


    private void OnTriggerEnter(Collider other)
    {
        GameObject player = GameObject.Find("Player");
        if (other.gameObject.tag == "last" || other.gameObject.tag=="stack")
        {
            
            //PlayerMovement.instance.speed = 2f;
            PlayerController.instance.Shake();
            if (player.transform.childCount > 1)
            {
                StartCoroutine(stackDestroy(other.gameObject));
            }
            else
            {
                UiController.instance.OpenLosePanel();
            }
        }
        else if (other.gameObject.tag=="Player")
        {

            UiController.instance.OpenLosePanel();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject ss = PlayerController.instance.transform.GetChild(PlayerController.instance.transform.childCount - 1).gameObject;
        ss.tag = "last";
    }


    IEnumerator destroyCrack(GameObject obj)
    {
        yield return new WaitForSeconds(1f);     
        Destroy(obj);           
    }
    IEnumerator stackDestroy(GameObject other)
    {
        yield return new WaitForSeconds(.01f);
        int num = other.gameObject.transform.GetSiblingIndex();
        cargosCount = NodeMovement.instance.cargo.Count-1;
        int adet = cargosCount - num;
     
        //other.transform.DOKill();
        for (int i = 0; i < adet; i++)
        {
            
            if (NodeMovement.instance.cargo[NodeMovement.instance.cargo.Count - 1] != null)
            {
                box = Instantiate(crack, transform.position, transform.rotation);
                StartCoroutine(destroyCrack(box));
                NodeMovement.instance.count--;
                NodeMovement.instance.cargo[NodeMovement.instance.cargo.Count - 1].transform.DOKill();
                  Destroy(NodeMovement.instance.cargo[NodeMovement.instance.cargo.Count - 1].gameObject);
                NodeMovement.instance.cargo.Remove(NodeMovement.instance.cargo[NodeMovement.instance.cargo.Count - 1]);
             
            }

        }
    }
}

