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
    public GameObject player;

    private void Update()
    {
        if (GameManager.instance.isContinue==true)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            if (player.transform.childCount > 0)
            {
                GameObject ss = player.transform.GetChild(player.transform.childCount - 1).gameObject;
                ss.tag = "last";
                if (player.transform.GetChild(player.transform.childCount - 1).gameObject.tag == "last")
                {
                    for (int i = 0; i < player.transform.childCount - 1; i++)
                    {
                        player.transform.GetChild(i).gameObject.tag = "stack";
                    }

                }
                else
                {
                    GameObject son = player.transform.GetChild(player.transform.childCount - 1).gameObject;
                    son.tag = "last";
                }
            }
        }
       
        
    }

}
