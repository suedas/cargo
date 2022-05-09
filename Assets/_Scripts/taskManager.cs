using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class taskManager : MonoBehaviour
{
    //public GameObject target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("stack"))
        {
            //target = GameObject.Find("motorTarget");
            PlayerMovement.instance.speed = 1f;
            if (gameObject.transform.childCount<3)
            {
                int count = gameObject.transform.childCount;
               other.gameObject.transform.DOJump(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+0.2f+count, gameObject.transform.position.z), 1, 1, .2f)
              .OnComplete(() => other.gameObject.transform.parent = gameObject.transform);
                NodeMovement.instance.cargo.Remove(other.gameObject);

                // g�nderirken ayn� zamanda swerve yaparsan cargo ba�ka ba�ka yerlere gididyor.

                if (gameObject.transform.childCount==2)
                {
                    StartCoroutine(taskComplete());
                    
                }

            }
            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
                PlayerMovement.instance.speed = 4f;

            }
       
        }
        

    }
    public IEnumerator taskComplete()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.transform.DOMove(new Vector3(0.1f, 0, 111f), 2f).OnComplete(() => Destroy(gameObject));
    }
 
  

}
