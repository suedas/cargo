using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BicycleTask : MonoBehaviour
{
    public GameObject target;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject paketText;
    public GameObject paket;
    int c = 0;
     
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("last"))
        {
            int total = 6;
            PlayerMovement.instance.speed = 3f;
            if (gameObject.transform.childCount <= total)
            {
                int count = gameObject.transform.childCount;
                NodeMovement.instance.count--;

                StartCoroutine(DelayAndJump(other.gameObject, count-1));
                other.GetComponent<Collider>().enabled = false;
                other.tag = "Untagged";


                if (gameObject.transform.childCount == total)
                {
                    Debug.Log("task");
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
        PlayerMovement.instance.speed = 6f;
    }



    IEnumerator DelayAndJump(GameObject obj, int count)
    {
        
        obj.transform.parent = null;
        obj.transform.DOKill();
        yield return new WaitForSeconds(.05f);
        NodeMovement.instance.cargo.Remove(obj);

        GameObject player = GameObject.Find("Player");
        GameObject ss = player.transform.GetChild(player.transform.childCount - 1).gameObject;
        ss.tag = "last";
        //yield return new WaitForSeconds(.1f);
        obj.transform.position = target.transform.position + new Vector3(0, -.5f, -1f);
        //obj.transform.position 
        //obj.transform.localPosition = Vector3.zero;
        //obj.transform.parent = transform;
        //obj.gameObject.transform.DOJump(new Vector3(target.transform.position.x, target.transform.position.y - 3f + count, target.transform.position.z), 1, 1, .2f);
        //obj.transform.parent = transform;

        obj.gameObject.transform.DOJump(new Vector3(target.transform.position.x, target.transform.position.y + count, target.transform.position.z), 1, 1, .08f)
              .OnComplete(() => obj.gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + count, target.transform.position.z));

        obj.transform.parent = transform;
        c++;
        paketText.GetComponent<TextMeshPro>().text = c + "/2";
      
             
    }

    public IEnumerator taskComplete()
    {
        if (gameObject.transform.position.x > 0)
        {


            yield return new WaitForSeconds(.05f);

            paket.SetActive(false);
            gameObject.transform.DOMove(target1.transform.position, .5f).SetEase(Ease.Linear).OnComplete(() => transform.Rotate(0, -45, 0));
            yield return new WaitForSeconds(.5f);
            gameObject.transform.DOMove(target2.transform.position, 2f).OnComplete(() => transform.Rotate(0, -50, 0));
            yield return new WaitForSeconds(2f);
            gameObject.transform.DOMove(target3.transform.position, .5f).OnComplete(() => Destroy(gameObject));
        }
        else
        {
            yield return new WaitForSeconds(.05f);

            paket.SetActive(false);
            gameObject.transform.DOMove(target1.transform.position, .5f).SetEase(Ease.Linear).OnComplete(() => transform.Rotate(0, 45, 0));
            yield return new WaitForSeconds(.5f);
            gameObject.transform.DOMove(target2.transform.position, 2f).OnComplete(() => transform.Rotate(0, 50, 0));
            yield return new WaitForSeconds(2f);
            gameObject.transform.DOMove(target3.transform.position, .5f).OnComplete(() => Destroy(gameObject));

        }

    }
}
