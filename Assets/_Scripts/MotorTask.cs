using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MotorTask : MonoBehaviour
{

    public GameObject target;
    private void OnTriggerEnter(Collider other)
    {
     
        if (other.gameObject.CompareTag("last"))
        {
          
            PlayerMovement.instance.speed = 1f;
            if (gameObject.transform.childCount <= 2)
            {
                int count = gameObject.transform.childCount;
                NodeMovement.instance.cargo.Remove(other.gameObject);
                StartCoroutine(DelayAndJump(other.gameObject,count));


               
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
        //obj.transform.parent = null;
        yield return new WaitForSeconds(.05f);
        NodeMovement.instance.Origin();
        yield return new WaitForSeconds(.05f);
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
        yield return new WaitForSeconds(.5f);
        gameObject.transform.DOMove(new Vector3(0.1f, 0, 111f), 2f).OnComplete(() => Destroy(gameObject));

    }
}
