using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarTask : MonoBehaviour
{
    public GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("last"))
        {
           // PlayerMovement.instance.speed = 1f;
            if (gameObject.transform.childCount <= 4)
            {
                int count = gameObject.transform.childCount;
                NodeMovement.instance.count--;

                StartCoroutine(DelayAndJump(other.gameObject, count));
                other.tag = "Untagged";

                if (gameObject.transform.childCount == 4)
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
        PlayerMovement.instance.speed = 4f;
    }
    IEnumerator DelayAndJump(GameObject obj, int count)
    {
        obj.transform.parent = null;
        obj.transform.DOKill();
        GameObject player = GameObject.Find("Player");
        yield return new WaitForSeconds(.05f);
        NodeMovement.instance.cargo.Remove(obj);

        //obj.transform.position 
        //obj.transform.localPosition = Vector3.zero;
        //obj.transform.parent = transform;
        //obj.gameObject.transform.DOJump(new Vector3(target.transform.position.x, target.transform.position.y - 3f + count, target.transform.position.z), 1, 1, .2f);
        //obj.transform.parent = transform;
        GameObject ss = player.transform.GetChild(player.transform.childCount - 1).gameObject;
        ss.tag = "last";

        obj.gameObject.transform.DOJump(new Vector3(target.transform.position.x, target.transform.position.y + count, target.transform.position.z), 1, 1, .2f)
              .OnComplete(() => obj.gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + count, target.transform.position.z));
        obj.transform.parent = transform;
       
    }
    public IEnumerator taskComplete()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.transform.DOMove(new Vector3(0.1f, 0, 111f), 2f).OnComplete(() => Destroy(gameObject));

    }
}
