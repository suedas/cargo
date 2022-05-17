using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaneTask : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("last"))
        {
            //target = GameObject.Find("motorTarget");
            PlayerMovement.instance.speed = 1f;
            if (gameObject.transform.childCount <= 7)
            {
                int count = gameObject.transform.childCount;
                other.gameObject.transform.DOJump(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f + count, gameObject.transform.position.z), 1, 1, .2f)
               .OnComplete(() => other.gameObject.transform.parent = gameObject.transform);
                NodeMovement.instance.cargo.Remove(other.gameObject);

                // gönderirken ayný zamanda swerve yaparsan cargo baþka baþka yerlere gididyor.??????????????????????

                if (gameObject.transform.childCount == 7)
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
    public IEnumerator taskComplete()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.transform.DOMove(new Vector3(0.1f, 0, 111f), 2f).OnComplete(() => Destroy(gameObject));

    }
}
