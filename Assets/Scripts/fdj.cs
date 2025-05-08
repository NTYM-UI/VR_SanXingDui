using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomShaderScreenPos : MonoBehaviour {
    public Material testone;
    private float textureWidth, textureHeight;
    private float screenWidth, screenHeight;

    private void Awake()
    {
        testone = this.GetComponent<Image>().material;
        textureWidth = testone.GetTexture("_MainTex").width;
        textureHeight = testone.GetTexture("_MainTex").height;
        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }
    private void Update()
{
    testone.SetVector("_CircleCenter", new Vector4(Input.mousePosition.x/screenWidth, Input.mousePosition.y/screenHeight, 0, 0));
}
}