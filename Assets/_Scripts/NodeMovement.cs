using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NodeMovement : MonoBehaviour
{
    public List<GameObject> cargo = new List<GameObject>();
    //public GameObject player;
    //public Transform connectedNode;
    public float delay = 0.05f;
    public int count;
    #region Singleton
    public static NodeMovement instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    #endregion


    public void StackCube(GameObject other, int index)
    {

        other.transform.parent = transform;
        Vector3 newPos = cargo[index].transform.localPosition;
        newPos.z += 1;
        other.transform.localPosition = newPos;
        cargo.Add(other);
        count = cargo.Count - 1;     
        StartCoroutine(Scale());      
    }
    public IEnumerator Scale()
    {     
        for (int i = count; i > 0; i--)
        {
            int index = i;
            Vector3 scale = new Vector3(.75f,.75f,.75f);
            scale *= 1f;
            cargo[index].transform.DOScale(scale, 0.1f).OnComplete(() =>
            cargo[index].transform.DOScale(new Vector3(.5f, .5f, .5f), 0.1f));
            yield return new WaitForSeconds(0.05f);
        }
      

    }
    public void Lerp()
    {
        for (int i = 1; i <=count; i++)
        {           
            Vector3 pos = cargo[i].transform.localPosition;
            pos.x = cargo[i - 1].transform.localPosition.x;
            cargo[i].transform.DOLocalMove(pos, delay);
        }
    }

    public void Origin()
    {
        for (int i = 1; i <= count; i++)
        {
            Vector3 pos = cargo[i].transform.localPosition;
            pos.x = cargo[0].transform.localPosition.x;
            cargo[i].transform.DOLocalMove(pos, 0.70f);
        }
    }

    public void KillDoTween(GameObject obj)
    {
        obj.transform.DOKill();
    }
}
