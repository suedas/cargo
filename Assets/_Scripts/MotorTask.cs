using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MotorTask : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
     
        if (other.gameObject.CompareTag("last"))
        {
            GameObject target = GameObject.Find("motorTarget");
          
            PlayerMovement.instance.speed = 1f;
            if (gameObject.transform.childCount <= 5)
            {
                int count = gameObject.transform.childCount;
                other.gameObject.transform.DOJump(new Vector3(target.transform.position.x, target.transform.position.y-3f+ count, target.transform.position.z), 1, 1, .2f)
               .OnComplete(() => other.gameObject.transform.parent = gameObject.transform);
                NodeMovement.instance.cargo.Remove(other.gameObject);

                //.Append(other.gameObject.transform.DOScale(2,1f)).Append(other.gameObject.transform.DOScale(1,1f)) 

                // gönderirken ayný zamanda swerve yaparsan cargo baþka baþka yerlere gididyor.??????????????????????

                if (gameObject.transform.childCount == 5)
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
    public IEnumerator taskComplete()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.transform.DOMove(new Vector3(0.1f, 0, 111f), 2f).OnComplete(() => Destroy(gameObject));

    }
}
