using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="last")
        {
            PlayerMovement.instance.speed = 20f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerMovement.instance.speed = 10f;
    }
}
