using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class PlaneTask : MonoBehaviour
{
    public GameObject target;
    int c = 0;
    public GameObject paketText;
    public GameObject paket;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("last"))
        {
            int total = 3;
           // PlayerMovement.instance.speed = 1f;
            if (gameObject.transform.childCount <= total)
            {
                int count = gameObject.transform.childCount;
                NodeMovement.instance.count--;
                other.tag = "Untagged";
                StartCoroutine(DelayAndJump(other.gameObject, count));
                if (gameObject.transform.childCount ==total)
                {
                    StartCoroutine(taskComplete());
                }
            }
            if (GameObject.Find("Player").transform.childCount == 1)
            {
                UiController.instance.OpenLosePanel();

            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
       // PlayerMovement.instance.speed = 4f;
    }
    IEnumerator DelayAndJump(GameObject obj, int count)
    {
        //obj.transform.parent = null;

            obj.transform.DOKill();
            obj.transform.parent = null;
            GameObject ss = PlayerController.instance.transform.GetChild(PlayerController.instance.transform.childCount - 1).gameObject;
            ss.tag = "last"; 
        
        if (PlayerController.instance.transform.childCount > 1)
        {
            //if (PlayerController.instance.transform.GetChild(PlayerController.instance.transform.childCount - 1).gameObject != NodeMovement.instance.cargo[0])
            //{
            //}

        }
        yield return new WaitForSeconds(.05f);
        NodeMovement.instance.cargo.Remove(obj);

        //obj.transform.position 
        //obj.transform.localPosition = Vector3.zero;
        //obj.transform.parent = transform;
        //obj.gameObject.transform.DOJump(new Vector3(target.transform.position.x, target.transform.position.y - 3f + count, target.transform.position.z), 1, 1, .2f);
        //obj.transform.parent = transform;

        obj.gameObject.transform.DOJump(new Vector3(target.transform.position.x, target.transform.position.y + count, target.transform.position.z), 1, 1, .05f)
              .OnComplete(() => obj.gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + count, target.transform.position.z));
        obj.transform.parent = transform;
        c++;
        paketText.GetComponent<TextMeshPro>().text = c + "/1";


    }
    public IEnumerator taskComplete()
    {
        paket.SetActive(false);
        gameObject.transform.DOKill();
        yield return new WaitForSeconds(.1f);
        gameObject.transform.DOJump(new Vector3(transform.position.x, 20, transform.position.z),1,1,2f).OnComplete(()=>Destroy(gameObject));
       
        //gameObject.transform.DOMove(new Vector3(0.1f, 0, 111f), 2f).OnComplete(() => Destroy(gameObject));

    }
}
