using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NodeMovement : MonoBehaviour
{
    public List<GameObject> cargo = new List<GameObject>();
    //public GameObject player;
    //public Transform connectedNode;
    public float delay = 0.25f;

    #region Singleton
    public static NodeMovement instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    #endregion
   
   
    public void StackCube(GameObject other , int index)
    {
        other.transform.parent = transform;
        Vector3 newPos = cargo[index].transform.localPosition;
        newPos.z += 1;
        other.transform.localPosition = newPos;
        cargo.Add(other);
        StartCoroutine(Scale());
       
        
    }
    public IEnumerator Scale()
    {
        Debug.Log("scale1");
        //fora hiç girmiyor ???
        int count = cargo.Count - 1;
        for (int i = count; i < 0; i--)
        {
            Debug.Log("scale2");
            int index = i;
            Vector3 scale = new Vector3(1, 1, 1);
            scale *= 1.5f;

            cargo[index].transform.DOScale(scale, 0.1f).OnComplete(() =>
            cargo[index].transform.DOScale(new Vector3(1, 1, 1), 0.1f));
            yield return new WaitForSeconds(0.001f);
        }
       
       
    }
    public void Lerp()
    {
        for (int i = 1; i <cargo.Count; i++)
        {           
            Vector3 pos = cargo[i].transform.localPosition;
            pos.x = cargo[i - 1].transform.localPosition.x;
            cargo[i].transform.DOLocalMove(pos, delay);

        }
    }
    public void Origin()
    {
        for (int i = 1; i < cargo.Count; i++)
        {
            Vector3 pos = cargo[i].transform.localPosition;
            pos.x = cargo[0].transform.localPosition.x;
            cargo[i].transform.DOLocalMove(pos, 0.70f);

        }
    }
}
