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
            Debug.Log("girdi");
            //PlayerMovement.instance.speed = 2f;
            if (c <= maxCargoCount && PlayerController.instance.transform.childCount>1)
            {
               
                    c++;
                    NodeMovement.instance.count--;
                    StartCoroutine(DelayAndJump(other.gameObject, c));
                    other.tag = "Untagged";

                    if (c == maxCargoCount)
                    {
                        Debug.Log("dronetask");
                        StartCoroutine(taskComplete());
                    }

              
          
            }
            else if(c <= maxCargoCount && PlayerController.instance.transform.childCount == 1)
            {
                Debug.Log("burda");
                UiController.instance.OpenLosePanel();
            }                    
        }
    }

    IEnumerator DelayAndJump(GameObject obj, int count)
    {
        if (PlayerController.instance.transform.childCount >1)
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
     
      
      
   
    }
    public IEnumerator taskComplete()
    {
        // PlayerMovement.instance.speed = 6f;
        if (PlayerController.instance.transform.childCount >=1)
        {
            Debug.Log("hizlandi");
            if (isParticle == true)
            {
                efect.SetActive(true);
            }
            if (transform.CompareTag("plane"))
            {

                yield return new WaitForSeconds(.5f);
                paket.SetActive(false);
                gameObject.transform.DOJump(new Vector3(transform.position.x, 20, transform.position.z), 1, 1, 2f).OnComplete(() => Destroy(gameObject));
            }
            else
            {
                yield return new WaitForSeconds(.05f);
                paket.SetActive(false);
                gameObject.transform.DOMove(target1.transform.position, .5f).SetEase(Ease.Linear).OnComplete(() => transform.LookAt(target2.transform, Vector3.up));
                yield return new WaitForSeconds(.5f);
                gameObject.transform.DOMove(target2.transform.position, 3f).OnComplete(() => transform.LookAt(target3.transform, Vector3.up));
                yield return new WaitForSeconds(3f);
                gameObject.transform.DOMove(target3.transform.position, .5f).OnComplete(() => Destroy(gameObject));
            }

        }
       


        //if (gameObject.transform.position.x > 0)
        //{
        //    yield return new WaitForSeconds(.05f);
        //    paket.SetActive(false);
        //    gameObject.transform.DOMove(target1.transform.position, .5f).SetEase(Ease.Linear).OnComplete(() =>transform.LookAt(target2.transform,Vector3.up));
        //    yield return new WaitForSeconds(.5f);
        //    gameObject.transform.DOMove(target2.transform.position, 3f).OnComplete(() =>transform.LookAt(target3.transform,Vector3.up));
        //    yield return new WaitForSeconds(3f);
        //    gameObject.transform.DOMove(target3.transform.position, .5f).OnComplete(() => Destroy(gameObject));
        //}
        //else
        //{
        //    yield return new WaitForSeconds(.05f);      
        //    paket.SetActive(false);
        //    gameObject.transform.DOMove(target1.transform.position, .5f).SetEase(Ease.Linear).OnComplete(() => transform.LookAt(target2.transform,Vector3.up));
        //    yield return new WaitForSeconds(.5f);
        //    gameObject.transform.DOMove(target2.transform.position, 3f).OnComplete(() => transform.LookAt(target3.transform,Vector3.up));
        //    yield return new WaitForSeconds(3f);
        //    gameObject.transform.DOMove(target3.transform.position, .5f).OnComplete(() => Destroy(gameObject));
        //}

    }
}
