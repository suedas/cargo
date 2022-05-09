using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Untagged")
        {
            Destroy(other.gameObject);
            NodeMovement.instance.cargo.Remove(other.gameObject);
        }
    }

}
