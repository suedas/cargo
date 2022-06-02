using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TricycleTask : MonoBehaviour
{
    public GameObject target;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject paketText;
    public GameObject paket;
    public GameObject efect;

    int c = 0;
    private void OnTriggerEnter(Collider other)
    {
        int total = 8;
        if (other.gameObject.CompareTag("last"))
        {
            //PlayerMovement.instance.speed = 1f;
            if (gameObject.transform.childCount <= total)
            {
                int count = gameObject.transform.childCount;
                NodeMovement.instance.count--;

                StartCoroutine(DelayAndJump(other.gameObject, count));
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

        obj.gameObject.transform.DOJump(new Vector3(target.transform.position.x, target.transform.position.y + count, target.transform.position.z), 1, 1, .2f)
              .OnComplete(() => obj.gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + count, target.transform.position.z));

        obj.transform.parent = transform;
        c++;
        paketText.GetComponent<TextMeshPro>().text = c + "/5";

    }

    public IEnumerator taskComplete()
    {
        efect.SetActive(true);
        if (gameObject.transform.position.x >0)
        {
            yield return new WaitForSeconds(.5f);
            paket.SetActive(false);

            gameObject.transform.DOMove(target1.transform.position, .5f).SetEase(Ease.Linear).OnComplete(() => transform.Rotate(0, -40, 0));
            yield return new WaitForSeconds(.5f);
            gameObject.transform.DOMove(target2.transform.position, 2f).OnComplete(() => transform.Rotate(0, -45, 0));
            yield return new WaitForSeconds(2f);
            gameObject.transform.DOMove(target3.transform.position, .5f).OnComplete(() => Destroy(gameObject));
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            paket.SetActive(false);

            gameObject.transform.DOMove(target1.transform.position, .5f).SetEase(Ease.Linear).OnComplete(() => transform.Rotate(0, 40, 0));
            yield return new WaitForSeconds(.5f);
            gameObject.transform.DOMove(target2.transform.position, 2f).OnComplete(() => transform.Rotate(0, 45, 0));
            yield return new WaitForSeconds(2f);
            gameObject.transform.DOMove(target3.transform.position, .5f).OnComplete(() => Destroy(gameObject));
        }

    }
}
