using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xray : MonoBehaviour
{
    public Material xrayTexture;
    float speed = 5f;

    void Update()
    {
        xrayTexture.mainTextureOffset = new Vector2(-speed * Time.deltaTime, 0);
    }
}
