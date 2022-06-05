using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TaskManager : MonoBehaviour
{

    public GameObject target;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject paketText;
    public GameObject paket;
    public GameObject efect;
    int c = 0;
    public int maxCargoCount;
    public bool isParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("last"))
        {
            //PlayerMovement.instance.speed = 2f;
            if (c <= maxCargoCount)
            {
                c++;
                NodeMovement.instance.count--;
                StartCoroutine(DelayAndJump(other.gameObject, c));
                other.tag = "Untagged";

                if (c == maxCargoCount)
                {
                    StartCoroutine(taskComplete());
                }
            }
            if (PlayerController.instance.transform.childCount == 1)
            {
                UiController.instance.OpenLosePanel();
            }
        }
    }

    IEnumerator DelayAndJump(GameObject obj, int count)
    {
        obj.transform.parent = null;
        obj.transform.DOKill();
        GameObject ss = PlayerController.instance.transform.GetChild(PlayerController.instance.transform.childCount - 1).gameObject;
        ss.tag = "last";
        yield return new WaitForSeconds(.05f);
        NodeMovement.instance.cargo.Remove(obj);
        obj.gameObject.transform.DOJump(new Vector3(target.transform.position.x, target.transform.position.y + count - 1, target.transform.position.z), 1, 1, .08f)
              .OnComplete(() => obj.gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + count - 1, target.transform.position.z));
        obj.transform.parent = transform;
        paketText.GetComponent<TextMeshPro>().text = c + "/" + maxCargoCount.ToString();
    }
    public IEnumerator taskComplete()
    {
       // PlayerMovement.instance.speed = 6f;
        Debug.Log("hizlandi");
        if (isParticle == true)
        {
            efect.SetActive(true);
        }
     
        if (gameObject.transform.position.x > 0)
        {
            yield return new WaitForSeconds(.05f);
            paket.SetActive(false);
            gameObject.transform.DOMove(target1.transform.position, .5f).SetEase(Ease.Linear).OnComplete(() =>transform.LookAt(target2.transform,Vector3.up));
            yield return new WaitForSeconds(.5f);
            gameObject.transform.DOMove(target2.transform.position, 3f).OnComplete(() =>transform.LookAt(target3.transform,Vector3.up));
            yield return new WaitForSeconds(3f);
            gameObject.transform.DOMove(target3.transform.position, .5f).OnComplete(() => Destroy(gameObject));
        }
        else
        {
            yield return new WaitForSeconds(.05f);      
            paket.SetActive(false);
            gameObject.transform.DOMove(target1.transform.position, .5f).SetEase(Ease.Linear).OnComplete(() => transform.LookAt(target2.transform,Vector3.down));
            yield return new WaitForSeconds(.5f);
            gameObject.transform.DOMove(target2.transform.position, 3f).OnComplete(() => transform.LookAt(target3.transform,Vector3.down));
            yield return new WaitForSeconds(3f);
            gameObject.transform.DOMove(target3.transform.position, .5f).OnComplete(() => Destroy(gameObject));
        }

    }
}
