using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MotorTask : MonoBehaviour
{

    public GameObject target;
    public GameObject sag;
    public GameObject sagFinish;
    public GameObject player;
    public GameObject sol;
    public GameObject solFinish;
    public GameObject target1;
    public GameObject target2;
 
    private void OnTriggerEnter(Collider other)
    {
     
        if (other.gameObject.CompareTag("last"))
        {         
            //PlayerMovement.instance.speed = 1f;
            if (gameObject.transform.childCount <= 2)
            {
                int count = gameObject.transform.childCount;
                NodeMovement.instance.count--;

                StartCoroutine(DelayAndJump(other.gameObject,count));
                other.GetComponent<Collider>().enabled = false;
                other.tag = "Untagged";
               // NodeMovement.instance.Origin();


                //// other.transform.localPosition = Vector3.zero;
                // other.gameObject.transform.DOLocalJump(new Vector3(target.transform.localPosition.x, target.transform.localPosition.y-3f+ count, target.transform.localPosition.z), 1, 1, .2f)
                //.OnComplete(() => other.gameObject.transform.position = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y - 3f + count, target.transform.localPosition.z));


                //.Append(other.gameObject.transform.DOScale(2,1f)).Append(other.gameObject.transform.DOScale(1,1f)) 

                // gönderirken ayný zamanda swerve yaparsan cargo baþka baþka yerlere gididyor.??????????????????????

                if (gameObject.transform.childCount == 2)
                {
                   StartCoroutine(taskComplete());

                }
            }
            if (GameObject.Find("Player").transform.childCount==1)
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
        yield return new WaitForSeconds(.05f);
        NodeMovement.instance.cargo.Remove(obj);

        GameObject player = GameObject.Find("Player");
        GameObject ss = player.transform.GetChild(player.transform.childCount - 1).gameObject;
        ss.tag = "last";
        //yield return new WaitForSeconds(.1f);
        obj.transform.position = target.transform.position + new Vector3(0,-.5f,-1f);
        //obj.transform.position 
        //obj.transform.localPosition = Vector3.zero;
        //obj.transform.parent = transform;
        //obj.gameObject.transform.DOJump(new Vector3(target.transform.position.x, target.transform.position.y - 3f + count, target.transform.position.z), 1, 1, .2f);
        //obj.transform.parent = transform;

        obj.gameObject.transform.DOJump(new Vector3(target.transform.position.x, target.transform.position.y + count, target.transform.position.z), 1, 1, .2f)
              .OnComplete(() => obj.gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y  + count, target.transform.position.z));

        obj.transform.parent = transform;



    }

    public IEnumerator taskComplete()
    {
        if (gameObject.transform.position.x > 0)
        {
            yield return new WaitForSeconds(.5f);

            gameObject.transform.DOMove(target1.transform.position, .5f).SetEase(Ease.Linear).OnComplete(() => transform.Rotate(0, -90, 0));
            yield return new WaitForSeconds(.5f);
            gameObject.transform.DOMove(target2.transform.position, 3f).OnComplete(() => Destroy(gameObject));

        }
 
    }
    //public IEnumerator taskComplete()
    //{
    //    if (gameObject.transform.position.x>0)
    //    {
    //        yield return new WaitForSeconds(.5f);

    //        gameObject.transform.DOMove(sag.transform.position, .8f).OnComplete(() => transform.Rotate(0, -90, 0));
    //        yield return new WaitForSeconds(.5f);
    //        gameObject.transform.DOMove(sagFinish.transform.position, 3f).OnComplete(() => Destroy(gameObject));

    //    }
    //    else
    //    {
    //        yield return new WaitForSeconds(.5f);
    //        gameObject.transform.DOMove(sol.transform.position, .8f).OnComplete(() => transform.Rotate(0, -90, 0));
    //        yield return new WaitForSeconds(.5f);
    //        gameObject.transform.DOMove(solFinish.transform.position, 3f).OnComplete(() => Destroy(gameObject));

    //    }


    //    // gameObject.transform.DOMove(new Vector3(0.1f, 0, 111f), 2f).OnComplete(() => Destroy(gameObject));

    //}
}
