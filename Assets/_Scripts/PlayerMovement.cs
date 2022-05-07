using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Singleton
    public static PlayerMovement instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    #endregion
    
    public float speed = 4f;

    private void Update()
    {

        transform.Translate(0, 0, speed * Time.deltaTime);

        //???????????????????????????????????????????????
        //if (NodeMovement.instance.cargo.Count==0)
        //{
        //    UiController.instance.OpenLosePanel();
        //}
      

       
    }

}
