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
    
    public float speed = 10f;
    public GameObject player;

    private void Update()
    {
        if (GameManager.instance.isContinue==true)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);          
        }             
    }

}
